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
using System.Windows.Shapes;

namespace Gallium_v1.Vue
{
    /// <summary>
    /// Logique d'interaction pour ModificationUser.xaml
    /// </summary>
    public partial class ModificationUser : Window
    {
        private User oldUser;
        private User user;
        public User User { get => user; set => user = value; }



        public ModificationUser(User u)
        {
            InitializeComponent();
            PositionCaretIndex();
            this.oldUser = u;
            this.user = u;

        }

        /// <summary>
        /// Charge dans la comboBox tous les élements de l'énumration rang utilisatuer
        /// </summary>
        private void ChargeListCategorie()
        {
            foreach (Rang element in Enum.GetValues(typeof(Rang)))
            {
                roleUser.Items.Add(element);
            }
        }

        /// <summary>
        /// Initialise position du Caret
        /// </summary>
        private void PositionCaretIndex()
        {

            identifiantUser.CaretIndex = identifiantUser.Text.Length;
            nomUser.CaretIndex = nomUser.Text.Length;
            prénomUser.CaretIndex = prénomUser.Text.Length;
            mdpUser.CaretIndex = mdpUser.Text.Length;
        }

        private void ValiderModif(object sender, RoutedEventArgs e)
        {
            // demande si l'utilisateur est sur de la modification
            MessageBoxResult validation = MessageBox.Show("Vous allez modifier ct utilisateur.", "Modifier l'utilisateur ?", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (validation.Equals(MessageBoxResult.OK))
            {
                user.IdentifiantUser = identifiantUser.Text;
                user.PrenomUser = prénomUser.Text;
                // manque rang
                user.NomUser = nomUser.Text;
                this.Close();
            }
        }

        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.User = this.oldUser;
            this.Close();
        }
    }
}
