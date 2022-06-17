using Gallium_v1.Data;
using Gallium_v1.Logique;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
    /// Logique d'interaction pour ConnexionFenetre.xaml
    /// </summary>
    public partial class ConnexionFenetre : Window
    {
        public ConnexionFenetre()
        {
            InitializeComponent();
            Username.Focus();
            Adherents.ajoutUser("MARTEAU", "Florian", "fm427410", 100, "caca");
            Adherents.ajoutUser("CHABRET", "Damien", "dc393609", 10, "caca");
            Adherents.ajoutUser("BADET", "matteo", "petitemerde", 0, "caca");
            Adherents.ajoutUser("ROURAT", "Aimeric", "ar00000", 30, "caca");
            Adherents.ajoutUser("Resin", "Nicos", "rn000000", 10000, "caca");
            Adherents.ajoutUser("Legrand", "Simonax", "pitiemonsieur", -30, "caca");
            Stock.ajoutProduit("Cafe", 35.00, "test", Category.BOISSON, 100);
            Stock.ajoutProduit("Chips", 0.80, "test", Category.SNACKS, 10);
            Stock.ajoutProduit("Monster", 3.50, "test", Category.BOISSON, 1);
            Stock.ajoutProduit("Kit Kat", 7.00, "test", Category.SNACKS, 35);

        }

        /// <summary>
        /// Lance la méthode pour se connecter quand on appuie sur une touche 
        /// </summary>
        /// <Author> Damien.C </Author>
        private void EntreeDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.ConnexionToAccount();
            }
        }

        /// <summary>
        /// Connecte au compte si le bouton est cliqué
        /// </summary>
        /// <Author> Damien.C </Author>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ConnexionToAccount();
        }

        /// <summary>
        /// Méthode qui permet de se connecter et d'ouvrir une nouvelle fenêtre si la connection est réussi
        /// </summary>
        /// <Author> Damien.C </Author>
        private void ConnexionToAccount()
        {
           
            if (UserDAO.ConnexionUser(Username.Text, Password.Password) != null)
            {
                GalliumFenetre gallium = new GalliumFenetre();
                this.Close();
                gallium.Show();
            }

        }

        /// <summary>
        /// Fonction mystère
        /// </summary>
        private void Mysterious(Object sender, MouseEventArgs e)
        {
            Random random = new Random();


            string[] files = Directory.GetFiles(@".\Vue\Assets\Son", "*.wav");
            int msc = random.Next(files.Count());

            SoundPlayer soundPlayer = new SoundPlayer(files[msc]);
            soundPlayer.Play();
        }

    }
}
