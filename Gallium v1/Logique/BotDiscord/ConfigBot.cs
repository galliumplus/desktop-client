using System;
using System.Collections.Generic;
using System.Text;

namespace BotETIQ
{
    public static class ConfigBot
    {

        // mettre Discord Token  dans variable d'environneemnt utilisateur
        /*
           * Recupère token discord dans variable d'environnement
           await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User)); 
           */
        private static string token = "OTg0ODU1Nzg1NTY3ODM0MjA1.G3YaLS.w3xlYieXP9UI_CWf3TQRrZWfyCkOrxPJ7__XBw";
        private static string channelETIQ = "957715276139479223";
        private static string channelPersonnel = "986048980729348096";
        private static string database = "";
        private static string host = "";
        private static string user = "";
        private static string password = "";
        private static int port = 3306;
        private static bool botState = false;

        /// <summary>
        /// Identifiant du bot discord
        /// </summary>
        public static string Token 
        { 
            get => token; 
            set => token = value; 
        }
        public static bool BotState
        {
            get => botState;
            set => botState = value;
        }

        #region salon discord
        /// <summary>
        /// Channel discord
        /// </summary>
        public static string ChannelETIQ 
        { 
            get => channelETIQ; 
            set => channelETIQ = value; 
        }

        /// <summary>
        /// Channel discord
        /// </summary>
        public static string ChannelPersonnel 
        { 
            get => channelPersonnel; 
            set => channelPersonnel = value; 
        }
        #endregion

        #region information database
        /// <summary>
        /// Nom de la base de donnée
        /// </summary>
        public static string Database 
        { 
            get => database; 
            set => database = value; 
        }

        /// <summary>
        /// Hôte
        /// </summary>
        public static string Host 
        { 
            get => host; 
            set => host = value; 
        }

        /// <summary>
        /// Utilisateur
        /// </summary>
        public static string User 
        { 
            get => user; 
            set => user = value; 
        }

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        public static string Password 
        { 
            get => password; 
            set => password = value; 
        }

        /// <summary>
        /// Port du serveur 
        /// </summary>
        public static int Port 
        { 
            get => port; 
            set => port = value; 
        }
        #endregion
    }
}
