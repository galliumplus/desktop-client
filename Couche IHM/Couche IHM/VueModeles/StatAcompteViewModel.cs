using Couche_Métier.Utilitaire;
using Modeles;

namespace Couche_IHM.VueModeles
{
    public class StatAcompteViewModel
    {
        #region attributes
        private AcompteViewModel adherentViewModel;
        private float argent;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du stat acompte vue modele
        /// </summary>
        public StatAcompteViewModel(StatAcompte stat,AcompteViewModel adherentViewModel)
        {
            this.adherentViewModel = adherentViewModel;
            this.argent = stat.Money;
        }
        #endregion

        #region properties
        /// <summary>
        /// Acompte de la stat
        /// </summary>
        public AcompteViewModel AdherentViewModel { get => adherentViewModel; set => adherentViewModel = value; }
        /// <summary>
        /// Argent dépensé formatté
        /// </summary>
        public string FormattedArgent
        {
            get
            {
                return ConverterFormatArgent.ConvertToString(argent);
            }
            
        }
        /// <summary>
        /// Argent dépensé par l'acompte
        /// </summary>
        public float Argent { get => argent; set => argent = value; }
        #endregion
    }
}
