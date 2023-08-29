
using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data.Dao
{
    public class AcompteDAO : IAcompteDao
    {

        public void CreateAdhérent(Acompte adhérent)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"INSERT INTO acompte VALUES (0,'{adhérent.Nom}','{adhérent.Prenom}',{adhérent.Argent},{adhérent.StillAdherent},'{adhérent.Formation}','{adhérent.Identifiant}')";
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


        public List<Acompte> GetAdhérents()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM acompte ORDER BY login";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Acompte> acomptes = new List<Acompte>();
            while (rdr.Read())
            {
                acomptes.Add(new Acompte(rdr.GetInt32("acompte_id"), rdr.GetString("login"), rdr.GetString("nom"), rdr.GetString("prenom"), rdr.GetFloat("balance"), rdr.GetString("formation"),true,rdr.GetBoolean("isAdherent")));
            }

            rdr.Close();
            dbsDAO.Instance.CloseDatabase();
            return acomptes;
        }

        public void RemoveAdhérent(Acompte adhérent)
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

        public void UpdateAdhérent(Acompte adhérent)
        {
                //Connection
                dbsDAO.Instance.OpenDataBase();

                //Requette SQL
                string formattedAmountMoney = adhérent.Argent.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string stm = $"UPDATE acompte set nom = '{adhérent.Nom}', prenom = '{adhérent.Prenom}', balance = {formattedAmountMoney}, isAdherent = {adhérent.StillAdherent}, formation = '{adhérent.Formation}', login = '{adhérent.Identifiant}' WHERE acompte_id = {adhérent.Id}";
                MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
                cmd.Prepare();

                //lecture de la requette
                cmd.ExecuteNonQuery();

                dbsDAO.Instance.CloseDatabase();
            
        }
    }
}
