using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class RequestWithChildren
    {
        public Request request { get; set; }
        public Status status { get; set; }
        public Printer printer { get; set; }
        public User user { get; set; }
    }

}
