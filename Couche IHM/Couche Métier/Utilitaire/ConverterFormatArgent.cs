
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
        public string ConvertToString(double argent)
        {
            string ret = Convert.ToString(argent);
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
        public double ConvertToDouble(string argent)
        {
            double argentresult = 0;

            // Mise à jour de l'argent
            string argentFormat = argent.Replace(".", ",");
            argentFormat = argentFormat.Replace("€", " ");
            argentFormat = argentFormat.Trim();

            if (new Regex("^[0-9]+$").IsMatch(argentFormat) || new Regex("^[0-9]+,[0-9]+$").IsMatch(argentFormat))
            {
                argentresult = Convert.ToDouble(argentFormat);
            }
            else
            {
                throw new Exception("ArgentFormat");
            }

            return argentresult;
        }
    }
}
