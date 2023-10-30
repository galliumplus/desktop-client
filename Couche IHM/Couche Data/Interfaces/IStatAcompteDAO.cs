using Modeles;

namespace Couche_Data.Interfaces
{
    public interface IStatAcompteDAO
    {
        void CreateStat(StatAcompte stat);
        List<StatAcompte> GetStat();
    }
}