using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Log
    {
        public string Date { get; set; }
        public string Message { get; set; }

        public string Type { get; set; } //Error, Info, Warning.....
    }
}
