﻿#pragma checksum "C:\Users\saeed\Documents\Visual Studio 2013\Projects\Zires Explorer\Zires Explorer\Player\VideoPlayer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FD9FE73ADCE1327555002E6B0BE14F36"
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


namespace Zires_Explorer.Player {
    
    
    public partial class VideoPlayer : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard Open_Controls;
        
        internal System.Windows.Media.Animation.Storyboard Close_Controls;
        
        internal System.Windows.Media.Animation.ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames;
        
        internal System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames_1;
        
        internal System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames_2;
        
        internal System.Windows.Media.Animation.Storyboard Open_Zoompage;
        
        internal System.Windows.Media.Animation.Storyboard Close_Zoompage;
        
        internal System.Windows.Media.Animation.Storyboard Open_detailsPage;
        
        internal System.Windows.Media.Animation.Storyboard Close_detailsPage;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid background;
        
        internal System.Windows.Controls.ScrollViewer ContentPanel;
        
        internal System.Windows.Controls.MediaElement player;
        
        internal System.Windows.Controls.Grid controls;
        
        internal System.Windows.Controls.Grid AudioControls;
        
        internal System.Windows.Media.PlaneProjection controlsButtomPlaneProjection;
        
        internal System.Windows.Controls.TextBlock durationHours;
        
        internal System.Windows.Controls.TextBlock durationMinutes;
        
        internal System.Windows.Controls.TextBlock durationSeconds;
        
        internal System.Windows.Controls.TextBlock Dimensions;
        
        internal System.Windows.Controls.TextBlock lenght;
        
        internal System.Windows.Controls.Slider slider;
        
        internal System.Windows.Controls.Button play;
        
        internal System.Windows.Controls.Button pause;
        
        internal System.Windows.Media.PlaneProjection pau;
        
        internal System.Windows.Controls.Button stop;
        
        internal System.Windows.Media.PlaneProjection sto;
        
        internal System.Windows.Controls.Button next;
        
        internal System.Windows.Media.PlaneProjection nex;
        
        internal System.Windows.Controls.Button previous;
        
        internal System.Windows.Media.PlaneProjection pre;
        
        internal System.Windows.Controls.Button inFo;
        
        internal System.Windows.Media.PlaneProjection pre1;
        
        internal System.Windows.Controls.Button zoom;
        
        internal System.Windows.Media.PlaneProjection sto1;
        
        internal System.Windows.Controls.Primitives.ToggleButton Stretch;
        
        internal System.Windows.Media.PlaneProjection Stre;
        
        internal System.Windows.Media.PlaneProjection controlsTopPlaneProjection;
        
        internal System.Windows.Controls.TextBlock name;
        
        internal System.Windows.Controls.TextBlock time;
        
        internal System.Windows.Controls.Border zoomPage;
        
        internal System.Windows.Media.PlaneProjection ZoomPlaneProjection;
        
        internal System.Windows.Controls.Button ZoomPageBack;
        
        internal System.Windows.Media.PlaneProjection PlaneProjection1;
        
        internal System.Windows.Media.PlaneProjection PlaneProjection2;
        
        internal System.Windows.Media.PlaneProjection PlaneProjection3;
        
        internal System.Windows.Media.PlaneProjection PlaneProjection4;
        
        internal System.Windows.Media.PlaneProjection PlaneProjection5;
        
        internal System.Windows.Controls.Grid detailsPage;
        
        internal System.Windows.Media.PlaneProjection PlaneProjectionDetails;
        
        internal System.Windows.Controls.Button detailsPageBack;
        
        internal System.Windows.Controls.TextBlock detailsName;
        
        internal System.Windows.Controls.TextBlock detailsFrameWidth;
        
        internal System.Windows.Controls.TextBlock detailsFrameHeight;
        
        internal System.Windows.Controls.TextBlock detailsLength;
        
        internal System.Windows.Controls.TextBlock detailsFolderPath;
        
        internal System.Windows.Controls.TextBlock detailsSize;
        
        internal System.Windows.Controls.TextBlock detailsDateCreated;
        
        internal System.Windows.Controls.TextBlock detailsLastAccessTime;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Zires%20Explorer;component/Player/VideoPlayer.xaml", System.UriKind.Relative));
            this.Open_Controls = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Open_Controls")));
            this.Close_Controls = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Close_Controls")));
            this.objectAnimationUsingKeyFrames = ((System.Windows.Media.Animation.ObjectAnimationUsingKeyFrames)(this.FindName("objectAnimationUsingKeyFrames")));
            this.doubleAnimationUsingKeyFrames_1 = ((System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)(this.FindName("doubleAnimationUsingKeyFrames_1")));
            this.doubleAnimationUsingKeyFrames_2 = ((System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)(this.FindName("doubleAnimationUsingKeyFrames_2")));
            this.Open_Zoompage = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Open_Zoompage")));
            this.Close_Zoompage = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Close_Zoompage")));
            this.Open_detailsPage = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Open_detailsPage")));
            this.Close_detailsPage = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Close_detailsPage")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.background = ((System.Windows.Controls.Grid)(this.FindName("background")));
            this.ContentPanel = ((System.Windows.Controls.ScrollViewer)(this.FindName("ContentPanel")));
            this.player = ((System.Windows.Controls.MediaElement)(this.FindName("player")));
            this.controls = ((System.Windows.Controls.Grid)(this.FindName("controls")));
            this.AudioControls = ((System.Windows.Controls.Grid)(this.FindName("AudioControls")));
            this.controlsButtomPlaneProjection = ((System.Windows.Media.PlaneProjection)(this.FindName("controlsButtomPlaneProjection")));
            this.durationHours = ((System.Windows.Controls.TextBlock)(this.FindName("durationHours")));
            this.durationMinutes = ((System.Windows.Controls.TextBlock)(this.FindName("durationMinutes")));
            this.durationSeconds = ((System.Windows.Controls.TextBlock)(this.FindName("durationSeconds")));
            this.Dimensions = ((System.Windows.Controls.TextBlock)(this.FindName("Dimensions")));
            this.lenght = ((System.Windows.Controls.TextBlock)(this.FindName("lenght")));
            this.slider = ((System.Windows.Controls.Slider)(this.FindName("slider")));
            this.play = ((System.Windows.Controls.Button)(this.FindName("play")));
            this.pause = ((System.Windows.Controls.Button)(this.FindName("pause")));
            this.pau = ((System.Windows.Media.PlaneProjection)(this.FindName("pau")));
            this.stop = ((System.Windows.Controls.Button)(this.FindName("stop")));
            this.sto = ((System.Windows.Media.PlaneProjection)(this.FindName("sto")));
            this.next = ((System.Windows.Controls.Button)(this.FindName("next")));
            this.nex = ((System.Windows.Media.PlaneProjection)(this.FindName("nex")));
            this.previous = ((System.Windows.Controls.Button)(this.FindName("previous")));
            this.pre = ((System.Windows.Media.PlaneProjection)(this.FindName("pre")));
            this.inFo = ((System.Windows.Controls.Button)(this.FindName("inFo")));
            this.pre1 = ((System.Windows.Media.PlaneProjection)(this.FindName("pre1")));
            this.zoom = ((System.Windows.Controls.Button)(this.FindName("zoom")));
            this.sto1 = ((System.Windows.Media.PlaneProjection)(this.FindName("sto1")));
            this.Stretch = ((System.Windows.Controls.Primitives.ToggleButton)(this.FindName("Stretch")));
            this.Stre = ((System.Windows.Media.PlaneProjection)(this.FindName("Stre")));
            this.controlsTopPlaneProjection = ((System.Windows.Media.PlaneProjection)(this.FindName("controlsTopPlaneProjection")));
            this.name = ((System.Windows.Controls.TextBlock)(this.FindName("name")));
            this.time = ((System.Windows.Controls.TextBlock)(this.FindName("time")));
            this.zoomPage = ((System.Windows.Controls.Border)(this.FindName("zoomPage")));
            this.ZoomPlaneProjection = ((System.Windows.Media.PlaneProjection)(this.FindName("ZoomPlaneProjection")));
            this.ZoomPageBack = ((System.Windows.Controls.Button)(this.FindName("ZoomPageBack")));
            this.PlaneProjection1 = ((System.Windows.Media.PlaneProjection)(this.FindName("PlaneProjection1")));
            this.PlaneProjection2 = ((System.Windows.Media.PlaneProjection)(this.FindName("PlaneProjection2")));
            this.PlaneProjection3 = ((System.Windows.Media.PlaneProjection)(this.FindName("PlaneProjection3")));
            this.PlaneProjection4 = ((System.Windows.Media.PlaneProjection)(this.FindName("PlaneProjection4")));
            this.PlaneProjection5 = ((System.Windows.Media.PlaneProjection)(this.FindName("PlaneProjection5")));
            this.detailsPage = ((System.Windows.Controls.Grid)(this.FindName("detailsPage")));
            this.PlaneProjectionDetails = ((System.Windows.Media.PlaneProjection)(this.FindName("PlaneProjectionDetails")));
            this.detailsPageBack = ((System.Windows.Controls.Button)(this.FindName("detailsPageBack")));
            this.detailsName = ((System.Windows.Controls.TextBlock)(this.FindName("detailsName")));
            this.detailsFrameWidth = ((System.Windows.Controls.TextBlock)(this.FindName("detailsFrameWidth")));
            this.detailsFrameHeight = ((System.Windows.Controls.TextBlock)(this.FindName("detailsFrameHeight")));
            this.detailsLength = ((System.Windows.Controls.TextBlock)(this.FindName("detailsLength")));
            this.detailsFolderPath = ((System.Windows.Controls.TextBlock)(this.FindName("detailsFolderPath")));
            this.detailsSize = ((System.Windows.Controls.TextBlock)(this.FindName("detailsSize")));
            this.detailsDateCreated = ((System.Windows.Controls.TextBlock)(this.FindName("detailsDateCreated")));
            this.detailsLastAccessTime = ((System.Windows.Controls.TextBlock)(this.FindName("detailsLastAccessTime")));
        }
    }
}

