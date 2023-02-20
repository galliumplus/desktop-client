using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    public class ExportAdherentToExcel : IExportableAdherent
    {
        public void Export(List<Adhérent> adhérents)
        {
            // Chemin du fichier vers le bureau
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathFile = $"{pathDesktop}\\ListeAdherents{DateTime.Now.Year}-{DateTime.Now.Year + 1}.xlsx";

            // Si le fichier existe on le met à jour
            if (File.Exists(pathFile))
            {
                using (XLWorkbook workbook = new XLWorkbook(pathFile))
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Worksheet("Adhérents");
                    worksheet.Clear();
                    worksheet.Cell("A6").Value = "caca";
                    workbook.Save();

                }
            }
            else if (DateTime.Now.Month != 9)
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("Adhérents");

                    
                    workbook.SaveAs(pathFile);

                }
            }
             
        }


    }
}
