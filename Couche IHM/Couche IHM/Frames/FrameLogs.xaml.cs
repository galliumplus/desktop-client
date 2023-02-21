using Couche_Métier;
using Couche_Métier.Log;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameLogs.xaml
    /// </summary>
    public partial class FrameLogs : Page
    {
        public FrameLogs()
        {
            InitializeComponent();
            FillListView();
        }

        /// <summary>
        /// Remplis la list view en lisant les logs
        /// </summary>
        private void FillListView()
        {

            // TEST =================================================================================
            List<string> listMonthYear = new List<string>();
            string montYear = string.Empty;
            // ======================================================================================

            ILog log = new LogToTXT();
            List<string> logsLine = log.loadLog();
            List<Log> list = new List<Log>();
            for(int i = logsLine.Count - 1; i > 0; i--)
            {
                string date = logsLine[i].Split('|')[0];
                string action = logsLine[i].Split('|')[1];
                string message = logsLine[i].Split('|')[2];
                string auteur = logsLine[i].Split('|')[3];
                list.Add(new Log(date, action, message, auteur));


                // TEST =================================================================================
                if(montYear != DateTime.Parse(date).ToString("MMMM yyyy"))
                {
                    montYear = DateTime.Parse(date).ToString("MMMM yyyy");
                    listMonthYear.Add(montYear);
                }
                // ========================================================================================
            }
            this.listLogs.ItemsSource = list;
        }

        /// <summary>
        /// Créer enumération 
        /// </summary>
        /// <param name="list"> Ce que contient l'enum </param>
        /// <returns> renvoie l'enumération </returns>
        private Array createEnum(List<string> list)
        {
            // Get the current application domain for the current thread
            AppDomain currentDomain = AppDomain.CurrentDomain;

            // Create a dynamic assembly in the current application domain,
            // and allow it to be executed and saved to disk.
            AssemblyName name = new AssemblyName("MyEnums");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()),AssemblyBuilderAccess.Run);

            // Define a dynamic module in "MyEnums" assembly.
            // For a single-module assembly, the module has the same name as the assembly.
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(name.Name);

            // Define a public enumeration with the name "MyEnum" and an underlying type of Integer.
            EnumBuilder myEnum = moduleBuilder.DefineEnum("EnumeratedTypes.MyEnum",
                                     TypeAttributes.Public, typeof(int));

            for(int i = 0; i < list.Count; i++)
            {
                myEnum.DefineLiteral(list[i], i);
            }
            

            // Create the enum
            myEnum.CreateType();

            // Finally, save the assembly
            assemblyBuilder.CreateInstance(name.Name + ".dll");

            return Enum.GetValues(myEnum);
        }
    }
}
