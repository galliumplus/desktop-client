using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    /// <summary>
    /// Classe qui représente l'utilisateur
    /// </summary>
    public class User
    {
        private string compte;
        private string nom;
        private string prenom;
        private double balance;
        private string password;

        /// <summary>
        /// ID de l'utilisateur
        /// </summary>
        public string Compte
        {
            get => compte;
            set => compte = value;
        }

        /// <summary>
        /// Nom complet de l'utilisateur
        /// </summary>
        public string NomComplet
        {
            get => $"{Nom} {Prenom}";
        }

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom
        {
            get => nom.ToUpper();
            set => nom = value;
        }
        
        /// <summary>
        /// prénom de l'utilisateur
        /// </summary>
        public string Prenom
        {
            get => prenom;
            set => prenom = value;
        }


       

        /// <summary>
        /// Balance de l'utilisateur qui renvoie un double
        /// </summary>
        public double Balance 
        { 
            get => balance; 
            set => balance = value; 
        }

        /// <summary>
        /// Balance de l'utilisateur qui renvoie un string
        /// </summary>
        public string BalanceString 
        { 
            get
            {
                string ret = "" + Math.Round(Balance, 2);
                // 1 => 1,00
                if (new Regex("^[0-9]+$").IsMatch(ret))
                {
                    ret += ",00";
                }

                // 1,2 => 1,20
                if (new Regex("^[0-9]+,[0-9]$").IsMatch(ret))
                {
                    ret += "0";
                }

                ret += " €";
                return ret;
            } 
        }
        


        /// <summary>
        /// Constructeur de la classe User
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="compte"></param>
        public User(String nom,string prenom,string compte,double balance,string password)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Compte = compte;
            this.balance = balance;
            this.password = password;

        }

    }
}
