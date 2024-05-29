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
        private List<Account> accomptes;
        private List<string> les_morceaux;

        public DiscordBot(MainWindowViewModel mvvm) 
        {
            this.products = mvvm.ProductViewModel.Products.ToList();
            this.accomptes = mvvm.AccountManager.GetAdhérents();
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
            _client.SlashCommandExecuted += _client_SlashCommandExecuted;
            _client.Ready += _client_Ready;


            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = ID.TOKEN;

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
            SlashCommandBuilder accompte = new SlashCommandBuilder()
                .WithDescription("Consulter le solde de son accompte").WithName("accompte").AddOption("identifiant", type:ApplicationCommandOptionType.String, "Identifiant de votre accompte !", isRequired:true);
            await _client.CreateGlobalApplicationCommandAsync(produits.Build());
            await _client.CreateGlobalApplicationCommandAsync(achetable.Build());
            await _client.CreateGlobalApplicationCommandAsync(restock.Build());
            await _client.CreateGlobalApplicationCommandAsync(accompte.Build());
            
            
            #endregion
        }

        private async Task _client_SlashCommandExecuted(SocketSlashCommand arg)
        {
            string command = arg.Data.Name;

            int longueurMaximale = 995; // Nombre maximal de caractères par morceau
            switch (command)
            {
                #region Produits
                
                case "produits":
                    EmbedBuilder embedF = new EmbedBuilder();
                    embedF.WithTitle("Liste des produits");
                    embedF.WithColor(Color.DarkOrange);
                    embedF.WithCurrentTimestamp();
                    embedF.WithFooter("\u26a0 Attention : Il peut y avoir des erreurs dans les stocks !");


                    string listeBoisson = "";
                    List<ProductViewModel> boissons = products.FindAll(x => x.CategoryIHM.NomCat == "Boisson" && x.isDisponible == true).OrderBy(x => x.NomProduitIHM).ToList();
                    string msgBoisson1 = "";
                    string msgBoisson2 = "";
                    foreach (ProductViewModel p in boissons)
                    {
                        if (Convert.ToDouble(p.PrixAdherentIHM.Split(' ')[0]) <= 1)
                        {
                            msgBoisson1 += $"x{p.QuantiteIHM} - **{p.NomProduitIHM}** : *{p.PrixAdherentIHM}*\n";  
                        }

                        if (Convert.ToDouble(p.PrixAdherentIHM.Split(' ')[0]) > 1)
                        {
                            msgBoisson2 += $"x{p.QuantiteIHM} - **{p.NomProduitIHM}** : *{p.PrixAdherentIHM}*\n";
                        }
                    }
                    
                    List<ProductViewModel> snackis = products.FindAll(x => x.CategoryIHM.NomCat == "Snack" && x.isDisponible == true).OrderBy(x => x.NomProduitIHM).ToList();
                    string msgSnack = "";
                    foreach (ProductViewModel p in snackis)
                    {
                        msgSnack += $"x{p.QuantiteIHM} - **{p.NomProduitIHM}** : {p.PrixAdherentIHM}\n";
                    }
                    embedF.AddField(":beverage_box: Boissons petites économies", msgBoisson1);
                    embedF.AddField(":beverage_box: Boissons de riche", msgBoisson2);
                    embedF.AddField(":chocolate_bar: Snacks", msgSnack);
                    arg.RespondAsync("", embed: embedF.Build(), ephemeral: true);
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
                    finalEmbed.WithColor(Color.DarkOrange);
                    finalEmbed.WithFooter("\u26a0 Attention : Il peut y avoir des erreurs dans les stocks !");

                    arg.RespondAsync(embed: finalEmbed.Build(), ephemeral: true);
                    
                    break;
                
                #endregion
                
                #region Restock
                
                case "restock":

                    var guildUser = (IGuildUser)arg.User;
                    bool adminPermission = guildUser.GuildPermissions.Administrator;
                    
                    if(adminPermission)
                    {
                        List<ProductViewModel> produitsEnRuptureBoisson = this.products.FindAll(x => x.QuantiteIHM <= 30 && x.CategoryIHM.NomCat == "Boisson");
                        string messageBoisson = "";
                        foreach (ProductViewModel produits in produitsEnRuptureBoisson)
                        {
                            messageBoisson += $"**{produits.NomProduitIHM}** : x{produits.QuantiteIHM}\n";
                        }
                        
                        List<ProductViewModel> produitsEnRuptureMiam = this.products.FindAll(x => x.QuantiteIHM <= 30 && x.CategoryIHM.NomCat == "Snack");
                        string messageMiam = "";
                        foreach (ProductViewModel produits in produitsEnRuptureMiam)
                        {
                            messageMiam += $"**{produits.NomProduitIHM}** : x{produits.QuantiteIHM}\n";
                        }

                        EmbedBuilder embed = new EmbedBuilder().WithDescription("Voici les produits manquants à l'ETIQ :").WithTitle("Liste de courses :").AddField(":tropical_drink: Boissons", messageBoisson).AddField(":doughnut: Snack", messageMiam).WithCurrentTimestamp().WithColor(Color.DarkOrange).WithFooter("\u26a0 Attention : Il peut y avoir des erreurs dans les stocks !");
                        arg.RespondAsync(text: "", embed: embed.Build(),ephemeral:true);
                    }
                    else
                    {
                        EmbedBuilder embed =
                            new EmbedBuilder().WithDescription(
                                "Vous n'avez pas la permission nécessaire afin d'exécuter cette commande !").WithTitle("Erreur !").WithColor(Color.Red).WithCurrentTimestamp();
                        arg.RespondAsync(embed: embed.Build(), ephemeral:true);
                    }
                    break;
                
                #endregion

                #region Acompte

                case "accompte":
                    
                    var userG = (IGuildUser)arg.User;
                    bool adminP = userG.GuildPermissions.Administrator;

                    if (adminP)
                    {
                        string identifiant = Convert.ToString(arg.Data.Options.First().Value);
                        float money = 0;
                        bool find = false;
                        Account finalAccount = accomptes.First();
                        foreach (Account accompte in this.accomptes)
                        {
                            if (accompte.Identifiant == identifiant)
                            {
                                money = accompte.Argent;
                                find = true;
                                finalAccount = accompte;
                            }
                        }
                        if (find)
                        {
                            EmbedBuilder embed = new EmbedBuilder().WithTitle($"Argent de : {identifiant}")
                                .WithDescription($"Montant de l'accompte : {finalAccount.Argent}€").WithColor(Color.Orange)
                                .WithCurrentTimestamp();
                            arg.RespondAsync(embed:embed.Build(),ephemeral:true);
                        }
                        else
                        {
                            EmbedBuilder embed =
                                new EmbedBuilder().WithDescription(
                                    "Identifiant introuvable dans la base de donnée !").WithTitle("Erreur !").WithColor(Color.Red).WithCurrentTimestamp();
                            arg.RespondAsync(embed: embed.Build(), ephemeral:true);
                        }
                    }    
                    break;

                #endregion
            }
                
        }

      

    }
}
