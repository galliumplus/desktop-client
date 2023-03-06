using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Couche_Métier.Utilitaire;

namespace Couche_Métier
{
    public class Adhérent
    {
        private int idAdherent;
        private string identifiant;
        private string nom;
        private string prenom;
        private bool canPass;
        private double argent;
        private bool stillAdherent;
        private string formation;

        /// <summary>
        /// Constructeur de la classe adhérent
        /// </summary>
        /// <param name="id">id de l'adhérent</param>
        /// <param name="nom">nom de l'adhérent</param>
        /// <param name="prenom">prenom de l'adhérent</param>
        /// <param name="canPass">si le mdp peut être skip</param>
        /// <param name="argent">argent de l'adhérent</param>
        public Adhérent(string identifiant, string nom, string prenom, double argent,string formation, bool canPass = false,bool stillAdherent = true)
        {
            this.identifiant = identifiant;
            this.nom = nom;
            this.prenom = prenom;
            this.canPass = canPass;
            this.argent = argent;
            this.stillAdherent = stillAdherent;
            this.formation = formation;
        }

        public Adhérent()
        {

        }

        /// <summary>
        /// Id de l'identifiant dans la bdd
        /// </summary>
        public int Id { get => idAdherent; set => idAdherent = value;}

        /// <summary>
        /// Id de l'adhérent
        /// </summary>
        public string Identifiant { get => identifiant; set => identifiant = value; }

        /// <summary>
        /// Nom de l'adhérent
        /// </summary>
        public string Nom { get => nom; set => nom = value; }

        /// <summary>
        /// Prénom de l'adhérent
        /// </summary>
        public string Prenom { get => prenom; set => prenom = value; }

        /// <summary>
        /// Le mot de passe peut-il être facultatif
        /// </summary>
        public bool CanPass { get => canPass; set => canPass = value; }

        /// <summary>
        /// Argent de l'adhérent
        /// </summary>
        public double Argent { get => argent; set => argent = value; }

        /// <summary>
        /// Est ce que le compte est toujours adhérent
        /// </summary>
        public bool StillAdherent { get => stillAdherent; set => stillAdherent = value; }

        /// <summary>
        /// Formation de l'adhérent
        /// </summary>
        public string Formation { get => formation; set => formation = value; }

        #region properties used in IHM
        /// <summary>
        /// Nom complet de l'utilisateur
        /// </summary>
        public string NomCompletIHM
        {
            get => $"{Nom.ToUpper()} {Prenom}";
        }

        /// <summary>
        /// Renvoie l'argent de l'adhérent sous un string formatté
        /// </summary>
        public string ArgentIHM
        {
            get
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
                return converterFormatArgent.ConvertFormat(argent);
            }
        }

        
        #endregion

        public override string ToString()
        {
            return $"{Identifiant} {Nom} {Prenom} {Argent}€";
        }
    }
}
