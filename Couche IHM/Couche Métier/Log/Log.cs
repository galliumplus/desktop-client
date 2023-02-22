using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Log
{
    public class Log
    {
        private string date;
        private string action;
        private string message;
        private string auteur;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Log(string date, string action, string message, string auteur)
        {
            this.date = date;
            this.action = action;
            this.message = message;
            this.auteur = auteur;
        }

        /// <summary>
        /// Date de l'action
        /// </summary>
        public string Date 
        { 
            get => date; 
            set => date = value; 
        }

        /// <summary>
        /// Action de l'utilisateur
        /// </summary>
        public string Action 
        { 
            get => action; 
            set => action = value; 
        }

        /// <summary>
        /// Intitulé du message
        /// </summary>
        public string MessageCourt
        {
            get
            {
                return message.Split('/')[0];
            }
        }

        /// <summary>
        /// Message
        /// </summary>
        public string Message 
        { 
            get => message; 
            set => message = value; 
        }

        /// <summary>
        /// Auteur 
        /// </summary>
        public string Auteur 
        { 
            get => auteur; 
            set => auteur = value; 
        }
    }
}
