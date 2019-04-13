using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Models
{
    public class PrinterWithChildren
    {
        public Printer printer { get; set; }
        public Status  status { get; set; }
        public PrintColor printColor { get; set; }

    }
}
