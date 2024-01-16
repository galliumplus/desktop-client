using Modeles;

namespace Couche_Data.Interfaces
{
    public interface IStatAccountDAO
    {
        void CreateStat(StatAccount stat);
        List<StatAccount> GetStat(int semaine = 0, int mois = 0, int annee = 0);
    }
}