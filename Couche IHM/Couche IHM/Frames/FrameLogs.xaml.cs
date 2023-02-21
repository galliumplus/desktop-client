using Couche_Métier;
using Couche_Métier.Log;
using System;
using System.Collections.Generic;
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
            // Splits
            ILog log = new LogToTXT();
            List<string> logs = log.loadLog();

            foreach(string s in logs)
            {
                this.listLogs.Items.Add(s);
            }


        }
    }
}
