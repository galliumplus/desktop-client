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
    public class ConnexionDAO
    {

        private MySqlConnection sql;

        

        private static ConnexionDAO instance;
        /// <summary>
        /// Singleton qui permet d'avoir qu'une connexion
        /// </summary>
        public static ConnexionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnexionDAO();
                    
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
            set => cmd = value;
        }

        public ConnexionDAO()
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
        


    }
}
