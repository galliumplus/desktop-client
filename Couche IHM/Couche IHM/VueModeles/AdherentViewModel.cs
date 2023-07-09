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

        private string nomCompletIHM;
        private string argentIHM;
        private string identifiant;

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
            get => nomCompletIHM;
            set => nomCompletIHM = value;
        }

        /// <summary>
        /// Renvoie l'argent de l'adhérent sous un string formatté
        /// </summary>
        public string ArgentIHM
        {
            get => argentIHM;
            set => argentIHM = value;
        }

        /// <summary>
        /// Id de l'adhérent
        /// </summary>
        public string Identifiant 
        { 
            get => identifiant;
            set => identifiant = value;
        }

        #endregion

        public AdherentViewModel(Adhérent adherent)
        {
            this.adherent = adherent;
            this.log =  new LogAdherentToTxt();
            this.adhérentManager = new AdhérentManager();
            CancelAdherent();
        }

        /// <summary>
        /// Permet d'initialiser les champs avec les infos de l'adhérent
        /// </summary>
        public void CancelAdherent()
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
            this.argentIHM = converterFormatArgent.ConvertToString(adherent.Argent);
            this.identifiant = adherent.Identifiant;
            this.nomCompletIHM = $"{adherent.Nom.ToUpper()} {adherent.Prenom}";
        }

        /// <summary>
        /// Permet le changement rapide des adhérents
        /// </summary>
        public void ValidateAdherent()
        {
            try
            {
                // Mise à jour du nom et du prénom
                string nom;
                string prenom;
                string identifiant;
                double argent;

                if (this.nomCompletIHM.Contains(" "))
                {
                    string[] nomComplet = this.nomCompletIHM.Split(" ");
                    nom = nomComplet[0];
                    prenom = nomComplet[1];
                }
                else
                {
                    throw new Exception("IdentiteFormat");
                }


                // Mise à jour de l'id
                if (this.identifiant.Length == 8 && this.identifiant[0] == prenom.ToLower()[0] && this.identifiant[1] == nom.ToLower()[0])
                {
                    identifiant = this.identifiant;
                }
                else
                {
                    throw new Exception("IDFormat");
                }


                // Mise à jour de l'argent
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();
                argent = converterFormatArgent.ConvertToDouble(this.argentIHM);

                // Met à jour l'adhérent
                this.adherent.Identifiant = identifiant;
                this.adherent.Nom = nom;
                this.adherent.Prenom = prenom;
                this.adherent.Argent = argent;
                this.UpdateAdherent();


            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "ArgentFormat":
                        this.argentWarning.Visibility = Visibility.Visible;
                        break;
                    case "IDFormat":
                        this.compteWarning.Visibility = Visibility.Visible;
                        break;
                    case "IdentiteFormat":
                        this.identiteWarning.Visibility = Visibility.Visible;
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
            }
        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void UpdateAdherent()
        {
            // Changer la data
            adhérentManager.UpdateAdhérent(this.adherent);

            // Notifier la vue
            NotifyPropertyChanged(nameof(Identifiant));
            NotifyPropertyChanged(nameof(ArgentIHM));
            NotifyPropertyChanged(nameof(NomCompletIHM));

            // Log l'action
            this.log.registerLog(CategorieLog.UPDATE, this.adherent, MainWindowViewModel.Instance.CompteConnected);
    
        }
    }
}
