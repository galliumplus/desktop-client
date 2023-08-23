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
        private  MySqlCommand cmd;
        private  MySqlDataReader reader;
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
        public  MySqlCommand CMD
        {
            get => cmd;
            set => cmd = value; 
        }

        /// <summary>
        /// permet de lire les données
        /// </summary>
        public  MySqlDataReader Reader
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
        private object databaseLock = new object();

        public object DatabaseLock
        {
            get { return databaseLock; }
        }

        /// <summary>
        /// Se connecte à la base de donnée
        /// </summary>
        private void ConnexionToBdd()
        {
            string connString = String.Format("server={0};port={1};user id={2};password={3};database={4};SslMode={5}", "51.178.36.43", "3306", "c2_gallium", "DfD2no5UJc_nB", "c2_etismash", "none");
            this.sql = new MySqlConnection(connString);
        }

        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        public void OpenDataBase()
        {
            if (!isConnected)
            {
                sql.Open();
                isConnected = true;
            }
            
        }

        /// <summary>
        /// Ferme la connexion 
        /// </summary>
        public void CloseDatabase()
        {
            if (isConnected)
            {
                sql.Close();
                isConnected = false;
            }
        }

    }
}
