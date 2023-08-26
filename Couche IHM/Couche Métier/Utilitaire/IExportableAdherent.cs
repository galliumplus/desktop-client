using Modeles;


namespace Couche_Métier
{
    /// <summary>
    /// Permet d'exporter un adhérent
    /// </summary>
    public interface IExportableAdherent
    {
        /// <summary>
        /// Permet d'exporter des adhérents
        /// </summary>
        /// <param name="adhérents">liste des adhérents</param>
        public void Export(List<Acompte> adhérents);
    }
}
