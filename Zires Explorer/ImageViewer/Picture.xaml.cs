using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Zires_Explorer
{
    public partial class Picture : UserControl
    {
        public Picture()
        {
            InitializeComponent();
        }

    }

    public class PictureSource
    {
        Uri pictureSource { get; set; }
    }
}
