
namespace Couche_Data
{
    internal class InformationConnexion
    {
        
        // Information of database
        private static string server = "";
        private static string databases = "";
        private static string port = "";
        private static string uid = "";
        private static string pwd = "";
        
        public static string Server { get => server; set => server = value; }
        public static string Databases { get => databases; set => databases = value; }
        public static string Port { get => port; set => port = value; }
        public static string Uid { get => uid; set => uid = value; }
        public static string Pwd { get => pwd; set => pwd = value; }
    }
}
