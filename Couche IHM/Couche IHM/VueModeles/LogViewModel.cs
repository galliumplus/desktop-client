using MaterialDesignThemes.Wpf;
using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Couche_IHM.VueModeles
{
    public class LogViewModel
    {

        #region attributes
        private Log log;
        #endregion

        #region properties

        /// <summary>
        /// Date de l'opération
        /// </summary>
        public string Date 
        { 
            get => log.Date.ToString("g"); 
        }

        public DateTime DateTime
        {
            get => log.Date;
        }
        /// <summary>
        /// Représente la couleur de l'opération
        /// </summary>
        public SolidColorBrush ColorTheme
        {
            get
            {
                SolidColorBrush color = null;
                switch (log.Theme)
                {
                    case 1:
                        color = new SolidColorBrush(Colors.Green);
                        break;
                    case 2:
                        color = new SolidColorBrush(Colors.Orange);
                        break;
                    case 3:
                        color = new SolidColorBrush(Colors.LightGreen);
                        break;
                    case 6:
                        color = new SolidColorBrush(Colors.Gray);
                        break;
                    case 5:
                        color = new SolidColorBrush(Colors.LightBlue);
                        break;
                }
                return color;
            }
        }

        /// <summary>
        /// Theme general de l'operation
        /// </summary>
        public PackIconKind Theme
        {
            get
            {
                PackIconKind image = PackIconKind.Help;
                switch (log.Theme)
                {
                    case 1:
                        image = PackIconKind.AccountArrowRight;
                        break;
                    case 2:
                        image = PackIconKind.AccountCash;
                        break;
                    case 3:
                        image = PackIconKind.Food;
                        break;
                    case 6:
                        image = PackIconKind.AccountStar;
                        break;
                    case 5:
                        image = PackIconKind.Cart;
                        break;
                }

                return image;
            }
        }

        public int IdTheme
        {
            get => log.Theme;
        }

        /// <summary>
        /// Petit descriptif de l'opération
        /// </summary>
        public string MessageCourt
        { 
            get => log.Message; 
        }

        /// <summary>
        /// Auteur de l'opération
        /// </summary>
        public string Auteur 
        { 
            get => log.Auteur; 
        }
        #endregion


        /// <summary>
        /// Constructeur
        /// </summary>
        public LogViewModel(Log log)
        {
            this.log = log;
        }
    }
}
