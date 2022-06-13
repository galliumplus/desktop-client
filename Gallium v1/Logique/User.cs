using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public class User
    {
        private string nom;
        private string compte;
        private double balance;
        private string password;


        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
    
        public string Compte { get => compte; set => compte = value; }
        public double Balance { get => balance; set => balance = value; }
        public string BalanceString 
        { 
            get{
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
        public string Nom { get => nom; set => nom = value; }


        /// <summary>
        /// Constructeur de la classe User
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="compte"></param>
        public User(String nom,string prenom,string compte,double balance,string password)
        {
            this.Nom = $"{nom} {prenom}";
            this.Compte = compte;
            this.balance = balance;
            this.password = password;

        }



    }
}
