﻿#pragma checksum "C:\Users\saeed\Documents\Visual Studio 2013\Projects\Zires Explorer\Zires Explorer\ImageViewer\ImageViewer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5FC2790872F70BFF30E724E3D45792F4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Zires_Explorer.ImageViewer {
    
    
    public partial class ImageViewer : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage imageViewer;
        
        internal System.Windows.Media.PlaneProjection planProjection;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Primitives.ViewportControl viewPort;
        
        internal System.Windows.Controls.Image image;
        
        internal System.Windows.Controls.Button button;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Zires%20Explorer;component/ImageViewer/ImageViewer.xaml", System.UriKind.Relative));
            this.imageViewer = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("imageViewer")));
            this.planProjection = ((System.Windows.Media.PlaneProjection)(this.FindName("planProjection")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.viewPort = ((System.Windows.Controls.Primitives.ViewportControl)(this.FindName("viewPort")));
            this.image = ((System.Windows.Controls.Image)(this.FindName("image")));
            this.button = ((System.Windows.Controls.Button)(this.FindName("button")));
        }
    }
}
