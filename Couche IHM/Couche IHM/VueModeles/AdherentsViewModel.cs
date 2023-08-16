using Couche_Métier;
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Couche_IHM.VueModeles
{
    public class AdherentsViewModel : INotifyPropertyChanged
    {
        #region attributes
        private ObservableCollection<AdherentViewModel> adherents;
        private AdherentViewModel currentAdherent;
        private AdhérentManager adherentManager;
        private string searchFilter = "";
        private bool showAdherent = false;
        private bool showModifButtons = false;
        private bool showDeleteAcompte = false;
        private bool dialogModifAdherent = false;
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region events
        public RelayCommand OpenModifAdh { get; set; }
        #endregion

        #region properties
        /// <summary>
        /// Liste des adhérents
        /// </summary>
        public List<PodiumAdherent> PodiumAdherents
        {
            get
            {
                List<PodiumAdherent> podAdherents = new List<PodiumAdherent>();
                List<AdherentViewModel> adhs = adherents.OrderByDescending(a => a.PurchaseCount).Take(3).ToList();
                for (int i = 0; i < 3; i++)
                {
                    podAdherents.Add(new PodiumAdherent(adhs[i],i));
                }
                return podAdherents;
            }
        }

        /// <summary>
        /// Liste des adhérents
        /// </summary>
        public ObservableCollection<AdherentViewModel> Adherents 
        {
            get 
            {
                ObservableCollection<AdherentViewModel> adhs;
                if (searchFilter == "")
                {
                    adhs = this.adherents;
                }
                else
                {
                    adhs = new ObservableCollection<AdherentViewModel>(adherents.ToList().FindAll(adh =>
                    adh.NomCompletIHM.ToUpper().Contains(searchFilter.ToUpper()) ||
                    adh.IdentifiantIHM.ToUpper().Contains(searchFilter.ToUpper()))) ;
                }
                
                return adhs;
            }
            set => adherents = value; 
        }

        /// <summary>
        /// Filtre de barre de recherche
        /// </summary>
        public string SearchFilter 
        { 
            get => searchFilter;
            set 
            { 
                searchFilter = value; 
                NotifyPropertyChanged(nameof(Adherents)); 
            } 
        }

        /// <summary>
        /// Est ce qu'on affiche la fenetre de l adherent
        /// </summary>
        public bool ShowAdherent
        { 
            get => showAdherent;
            set 
            { 
                showAdherent = value; 
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Permet d'afficher les boutons de modification de l'adhérent
        /// </summary>
        public bool ShowModifButtons
        { 
            get => showModifButtons;
            set 
            {
                showModifButtons = value; 
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Représente l'adherent selectionné
        /// </summary>
        public AdherentViewModel CurrentAdherent 
        { 
            get => currentAdherent;
            set 
            {
                if (currentAdherent != null)
                {
                    this.currentAdherent.ResetAdherent();
                }
                
                currentAdherent = value;
                if (value != null)
                {
                    ShowAdherent = true;

                }
                else
                {
                    this.ShowAdherent = false;
                    
                }

                NotifyPropertyChanged(nameof(CurrentAdherent));
            }
        }

        public bool DialogModifAdherent
        {
            get => dialogModifAdherent;
            set
            {
                dialogModifAdherent = value;
                NotifyPropertyChanged(nameof(DialogModifAdherent));
            }
        }

        /// <summary>
        /// Permet d'afficher le bouton de suppresion
        /// </summary>
        public bool ShowDeleteAcompte 
        { 
            get => showDeleteAcompte;
            set 
            {
                showDeleteAcompte = value;
                NotifyPropertyChanged();
            }

        }


        #endregion

        /// <summary>
        /// Constructeur de la classe adhérents view model
        /// </summary>
        public AdherentsViewModel()
        {
            this.adherentManager = new AdhérentManager();
            this.adherents = new ObservableCollection<AdherentViewModel>();
            this.OpenModifAdh = new RelayCommand(x => this.OpenAcompteDetails((string)x));
            InitAdhérents();

        }

        #region methods
        /// <summary>
        /// Permet de récupérer la liste des adhérents
        /// </summary>
        private void InitAdhérents()
        {
            List<Adhérent> adherents = this.adherentManager.GetAdhérents();
            Random random = new Random();
            foreach (Adhérent adh in adherents)
            {
                int rand = random.Next(0, 100);
                this.adherents.Add(new AdherentViewModel(adh,this.adherentManager,rand));
            }
            this.CurrentAdherent = this.adherents[0];
        }

        /// <summary>
        /// Permet d'ouvrir le détail de l'adhérent
        /// </summary>
        /// <param name="action"></param>
        private void OpenAcompteDetails(string action)
        {

            if (action == "NEW" || currentAdherent.Action == "NEW")
            {
                ShowDeleteAcompte = false;
                CurrentAdherent = new AdherentViewModel(new Adhérent(),this.adherentManager, 0);
            }
            else
            {
                ShowDeleteAcompte = true;
            }

            DialogModifAdherent = true;
        }

        /// <summary>
        /// Permet de rajouter un acompte  dans la liste
        /// </summary>
        /// <param name="acompte"></param>
        public void AddAcompte(AdherentViewModel acompte)
        {
            this.adherents.Add(acompte);
            NotifyPropertyChanged(nameof(Adherents));

        }

        /// <summary>
        /// Permet de supprimer un acompte  dans la liste
        /// </summary>
        /// <param name="acompte"></param>
        public void RemoveAcompte(AdherentViewModel acompte)
        {
            this.adherents.Remove(acompte);
            NotifyPropertyChanged(nameof(Adherents));
        }


        #endregion


    }
}
