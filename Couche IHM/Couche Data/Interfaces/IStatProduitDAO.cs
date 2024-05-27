using Modeles;

namespace Couche_Data.Interfaces
{
    public interface IStatProduitDAO
    {
        void CreateStat(StatProduit stat);
        List<StatProduit> GetStatByWeek(int semaine,int year);
        List<StatProduit> GetStatByMonth(int month, int year);
        List<StatProduit> GetStatByYear(int year);
        List<StatProduit> GetStatBOfProductByMonth(int year, int product_id);
    }
}