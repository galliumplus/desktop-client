using Gallium_v1.Logique;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Gallium_v1.Vue.Frame
{
    /// <summary>
    /// Logique d'interaction pour AccueilFrame.xaml
    /// </summary>
    public partial class AccueilFrame : Page
    {
        public AccueilFrame()
        {
            InitializeComponent();
            AcompteName.Content = Adherent.calculPlusGrosAcompte().Nom;
            AcompteBalance.Content = $"{Adherent.calculPlusGrosAcompte().Balance} €";
        }

        private void IsDriveClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/document/d/1Bkuc-J_cueaCw7v5Wg68-nqqEbfaM76i/edit");
        }

        private void IsTwitterClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/Etiq_Dijon");
        }
    }
}
