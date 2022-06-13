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
    /// Logique d'interaction pour AcompteFrame.xaml
    /// </summary>
    public partial class AcompteFrame : Page
    {
        public AcompteFrame()
        {
            InitializeComponent();

            Adherent.ajoutUser("MARTEAU", "Florian", "fm427410", 100);
            Adherent.ajoutUser("CHABRET", "Damien", "dc393609", 10);
            Adherent.ajoutUser("MATTEO", "Badet", "petitemerde", 0);
            Adherent.ajoutUser("ROURAT", "Aimeric", "ar00000", 30);
            Adherent.ajoutUser("Resin", "Nicos", "rn000000", 10000);
            Adherent.ajoutUser("Legrand", "Simonax", "pitiemonsieur", -30);

            acomptelist.ItemsSource = Adherent.Users;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.rechercheAcompte.IsFocused == true)
                {
                    afficheUser(this.rechercheAcompte.Text);
                }
            }
            
        }


        /// <summary>
        /// Permet d'afficher les informations d'un user
        /// </summary>
        /// <param name="nomUser"></param>
        private void afficheUser(string nomUser)
        {
            User user = Adherent.findUser(nomUser);
            if(user != null)
            {
                this.compte.Text = user.Compte;
                this.balance.Text = Convert.ToString(user.Balance);
                this.infouser.Text = user.Nom;
            }
            
        }
    }
}
