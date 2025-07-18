using System.Collections.Generic;
using System.Data;
using System.Linq;
using P4DHelperClass = Plant4D;


namespace Process_Line_Data_Manager
{
    /// <summary>
    /// Class to hold data for use between various forms in the program.
    /// </summary>
    public class DataHolder
    {
        //Holds data imported from HRU database
        public static DataTable HRUData = new DataTable();

        //Holds HRU data imported from P4D database
        public static DataTable P4DData = new DataTable();

        //Holds comparison data of NE_HRUData and HRU
        public static DataTable Differences = new DataTable();

        //Holds the currently selected server instances
        public static P4DHelperClass.Plant4DServer SelectedServer = new P4DHelperClass.Plant4DServer();
        public static P4DHelperClass.Plant4DServer ProjectServer = new P4DHelperClass.Plant4DServer();

        //Holds the currently selected project database
        public static P4DProject SelectedProject = new P4DProject();

        //Holds the project list from PCE database
        public static DataTable PCEProjects = new DataTable();
        public static List<P4DProject> P4DProjects = new List<P4DProject>();


        /// <summary>
        /// Compares P4D and HRU data tables to see if they differ. Returns TRUE if they are different.
        /// </summary>
        public static bool HRUP4DAreDifferent
        {
            get
            {
                return DataFunctions.TableHasChanges(Differences);
            }
        }

        public static void LoadP4DProjectList()
        {
            List<P4DProject> projects = new List<P4DProject>();
            projects = (from DataRow dr in PCEProjects.Rows
                        select new P4DProject()
                        {
                            Name = dr["NAME"].ToString(),
                            DbName = dr["DBNAME"].ToString(),
                            ServerName = dr["SERVERNAME"].ToString(),
                            Year = dr["YEAR"].ToString(),
                            Directory = dr["DIRECTORY"].ToString()
                        }).ToList();
            P4DProjects = projects;
        }

        public static void AssignP4DProject(string selectedProjDBName)
        {
            var result = P4DProjects.Find(delegate (P4DProject p) { return p.DbName == selectedProjDBName; });
            SelectedProject = result;
        }

        public static void ResetP4DProject()
        {
            HRUData = new DataTable();
            P4DData = new DataTable();
            Differences = new DataTable();
            ProjectServer = new P4DHelperClass.Plant4DServer();
            SelectedProject = new P4DProject();
        }

        public DataHolder() { }
    }
}
