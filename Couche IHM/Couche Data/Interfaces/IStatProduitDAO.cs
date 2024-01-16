using Modeles;

namespace Couche_Data.Interfaces
{
    public interface IStatProduitDAO
    {
        void CreateStat(StatProduit stat);
        List<StatProduit> GetStat(int semaine = 0, int mois = 0, int annee = 0);
    }
}