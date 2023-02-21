using Couche_Métier;
using Couche_Métier.Log;
using DocumentFormat.OpenXml.Bibliography;
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
            ILog log = new LogToTXT();
            
            List<string> logsLine = new List<string>();
            List<Log> list = new List<Log>();
            for(int i = logsLine.Count; i > 0; i--)
            {
                string date = logsLine[i].Split('|')[0];
                string action = logsLine[i].Split('|')[1];
                string message = logsLine[i].Split('|')[2];
                string auteur = logsLine[i].Split('|')[3];
                list.Add(new Log(date, action, message, auteur));
            }
            foreach(string logItem in log.loadLog())
            {
                
            }
            this.listLogs.ItemsSource = list;
        }
    }
}
