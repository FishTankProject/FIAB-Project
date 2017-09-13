using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPDFHelperNameSpace
{
    public interface PDFExtractorInterface
    {
        void ProcessPage(string page_content);
    }
}
