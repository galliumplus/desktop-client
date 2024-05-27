using Couche_Métier.Utilitaire;
using Modeles;
using System.Globalization;

namespace Couche_IHM.VueModeles
{
    public class StatAccountViewModel
    {
        #region attributes
        private AccountViewModel adherentViewModel;
        private float argent;
        private StatAccount stat;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du stat acompte vue modele
        /// </summary>
        public StatAccountViewModel(StatAccount stat,AccountViewModel adherentViewModel)
        {
            this.stat = stat;
            this.adherentViewModel = adherentViewModel;
            this.argent = stat.Money;
        }
        #endregion

        #region properties
        /// <summary>
        /// Account de la stat
        /// </summary>
        public AccountViewModel AccountsViewModel { get => adherentViewModel; set => adherentViewModel = value; }
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

        public string Date
        {
            get => stat.Date.ToString("MMMM", new CultureInfo("fr-FR"));
        }

        /// <summary>
        /// Argent dépensé par l'acompte
        /// </summary>
        public float Argent { get => argent; set => argent = value; }
        #endregion
    }
}
