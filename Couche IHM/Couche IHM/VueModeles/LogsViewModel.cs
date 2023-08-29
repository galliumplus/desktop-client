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
        private int currentAnnee;
        private string currentMois;
        private List<string> mois = new List<string>()
        {
            "janvier",
            "février",
            "mars",
            "avril",
            "mai",
            "juin",
            "juillet",
            "août",
            "septembre",
            "octobre",
            "novembre",
            "décembre"
        };
        private List<int> annees;
        private List<int> themeLog;
        private bool selectVente;
        private bool selectConnexion;
        private bool selectProduct;
        private bool selectAcompte;
        private bool selectCompte;
        #endregion

        #region constructor

        /// <summary>
        /// Constructeur du logs vue modele
        /// </summary>
        public LogsViewModel(UserManager userManager,LogManager logManager)
        {
            // Initialisation des datas
            this.themeLog = new List<int>();
            this.userManager = userManager;
            this.logManager = logManager;
            DateTime currentDate = DateTime.Now;
            currentAuteur = Auteurs[0];
            int année = Convert.ToInt16(currentDate.ToString("yyyy"));
            this.annees = new List<int>() { année, année - 1, année - 2 };
            currentMois = currentDate.ToString("MMMM");
            currentAnnee = année;
            this.SelectVente = true;
            this.SelectAcompte = false;
            this.SelectConnexion = false;
            this.SelectProduct = false;
            this.SelectCompte = false;
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
                NotifyPropertyChanged(nameof(Logs));
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
                    auteurs.Add($"{u.Prenom} {u.Nom}");
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
                List<LogViewModel> logsFiltres = logs.ToList().FindAll(x => themeLog.Contains(x.IdTheme));
                if (currentAuteur != "Tout le monde")
                {
                    logsFiltres = logsFiltres.FindAll(x => x.Auteur == currentAuteur);
                }
                return new ObservableCollection<LogViewModel>(logsFiltres);
            }
            set => logs = value; 
        }

        /// <summary>
        /// Filtre afficher les ventes
        /// </summary>
        public bool SelectVente 
        { 
            get => selectVente;
            set 
            {
                if (value)
                {
                    this.themeLog.Add(5);
                }
                else
                {
                    this.themeLog.Remove(5);
                }
                selectVente = value;
                NotifyPropertyChanged(nameof(Logs));
            }
        }

        /// <summary>
        /// Filtre afficher les connexions
        /// </summary>
        public bool SelectConnexion
        {
            get => selectConnexion;
            set 
            {
                if (value)
                {
                    this.themeLog.Add(1);
                }
                else
                {
                    this.themeLog.Remove(1);
                }
                selectConnexion = value;
                NotifyPropertyChanged(nameof(Logs));
            } 
        }
        /// <summary>
        /// Filtre afficher les produits
        /// </summary>
        public bool SelectProduct 
        { 
            get => selectProduct;
            set 
            {
                if (value)
                {
                    this.themeLog.Add(3);
                }
                else
                {
                    this.themeLog.Remove(3);
                }
                selectProduct = value;
                NotifyPropertyChanged(nameof(Logs));
            }
        }

        /// <summary>
        /// Filtre afficher les acomptes
        /// </summary>
        public bool SelectAcompte 
        { 
            get => selectAcompte;
            set
            {
                if (value)
                {
                    this.themeLog.Add(2);
                }
                else
                {
                    this.themeLog.Remove(2);
                }
                selectAcompte = value;
                NotifyPropertyChanged(nameof(Logs));
            }
        }

        /// <summary>
        /// Filtre afficher les comptes
        /// </summary>
        public bool SelectCompte 
        { 
            get => selectCompte;
            set 
            {
                if (value)
                {
                    this.themeLog.Add(6);
                }
                else
                {
                    this.themeLog.Remove(6);
                }
                selectCompte = value;
                NotifyPropertyChanged(nameof(Logs));
            }
        }
        /// <summary>
        /// Liste des mois disponibles
        /// </summary>
        public List<string> Mois { get => mois; set => mois = value; }

        /// <summary>
        /// Liste des années disponibles
        /// </summary>
        public List<int> Annees { get => annees; set => annees = value; }

        /// <summary>
        /// Mois sélectionné
        /// </summary>
        public string CurrentMois 
        { 
            get => currentMois;
            set
            {
                currentMois = value;
                this.logs.Clear();
                InitLogs(this.mois.IndexOf(currentMois) + 1, this.currentAnnee);
                NotifyPropertyChanged(nameof(Logs));
            }
        }

        /// <summary>
        /// Annee selectionné
        /// </summary>
        public int CurrentAnnee 
        { 
            get => currentAnnee;
            set
            {
                currentAnnee = value;
                this.logs.Clear();
                InitLogs(this.mois.IndexOf(currentMois) + 1, this.currentAnnee);
                NotifyPropertyChanged(nameof(Logs));
            }
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
        public void InitLogs(int mois=0,int annee=0)
        {
            List<Log> logs = this.logManager.GetLogs(mois,annee);
            foreach (Log log in logs)
            {
                this.logs.Add(new LogViewModel(log));
            }

        }

        /// <summary>
        /// Permet d'ajouter un log
        /// </summary>
        /// <param name="log">og à ajouter</param>
        public void AddLog(LogViewModel log)
        {

            if (mois.IndexOf(currentMois)+1 == Convert.ToInt16(log.DateTime.ToString("MM")) && currentAnnee == Convert.ToInt16(log.DateTime.ToString("yyyy")))
            {
                this.logs.Insert(0, log);
            }
            
        }

        #endregion
    }
}
