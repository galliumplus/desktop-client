
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    public class AdhérentDao : IAdhérentDao
    {

        public AdhérentDao()
        {
            
        }

        public void CreateAdhérent(Adhérent adhérent)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"INSERT INTO acompte VALUES (0,'{adhérent.Nom}','{adhérent.Prenom}',{adhérent.Argent},{adhérent.StillAdherent},'{adhérent.Identifiant}','no')";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();
            using (MySqlCommand selectCommand = new MySqlCommand("SELECT LAST_INSERT_ID() AS nouvel_id", dbsDAO.Instance.Sql))
            {
                int nouvelId = Convert.ToInt32(selectCommand.ExecuteScalar());
                adhérent.Id = nouvelId;
            }

            dbsDAO.Instance.CloseDatabase();
        }


        public List<Adhérent> GetAdhérents()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM acompte ORDER BY nom";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Adhérent> acomptes = new List<Adhérent>();
            while (rdr.Read())
            {
                acomptes.Add(new Adhérent(rdr.GetInt32("acompte_id"), rdr.GetString("login"), rdr.GetString("nom"), rdr.GetString("prenom"), rdr.GetFloat("balance"), ""));
            }

            rdr.Close();
            dbsDAO.Instance.CloseDatabase();
            return acomptes;
        }

        public void RemoveAdhérent(Adhérent adhérent)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"DELETE FROM  acompte WHERE acompte_id = {adhérent.Id}";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

            dbsDAO.Instance.CloseDatabase();
        }

        public void UpdateAdhérent(Adhérent adhérent)
        {

            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"UPDATE acompte set nom = '{adhérent.Nom}', prenom = '{adhérent.Prenom}', balance = {adhérent.Argent}, isAdherent = {adhérent.StillAdherent}, login = '{adhérent.Identifiant}'";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

            dbsDAO.Instance.CloseDatabase();
        }
    }
}
