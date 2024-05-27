using Modeles;

namespace Couche_Data.Interfaces
{
    public interface IStatAccountDAO
    {
        void CreateStat(StatAccount stat);
        List<StatAccount> GetStatByWeek(int semaine,int year);
        List<StatAccount> GetStatByMonth(int month, int year);
        List<StatAccount> GetStatByYear(int year);
        List<StatAccount> GetStatBOfAcompteByMonth(int year, int acompte_id);
    }
}