﻿#pragma checksum "C:\Users\saeed\Documents\Visual Studio 2013\Projects\Zires Explorer\Zires Explorer\SongTitleControls.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D1335BB17EB90D0E57E6490D30958266"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace MusicByComposer {
    
    
    public partial class SongTitleControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock txtblkTitle;
        
        internal System.Windows.Controls.TextBlock txtblkTime;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Zires%20Explorer;component/SongTitleControls.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txtblkTitle = ((System.Windows.Controls.TextBlock)(this.FindName("txtblkTitle")));
            this.txtblkTime = ((System.Windows.Controls.TextBlock)(this.FindName("txtblkTime")));
        }
    }
}

