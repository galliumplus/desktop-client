using Discord.WebSocket;
using Discord;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Modeles;
using Couche_IHM.VueModeles;
using Discord.Net;
using Newtonsoft.Json;
using Discord.Rest;

namespace Couche_IHM
{
    public class DiscordBot
    {
        private DiscordSocketClient _client;
        private List<ProductViewModel> products;
        private List<string> les_morceaux;

        public DiscordBot(MainWindowViewModel mvvm) 
        {
            this.products = mvvm.ProductViewModel.Products.ToList();
            Connect();
        }

        private async void Connect()
        {
            var config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences
            };
            _client = new DiscordSocketClient(config);
            _client.ButtonExecuted += _client_ButtonExecuted;
            _client.SlashCommandExecuted += _client_SlashCommandExecuted;
            _client.Ready += _client_Ready;


            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = "TOKEN";

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _client.SetCustomStatusAsync("Local ouvert !");

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }


        private async Task _client_Ready()
        {
            #region SetSlashCommand
            
            SlashCommandBuilder produits = new SlashCommandBuilder().WithName("achetable").WithDescription("Permet de récupérer la liste des produits achetables avec l'argent disponible").AddOption("argent", type:ApplicationCommandOptionType.Integer, "Argent que vous possédez", isRequired:true);
            SlashCommandBuilder achetable = new SlashCommandBuilder().WithName("produits").WithDescription("Permet de récupérer la liste des produits disponibles à l'ETIQ (actuellement)");
            SlashCommandBuilder restock = new SlashCommandBuilder()
                .WithDescription("Permet de retourner les articles ayant besoin d'un restock").WithName("restock");
            await _client.CreateGlobalApplicationCommandAsync(produits.Build());
            await _client.CreateGlobalApplicationCommandAsync(achetable.Build());
            await _client.CreateGlobalApplicationCommandAsync(restock.Build());
            
            #endregion
        }
        
        private async Task _client_ButtonExecuted(SocketMessageComponent arg)
        {

            string part = les_morceaux[Convert.ToInt16(arg.Data.CustomId)-1];
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle("__:beverage_box: Les Boissons__");
            embedBuilder.WithColor(Color.Red);
            embedBuilder.WithCurrentTimestamp();

            embedBuilder.AddField($"Page {arg.Data.CustomId}", "```" + part + "```");
            await arg.UpdateAsync(x => x.Embed = embedBuilder.Build());
        }

        private async Task _client_SlashCommandExecuted(SocketSlashCommand arg)
        {
            string command = arg.Data.Name;

            int longueurMaximale = 995; // Nombre maximal de caractères par morceau
            switch (command)
            {
                #region Produits
                
                case "produits":
                    EmbedBuilder embedBuilder = new EmbedBuilder();
                    embedBuilder.WithTitle("__:beverage_box: Les Boissons__");
                    embedBuilder.WithColor(Color.Red);
                    embedBuilder.WithCurrentTimestamp();


                    string listeBoisson = "";
                    List<ProductViewModel> boissons = products.FindAll(x => x.CategoryIHM.NomCat == "Boisson" && x.isDisponible == true).OrderBy(x => x.NomProduitIHM).ToList();
                    foreach (ProductViewModel p in boissons)
                    {

                        // Formate la chaîne avec les espaces appropriés
                        string produit = string.Format("x{0,-8}{1,-30}{2,-10}", p.QuantiteIHM, p.NomProduitIHM, p.PrixAdherentIHM);
                        listeBoisson += produit + "\n";
                    }


                    List<string> morceaux = new List<string>();



                    for (int i = 0; i < listeBoisson.Length; i += longueurMaximale)
                    {
                        int longueur4 = Math.Min(longueurMaximale, listeBoisson.Length - i);
                        string morceau = listeBoisson.Substring(i, longueur4);
                        morceaux.Add(morceau);
                    }
                    this.les_morceaux = morceaux;

                    // Construire les composants de message avec les boutons
                    var components = new ComponentBuilder();


                    embedBuilder.AddField($"Page 1", "```" + morceaux[0] + "```");
                    var button2 = new ButtonBuilder()
                           .WithLabel("1")
                           .WithCustomId("1")
                           .WithDisabled(true)
                           .WithStyle(ButtonStyle.Primary);

                    components.WithButton(button2);
                    for (int i = 1; i < morceaux.Count; i++)
                    {
                        
                        var button = new ButtonBuilder()
                           .WithLabel((i+1).ToString())
                           .WithCustomId((i+1).ToString())
                           .WithStyle(ButtonStyle.Primary);

                        components.WithButton(button);
                        //embedBuilder.AddField($"Part {i} :", "```" + morceaux[i] + "```");
                    }
                    
                    arg.RespondAsync("", embed:embedBuilder.Build(),components:components.Build(), ephemeral:true);

                break;
                
                #endregion
                
                #region Achetable

                case "achetable":
                    double argent = Convert.ToDouble(arg.Data.Options.ToList()[0].Value);
                    string listeBoisson2 = "";
                    List<ProductViewModel> boissons2 = products.FindAll(x => x.CategoryIHM.NomCat == "Boisson" && x.isDisponible == true && x.test <= argent).OrderBy(x => x.NomProduitIHM).ToList();
                    EmbedBuilder finalEmbed = new EmbedBuilder();
                    foreach (ProductViewModel p in boissons2)
                    {

                        // Formate la chaîne avec les espaces appropriés
                        string produit = string.Format($"**{p.NomProduitIHM}** {p.PrixAdherentIHM}/u");
                        listeBoisson2 += produit + "\n";

                    }

                    List<string> morceaux4 = new List<string>();


                    int newlongueur = Math.Min(longueurMaximale, listeBoisson2.Length);
                    for (int i = 0; i < listeBoisson2.Length; i += newlongueur)
                    {
                        int longueur6 = Math.Min(longueurMaximale, listeBoisson2.Length - i);
                        string morceau = listeBoisson2.Substring(i, longueur6);
                        morceaux4.Add(morceau);
                    }

                    string messageBoissons = "";
                    foreach (string morceau in morceaux4)
                    {
                        messageBoissons += morceau;
                    }
                    
                    string listeSnacks2 = "";
                    List<ProductViewModel> snacks2 = products.FindAll(x => x.CategoryIHM.NomCat == "Snack" && x.isDisponible == true && x.test <= argent).OrderBy(x => x.NomProduitIHM).ToList();
                    foreach (ProductViewModel p in snacks2)
                    {

                        // Formate la chaîne avec les espaces appropriés
                        string produit = string.Format($"**{p.NomProduitIHM}** : {p.PrixAdherentIHM}");
                        listeSnacks2 += produit + "\n";
                    }


                    List<string> morceaux3 = new List<string>();


                    string messageSnack = "";
                    int longueur = Math.Min(longueurMaximale, listeSnacks2.Length);
                    for (int i = 0; i < listeSnacks2.Length; i += longueur)
                    {
                        int longueur2 = Math.Min(longueurMaximale, listeSnacks2.Length - i);
                        string morceau = listeSnacks2.Substring(i, longueur2);
                        morceaux3.Add(morceau);
                    }
                    foreach (string morceau in morceaux3)
                    {
                        messageSnack += morceau;
                    }

                    finalEmbed.Title = $"Produit Achetable avec {argent}€ :";
                    finalEmbed.Description =
                        ":warning: Les prix affichés sont ceux pour les **adhérents**, sinon, c'est +0,20€ pour chaque consommations";
                    finalEmbed.AddField(":beverage_box: Boissons", messageBoissons);
                    finalEmbed.AddField(":chocolate_bar: Snacks", messageSnack);

                    arg.RespondAsync(embed: finalEmbed.Build(), ephemeral: true);
                    
                    break;
                
                #endregion
                
                #region Restock
                
                case "restock":

                    List<ProductViewModel> produitsEnRuptureBoisson = this.products.FindAll(x => x.QuantiteIHM <= 30 && x.CategoryIHM.NomCat == "Boisson");
                    string messageBoisson = "";
                    foreach (ProductViewModel produits in produitsEnRuptureBoisson)
                    {
                        messageBoisson += $"**{produits.NomProduitIHM}** : x{produits.QuantiteIHM}\n";
                    }
                    
                    List<ProductViewModel> produitsEnRuptureMiam = this.products.FindAll(x => x.QuantiteIHM <= 30 && x.CategoryIHM.NomCat == "Snack");
                    string messageMiam = "";
                    foreach (ProductViewModel produits in produitsEnRuptureBoisson)
                    {
                        messageMiam += $"**{produits.NomProduitIHM}** : x{produits.QuantiteIHM}\n";
                    }

                    EmbedBuilder embed = new EmbedBuilder().WithDescription("Voici les produits manquants à l'ETIQ :").WithTitle("Liste de courses :").AddField(":tropical_drink: Boissons", messageBoisson).AddField(":doughnut: Snack", messageMiam);
                    arg.RespondAsync(text: "", embed: embed.Build(),ephemeral:true);
                    
                    break;
                
                #endregion
            }
            
                
        }

      

    }
}
