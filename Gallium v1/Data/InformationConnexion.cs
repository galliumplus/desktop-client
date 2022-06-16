using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Data
{
    internal class InformationConnexion
    {

        // Information of database
        private static string server = "45.90.160.99";
        private static string databases = "c3TestDev";
        private static string port = "3306";
        private static string uid = "cd_gallium";
        private static string pwd = "hb2T_Gvhj";

        public static string Server { get => server; set => server = value; }
        public static string Databases { get => databases; set => databases = value; }
        public static string Port { get => port; set => port = value; }
        public static string Uid { get => uid; set => uid = value; }
        public static string Pwd { get => pwd; set => pwd = value; }
    }
}
