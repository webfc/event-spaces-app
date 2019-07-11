using System;

namespace ExemploLogCore.Model
{
    public class LogEvento
    {
        public int? EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
