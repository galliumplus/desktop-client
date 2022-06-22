using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotETIQ
{
    /// <summary>
    /// Classe qui représente allume le bot discord au lancement de gallium
    /// </summary>
    public class EtiqClient
    {
        // --> Oui bonjour si tu regarde cette classe pour le moment le bot est global est spécifique sur aucun channel mais tous, uniquement sur le serveur
        // où je l'ai mis et oui il est en effet en c#
        private DiscordSocketClient client; // Connexion discord
        private CommandService commands = new CommandService(); // Gestionnaire de commande

        /// <summary>
        /// Instancie client
        /// </summary>
        /// <returns></returns>
        public async Task RunBotAsync()
        {
            // Permet d'avoir les informations de connexion
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });
            client.Log += Log;

            // Envoie comme message que le bot est en ligne
            client.Ready += () =>
            {
                Console.WriteLine("> Bot en ligne");
                return Task.CompletedTask;
            };

            // Installe les commandes
            await InstallCommandsAsync();

            // Configure le bot
            await client.LoginAsync(TokenType.Bot, ConfigBot.Token);
            await client.StartAsync();
            await Task.Delay(-1); // délais pour pas que l'app se termine
           

        }

        /// <summary>
        /// Permet d'identifier les commandes valables
        /// </summary>
        /// <returns></returns>
        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        /// <summary>
        /// Ouais oauis
        /// </summary>
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            // Message vaut null
            if (message == null) return;

            int argPos = 0;

            // Message n'a pas le bon suffixe
            if (!message.HasCharPrefix('&', ref argPos)) return;

            var context = new SocketCommandContext(client, message);
            var result = await commands.ExecuteAsync(context, argPos, null);

            // Message erreur
            if (!result.IsSuccess) await context.Channel.SendMessageAsync(result.ErrorReason);
        }


        /// <summary>
        /// Permet d'avoir les logs du bot discord
        /// </summary>
        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }



    }
}
