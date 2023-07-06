using MySql.Data.MySqlClient;

namespace Couche_Data
{
    /// <summary>
    /// Classe qui permet de créer une connexion avec la database
    /// </summary>
    /// <Author> Damien C.</Author>
    public class dbsDAO
    {
        #region attribut
        private MySqlConnection sql;
        private static dbsDAO instance = null;
        private static MySqlCommand cmd;
        private static MySqlDataReader reader;
        private static bool isConnected;
        #endregion

        /// <summary>
        /// Singleton qui permet d'avoir qu'une connexion
        /// </summary>
        public static dbsDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new dbsDAO();
                    
                }
                return instance;
            }
        }
        
        /// <summary>
        /// Permet de faire des requêtes
        /// </summary>
        public static MySqlCommand CMD
        {
            get => cmd;
            set => cmd = value; 
        }

        /// <summary>
        /// permet de lire les données
        /// </summary>
        public static MySqlDataReader Reader
        {
            get => reader;
            set => reader = value;
        }
        
        /// <summary>
        /// Vérifie si la connexion à la bdd existe 
        /// </summary>
        public static bool IsConnected
        {
            get => isConnected;
        }
        public MySqlConnection Sql { get => sql; set => sql = value; }

        /// <summary>
        /// Permet la connexion au serveur
        /// </summary>
        private dbsDAO()
        {
            this.ConnexionToBdd();

        }

        /// <summary>
        /// Se connecte à la base de donnée
        /// </summary>
        private void ConnexionToBdd()
        {
            sql = new MySqlConnection("balb alba");
        }

        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        public void OpenDataBase()
        {
            sql.Open();
            isConnected = true;
        }

        /// <summary>
        /// Ferme la connexion 
        /// </summary>
        public void CloseDatabase()
        {
            sql.Close();
            isConnected = false;
        }

       /// <summary>
       /// Permet de récupérer des informations de la bdd
       /// </summary>
       /// <param name="requete"></param>
       /// <returns></returns>
       public string Fetch(string requete)
       {
            return "";
       }


        /// <summary>
        /// Permet d'executer une requête sur la bdd
        /// </summary>
        /// <param name="requete">requette en string</param>
        public void Execute(string requete)
        {

        }
    }
}
