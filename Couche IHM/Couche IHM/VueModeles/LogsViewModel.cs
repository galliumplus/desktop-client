using Couche_Métier;
using Couche_Métier.Log;
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
    public class LogsViewModel : INotifyPropertyChanged
    {

        #region attributes 
        private List<LogViewModel> logs = new List<LogViewModel>();
        private ILog logManager;
        private UserManager userManager;
        private string currentAuteur;
        public LogsViewModel()
        {
            
            this.userManager = new UserManager();
            this.CurrentAuteur = Auteurs[0];
            this.logManager = new LogAdherentToTxt();
            List<Log> logs = logManager.loadLog();
            foreach (Log l in logs)
            {
                this.logs.Add(new LogViewModel(l));
            }
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
                List<User> users = userManager.GetComptes();
                foreach (User u in users)
                {
                    auteurs.Add(u.NomComplet);
                }
                return auteurs;
            }
        }
        public List<LogViewModel> Logs 
        {
            get 
            { 
                return logs.FindAll(x => DateTime.Parse(x.Date).ToString("MMMM yyyy") == DateTime.Today.ToString("MMMM yyyy")); 
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
    }
}
