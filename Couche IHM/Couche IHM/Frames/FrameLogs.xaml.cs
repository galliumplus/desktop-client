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
        private readonly ILog log; // log
        private readonly List<string> logsLine; // Liste des logs

        public FrameLogs()
        {
            InitializeComponent();
            this.log = new LogToTXT();
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
            string montYear = DateTime.Parse(logsLine[logsLine.Count-1].Split('|')[0]).ToString("MMMM yyyy"); // Récupère la date la plus vieille

            // Pour tous les logs
            for (int i = logsLine.Count - 1; i > -1; i--) 
            {
                string[] splitedLosline = logsLine[i].Split('|'); // Sépare par catégorie
                string date = DateTime.Parse(splitedLosline[0]).ToString("g");
                string action = splitedLosline[1];
                string message = splitedLosline[2];
                string auteur = splitedLosline[3];

                // Affiche les logs que sur un mois
                if (montYear == DateTime.Parse(date).ToString("MMMM yyyy"))
                {
                    Log newLog = new Log(date, action, message, auteur);
                    // Adapte le message selon la catégorie
                    if(action == "UPDATE_ADHERENT")
                    {
                        newLog.MessageCourt = message.Split('/')[0];
                        string messageSplit = message.Split(":/")[1]; 
                        newLog.MessageComplete = string.Join('\n', messageSplit.Split('/'));
                    }
                    list.Add(newLog);
                }
            }
            this.listLogs.ItemsSource = list;

            // Change le titre de la page
            if (list.Count > 0)
                this.titleLog.Content = montYear.ToUpper()[0] + montYear.Substring(1);
        }
    }
}
