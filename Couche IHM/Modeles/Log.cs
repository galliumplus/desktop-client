

namespace Modeles
{
    public class Log 
    {
        #region attributes
        private DateTime date;
        private int theme;
        private string message;
        private string auteur;
        #endregion

        #region constructors
        /// <summary>
        /// Constructeur du modèle log
        /// </summary>
        /// <param name="date">date du log</param>
        /// <param name="theme">theme du log</param>
        /// <param name="message">message du log</param>
        /// <param name="auteur">auteur du log</param>
        public Log(DateTime date, int theme, string message, string auteur)
        {
            this.date = date;
            this.theme = theme;
            this.message = message;
            this.auteur = auteur;
        }
        #endregion

        #region properties
        /// <summary>
        /// Date de l'action
        /// </summary>
        public DateTime Date 
        {
            get => date;
        }

        /// <summary>
        /// Action de l'utilisateur
        /// </summary>
        public int Theme 
        { 
            get => theme; 
            set => theme = value; 
        }

        /// <summary>
        /// Intitulé du message sans les détails
        /// </summary>
        public string Message
        {
            get => message;
            set => message = value;
        }


        /// <summary>
        /// Auteur 
        /// </summary>
        public string Auteur
        {
            get => auteur;
            set => auteur = value;
        }
        #endregion
    }
}
