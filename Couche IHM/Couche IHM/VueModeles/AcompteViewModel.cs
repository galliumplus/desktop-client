
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
        private string action;
        private bool showConfirmationDelete;

        #endregion

        #region events
        public RelayCommand ModifyAdherent { get; set; }
        public RelayCommand ResetAdh { get; set; }
        public RelayCommand CreateAdh { get; set; }
        public RelayCommand PreviewAdh { get; set; }
        public RelayCommand DeleteAdh { get; set; }
        public RelayCommand CancelDeleteAdh { get; set; }
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
                string result = $"{acompte.Nom.ToUpper()} {acompte.Prenom}";
                if (result == " ")
                {
                    result = "----------------------------------";
                }
                return result;
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
                return this.action;
            }
        }

        public bool ShowConfirmationDelete 
        { 
            get => showConfirmationDelete;
            set 
            { 
                showConfirmationDelete = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du acompteViewModel
        /// </summary>
        public AcompteViewModel(Acompte acompte,AcompteManager acompteManager,string action = "UPDATE")
        {
            this.acompte = acompte;
            this.acompteManager = acompteManager;
            this.action = action;

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
            this.PreviewAdh = new RelayCommand(x => ShowConfirmationDelete = true);
            this.CancelDeleteAdh = new RelayCommand(x => ShowConfirmationDelete = false);
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
            if (MessageBoxErrorHandler.DoesntThrow(() => this.acompteManager.RemoveAdhérent(this.acompte)))
            {
                // Log l'action
                Log log = new Log(DateTime.Now, 2, $"Suppresion de l acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);

                // Notifier la vue
                MainWindowViewModel.Instance.AdherentViewModel.RemoveAcompte(this);
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
                MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
                MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;
            }
            ShowConfirmationDelete = false;
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
            this.action = "UPDATE";
            
            if (MessageBoxErrorHandler.DoesntThrow(() => acompteManager.CreateAdhérent(this.acompte)))
            {
                // Log l'action
                Log log = new Log(DateTime.Now, 2, $"Création de l acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);

                // Notifier la vue
                MainWindowViewModel.Instance.AdherentViewModel.AddAcompte(this);
                NotifyPropertyChanged(nameof(this.Action));
                NotifyPropertyChanged(nameof(IdentifiantIHM));
                NotifyPropertyChanged(nameof(ArgentIHM));
                NotifyPropertyChanged(nameof(NomCompletIHM));
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
            }
            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;
        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void UpdateAcompte(bool doLog = true, bool persistChanges = true)
        {
            // Log l'action
            float argent = ConverterFormatArgent.ConvertToDouble(this.ArgentIHM);
            if (doLog && acompte.Argent != argent)
            {
                // Si suppresion argent
                string logMessage;
                if (acompte.Argent > argent)
                {
                    logMessage = $"Prélèvement de {ConverterFormatArgent.ConvertToString(acompte.Argent - argent)} sur {acompte.Identifiant}";
                }
                else
                {
                    logMessage = $"Ajout de {ConverterFormatArgent.ConvertToString(argent - acompte.Argent)} sur {acompte.Identifiant}";
                }

                Log log = new Log(DateTime.Now, 2, logMessage, MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
            }

            // Changer la data
            this.acompte.Nom = this.nomIHM;
            this.acompte.Prenom = this.prenomIHM;
            this.acompte.Argent = ConverterFormatArgent.ConvertToDouble(this.ArgentIHM);
            this.acompte.Formation = this.formationIHM;
            this.acompte.Identifiant = this.identifiantIHM;
            this.acompte.StillAdherent = this.isAdherentIHM;

            if (persistChanges && MessageBoxErrorHandler.DoesntThrow(() => acompteManager.UpdateAdhérent(this.acompte)))
            {
                // Notifier la vue
                NotifyPropertyChanged(nameof(IdentifiantIHM));
                NotifyPropertyChanged(nameof(ArgentIHM));
                NotifyPropertyChanged(nameof(NomCompletIHM));
            }
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

        public override string? ToString()
        {
            return $"{acompte.Identifiant} {acompte.Nom} {acompte.Prenom}";
        }


        #endregion
    }
}
