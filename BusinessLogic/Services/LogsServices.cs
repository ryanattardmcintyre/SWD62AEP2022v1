using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
   public  class LogsServices
    {
        private ILogRepository _logRepo;
        public LogsServices(ILogRepository logRepo)
        {
            _logRepo = logRepo;

        }

        public void LogMessage(string message, string type)
        {
            Log l = new  Log();
            l.Message = message; l.Type = type;
            l.Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _logRepo.Log(l);
        }
    }
}
