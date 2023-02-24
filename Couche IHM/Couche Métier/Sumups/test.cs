using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Sumups
{
    public class test
    {
        // Nom : GalliumAPI
        // tokken : sup_sk_1D0jsPlDevsE9kjCh6e2GJ56KN0zm15g1 API KEYS

        // Install-Package SumUp.SDK -Version 1.1.0 Authorization: Bearer 
        public test()
        {
        }

        // Doit renvoyer "Task<type>
        public void TestSumup()
        {
            // string sumupToken = "sup_sk_1D0jsPlDevsE9kjCh6e2GJ56KN0zm15g1";
            // Envoyer requête pour générer tokken

            // Entrez vos identifiants API SumUp ici https://developer.sumup.com/protected/oauth-app/?id=CPZF26G7
            string clientId = "";
            string clientSecret = "";

            // Montant à envoyer
            double amount = 10.0;
            string currency = "EUR";

            // Créez un objet HttpClient pour envoyer la requête
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.sumup.com/v0.1");

            // Créez l'en-tête d'authentification pour la requête
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{credentials}");

            // Créez l'objet JSON avec les informations de paiement
            var body = new
            {
                amount = amount,
                currency = currency
            };
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            // Envoyez la requête à l'API SumUp
            string requete = "/terminal/send-payment-request";
            requete = "v0.1/checkouts";
            // requete = "/payments";
            var response = client.PostAsync(requete, content);
            var aaaaas = response.Result.Content.ReadAsStreamAsync().Result;

            // Vérifiez si la requête a réussi et affichez la réponse
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Paiement envoyé avec succès");   
            }
            else
            {
                Console.WriteLine($"Erreur lors de l'envoi du paiement");
            }

            // Attendez que l'utilisateur appuie sur une touche pour quitter
            // Console.ReadKey();

        }

        private async Task<bool> CreatePayament(int amount)
        {
            using (HttpClient clientAPI = new HttpClient())
            {
                var client = new RestClient("https://api.sumup.com/token");
                var request = new RestRequest("", Method.Put);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                var body = @"{" + "\n" +
                @"  ""grant_type"": ""authorization_code""," + "\n" +
                @"  ""client_id"": ""cc_classic_2MLpttwJ8QAMfIhu085rjs1xtegMq""," + "\n" +
                @"  ""client_secret"": ""cc_sk_classic_l8oWCUS8zwBuSSmWQWQgTzydJvSLqCU8NOiZmnLEHf4IB7EnIa""," + "\n" +
                @"  ""code"": """"" + "\n" +
                @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
                return false;
            }
        }
    }
}
