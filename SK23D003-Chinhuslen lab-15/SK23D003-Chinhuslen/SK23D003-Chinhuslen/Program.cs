using Microsoft.Data.SqlClient;

namespace SK23D003_Chinhuslen
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }

    public class Connections
    {
        string dd;
        public string conn_string() { return dd; }
        public Connections(string name)
        {
            dd= name;
        }
    }
    public static class MyConnection
    {
        public static SqlConnection getConnection()
        {
            string Database = "server=L307-14;database=library_registration;";
            Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            SqlConnection connect = new SqlConnection(Database);
            return connect;  
        }
    }
}