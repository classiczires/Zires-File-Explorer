﻿#pragma checksum "C:\Users\saeed\Documents\Visual Studio 2013\Projects\Zires Explorer\Zires Explorer\Properties.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D00F11DB97670016B71C8A65B1EA4C7"
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
    
    
    public partial class Properties : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage PropertiesPage;
        
        internal System.Windows.Media.PlaneProjection planeProjection;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot pivot;
        
        internal System.Windows.Controls.TextBox name;
        
        internal System.Windows.Controls.TextBox location;
        
        internal System.Windows.Controls.TextBlock type;
        
        internal System.Windows.Controls.TextBlock size;
        
        internal System.Windows.Controls.TextBlock contines;
        
        internal System.Windows.Controls.TextBlock create;
        
        internal System.Windows.Controls.CheckBox read_only;
        
        internal System.Windows.Controls.CheckBox hidden;
        
        internal System.Windows.Controls.CheckBox archive;
        
        internal System.Windows.Controls.Image icon;
        
        internal System.Windows.Controls.Grid music;
        
        internal System.Windows.Controls.TextBox songDuration;
        
        internal System.Windows.Controls.TextBox albumDuration;
        
        internal System.Windows.Controls.TextBox songTitle;
        
        internal System.Windows.Controls.TextBox albumArtist;
        
        internal System.Windows.Controls.Image albumArt;
        
        internal System.Windows.Controls.Button remove;
        
        internal System.Windows.Controls.Button changeOrSet;
        
        internal System.Windows.Controls.TextBox album;
        
        internal System.Windows.Controls.TextBox artist;
        
        internal System.Windows.Controls.TextBox genre;
        
        internal System.Windows.Controls.TextBox track;
        
        internal System.Windows.Controls.Grid picture;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Zires%20Explorer;component/Properties.xaml", System.UriKind.Relative));
            this.PropertiesPage = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("PropertiesPage")));
            this.planeProjection = ((System.Windows.Media.PlaneProjection)(this.FindName("planeProjection")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.pivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivot")));
            this.name = ((System.Windows.Controls.TextBox)(this.FindName("name")));
            this.location = ((System.Windows.Controls.TextBox)(this.FindName("location")));
            this.type = ((System.Windows.Controls.TextBlock)(this.FindName("type")));
            this.size = ((System.Windows.Controls.TextBlock)(this.FindName("size")));
            this.contines = ((System.Windows.Controls.TextBlock)(this.FindName("contines")));
            this.create = ((System.Windows.Controls.TextBlock)(this.FindName("create")));
            this.read_only = ((System.Windows.Controls.CheckBox)(this.FindName("read_only")));
            this.hidden = ((System.Windows.Controls.CheckBox)(this.FindName("hidden")));
            this.archive = ((System.Windows.Controls.CheckBox)(this.FindName("archive")));
            this.icon = ((System.Windows.Controls.Image)(this.FindName("icon")));
            this.music = ((System.Windows.Controls.Grid)(this.FindName("music")));
            this.songDuration = ((System.Windows.Controls.TextBox)(this.FindName("songDuration")));
            this.albumDuration = ((System.Windows.Controls.TextBox)(this.FindName("albumDuration")));
            this.songTitle = ((System.Windows.Controls.TextBox)(this.FindName("songTitle")));
            this.albumArtist = ((System.Windows.Controls.TextBox)(this.FindName("albumArtist")));
            this.albumArt = ((System.Windows.Controls.Image)(this.FindName("albumArt")));
            this.remove = ((System.Windows.Controls.Button)(this.FindName("remove")));
            this.changeOrSet = ((System.Windows.Controls.Button)(this.FindName("changeOrSet")));
            this.album = ((System.Windows.Controls.TextBox)(this.FindName("album")));
            this.artist = ((System.Windows.Controls.TextBox)(this.FindName("artist")));
            this.genre = ((System.Windows.Controls.TextBox)(this.FindName("genre")));
            this.track = ((System.Windows.Controls.TextBox)(this.FindName("track")));
            this.picture = ((System.Windows.Controls.Grid)(this.FindName("picture")));
        }
    }
}

