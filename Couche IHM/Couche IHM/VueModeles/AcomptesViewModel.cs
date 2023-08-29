
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Couche_IHM.VueModeles
{
    public class AcomptesViewModel : INotifyPropertyChanged
    {
        #region attributes
        private ObservableCollection<AcompteViewModel> acomptes;
        private AcompteViewModel currentAcompte;
        private AcompteManager acompteManager;
        private string searchFilter = "";
        private bool showAcompte = false;
        private bool showModifButtons = false;
        private bool showDeleteAcompte = false;
        private bool dialogModifAcompte = false;
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
        /// Liste des acomptes
        /// </summary>
        public ObservableCollection<AcompteViewModel> Acomptes 
        {
            get 
            {
                ObservableCollection<AcompteViewModel> adhs;
                if (searchFilter == "")
                {
                    adhs = this.acomptes;
                }
                else
                {
                    adhs = new ObservableCollection<AcompteViewModel>(acomptes.ToList().FindAll(adh =>
                    adh.NomCompletIHM.ToUpper().Contains(searchFilter.ToUpper()) ||
                    adh.IdentifiantIHM.ToUpper().Contains(searchFilter.ToUpper()))) ;
                }
                
                return adhs;
            }
            set => acomptes = value; 
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
                NotifyPropertyChanged(nameof(Acomptes)); 
            } 
        }

        /// <summary>
        /// Est ce qu'on affiche la fenetre de l acompte
        /// </summary>
        public bool ShowAcompte
        { 
            get => showAcompte;
            set 
            {
                showAcompte = value; 
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Permet d'afficher les boutons de modification de l'acompte
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
        /// Représente l'acompte selectionné
        /// </summary>
        public AcompteViewModel CurrentAcompte 
        { 
            get => currentAcompte;
            set 
            {
                if (currentAcompte != null)
                {
                    this.currentAcompte.ResetAcompte();
                }

                currentAcompte = value;
                if (value != null)
                {
                    ShowAcompte = true;

                }
                else
                {
                    this.ShowAcompte = false;
                    
                }

                NotifyPropertyChanged(nameof(CurrentAcompte));
            }
        }
        /// <summary>
        /// Ouvrir la fenetre pour modifier l'acompte
        /// </summary>
        public bool DialogModifAdherent
        {
            get => dialogModifAcompte;
            set
            {
                dialogModifAcompte = value;
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

        #region constructor
        /// <summary>
        /// Constructeur de la classe acompte view model
        /// </summary>
        public AcomptesViewModel(AcompteManager acompteManager)
        {
            // Initialisation des datas
            this.acompteManager = acompteManager;
            this.acomptes = new ObservableCollection<AcompteViewModel>();
            InitAcomptes();

            // Initialisation des events
            this.OpenModifAdh = new RelayCommand(x => this.OpenAcompteDetails((string)x));


        }
        #endregion

        #region methods
        /// <summary>
        /// Permet de récupérer la liste des acomptes
        /// </summary>
        private void InitAcomptes()
        {
            List<Acompte> adherents = this.acompteManager.GetAdhérents();
            foreach (Acompte adh in adherents)
            {
                this.acomptes.Add(new AcompteViewModel(adh,this.acompteManager));
            }
        }

        /// <summary>
        /// Permet d'ouvrir le détail de l'acompte
        /// </summary>
        private void OpenAcompteDetails(string action)
        {

            if (action == "NEW" || currentAcompte == null || currentAcompte.Action == "NEW")
            {
                ShowDeleteAcompte = false;
                CurrentAcompte = new AcompteViewModel(new Acompte(),this.acompteManager,"NEW");
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
        public void AddAcompte(AcompteViewModel acompte)
        {
            this.acomptes.Add(acompte);
            NotifyPropertyChanged(nameof(Acomptes));

        }

        /// <summary>
        /// Permet de supprimer un acompte  dans la liste
        /// </summary>
        public void RemoveAcompte(AcompteViewModel acompte)
        {
            this.acomptes.Remove(acompte);
            NotifyPropertyChanged(nameof(Acomptes));
        }

        #endregion


    }
}
