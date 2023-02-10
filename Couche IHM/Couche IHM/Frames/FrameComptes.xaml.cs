using Couche_Métier;
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
    /// Logique d'interaction pour FrameComptes.xaml
    /// </summary>
    public partial class FrameComptes : Page
    {
        public FrameComptes(UserManager userManager)
        {
            InitializeComponent();
            this.listUser.ItemsSource = userManager.Comptes;
            ListViewStockHeader();
        }

        private void ListViewStockHeader()
        {
            foreach (PropertyInfo p in typeof(User).GetProperties())
            {
                GridViewColumn column = new GridViewColumn();
                column.Header = p.Name;

                column.DisplayMemberBinding = new Binding(p.Name);
                this.GridViewStockControl.Columns.Add(column);
            }
        }
    }
}
