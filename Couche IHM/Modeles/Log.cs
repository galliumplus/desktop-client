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
        private int id;
        private string date;
        private int theme;
        private string message;
        private string auteur;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Log(int id, string date, int theme, string message, string auteur)
        {
            this.id = id;
            this.date = date;
            this.theme = theme;
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
        public int Theme 
        { 
            get => theme; 
            set => theme = value; 
        }

        /// <summary>
        /// Intitulé du message sans les détails
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
        public int Id { get => id; set => id = value; }
    }
}
