using Couche_Data.Dao;
using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace GalliumPlusApi.Dao
{
    public class StatAccountDao : IStatAccountDAO
    {

        public List<StatAccount> GetStatByWeek(int semaine, int year )
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionStringV);

            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT acompte_id,sum(amount) as argent FROM best_acomptes WHERE WEEK(date) = {semaine} AND " +
                  $"YEAR(date) = {year}  group by acompte_id order by argent desc";

                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatAccount> statAccountList = new List<StatAccount>();
                while (rdr.Read())
                {
                    statAccountList.Add(new StatAccount(0, DateTime.Now, rdr.GetFloat("argent"), rdr.GetInt32("acompte_id")));
                }

                rdr.Close();
                sql.Close();
                return statAccountList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats acompte : {ex.Message}");
                return new();
            }
        }

        public List<StatAccount> GetStatByMonth(int month, int year)
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionStringV);

            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT acompte_id,sum(amount) as argent FROM best_acomptes WHERE month(date) = {month} AND " +
                  $"YEAR(date) = {year}  group by acompte_id order by argent desc";

                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatAccount> statAccountList = new List<StatAccount>();
                while (rdr.Read())
                {
                    statAccountList.Add(new StatAccount(0, DateTime.Now, rdr.GetFloat("argent"), rdr.GetInt32("acompte_id")));
                }

                rdr.Close();
                sql.Close();
                return statAccountList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats acompte : {ex.Message}");
                return new();
            }
        }

        public List<StatAccount> GetStatByYear(int year)
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionStringV);

            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT acompte_id,sum(amount) as argent FROM best_acomptes WHERE " +
                  $"YEAR(date) = {year}  group by acompte_id order by argent desc";

                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatAccount> statAccountList = new List<StatAccount>();
                while (rdr.Read())
                {
                    statAccountList.Add(new StatAccount(0, DateTime.Now, rdr.GetFloat("argent"), rdr.GetInt32("acompte_id")));
                }

                rdr.Close();
                sql.Close();
                return statAccountList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats acompte : {ex.Message}");
                return new();
            }
        }

        public List<StatAccount> GetStatBOfAcompteByMonth(int year, int acompte_id)
        {
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionStringV);
            try
            {
                sql.Open();

                //Requette SQL

                string stm = $"SELECT NVL(acompte_id,{acompte_id}) as acompte_id,lm.mois as date,NVL(SUM(bs.amount), 0) AS argent FROM ( SELECT 1 AS mois UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12) lm LEFT JOIN best_acomptes bs ON MONTH(bs.date) = lm.mois AND YEAR(bs.date) = {year} AND bs.acompte_id = {acompte_id} GROUP BY lm.mois ORDER BY lm.mois;";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatAccount> statAccountList = new List<StatAccount>();
                while (rdr.Read())
                {
                    statAccountList.Add(new StatAccount(0, new DateTime(year, rdr.GetInt16("date"), 1), rdr.GetFloat("argent"), rdr.GetInt32("acompte_id")));
                }

                rdr.Close();
                sql.Close();
                return statAccountList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats acompte : {ex.Message}");
                return new();
            }
        }

        public void CreateStat(StatAccount stat)
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionStringV);
            try
            {
                sql.Open();

                //Requette SQL
                string formattedDate = stat.Date.ToString("yyyy-MM-dd");
                string formattedAmountMoney = stat.Money.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string stm = $"INSERT INTO best_acomptes VALUES(0,{formattedAmountMoney},'{formattedDate}',{stat.Account_Id})";
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
