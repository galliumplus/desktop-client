using Couche_Métier;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        private Role role;

        
        private UserManager userManager;

        #endregion
        #region events
        public RelayCommand ShowUpdate { get; set; }
        public RelayCommand ResetU { get; set; }
        public RelayCommand UpdateU { get; set; }

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
            get => nom + " " + prenom;
        }
        public Role RoleIHM { get => role; set => role = value; }
        #endregion
        public UserViewModel(User user,UserManager userManager)
        {
            // Initialisation des datas
            this.user = user;
            this.nom = user.Nom;
            this.prenom = user.Prenom;
            this.email = user.Mail;
            this.role = userManager.GetRoles().Find(x => x.Id == user.IdRole);

            this.userManager = userManager;

            // Initialisation des events
            this.ResetU = new RelayCommand(x => this.ResetUser());
            this.ShowUpdate = new RelayCommand(x => MainWindowViewModel.Instance.UserViewModel.CurrentUser = this);
            this.UpdateU = new RelayCommand(x => this.UpdateUser());
        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'user
        /// </summary>
        public void UpdateUser()
        {
            // Changer la data
            this.user.Nom = this.nom;
            this.user.Prenom = this.prenom; 
            this.user.Mail = this.email;
            this.user.IdRole = this.role.Id;
            this.userManager.UpdateCompte(this.user);

            // Log l'action
            Log log = new Log(0, DateTime.Now.ToString("g"), 6, $"Modification de l'acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            NotifyPropertyChanged(nameof(this.NomIHM));
            NotifyPropertyChanged(nameof(this.PrenomIHM));
            NotifyPropertyChanged(nameof(this.EmailIHM));
            NotifyPropertyChanged(nameof(this.RoleIHM));
            MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
            MainWindowViewModel.Instance.UserViewModel.ShowModifCreateUser = false;
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
    }
}
