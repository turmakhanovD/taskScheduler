using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    public class ActionTask
    {
        public string TaskName { get; set; }
        public Periodicity Periodicity { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DownloadDirectory { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
    }
}
