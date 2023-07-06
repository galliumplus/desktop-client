using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class AdherentViewModel
    {
        #region attributes
        /// <summary>
        /// Représente le modèle adhérent
        /// </summary>
        private Adhérent adherent;
        #endregion

        #region properties
        /// <summary>
        /// Nom complet de l'utilisateur
        /// </summary>
        public string NomCompletIHM
        {
            get => $"{adherent.Nom.ToUpper()} {adherent.Prenom}";
        }

        /// <summary>
        /// Renvoie l'argent de l'adhérent sous un string formatté
        /// </summary>
        public string ArgentIHM
        {
            get
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
                return converterFormatArgent.ConvertToString(adherent.Argent);
            }
        }

        /// <summary>
        /// Id de l'adhérent
        /// </summary>
        public string Identifiant { get => adherent.Identifiant;  }

        #endregion

        public AdherentViewModel(Adhérent adherent)
        {
            this.adherent = adherent;
        }
    }
}
