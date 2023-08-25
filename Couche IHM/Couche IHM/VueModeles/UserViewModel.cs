using Couche_Métier;
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Couche_IHM.VueModeles
{
    public class UserViewModel : INotifyPropertyChanged
    {

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private User user;
        private string nom;
        private string prenom;
        private string email;
        private string mdpIHM1 = "";
        private string mdpIHM2 = "";
        private string action;
        private Role role;

        
        private UserManager userManager;

        #endregion
        #region events
        public RelayCommand ShowUpdate { get; set; }
        public RelayCommand ResetU { get; set; }
        public RelayCommand UpdateU { get; set; }
        public RelayCommand DeleteU { get; set; }
        public RelayCommand CreateU { get;set; }

        #endregion

        #region properties
        public string NomIHM { get => nom; set => nom = value; }
        public string PrenomIHM { get => prenom; set => prenom = value; }
        public string EmailIHM { get => email; set => email = value; }

        /// <summary>
        /// Nom complet de l'utilisateur 
        /// </summary>
        public string NomCompletIHM
        {
            get => prenom + " " + nom;
        }
        public Role RoleIHM { get => role; set => role = value; }
        public string MdpIHM2 { get => mdpIHM2; set => mdpIHM2 = value; }
        public string MdpIHM1 { get => mdpIHM1; set => mdpIHM1 = value; }
        public string Action
        {
            get => action;
            set { action = value;
                NotifyPropertyChanged();
        }
        }

        #endregion
        public UserViewModel(User user, UserManager userManager)
        {
            // Initialisation des datas
            this.action = "UPDATE";
            this.user = user;
            this.nom = user.Nom;
            this.prenom = user.Prenom;
            this.email = user.Mail;
            this.role = userManager.GetRoles().Find(x => x.Id == user.IdRole);

            this.userManager = userManager;

            // Initialisation des events
            this.ResetU = new RelayCommand(x => this.ResetUser());
            this.ShowUpdate = new RelayCommand(x => 
            {
            MainWindowViewModel.Instance.UserViewModel.CurrentUser = this;
                MainWindowViewModel.Instance.UserViewModel.OpenUserDetails("UPDATE"); });
            this.UpdateU = new RelayCommand(x => this.UpdateUser());
            this.CreateU = new RelayCommand(x => this.CreateUser());
            this.DeleteU = new RelayCommand(x => this.DeleteUser());
        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'user
        /// </summary>
        public void UpdateUser()
        {
            if (this.mdpIHM1 == this.mdpIHM2)
            {
                // Changer la data
                this.user.Nom = this.nom;
                this.user.Prenom = this.prenom;
                this.user.Mail = this.email;
                this.user.IdRole = this.role.Id;
                if (this.mdpIHM1 != "")
                {
                    this.user.HashedPassword = CryptStringToSHA256.Hash(this.mdpIHM2);
                }
                this.userManager.UpdateCompte(this.user);
                this.MdpIHM1 = "";
                this.MdpIHM2 = "";

                // Log l'action
                Log log = new Log(0, DateTime.Now, 6, $"Modification du compte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);

                // Notifier la vue
                NotifyPropertyChanged(nameof(this.NomCompletIHM));
                NotifyPropertyChanged(nameof(this.NomIHM));
                NotifyPropertyChanged(nameof(this.PrenomIHM));
                NotifyPropertyChanged(nameof(this.EmailIHM));
                NotifyPropertyChanged(nameof(this.RoleIHM));
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
                MainWindowViewModel.Instance.UserViewModel.ShowModifCreateUser = false;
            }
            else
            {
                MessageBox.Show("Les deux mots de passe ne correspondent pas");
            }
          
        }

        /// <summary>
        /// Permet de créer visuellement les modifications de l'user
        /// </summary>
        public void CreateUser()
        {
            if (this.mdpIHM1 == this.mdpIHM2)
            {
                // Changer la data
                this.user.Nom = this.nom;
                this.user.Prenom = this.prenom;
                this.user.Mail = this.email;
                this.user.IdRole = this.role.Id;
                this.user.HashedPassword = CryptStringToSHA256.Hash(this.mdpIHM2);

                this.userManager.CreateCompte(this.user);
                this.MdpIHM1 = "";
                this.MdpIHM2 = "";
                this.action = "UPDATE";

                // Log l'action
                Log log = new Log(0, DateTime.Now, 6, $"Création du compte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);

                // Notifier la vue
                NotifyPropertyChanged(nameof(this.NomIHM));
                NotifyPropertyChanged(nameof(this.PrenomIHM));
                NotifyPropertyChanged(nameof(this.EmailIHM));
                NotifyPropertyChanged(nameof(this.RoleIHM));
                MainWindowViewModel.Instance.UserViewModel.AddUser(this);
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
                MainWindowViewModel.Instance.UserViewModel.ShowModifCreateUser = false;
            }
            else
            {
                MessageBox.Show("Les deux mots de passe ne correspondent pas");
            }

        }



        /// <summary>
        /// Permet de reset les propriétés de l'user
        /// </summary>
        public void ResetUser()
        {
            // Initialisation propriétés
            this.nom = user.Nom;
            this.prenom = user.Prenom;
            this.email = user.Mail;
            this.role = userManager.GetRoles().Find(x => x.Id == user.IdRole);

            // Notifier la vue
            NotifyPropertyChanged(nameof(NomIHM));
            NotifyPropertyChanged(nameof(PrenomIHM));
            NotifyPropertyChanged(nameof(EmailIHM));
            NotifyPropertyChanged(nameof(RoleIHM));

            MainWindowViewModel.Instance.UserViewModel.ShowModifCreateUser = false;
        }

        /// <summary>
        /// Permet de reset les propriétés de l'user
        /// </summary>
        public void DeleteUser()
        {
            // Initialisation propriétés
            this.userManager.RemoveCompte(this.user);

            // Log l'action
            Log log = new Log(0, DateTime.Now, 6, $"Suppression du compte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            MainWindowViewModel.Instance.UserViewModel.RemoveUser(this);
            MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
            MainWindowViewModel.Instance.UserViewModel.ShowModifCreateUser = false;
        }

    }
}
