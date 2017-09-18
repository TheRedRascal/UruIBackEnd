using System.Collections;

namespace UruIT.GameOfDrones.Dtl.Master
{
    public class MessageDto
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public IList Data { get; set; }

        public void Clear()
        {
            Message = string.Empty;
            Status = string.Empty;
            Data = null;
        }
    }
}
