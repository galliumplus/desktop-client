
using Couche_Métier.Manager;
using Couche_Métier.Utilitaire;
using Modeles;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Couche_IHM.VueModeles
{
    public class AcompteViewModel : INotifyPropertyChanged
    {
        #region attributes

        private Acompte acompte;
        private AcompteManager acompteManager;
        private string argentIHM;
        private string identifiantIHM;
        private bool isAdherentIHM;
        private string nomIHM;
        private string prenomIHM;
        private string formationIHM;
        
        #endregion

        #region events
        public RelayCommand ModifyAdherent { get; set; }
        public RelayCommand ResetAdh { get; set; }
        public RelayCommand CreateAdh { get; set; }
        public RelayCommand DeleteAdh { get; set; }
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
        /// Nom complet de l'acompte
        /// </summary>
        public string NomCompletIHM
        {
            get 
            { 
                return $"{acompte.Nom.ToUpper()} {acompte.Prenom}";
            }
            

        }
        /// <summary>
        /// Id de l'acompte
        /// </summary>
        public int Id
        {
            get => acompte.Id;
        }

        /// <summary>
        /// Renvoie l'argent de l'adhérent sous un string formatté
        /// </summary>
        public string ArgentIHM
        {
            get => argentIHM;
            set
            {
                argentIHM = value;
                MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = true;
            }
        }

        /// <summary>
        /// Id de l'adhérent
        /// </summary>
        public string IdentifiantIHM
        {
            get => identifiantIHM;
            set
            {
                identifiantIHM = value;
                MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = true;
            }
        }
        /// <summary>
        /// Nom de l'acompte
        /// </summary>
        public string NomIHM { get => nomIHM; set => nomIHM = value; }

        /// <summary>
        /// Prenom de l'acompte
        /// </summary>
        public string PrenomIHM { get => prenomIHM; set => prenomIHM = value; }
        
        /// <summary>
        /// Formation de l'acompte
        /// </summary>
        public string FormationIHM { get => formationIHM; set => formationIHM = value; }

        /// <summary>
        /// Est ce que l'acompte est adhérent
        /// </summary>
        public bool IsAdherentIHM { get => isAdherentIHM; set => isAdherentIHM = value; }

        /// <summary>
        /// Action à réaliser sur l'acompte
        /// </summary>
        public string Action
        {
            get
            {
                return this.acompte.Prenom == "" ? "NEW" : "UPDATE";
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du acompteViewModel
        /// </summary>
        public AcompteViewModel(Acompte acompte,AcompteManager acompteManager)
        {
            this.acompte = acompte;
            this.acompteManager = acompteManager;

            // Initialisation propriétés
            this.argentIHM = ConverterFormatArgent.ConvertToString(acompte.Argent);
            this.identifiantIHM = acompte.Identifiant;
            this.formationIHM = acompte.Formation;
            this.isAdherentIHM = acompte.StillAdherent;
            this.nomIHM = acompte.Nom;
            this.prenomIHM = acompte.Prenom;

            // Initialisation des events
            this.ModifyAdherent = new RelayCommand(x => this.UpdateAcompte());
            this.ResetAdh = new RelayCommand(x => this.ResetAcompte());
            this.CreateAdh = new RelayCommand(x => this.CreateAcompte());
            this.DeleteAdh = new RelayCommand(x => this.DeleteAcompte());

        }
        #endregion

        #region methods
        /// <summary>
        /// Permet de supprimer un acompte
        /// </summary>
        private void DeleteAcompte()
        {
            // Modifier la data
            this.acompteManager.RemoveAdhérent(this.acompte);

            // Log l'action
            Log log = new Log(DateTime.Now, 2, $"Suppresion de l acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            MainWindowViewModel.Instance.AdherentViewModel.RemoveAcompte(this);
            MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;

        }

        /// <summary>
        /// Permet de créer un acompte
        /// </summary>
        private void CreateAcompte()
        {

            // Changer la data
            this.acompte.Nom = this.nomIHM;
            this.acompte.Prenom = this.prenomIHM;
            this.acompte.Argent = ConverterFormatArgent.ConvertToDouble(this.ArgentIHM);
            this.acompte.Formation = this.formationIHM;
            this.acompte.Identifiant = this.identifiantIHM;
            this.acompte.StillAdherent = this.isAdherentIHM;
            acompteManager.CreateAdhérent(this.acompte);

            // Log l'action
            Log log = new Log(DateTime.Now, 2, $"Création de l acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            MainWindowViewModel.Instance.AdherentViewModel.AddAcompte(this);
            NotifyPropertyChanged(nameof(IdentifiantIHM));
            NotifyPropertyChanged(nameof(ArgentIHM));
            NotifyPropertyChanged(nameof(NomCompletIHM));
            MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;
        }


        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void UpdateAcompte(bool doLog = true)
        {

            // Changer la data
            this.acompte.Nom = this.nomIHM;
            this.acompte.Prenom = this.prenomIHM;
            this.acompte.Argent = ConverterFormatArgent.ConvertToDouble(this.ArgentIHM);
            this.acompte.Formation = this.formationIHM;
            this.acompte.Identifiant = this.identifiantIHM;
            this.acompte.StillAdherent = this.isAdherentIHM;
            acompteManager.UpdateAdhérent(this.acompte);

            // Log l'action
            if (doLog)
            {
                Log log = new Log(DateTime.Now, 2, $"Modification de l acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
            }

            // Notifier la vue
            NotifyPropertyChanged(nameof(IdentifiantIHM));
            NotifyPropertyChanged(nameof(ArgentIHM));
            NotifyPropertyChanged(nameof(NomCompletIHM));
            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;
        }



        /// <summary>
        /// Permet de reset les propriétés de l'acompte
        /// </summary>
        public void ResetAcompte()
        {

            // Initialisation propriétés
            this.argentIHM = ConverterFormatArgent.ConvertToString(acompte.Argent);
            this.identifiantIHM = acompte.Identifiant;
            this.formationIHM = acompte.Formation;
            this.nomIHM = acompte.Nom;
            this.prenomIHM = acompte.Prenom;
            this.isAdherentIHM = acompte.StillAdherent;

            // Notifier la vue
            NotifyPropertyChanged(nameof(IdentifiantIHM));
            NotifyPropertyChanged(nameof(ArgentIHM));
            NotifyPropertyChanged(nameof(NomCompletIHM));
            NotifyPropertyChanged(nameof(NomIHM));
            NotifyPropertyChanged(nameof(PrenomIHM));
            NotifyPropertyChanged(nameof(FormationIHM));
            NotifyPropertyChanged(nameof(IsAdherentIHM));

            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;
        }
        #endregion
    }
}
