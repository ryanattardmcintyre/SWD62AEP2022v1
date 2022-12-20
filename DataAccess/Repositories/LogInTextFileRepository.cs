using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess.Repositories
{
    public class LogInTextFileRepository : ILogRepository
    {
        private string _fileName;
        public LogInTextFileRepository(string filename)
        {
            _fileName = filename;
        }
        public void Log(Log l)
        {
            using(StreamWriter sw = new StreamWriter(_fileName,true))
            {
                //sw.WriteLine($"Type: {l.Type}, Message: {l.Message}")

                //Json serialization
                string logAsAString = JsonConvert.SerializeObject(l);
                sw.WriteLine(logAsAString);
            }
        }
    }
}
