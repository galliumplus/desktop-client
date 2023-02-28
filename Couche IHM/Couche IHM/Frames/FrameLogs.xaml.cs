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
            this.log = new LogToTxt();
            this.logsLine = log.loadLog();

            // Si il y a des logs
            if(logsLine.Count > 0)
            {
                FillListViewLogs();
            }
            
        }

        /// <summary>
        /// Remplis la list view en lisant les logs
        /// </summary>
        private void FillListViewLogs()
        {
            // Affiche les logs du mois actuels
            List<Log> list = new List<Log>();
            string actualMonth = DateTime.Today.ToString("MMMM yyyy");

            // Pour tous les logs
            for (int i = logsLine.Count - 1; i > -1; i--) 
            {
                string[] splitedLosline = logsLine[i].Split('|'); // Sépare par catégorie
                string date = DateTime.Parse(splitedLosline[0]).ToString("g");
                string action = splitedLosline[1];
                string message = splitedLosline[2];
                string auteur = splitedLosline[3];

                // Affiche les logs du mois actuels
                if (DateTime.Parse(date).ToString("MMMM yyyy") == actualMonth)
                {
                    Log newLog = new Log(date, action, message, auteur);
                    // Adapte le message selon la catégorie
                    switch (action)
                    {
                        case "UPDATE":
                            newLog.MessageCourt = message.Split('/')[0];
                            string messageSplit = message.Split(":/")[1];
                            newLog.MessageComplete = string.Join('\n', messageSplit.Split('/'));
                            break;
                        case "CREATE":
                            break;
                        case "DELETE":
                            break;
                    }

                    list.Add(newLog);
                }
            }
            this.listLogs.ItemsSource = list;

            // Change le titre de la page
            if (list.Count > 0)
                this.titleLog.Content = actualMonth[0].ToString().ToUpper() + actualMonth.Substring(1);
        }

        /// <summary>
        /// Si une ligne n'a pas de détails, elle ne s'affichera pas
        /// </summary>
        private void ToggleRowDetails(object sender, SelectionChangedEventArgs e)
        {
            Log log = (Log)this.listLogs.SelectedItem;

            // Affichage des row details si un log est sélectionné avec un message
            if (this.listLogs.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.VisibleWhenSelected)
            {
                this.listLogs.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
            }
            else if (!string.IsNullOrEmpty(log.MessageComplete))
            {
                this.listLogs.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
        }
    }
}
