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
using System.Windows.Media;

namespace Couche_IHM.VueModeles
{
    public class AdherentsViewModel : INotifyPropertyChanged
    {
        #region attributes
        private List<AdherentViewModel> adherents;
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
        public List<AdherentViewModel> Adherents 
        {
            get 
            {
                List<AdherentViewModel> adhs;
                if (searchFilter == "")
                {
                    adhs = this.adherents;
                }
                else
                {
                   
                    adhs = adherents.FindAll(adh =>
                    adh.NomCompletIHM.ToUpper().Contains(searchFilter.ToUpper()) ||
                    adh.IdentifiantIHM.ToUpper().Contains(searchFilter.ToUpper())); ;
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
                this.ShowModifButtons = false;

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
            this.adherents = new List<AdherentViewModel>();
            this.OpenModifAdh = new RelayCommand(x => this.DialogModifAdherent = true);
            InitAdhérents();

        }

        #region methods
        /// <summary>
        /// Permet de récupérer la liste des adhérents
        /// </summary>
        private void InitAdhérents()
        {
            List<Adhérent> adherents = this.adherentManager.GetAdhérents();
            foreach (Adhérent adh in adherents)
            {
                this.adherents.Add(new AdherentViewModel(adh));
            }
        }


  


        #endregion


    }
}
