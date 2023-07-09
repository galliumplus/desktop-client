using Couche_Métier;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private bool showModifAdherent = false;
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
                    adh.Identifiant.ToUpper().Contains(searchFilter.ToUpper())); ;
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
                NotifyPropertyChanged(nameof(ShowAdherent));
            }
        }

        /// <summary>
        /// Est ce qu on affiche les choix sur l adherent
        /// </summary>
        public bool ShowModifAdherent 
        { 
            get => showModifAdherent;
            set 
            { 
                showModifAdherent = value; 
                NotifyPropertyChanged(nameof(ShowModifAdherent));
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
                currentAdherent = value;
                if (value != null)
                {
                    ShowAdherent = true;
                    ShowModifAdherent = true;
                }
                else
                {
                    this.ShowAdherent = false;
                    this.ShowModifAdherent = false;
                }
                NotifyPropertyChanged(nameof(CurrentAdherent));
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
