﻿using Couche_Data.Dao;
using Couche_Data.Interfaces;
using GalliumPlusApi.Dao;
using Modeles;

namespace Couche_Métier.Manager
{
    public class StatAccountManager
    {
        #region attributes
        /// <summary>
        /// Dao permettant de gérer les données des stats d'acompte
        /// </summary>
        private IStatAccountDAO dao;

        /// <summary>
        /// Liste des stats d'acompte
        /// </summary>
        private List<StatAccount> statAccountList;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du statProduit Manager
        /// </summary>
        public StatAccountManager()
        {
            if (DevelopmentInfo.isDevelopment)
            {
                dao = new StatAccountDAO();
            }
            else
            {
                dao = new StatAccountDao();
            }
            
            this.statAccountList = new List<StatAccount>();
            

        }
        #endregion

        #region methods
        public void CreateStat(StatAccount stat)
        {
            dao.CreateStat(stat);
        }

        public List<StatAccount> GetStatsByWeek(int semaine, int year )
        {

            this.statAccountList = dao.GetStatByWeek(semaine,year);
            return this.statAccountList;
        }

        public List<StatAccount> GetStatsByMonth(int month, int year)
        {

            this.statAccountList = dao.GetStatByMonth(month, year);
            return this.statAccountList;
        }

        public List<StatAccount> GetStatBOfAcompteByMonth(int year, int acompte_id)
        {
            statAccountList = dao.GetStatBOfAcompteByMonth(year, acompte_id);
            return this.statAccountList;
        }

        public List<StatAccount> GetStatsByYear(int year)
        {

            this.statAccountList = dao.GetStatByYear(year);
            return this.statAccountList;
        }
        #endregion
    }
}
