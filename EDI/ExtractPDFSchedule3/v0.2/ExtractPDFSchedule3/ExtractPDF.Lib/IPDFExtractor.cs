using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPDF.Lib
{
    public interface IPDFExtractor
    {
        void ProcessPage(string page_content);
    }
}
