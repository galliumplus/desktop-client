using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Couche_IHM
{
    public class LogIHM 
    {
        private string date;
        private string action;
        private string message;
        private string messageComplete;
        private string auteur;
        private SolidColorBrush colorOperation;

        /// <summary>
        /// Constructeur
        /// </summary>
        public LogIHM(string date, string action, string message, string auteur)
        {
            this.date = date;
            this.action = action;
            this.message = message;
            this.auteur = auteur;
            this.colorOperation = new SolidColorBrush();
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
        /// Intitulé du message sans les détails
        /// </summary>
        public string MessageCourt
        {
            get => message;
            set => message = value;
        }

        /// <summary>
        /// Message détaillés sans l'intitulé du message
        /// </summary>
        public string MessageComplete
        {
            get => messageComplete;
            set => messageComplete = value;
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
        public SolidColorBrush ColorOperation { get => colorOperation; set => colorOperation = value; }
    }
}
