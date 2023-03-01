using Couche_Métier;
using Couche_Métier.Log;
using DocumentFormat.OpenXml.Bibliography;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameLogs.xaml
    /// </summary>
    public partial class FrameLogs : Page
    {
        // Attribut permettant de gérer les logs
        private readonly ILog log; 
        // Liste de logs sous forme de string
        private readonly List<string> logsLine; 


        public FrameLogs()
        {
            InitializeComponent();
            this.log = new LogAdherentToTxt();
            this.logsLine = log.loadLog();

            // Si il y a des logs
            if (logsLine.Count > 0)
            {
                FillListViewLogs();
            }

            string actualMonth = DateTime.Today.ToString("MMMM yyyy");
            this.titleLog.Content = actualMonth[0].ToString().ToUpper() + actualMonth.Substring(1);
        }


        /// <summary>
        /// Remplis la list view en lisant les logs
        /// </summary>
        private void FillListViewLogs()
        {
            // Affiche les logs du mois actuels
            List<LogIHM> list = new List<LogIHM>();
            string actualMonth = DateTime.Today.ToString("MMMM yyyy");

            
            // Pour tous les logs
            for (int i = logsLine.Count - 1; i > -1; i--) 
            {
                // Récupération des différents champs
                string[] splitedLosline = logsLine[i].Split('|');
                string date = DateTime.Parse(splitedLosline[0]).ToString("g");
                int heureCourte = DateTime.Parse(splitedLosline[0]).Hour;
                string type = splitedLosline[1];
                string message = splitedLosline[2];
                string auteur = splitedLosline[3];
                string operation = splitedLosline[4];

                // Affiche les logs selon les critères
                if ((DateTime.Parse(date).ToString("MMMM yyyy") == actualMonth && this.timespan.HigherValue >= Convert.ToInt16(heureCourte) && this.timespan.LowerValue <= Convert.ToInt16(heureCourte)))
                {
                    
                    LogIHM newLog = null;
                    switch (type)
                    {
                        case "ADHERENT":
                            if (this.AdherentActivated.IsChecked == true)
                            {
                                        newLog = new LogIHM(date, type, message, auteur,operation);
                            }
                            break;
                        case "PRODUIT":
                                if (this.produitActivated.IsChecked == true)
                                {
                                        newLog = new LogIHM(date, type, message, auteur,operation);
                                }
                            break;
                        case "COMPTE":
                            if (this.CompteActivated.IsChecked == true)
                            {
                                    newLog = new LogIHM(date, type, message, auteur,operation);
                            }
                            break;
                        case "ACHAT":
                            if (this.produitActivated.IsChecked == true)
                            {
                                newLog = new LogIHM(date, type, message, auteur, operation);
                            }
                            break;
                        case "VENTE":
                            if (this.CompteActivated.IsChecked == true)
                            {
                                newLog = new LogIHM(date, type, message, auteur, operation);
                            }
                            break;
                    }


                    // Si le log existe alors on l'affiche avec la bonne couleur et on l 'ajoute
                    if (newLog != null)
                    {
                        
                        switch (operation)
                        {
                            case "CREATE":
                                if (this.createActivated.IsChecked == true)
                                {
                                    list.Add(newLog);
                                }
                                break;
                            case "UPDATE":
                                if (this.updateActivated.IsChecked == true)
                                {
                                    list.Add(newLog);
                                }
                                break;
                            case "DELETE":
                                if (this.deleteActivated.IsChecked == true)
                                {
                                    list.Add(newLog);
                                }
                                break;
                        }  
                    }
                }
            }

            this.listLogs.ItemsSource = list;
            
        }


        /// <summary>
        /// Permet de mettre à jour la liste des logs selon les différents critères
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetCriteria(object sender, RoutedEventArgs e)
        {
            if (logsLine != null)
            {
                FillListViewLogs();
            }
            
        }
    }
}
