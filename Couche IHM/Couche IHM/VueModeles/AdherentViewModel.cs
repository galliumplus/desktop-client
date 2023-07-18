using Couche_Métier;
using Couche_Métier.Log;
using Couche_Métier.Utilitaire;
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
    public class AdherentViewModel : INotifyPropertyChanged
    {
        #region attributes
        /// <summary>
        /// Représente le modèle adhérent
        /// </summary>
        private Adhérent adherent;

        /// <summary>
        /// Représente le manager des adhérents
        /// </summary>
        private AdhérentManager adhérentManager;

        /// <summary>
        /// Permet de log les actions des adhérents
        /// </summary>
        private ILog log; 

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

        /// <summary>
        /// Renvoie l'argent de l'adhérent sous un string formatté
        /// </summary>
        public string ArgentIHM
        {
            get => argentIHM;
            set
            {
                argentIHM = value;
                MainWindowViewModel.Instance.AdherentViewModel.ShowModifAdherent = true;
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
                MainWindowViewModel.Instance.AdherentViewModel.ShowModifAdherent = true;
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




        #endregion

        public AdherentViewModel(Adhérent adherent)
        {
            this.adherent = adherent;
            this.log =  new LogAdherentToTxt();
            this.adhérentManager = new AdhérentManager();
            this.ModifyAdherent = new RelayCommand(x => this.UpdateAdherent());
            this.ResetAdh = new RelayCommand(x => this.ResetAdherent());
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Initialisation propriétés
            this.argentIHM = converterFormatArgent.ConvertToString(adherent.Argent);
            this.identifiantIHM = adherent.Identifiant;
            this.formationIHM = adherent.Formation;
            this.isAdherentIHM = adherent.StillAdherent;
            this.nomIHM = adherent.Nom;
            this.prenomIHM = adherent.Prenom;

        }


        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void UpdateAdherent()
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


            // Notifier la vue
            NotifyPropertyChanged(nameof(IdentifiantIHM));
            NotifyPropertyChanged(nameof(ArgentIHM));
            NotifyPropertyChanged(nameof(NomCompletIHM));

            // Log l'action
            this.log.registerLog(CategorieLog.UPDATE, this.adherent, MainWindowViewModel.Instance.CompteConnected);


            MainWindowViewModel.Instance.AdherentViewModel.DialogModifAdherent = false;
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifAdherent = false;
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
            MainWindowViewModel.Instance.AdherentViewModel.ShowModifAdherent = false;
        }
    }
}
