using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gallium_v1.Data
{
    /// <summary>
    /// Classe qui permet de créer une connexion avec la database
    /// </summary>
    public class dbsDAO
    {

        private MySqlConnection sql;

        

        private static dbsDAO instance;
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

        
        private MySqlCommand cmd;
        /// <summary>
        /// Commande SQL 
        /// </summary>
        public MySqlCommand CMD
        {
            get => cmd;
        }

        private static MySqlDataReader reader;
        /// <summary>
        /// permet de lire les données
        /// </summary>
        public static MySqlDataReader Reader
        {
            get => reader;
            set => reader = value;
        }

        /// <summary>
        /// Permet la connexion
        /// </summary>
        private dbsDAO()
        {
            try
            {
                sql = new MySqlConnection($"SERVER={InformationConnexion.Databases};PORT={InformationConnexion.Port};DATABASE={InformationConnexion.Databases};UID={InformationConnexion.Uid};PWD={InformationConnexion.Pwd}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Problème connexion à database");
            }
        }
        
        /// <summary>
        /// Permet de faire une requête SQL avec la base de donnée et d'intéragir avec 
        /// </summary>
        /// <param name="requete"> requete sql </param>
        public void RequeteSQL(string requete)
        {
            cmd = new MySqlCommand(requete, sql);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear(); 
        }

        /// <summary>
        /// Renvoie des éléments de la base de donnée
        /// </summary>
        /// <param name="requete"></param>
       public string FetchSQL(string requete)
       {
            cmd = new MySqlCommand(requete, sql);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return cmd.ToString();
        }


    }
}
