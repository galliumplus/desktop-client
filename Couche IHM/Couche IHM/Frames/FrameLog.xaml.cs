using Couche_Métier;
using Couche_Métier.Log;
using Modeles;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameLogs.xaml
    /// </summary>
    public partial class FrameLog : Page
    {
        // Attribut permettant de gérer les logs
        private readonly ILog log; 
        // Liste de logs sous forme de string
        private readonly List<string> logsLine; 

        // Manager des différents comptes admin possibles
        private UserManager userManager = new UserManager();


        public FrameLog()
        {
            
            InitializeComponent();

            // Initialisation des attributs
            this.log = new LogAdherentToTxt();
            this.logsLine = log.loadLog();

            // Si il y a des logs
            if (logsLine.Count > 0)
            {
                FillListViewLogs();
            }

           

            // Initialisation des éléments de la combobox avec les auteurs possibles
            List<User> userList = userManager.GetComptes();
            this.auteurs.Items.Add("Tout le monde");
            foreach (User user in userList)
            {
                this.auteurs.Items.Add(user.NomComplet);
            }
            this.auteurs.SelectedItem = "Tout le monde";
        }


        /// <summary>
        /// Remplis la list view en lisant les logs
        /// </summary>
        private void FillListViewLogs()
        {
            // Affiche les logs du mois actuels
            List<Log> list = new List<Log>();
            string actualMonth = DateTime.Today.ToString("MMMM yyyy");

            
            // Parcoure tous les logs
            for (int i = logsLine.Count - 1; i > -1; i--) 
            {
                // Récupération des informations
                string[] SplitedLogLine = logsLine[i].Split('|');

                string date = DateTime.Parse(SplitedLogLine[0]).ToString("g");
                int heureCourte = DateTime.Parse(SplitedLogLine[0]).Hour;
                string type = SplitedLogLine[1];
                string message = SplitedLogLine[2];
                string auteur = SplitedLogLine[3];
                string operation = SplitedLogLine[4];


                // Affiche les logs selon les critères
                if (DateTime.Parse(date).ToString("MMMM yyyy") == actualMonth && RespectAuthorFilter(auteur) && RespectTimeSpanFilter(heureCourte))
                {
                    
                    Log newLog = null;
                    switch (type)
                    {
                        case "ADHERENT":
                            if (this.AdherentActivated.IsChecked == true)
                            {
                                        newLog = new Log(date, type, message, auteur,operation);
                            }
                            break;
                        case "PRODUIT":
                                if (this.produitActivated.IsChecked == true)
                                {
                                        newLog = new Log(date, type, message, auteur,operation);
                                }
                            break;
                        case "COMPTE":
                            if (this.CompteActivated.IsChecked == true)
                            {
                                    newLog = new Log(date, type, message, auteur,operation);
                            }
                            break;
                        case "ACHAT":
                            if (this.produitActivated.IsChecked == true)
                            {
                                newLog = new Log(date, type, message, auteur, operation);
                            }
                            break;
                        case "VENTE":
                            if (this.VenteActivated.IsChecked == true)
                            {
                                newLog = new Log(date, type, message, auteur, operation);
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
                            case "VENTE":
                                    list.Add(newLog);
                                break;
                        }  
                    }
                }
            }

            this.listLogs.ItemsSource = list;
            
        }

        /// <summary>
        /// Permet de savoir si le log respecte le filtre
        /// </summary>
        /// <returns></returns>
        private bool RespectAuthorFilter(string author)
        {
            bool result = false;
            if (auteurs.SelectedItem == "Tout le monde")
            {
                result = true;
            }
            else
            {
                result = ((string)auteurs.SelectedItem == author);
            }
            return result;
        }

        /// <summary>
        /// Permet de savoir si le log respecte le filtre de l'heure
        /// </summary>
        /// <param name="hour">Heure du log</param>
        /// <returns>si le log respecte le filtre</returns>
        private bool RespectTimeSpanFilter(int hour)
        {
            bool result = false;
            return this.timespan.HigherValue >= hour && this.timespan.LowerValue <= hour;
        }

        /// <summary>
        /// Permet de mettre à jour la liste des logs selon les différents critères
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetCriteria(object sender, RoutedEventArgs e)
        {
            if (logsLine != null && this.auteurs != null && this.auteurs.SelectedItem != null)
            {
                FillListViewLogs();
            }
            
        }

        /// <summary>
        /// Permet de mettre à jour la liste des logs selon les différents critères
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetCriteria(object sender, SelectionChangedEventArgs e)
        {
            if (logsLine != null && this.auteurs != null && this.auteurs.SelectedItem != null)
            {
                FillListViewLogs();
            }
        }
    }
}
