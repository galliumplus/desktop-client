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
using System.Windows.Navigation;

namespace Couche_IHM.VueModeles
{
    public class AdherentViewModel : INotifyPropertyChanged
    {
        #region attributes
        /// <summary>
        /// Représente le modèle adhérent
        /// </summary>
        private Adhérent adherent;
        private int random;
        /// <summary>
        /// Représente le manager des adhérents
        /// </summary>
        private AdhérentManager adhérentManager;


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
        /// Nom complet de l'utilisateur
        /// </summary>
        public string NomCompletIHM
        {
            get 
            { 
                return $"{adherent.Nom.ToUpper()} {adherent.Prenom}";
            }
            

        }

        public int PurchaseCount
        {
            get => random;
            set => random = value;
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

        public string NomIHM 
        { 
            get => nomIHM; 
            set => nomIHM = value; 
        }
        public string PrenomIHM 
        { 
            get => prenomIHM; 
            set => prenomIHM = value; 
        }
        public string FormationIHM 
        { 
            get => formationIHM;
            set => formationIHM = value; 
        }
        public bool IsAdherentIHM 
        { 
            get => isAdherentIHM; 
            set => isAdherentIHM = value; 
        }

        public string Action
        {
            get
            {
                return this.adherent.Prenom == "" ? "NEW" : "UPDATE";
            }
        }




        #endregion

        public AdherentViewModel(Adhérent adherent,AdhérentManager adherentManager,int random)
        {
            this.random = random;
            this.adherent = adherent;
            this.adhérentManager = adherentManager;
            
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Initialisation propriétés
            this.argentIHM = converterFormatArgent.ConvertToString(adherent.Argent);
            this.identifiantIHM = adherent.Identifiant;
            this.formationIHM = adherent.Formation;
            this.isAdherentIHM = adherent.StillAdherent;
            this.nomIHM = adherent.Nom;
            this.prenomIHM = adherent.Prenom;

            this.ModifyAdherent = new RelayCommand(x => this.UpdateAdherent());
            this.ResetAdh = new RelayCommand(x => this.ResetAdherent());
            this.CreateAdh = new RelayCommand(x => this.CreateAdherent());
            this.DeleteAdh = new RelayCommand(x => this.DeleteAdherent());

        }

        /// <summary>
        /// Permet de supprimer un acompte
        /// </summary>
        private void DeleteAdherent()
        {
            // Modifier la data
            this.adhérentManager.RemoveAdhérent(this.adherent);

            // Log l'action
            Log log = new Log(0, DateTime.Now.ToString("g"), 2, $"Suppresion de l'acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            MainWindowViewModel.Instance.AdherentViewModel.RemoveAcompte(this);
            MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;

        }

        /// <summary>
        /// Permet de créer un acompte
        /// </summary>
        private void CreateAdherent()
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Changer la data
            this.adherent.Nom = this.nomIHM;
            this.adherent.Prenom = this.prenomIHM;
            this.adherent.Argent = converterFormatArgent.ConvertToDouble(this.ArgentIHM);
            this.adherent.Formation = this.formationIHM;
            this.adherent.Identifiant = this.identifiantIHM;
            this.adherent.StillAdherent = this.isAdherentIHM;
            adhérentManager.CreateAdhérent(this.adherent);

            // Log l'action
            Log log = new Log(0, DateTime.Now.ToString("g"), 2, $"Création de l'acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            MainWindowViewModel.Instance.AdherentViewModel.AddAcompte(this);
            NotifyPropertyChanged(nameof(IdentifiantIHM));
            NotifyPropertyChanged(nameof(ArgentIHM));
            NotifyPropertyChanged(nameof(NomCompletIHM));
            MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;
        }


        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void UpdateAdherent(bool doLog = true)
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Changer la data
            this.adherent.Nom = this.nomIHM;
            this.adherent.Prenom = this.prenomIHM;
            this.adherent.Argent = converterFormatArgent.ConvertToDouble(this.ArgentIHM);
            this.adherent.Formation = this.formationIHM;
            this.adherent.Identifiant = this.identifiantIHM;
            this.adherent.StillAdherent = this.isAdherentIHM;
            adhérentManager.UpdateAdhérent(this.adherent);

            // Log l'action
            if (doLog)
            {
                Log log = new Log(0, DateTime.Now.ToString("g"), 2, $"Modification de l'acompte : {this.NomCompletIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);
                MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
            }

            // Notifier la vue
            NotifyPropertyChanged(nameof(IdentifiantIHM));
            NotifyPropertyChanged(nameof(ArgentIHM));
            NotifyPropertyChanged(nameof(NomCompletIHM));
            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifButtons = false;
        }



        /// <summary>
        /// Permet de reset les propriétés de l'adhérent
        /// </summary>
        public void ResetAdherent()
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Initialisation propriétés
            this.argentIHM = converterFormatArgent.ConvertToString(adherent.Argent);
            this.identifiantIHM = adherent.Identifiant;
            this.formationIHM = adherent.Formation;
            this.nomIHM = adherent.Nom;
            this.prenomIHM = adherent.Prenom;
            this.isAdherentIHM = adherent.StillAdherent;

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
    }
}
