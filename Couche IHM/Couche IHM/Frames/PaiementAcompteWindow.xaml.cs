
using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour PaiementAcompteWindow.xaml
    /// </summary>
    public partial class PaiementAcompteWindow : Window
    {
        // Représente le prix à payer
        private double priceA;
        private double priceNA;

        // Représente la liste des adhérents possible pour payer
        private List<Adhérent> adhérents;

        // Représente l'adhérent sélectionné pour payer
        private Adhérent? adhérentSelectionné = null;

        public Adhérent? AdhérentSelectionné { get => adhérentSelectionné;}

        public PaiementAcompteWindow(List<Adhérent> adhérents, double priceA, double priceNA)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.priceA = priceA;
            this.priceNA = priceNA;
            this.adhérents = adhérents;
            this.acompteList.ItemsSource = adhérents;
        }

        /// <summary>
        /// Permet d'annuler le paiement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Permet de payer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pay(object sender, RoutedEventArgs e)
        {/**
            try
            {
                // Si un adhérent a été selectionné
                if (this.adhérentSelectionné != null)
                {
                    // S'il n'a pas besoin de mettre son mot de passe
                    if (this.adhérentSelectionné.CanPass)
                    {
                        double priceToPay = this.priceNA;
                        // S'il est adhérent
                        if (this.adhérentSelectionné.StillAdherent)
                        {
                            priceToPay = this.priceA;
                        }

                        // S'il a assez d'argent
                        if (this.adhérentSelectionné.Argent >= priceToPay)
                        {
                            this.adhérentSelectionné.Argent -= priceToPay;
                            DialogResult = true;
                        }
                        else
                        {
                            throw new Exception("ARGENT");
                        }
                    }
                    else
                    {
                        if (this.passwordAcompte.Password == "MDPtemp")
                        {
                            double priceToPay = this.priceNA;
                            // S'il est adhérent
                            if (this.adhérentSelectionné.StillAdherent)
                            {
                                priceToPay = this.priceA;
                            }

                            // S'il a assez d'argent
                            if (this.adhérentSelectionné.Argent >= priceToPay)
                            {
                                this.adhérentSelectionné.Argent -= priceToPay; 
                                DialogResult = true;
                            }
                            else
                            {
                                throw new Exception("ARGENT");
                            }
                        }
                        else
                        {
                            throw new Exception("MDP");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            **/
        }

        /// <summary>
        /// Permet de choisir celui qui va payer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeBuyer(object sender, SelectionChangedEventArgs e)
        {
            if (this.acompteList.SelectedItem != null)
            {
                this.adhérentSelectionné = adhérents.Find(x => x.Identifiant == ((Adhérent)this.acompteList.SelectedItem).Identifiant);
                if (this.adhérentSelectionné != null && this.adhérentSelectionné.CanPass)
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    Random random = new Random();
                    string randomMdp = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
                    this.passwordAcompte.Password = randomMdp;
                    this.passwordAcompte.IsEnabled = false;
                    this.labelPassword.IsEnabled = false;
                }
                else
                {
                    this.passwordAcompte.Password = "";
                    this.passwordAcompte.IsEnabled = true;
                    this.labelPassword.IsEnabled = true;
                }
            }
            else
            {
                this.passwordAcompte.Password = "";
                this.passwordAcompte.IsEnabled = true;
                this.labelPassword.IsEnabled = true;
                this.adhérentSelectionné = null;
            }
            
        }
    }
}
