using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Couche_Data.Dao
{
    public class StatProduitDAO : IStatProduitDAO
    {
        public List<StatProduit> GetStatByYear(int year)
        {
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT product_id,sum(number_sales) as nb FROM best_sales WHERE YEAR(date_sale) = {year} group by product_id order by nb desc";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatProduit> statProduitList = new List<StatProduit>();
                while (rdr.Read())
                {
                    statProduitList.Add(new StatProduit(0, DateTime.Now, rdr.GetInt16("nb"), rdr.GetInt16("product_id")));
                }

                rdr.Close();
                sql.Close();
                return statProduitList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats produit : {ex.Message}");
                return new();
            }
        }

        public List<StatProduit> GetStatByWeek(int semaine, int year)
        {
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT product_id,sum(number_sales) as nb FROM best_sales WHERE week(date_sale) = {semaine} AND YEAR(date_sale) = {year} group by product_id order by nb desc";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatProduit> statProduitList = new List<StatProduit>();
                while (rdr.Read())
                {
                    statProduitList.Add(new StatProduit(0, DateTime.Now, rdr.GetInt16("nb"), rdr.GetInt16("product_id")));
                }

                rdr.Close();
                sql.Close();
                return statProduitList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats produit : {ex.Message}");
                return new();
            }
        }

        public void CreateStat(StatProduit stat)
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string formattedDate = stat.Date.ToString("yyyy-MM-dd");
                string stm = $"INSERT INTO best_sales VALUES(0,'{formattedDate}',{stat.Number_sales},{stat.Product_id})";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                cmd.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant l'ajout d'une stat produit : {ex.Message}");
            }
        }

        public List<StatProduit> GetStatByMonth(int month, int year)
        {
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT product_id,sum(number_sales) as nb FROM best_sales WHERE month(date_sale) = {month} AND YEAR(date_sale) = {year} group by product_id order by nb desc";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatProduit> statProduitList = new List<StatProduit>();
                while (rdr.Read())
                {
                    statProduitList.Add(new StatProduit(0, DateTime.Now, rdr.GetInt16("nb"), rdr.GetInt16("product_id")));
                }

                rdr.Close();
                sql.Close();
                return statProduitList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats produit : {ex.Message}");
                return new();
            }
        }

        public List<StatProduit> GetStatBOfProductByMonth(int year,int product_id)
        {
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            try
            {
                sql.Open();

                //Requette SQL
                string stm = $"SELECT NVL(product_id,{product_id}) as product_id,lm.mois as date,NVL(SUM(bs.number_sales), 0) AS nb FROM ( SELECT 1 AS mois UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12) lm LEFT JOIN best_sales bs ON MONTH(bs.date_sale) = lm.mois AND YEAR(bs.date_sale) = {year} AND bs.product_id = {product_id} GROUP BY lm.mois ORDER BY lm.mois;";
                MySqlCommand cmd = new MySqlCommand(stm, sql);
                cmd.Prepare();

                //lecture de la requette
                MySqlDataReader rdr = cmd.ExecuteReader();

                List<StatProduit> statProduitList = new List<StatProduit>();
                while (rdr.Read())
                {
                    statProduitList.Add(new StatProduit(0, rdr.GetDateTime("date"), rdr.GetInt16("nb"), rdr.GetInt16("product_id")));
                }

                rdr.Close();
                sql.Close();
                return statProduitList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur pendant le chargement des stats produit : {ex.Message}");
                return new();
            }
        }
    }
}
