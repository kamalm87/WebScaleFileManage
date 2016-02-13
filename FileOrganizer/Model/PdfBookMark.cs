using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace FileOrganizer.Pdf
{
    public class PdfBookMark
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PdfBookMark));
        public PdfBookMark()
        {
            children = new List<PdfBookMark>();
        }

        public PdfBookMark(string title, PdfBookMark parent = null)
        {
            Title = title;
            Parent = parent;
            children = new List<PdfBookMark>();
        }

        public void AddChild(PdfBookMark pdfBookMark)
        {
            children.Add(pdfBookMark);
        }

        public object Tag { get; set; }

        public string Title { get; set; }
        public PdfBookMark Parent { get; set; }
        public List<PdfBookMark> children { get; set; }          
    }
}
