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
    public partial class ModificationAcompte : Window
    {
        private Acompte acompte;

        /// <summary>
        /// Utilisateur en cours de modification
        /// </summary>
        public Acompte Acompte 
        { 
            get => acompte; 
    
        }

        public ModificationAcompte(Acompte u)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.acompte = u;
            this.chargeUser();
            this.positionCaretIndex();
            acompteUser.Focus();

            

        }

        /// <summary>
        /// Valide les modifications faite et de mettre à jour l'utilisateur
        /// </summary>
        /// <Author> Damien.C </Author>
        private void ValiderModif(object sender, RoutedEventArgs e)
        {
            // demande si l'utilisateur est sur de la modification
            MessageBoxResult validation = MessageBox.Show("Vous allez modifier cet utilisateur.", "Modifier l'utilisateur ?", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            
            if (validation.Equals(MessageBoxResult.OK))
            {
                double balance = Convert.ToDouble(this.balanceUser.Text);
                acompte.Compte = this.acompteUser.Text;
                acompte.Nom = this.nomUser.Text;
                acompte.Prenom = this.prénomUser.Text;
                acompte.Balance = balance;
                this.Close();
            }
        }

        /// <summary>
        /// Permet d'annuler les modifications
        /// </summary>
        /// <Author> Damien.C </Author>
        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Méthode qui charge les informations de l'acompte
        /// </summary>
        /// <Author> Damien.C </Author>
        private void chargeUser()
        {
            this.acompteUser.Text = this.acompte.Compte;
            this.nomUser.Text = this.acompte.Nom;
            this.prénomUser.Text = this.acompte.Prenom;
            this.balanceUser.Text = this.acompte.Balance.ToString();
        }

        /// <summary>
        /// Empêche l'utilisateur de mettre des lettres dans la textebox
        /// </summary>
        /// <Author> Damien.C</Author>
        private void balanceUserTextChanged(object sender, TextCompositionEventArgs e)
        {
            var tb = sender as TextBox;
            e.Handled = !double.TryParse(tb.Text + e.Text, out double d);
        }

        /// <summary>
        /// Position du caret dans le texte
        /// </summary>
        private void positionCaretIndex()
        {
           
            acompteUser.CaretIndex = acompteUser.Text.Length;
            nomUser.CaretIndex = nomUser.Text.Length;
            prénomUser.CaretIndex = prénomUser.Text.Length;
            balanceUser.CaretIndex = balanceUser.Text.Length;
            mdpUser.CaretIndex = mdpUser.Text.Length;
        }
    }
}
