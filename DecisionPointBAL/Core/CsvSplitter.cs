using System.Collections.Generic;
using System.IO;

namespace DecisionPointBAL.Core
{
   public class CsvSplitter
    {
       public List<string[]> parseCSV(string path,char splitChar,bool firstRowHeader)
       {
           List<string[]> parsedData = new List<string[]>();

           try
           {
               using (StreamReader readFile = new StreamReader(path))
               {
                   string line;
                   string[] row;

                   while ((line = readFile.ReadLine()) != null)
                   {
                       row = line.Split(splitChar);                       
                       parsedData.Add(row);                       
                   }
                   if (firstRowHeader)
                   {
                       parsedData.RemoveAt(0);
                   }
               }
           }
           catch
           {
               throw;
           }

           return parsedData;
       }
     
    }
}
