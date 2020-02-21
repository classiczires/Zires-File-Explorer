using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Phone.Info;

namespace Zires_Explorer.ViewModels
{
    public class ItemViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Path { get; set; }
        public string Source { get; set; }
        public Int64 Tag { get; set; }
        public SolidColorBrush color { get; set; }
    }
}
