using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Couche_Data.Dao
{
    public class StatAcompteDAO : IStatAcompteDAO
    {

        public List<StatAcompte> GetStat()
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string stm = "SELECT acompte_id,sum(amount) as argent FROM best_acomptes WHERE YEARWEEK(date) = YEARWEEK(CURRENT_DATE) group by acompte_id order by argent desc";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatAcompte> statAcompteList = new List<StatAcompte>();
                while (rdr.Read())
                {
                    statAcompteList.Add(new StatAcompte(0, DateTime.Now, rdr.GetFloat("argent"), rdr.GetInt32("acompte_id")));
                }

                rdr.Close();
                sql.Close();
                return statAcompteList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats acompte : {ex.Message}");
                return new();
            }
        }

        public void CreateStat(StatAcompte stat)
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string formattedDate = stat.Date.ToString("yyyy-MM-dd");
                string formattedAmountMoney = stat.Money.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string stm = $"INSERT INTO best_acomptes VALUES(0,{formattedAmountMoney},'{formattedDate}',{stat.Acompte_Id})";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                cmd.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant l'ajout d'une stat acompte : {ex.Message}");
            }
        }
    }
}
