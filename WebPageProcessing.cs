using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace ConsoleFindItems
{
    class WebPageProcessing
    {
        private List<String> _resultList;
        private string _fileBase = TransactionSettings.BaseDirectory;

        public WebPageProcessing(string baseDirectory)
        {
            _fileBase = baseDirectory;
            _resultList = new List<String>();
        }

        public void AddResultsToList(String filePath)
        {
            string line;
            // read file
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                _resultList.Add(line);
                // add urls to String[] array
            }
        }

        public List<string> GetResultList()
        {
                return _resultList;
        }

        public void RetrieveAndSaveWebPage(string urlString)
        {
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(urlString);
            string targetFile = _fileBase + "test2.html";
            string htmlString = "";

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    Console.WriteLine("{0}:{1}", i, sLine);
                    htmlString += sLine;
            }
            //Console.ReadLine();
            System.IO.File.WriteAllText(targetFile, htmlString);
        }

        public void ParseWebPageExtractJPG(string html)
        {

        }
    }
}
