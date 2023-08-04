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
        /// Représente l'image correspondant au type d'opération
        /// </summary>
        public PackIconKind KindOperationImage
        {
            get
            {
                PackIconKind image = PackIconKind.Help;
                switch (log.Operation)
                {
                    case "UPDATE":
                        image = PackIconKind.Edit;
                        break;
                    case "CREATE":
                        image = PackIconKind.Plus;
                        break;
                    case "DELETE":
                        image = PackIconKind.Delete;
                        break;
                    case "VENTE":
                        image = PackIconKind.Cart;
                        break;
                }


                return image;
            }
        }

        /// <summary>
        /// Représente la couleur de l'opération
        /// </summary>
        public SolidColorBrush ColorAction
        {
            get
            {
                SolidColorBrush color = null;
                switch (log.Operation)
                {
                    case "UPDATE":
                        color = new SolidColorBrush(Colors.Orange);
                        break;
                    case "CREATE":
                        color = new SolidColorBrush(Colors.Green);
                        break;
                    case "DELETE":
                        color = new SolidColorBrush(Colors.DarkRed);
                        break;
                    case "VENTE":
                        color = new SolidColorBrush(Colors.Yellow);
                        break;
                }
                return color;
            }
        }

        /// <summary>
        /// Date de l'opération
        /// </summary>
        public string Date 
        { 
            get => log.Date; 
        }

        /// <summary>
        /// Theme general de l'operation
        /// </summary>
        public string Theme
        { 
            get => log.Theme; 
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
