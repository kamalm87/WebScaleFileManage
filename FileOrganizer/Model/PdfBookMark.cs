using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizer.Pdf
{
    public class PdfBookMark
    {
        
        public PdfBookMark()
        {
            Children = new List<PdfBookMark>();
        }

        public PdfBookMark(string title, PdfBookMark parent = null)
        {
            Title = title;
            Parent = parent;
            Children = new List<PdfBookMark>();
        }

        public void AddChild(PdfBookMark pdfBookMark)
        {
            Children.Add(pdfBookMark);
        }

        public object Tag { get; set; }

        public string Title { get; set; }
        public PdfBookMark Parent { get; set; }
        public List<PdfBookMark> Children { get; set; }          
    }
}
