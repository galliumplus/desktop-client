using Couche_Métier;
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class LogsViewModel : INotifyPropertyChanged
    {

        #region attributes 
        private ObservableCollection<LogViewModel> logs = new ObservableCollection<LogViewModel>();
        private UserManager userManager;
        private LogManager logManager;
        private string currentAuteur;
        public LogsViewModel(UserManager userManager,LogManager logManager)
        {
            this.userManager = userManager;
            this.logManager = logManager;
            CurrentAuteur = Auteurs[0];
            InitLogs();
        }


        #endregion
        #region properties
        /// <summary>
        /// Auteur sélectionné
        /// </summary>
        public string CurrentAuteur
        {
            get => currentAuteur;
            set
            {
                currentAuteur = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Liste des auteurs potentiels
        /// </summary>
        public List<string> Auteurs
        {
            get
            {
                List<string> auteurs = new List<string>() { "Tout le monde" };
                List<User> users = this.userManager.GetComptes();
                foreach (User u in users)
                {
                    auteurs.Add(u.Nom);
                }
                return auteurs;
            }
        }

        /// <summary>
        /// Liste des logs
        /// </summary>
        public ObservableCollection<LogViewModel> Logs 
        {
            get 
            {
                return logs;
            }
            set => logs = value; 
        }
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region methods
        /// <summary>
        /// Permet d'initialiser la liste des logs
        /// </summary>
        public void InitLogs()
        {
            List<Log> logs = this.logManager.GetLogs();
            foreach (Log log in logs)
            {
                this.logs.Add(new LogViewModel(log));
            }

        }

        #endregion
    }
}
