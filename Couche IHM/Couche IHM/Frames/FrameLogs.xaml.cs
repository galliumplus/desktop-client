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
        public FrameLogs()
        {
            InitializeComponent();
            FillListView();
        }

        /// <summary>
        /// Remplis la list view en lisant les logs
        /// </summary>
        private void FillListView()
        {
            // Mois des derniers logs
            

            ILog log = new LogToTXT();
            List<string> logsLine = log.loadLog();
            List<Log> list = new List<Log>();
            string montYear = DateTime.Parse(logsLine[logsLine.Count-1].Split('|')[0]).ToString("MMMM yyyy"); // Récupère la date la plus vieille
            for (int i = logsLine.Count - 1; i > -1; i--)
            {
                string date = DateTime.Parse(logsLine[i].Split('|')[0]).ToString("g");
                string action = logsLine[i].Split('|')[1];
                string message = logsLine[i].Split('|')[2];
                string auteur = logsLine[i].Split('|')[3];
                // Affiche les logs que sur un mois
                if (montYear == DateTime.Parse(date).ToString("MMMM yyyy"))
                {
                    list.Add(new Log(date, action, message, auteur));
                }
            }
            this.listLogs.ItemsSource = list;

            if (list.Count > 0)
                this.titleLog.Content = montYear.ToUpper()[0] + montYear.Substring(1);
        }

        
    }
}
