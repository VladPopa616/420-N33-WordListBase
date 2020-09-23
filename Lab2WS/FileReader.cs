using System;
using System.IO;

namespace Lab2WS
{
    class FileReader
    {
        public string[] Read(string filename)
        {
            try
            {
                string[] contents = File.ReadAllLines(filename);
                return contents;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            
        }
    }
}
