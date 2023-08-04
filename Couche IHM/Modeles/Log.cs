using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Modeles
{
    public class Log 
    {
        private string date;
        private string theme;
        private string message;
        private string auteur;
        private string operation;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Log(string date, string theme, string message, string auteur,string operation)
        {
            this.date = date;
            this.theme = theme;
            this.message = message;
            this.auteur = auteur;
            this.operation = operation;
        }

        /// <summary>
        /// Renvoie le lien de l'image pour l'opération
        /// </summary>
        public string imageLinkOperation
        {
            get
            {
                string link = "";
                switch (operation)
                {
                    case "UPDATE":
                        link = "/Images/modi.png";
                        break;
                    case "CREATE":
                        link = "/Images/ajout.png";
                        break;
                    case "DELETE":
                        link = "/Images/supp.png";
                        break;
                    case "VENTE":
                        link = "/Images/vente.png";
                        break;
                }
                return link;
            }
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
        public string Theme 
        { 
            get => theme; 
            set => theme = value; 
        }

        /// <summary>
        /// Intitulé du message sans les détails
        /// </summary>
        public string MessageCourt
        {
            get => message;
            set => message = value;
        }


        /// <summary>
        /// Message complet intitulé et détails
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
        public string Operation { get => operation; set => operation = value; }
    }
}
