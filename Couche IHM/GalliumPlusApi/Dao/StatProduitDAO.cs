using Couche_Data.Interfaces;
using Modeles;

namespace GalliumPlusApi.Dao
{
    public class StatProduitDAO : IStatProduitDAO
    {
        public void CreateStat(StatProduit stat)
        {
            
        }

        public List<StatProduit> GetStat()
        {
            return new();
        }
    }
}
