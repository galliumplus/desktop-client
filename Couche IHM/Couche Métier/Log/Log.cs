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

        public Log(string date, string action, string message, string auteur)
        {
            this.date = date;
            this.action = action;
            this.message = message;
            this.auteur = auteur;
        }

        public string Date { get => date; set => date = value; }
        public string Action { get => action; set => action = value; }
        public string Message { get => message; set => message = value; }
        public string Auteur { get => auteur; set => auteur = value; }
    }
}
