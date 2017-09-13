using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPDFSpeifiedRequirement
{
    public interface PDFExtractorInterface
    {
        void ProcessPage(string page_content);
    }
}
