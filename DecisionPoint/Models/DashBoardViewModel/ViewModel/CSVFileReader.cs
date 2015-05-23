using System.Collections.Generic;
using System.IO;
using System.Web;
using CsvHelper;

namespace DecisionPoint.Models.DashBoardViewModel.ViewModel
{
    public class CSVFileReader
    {
        public static CsvReader ReadCSvFile(string path)
        {
            try
            {
                //Assign the file to file reader for read the content of files
                var fileReader = File.OpenText(path);
                //Read the content of File using CSV reader
                return new CsvReader(fileReader);
            }
            catch
            {
                throw;
            }
            finally
            {
                
            }
        }
        public static CsvWriter WriteCSvFile(string path)
        {
            try
            {
                //Assign the file to file reader for read the content of files
                var sw = new StreamWriter(HttpContext.Current.Server.MapPath(@"~\Content\documents\upload\")+ Path.GetFileName(path));
                //Read the content of File using CSV reader
                return new CsvWriter(sw);
            }
            catch
            {
                throw;
            }
        }
    }
}