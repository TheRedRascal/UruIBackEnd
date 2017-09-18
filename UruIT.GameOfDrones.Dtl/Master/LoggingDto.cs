using System;

namespace UruIT.GameOfDrones.Dtl.Master
{
    public class LoggingDto
    {
        public int LogId { get; set; }
        public string FullException { get; set; }
        public string SimpleError { get; set; }
        public string ModuleName { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
