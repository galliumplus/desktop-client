
using System.Text.RegularExpressions;


namespace Couche_Métier.Utilitaire
{
    /// <summary>
    /// Converti le format de l'argent
    /// </summary>
    public class ConverterFormatArgent
    {
        /// <summary>
        /// Permet de convertir l'argent en un string formatté
        /// </summary>
        /// <param name="argent"></param>
        public string ConvertToString(float argent)
        {
            // Mise à jour de l'argent
            string ret = "" + Math.Round(argent, 2);
            ret = ret.Replace(".", ",");
            ret = ret.Replace("€", " ");
            ret = ret.Trim();
            
            // 1 => 1,00
            if (new Regex("^[0-9]+$").IsMatch(ret))
            {
                ret += ",00";
            }

            // 1,2 => 1,20
            if (new Regex("^[0-9]+,[0-9]$").IsMatch(ret))
            {
                ret += "0";
            }

            ret += " €";
            return ret;
        }


        /// <summary>
        /// Permet de convertir l'argent formatté en double
        /// </summary>
        /// <param name="argent"></param>
        public float ConvertToDouble(string argent)
        {
            float argentresult = 0;

            // Mise à jour de l'argent
            string argentFormat = argent.Replace(".", ",");
            argentFormat = argentFormat.Replace("€", " ");
            argentFormat = argentFormat.Trim();

            if (new Regex("^[0-9]+$").IsMatch(argentFormat) || new Regex("^[0-9]+,[0-9]+$").IsMatch(argentFormat))
            {
                argentresult = (float)(Convert.ToDouble(argentFormat));
            }
            else
            {
                throw new Exception("ArgentFormat");
            }

            return argentresult;
        }
    }
}
