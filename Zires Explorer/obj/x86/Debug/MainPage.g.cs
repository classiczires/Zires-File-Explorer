﻿#pragma checksum "C:\Users\Saeed.Zires\Documents\Visual Studio 2013\Projects\Player\1\Zires Explorer\Zires Explorer\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C71BABBE451D79CF3B41D634EF9CEE4D"
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


namespace Zires_Explorer {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.LongListSelector list;
        
        internal System.Windows.Controls.TextBlock numbersOfItem;
        
        internal System.Windows.Controls.TextBlock selectedItem;
        
        internal System.Windows.Controls.TextBlock pathBlock;
        
        internal System.Windows.Controls.Grid meno;
        
        internal System.Windows.Controls.Button search;
        
        internal System.Windows.Controls.Button new_folder;
        
        internal System.Windows.Controls.Button cut;
        
        internal System.Windows.Controls.Button copy;
        
        internal System.Windows.Controls.Button paste;
        
        internal System.Windows.Controls.Button delete;
        
        internal System.Windows.Controls.Button rename;
        
        internal System.Windows.Controls.Button selectall;
        
        internal System.Windows.Controls.Button selectnone;
        
        internal System.Windows.Controls.Button invertselection;
        
        internal System.Windows.Controls.Button properties;
        
        internal System.Windows.Controls.Button pintostart;
        
        internal System.Windows.Controls.Button refresh;
        
        internal System.Windows.Controls.Button exit;
        
        internal System.Windows.Controls.Grid home1;
        
        internal System.Windows.Media.PlaneProjection MAnimation;
        
        internal Microsoft.Phone.Controls.LongListSelector homeList;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Zires%20Explorer;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.list = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("list")));
            this.numbersOfItem = ((System.Windows.Controls.TextBlock)(this.FindName("numbersOfItem")));
            this.selectedItem = ((System.Windows.Controls.TextBlock)(this.FindName("selectedItem")));
            this.pathBlock = ((System.Windows.Controls.TextBlock)(this.FindName("pathBlock")));
            this.meno = ((System.Windows.Controls.Grid)(this.FindName("meno")));
            this.search = ((System.Windows.Controls.Button)(this.FindName("search")));
            this.new_folder = ((System.Windows.Controls.Button)(this.FindName("new_folder")));
            this.cut = ((System.Windows.Controls.Button)(this.FindName("cut")));
            this.copy = ((System.Windows.Controls.Button)(this.FindName("copy")));
            this.paste = ((System.Windows.Controls.Button)(this.FindName("paste")));
            this.delete = ((System.Windows.Controls.Button)(this.FindName("delete")));
            this.rename = ((System.Windows.Controls.Button)(this.FindName("rename")));
            this.selectall = ((System.Windows.Controls.Button)(this.FindName("selectall")));
            this.selectnone = ((System.Windows.Controls.Button)(this.FindName("selectnone")));
            this.invertselection = ((System.Windows.Controls.Button)(this.FindName("invertselection")));
            this.properties = ((System.Windows.Controls.Button)(this.FindName("properties")));
            this.pintostart = ((System.Windows.Controls.Button)(this.FindName("pintostart")));
            this.refresh = ((System.Windows.Controls.Button)(this.FindName("refresh")));
            this.exit = ((System.Windows.Controls.Button)(this.FindName("exit")));
            this.home1 = ((System.Windows.Controls.Grid)(this.FindName("home1")));
            this.MAnimation = ((System.Windows.Media.PlaneProjection)(this.FindName("MAnimation")));
            this.homeList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("homeList")));
        }
    }
}

