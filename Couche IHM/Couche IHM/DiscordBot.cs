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



                    arg.Channel.SendMessageAsync("", embed:embedBuilder.Build(),components:components.Build());

                break;



                case "achetable":
                    double argent = Convert.ToDouble(arg.Data.Options.ToList()[0].Value);

                    await arg.User.SendMessageAsync($"Voici ce que vous pouvez acheter pour {argent} euros : \n");
                    await arg.User.SendMessageAsync(":beverage_box:  **Boissons**");
                    string listeBoisson2 = string.Format("{0,-10}{1,-30}{2,-15}{3,-15}\n\n", "Quantité", " Nom du produit", " Prix A", " Prix NA");
                    List<ProductViewModel> boissons2 = products.FindAll(x => x.CategoryIHM.NomCat == "Boisson" && x.isDisponible == true && x.test <= argent).OrderBy(x => x.NomProduitIHM).ToList();
                    foreach (ProductViewModel p in boissons2)
                    {

                        // Formate la chaîne avec les espaces appropriés
                        string produit = string.Format("x{0,-10}{1,-30}{2,-15}{3,-15}", p.QuantiteIHM, p.NomProduitIHM, p.PrixAdherentIHM, p.PrixNonAdherentIHM);
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
                    foreach (string morceau in morceaux4)
                    {
                        await arg.User.SendMessageAsync("```" + morceau + "```");
                    }

                    await arg.User.SendMessageAsync(":chocolate_bar:  **Snacks**");

                    string listeSnacks2 = "";
                    List<ProductViewModel> snacks2 = products.FindAll(x => x.CategoryIHM.NomCat == "Snack" && x.isDisponible == true && x.test <= argent).OrderBy(x => x.NomProduitIHM).ToList();
                    foreach (ProductViewModel p in snacks2)
                    {

                        // Formate la chaîne avec les espaces appropriés
                        string produit = string.Format("x{0,-10}{1,-30}{2,-15}{3,-15}", p.QuantiteIHM, p.NomProduitIHM, p.PrixAdherentIHM, p.PrixNonAdherentIHM);
                        listeSnacks2 += produit + "\n";
                    }


                    List<string> morceaux3 = new List<string>();


                    int longueur = Math.Min(longueurMaximale, listeSnacks2.Length);
                    for (int i = 0; i < listeSnacks2.Length; i += longueur)
                    {
                        int longueur2 = Math.Min(longueurMaximale, listeSnacks2.Length - i);
                        string morceau = listeSnacks2.Substring(i, longueur2);
                        morceaux3.Add(morceau);
                    }
                    foreach (string morceau in morceaux3)
                    {
                        await arg.User.SendMessageAsync("```" + morceau + "```");
                    }
                    break;
            }
        }

      

    }
}
