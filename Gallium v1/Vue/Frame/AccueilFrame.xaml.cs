using BotETIQ;
using Gallium_v1.Logique;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Acompte.Content = Adherents.calculPlusGrosAcompte();
        }

        private void IsDriveClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/document/d/1Bkuc-J_cueaCw7v5Wg68-nqqEbfaM76i/edit");
        }

        private void IsTwitterClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/Etiq_Dijon");
        }

        private void IsDiscordClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.com/login");
        }

        private void IsInstaClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://instagram.com/etiq_dijon/");
        }

        private void B_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            Rectangle rect = gd.Children[0] as Rectangle;
            rect.Fill = Brushes.Gray;
        }

        private void B_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid gd = sender as Grid;
            Rectangle rect = gd.Children[0] as Rectangle;
            rect.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#302F2F");
        }
        private void Alu_Bot(object sender, RoutedEventArgs e)
        {
            ConfigBot.BotState = true;
            StateBot.IsChecked = ConfigBot.BotState;
        }
        private void Etein_Bot(object sender, RoutedEventArgs e)
        {
            ConfigBot.BotState = false;
            StateBot.IsChecked = ConfigBot.BotState;
        }
        private void Redem_Bot(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
