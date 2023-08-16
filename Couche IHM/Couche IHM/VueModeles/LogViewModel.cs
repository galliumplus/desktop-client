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
                    case "ACOMPTE":
                        color = new SolidColorBrush(Colors.Orange);
                        break;
                    case "PRODUIT":
                        color = new SolidColorBrush(Colors.LightGreen);
                        break;
                    case "COMPTE":
                        color = new SolidColorBrush(Colors.Gray);
                        break;
                    case "VENTE":
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
                    case "ACOMPTE":
                        image = PackIconKind.AccountCash;
                        break;
                    case "PRODUIT":
                        image = PackIconKind.FoodApple;
                        break;
                    case "COMPTE":
                        image = PackIconKind.AccountStar;
                        break;
                    case "VENTE":
                        image = PackIconKind.Cart;
                        break;
                }

                return image;
            }
        }

        /// <summary>
        /// Petit descriptif de l'opération
        /// </summary>
        public string MessageCourt
        { 
            get => log.MessageCourt; 
        }

        /// <summary>
        /// Auteur de l'opération
        /// </summary>
        public string Auteur 
        { 
            get => log.Auteur; 
        }

        /// <summary>
        /// Action effectué par l'opération
        /// </summary>
        public string Operation 
        { 
            get => log.Operation; 
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
