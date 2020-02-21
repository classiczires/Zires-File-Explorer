using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.Windows.Input;
using System.IO;
using Zires_Explorer.Player;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zires_Explorer.Player
{
    public partial class VideoPlayer : PhoneApplicationPage
    {
        public static TimeSpan nowPositionOfPlayer { get; set; }


        public VideoPlayer()
        {
            try
            {
                InitializeComponent();     
                player.DataContext = this;
                time.DataContext = System.DateTime.Now;

                player.Source = new Uri((Application.Current as App).path, UriKind.Relative);

                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
                Touch.FrameReported += Touch_FrameReported;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        { 
            player.Source = new Uri((Application.Current as App).path, UriKind.Relative);
            player.Position = (Application.Current as App).playerPosition;
            timer.Start();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            (Application.Current as App).playerPosition = player.Position;
        }

        Point onePoint = new Point();
        Point towPoint = new Point();

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            TouchPointCollection touches = e.GetTouchPoints(ContentPanel);

            if (touches.Count == 2)
            {
                if ((onePoint.X - e.GetTouchPoints(ContentPanel)[0].Position.X) < (towPoint.X - e.GetTouchPoints(ContentPanel)[1].Position.X)) 
                {
                    player.Height += 2;
                    player.Width += 2;
                }
                else if ((towPoint.X - onePoint.X) < (e.GetTouchPoints(ContentPanel)[1].Position.X - e.GetTouchPoints(ContentPanel)[0].Position.X))
                {
                    if (player.Height > 50)
                    {
                        player.Height -= 2;
                        player.Width -= 2;

                    }
                }
                    
                onePoint = touches[0].Position;
                towPoint = touches[1].Position;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            slider.Value = player.Position.TotalSeconds;
        }

        public static DispatcherTimer timer = new DispatcherTimer();


        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                slider.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
                name.Text = (Application.Current as App).itemName;
                Dimensions.Text = "Dimensions: " + player.NaturalVideoWidth + " × " + player.NaturalVideoHeight;
                lenght.Text = string.Format("{0}:{1:D1}:{2:D1}", (int)player.NaturalDuration.TimeSpan.Hours, (int)player.NaturalDuration.TimeSpan.Minutes,
                    (int)player.NaturalDuration.TimeSpan.Seconds);

                player.Position = (Application.Current as App).playerPosition;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show(e.ErrorException.Message);
            if (mbr == MessageBoxResult.OK)
                NavigationService.GoBack();
        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            slider.Value = 0;
            play.Visibility = System.Windows.Visibility.Visible;
            pause.Visibility = System.Windows.Visibility.Collapsed;

            Open_Controls.Begin();
        }      

        private void ContentPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                if (controls.Visibility == System.Windows.Visibility.Collapsed)
                    Open_Controls.Begin();
                else
                    Close_Controls.Begin();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //buttons controls Events
        private void inFo_Click(object sender, RoutedEventArgs e)
        {
            player.Markers.Clear();
            FileInfo file = new FileInfo((Application.Current as App).path);

            detailsDateCreated.Text = file.CreationTime.ToString();
            detailsLastAccessTime.Text = file.LastAccessTime.ToString();
            detailsFolderPath.Text = file.Directory.FullName;
            detailsFrameHeight.Text = player.NaturalVideoHeight.ToString();
            detailsFrameWidth.Text = player.NaturalVideoWidth.ToString();
            detailsLength.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)player.NaturalDuration.TimeSpan.Hours, (int)player.NaturalDuration.TimeSpan.Minutes,
                    (int)player.NaturalDuration.TimeSpan.Seconds);
            detailsName.Text = file.Name;

            long si = file.Length;
            if (si < 1024)
                detailsSize.Text = si + " byte";
            else if (si >= 1024 && si < 1048576)
                detailsSize.Text = si / 1024 + " KB";
            else if (si >= 1048576 && si < 1073741824)
                detailsSize.Text = Math.Round(si / 1048576.0, 2) + " MB";
            else if (si >= 1073741824)
                detailsSize.Text = Math.Round((si / 1048576.0) / 1024, 2) + " GB";


            Open_detailsPage.Begin();
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            if (player.Source != null)
            {
                if (player.Position < TimeSpan.FromSeconds(player.NaturalDuration.TimeSpan.TotalSeconds / 10))
                    player.Position = TimeSpan.Zero;
                else
                    player.Position -= TimeSpan.FromSeconds(player.NaturalDuration.TimeSpan.TotalSeconds / 10);

                player.Markers.Clear();
                player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3)) });
            }
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            play.Visibility = System.Windows.Visibility.Collapsed;
            player.Play();
            timer.Start();
            pause.Visibility = System.Windows.Visibility.Visible;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            pause.Visibility = System.Windows.Visibility.Collapsed;
            player.Pause();
            play.Visibility = System.Windows.Visibility.Visible;
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (player.Source != null)
            {
                if ((player.NaturalDuration.TimeSpan - player.Position) < TimeSpan.FromSeconds(player.NaturalDuration.TimeSpan.TotalSeconds / 10))
                    player.Position = TimeSpan.Zero;
                else
                    player.Position += TimeSpan.FromSeconds(player.NaturalDuration.TimeSpan.TotalSeconds / 10);

                player.Markers.Clear();
                player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3)) });
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            timer.Stop();
            slider.Value = 0;
            play.Visibility = System.Windows.Visibility.Visible;
            pause.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void zoom_Click(object sender, RoutedEventArgs e)
        {
            player.Markers.Clear();
            Open_Zoompage.Begin();
        }

        private void background_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (zoomPage.Visibility == System.Windows.Visibility.Visible)
                Close_Zoompage.Begin();
            if (detailsPage.Visibility == System.Windows.Visibility.Visible)
                Close_detailsPage.Begin();
        }



        private void slider_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            timer.Stop();
        }

        private void slider_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            player.Position = TimeSpan.FromSeconds(slider.Value);
        }

        private void slider_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            player.Position = TimeSpan.FromSeconds(slider.Value);
            timer.Start();

            player.Markers.Clear();
            player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3)) });
        }

        private void ContentPanel_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            
        }

        private void ContentPanel_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }



        //Zoom Page Events
        private void ZoomPageBack_Click(object sender, RoutedEventArgs e)
        {
            player.Markers.Clear();
            player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3)) });

            Close_Zoompage.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            alignCenters();
            Close_Zoompage.Begin();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            player.Height = 240;
            player.Width = 400;
            Close_Zoompage.Begin();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) <= 721)
            {
                player.Width = ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) + 288);
                player.Height = 480;
            }
            else if ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) <= 769)
            {
                player.Width = ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) + 478);
                player.Height = 720;
            }
            else if (Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5 <= 1081)
            {
                player.Width = ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) + 806);
                player.Height = 1080;
            }

            
            Close_Zoompage.Begin();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            player.Height = 720;
            player.Width = 1200;

            Close_Zoompage.Begin();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            player.Height = 960;
            player.Width = 1600;

            Close_Zoompage.Begin();
        }

        public void alignCenters()
        {
            ContentPanel.ScrollToHorizontalOffset(ContentPanel.ScrollableWidth * 0.5);
            ContentPanel.ScrollToVerticalOffset(ContentPanel.ScrollableHeight * 0.5);
        }


        //Details Page Events
        private void detailsPageBack_Click(object sender, RoutedEventArgs e)
        {
            player.Markers.Clear();
            player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3)) });

            Close_detailsPage.Begin();
        }



        private void PhoneApplicationPage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            Size screenSize = new Size();
            
            if ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) <= 481)
            {
                screenSize.Width = ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) + 320);
                screenSize.Height = 480;
            }
            else if ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) <= 769)
            {
                screenSize.Width = ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) + 800);
                screenSize.Height = 720;
            }
            else if (Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5 <= 1081)
            {
                screenSize.Width = ((Windows.Graphics.Display.DisplayProperties.LogicalDpi * 5) + 806);
                screenSize.Height = 1080;
            }


            if (player.Height > screenSize.Height)
                ContentPanel.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            else
                ContentPanel.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;


            if (player.Width > screenSize.Width)
                ContentPanel.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            else
                ContentPanel.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }

        private void slider_Loaded(object sender, RoutedEventArgs e)
        {
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 0) as Rectangle).Height = 2;
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 1) as Rectangle).Height = 7;

            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).Fill = new SolidColorBrush(Colors.White);
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).Width = (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).Height = 20;
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).RadiusX = 100;
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).RadiusY = 100;

        }

        private void ContentPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void player_DownloadProgressChanged(object sender, RoutedEventArgs e)
        {

        }

        private void PhoneApplicationPage_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            time.Text = string.Format("{0:D2}:{1:D2}", DateTime.Now.Hour, DateTime.Now.Minute);

        }

        private void Open_Controls_Completed(object sender, EventArgs e)
        {
            player.Markers.Clear();
            player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3))});


        }
        private void Close_Controls_Completed(object sender, EventArgs e)
        {

        }

        private void player_MarkerReached(object sender, TimelineMarkerRoutedEventArgs e)
        {
            Close_Controls.Begin();
        }

        private void player_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            player.Markers.Clear();
            player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3)) });

            if (player.CurrentState == MediaElementState.Stopped)
                player.Markers.Clear();
        }

        private void Stretch_Checked(object sender, RoutedEventArgs e)
        {
            player.Stretch = System.Windows.Media.Stretch.Fill;
        }

        private void Stretch_Unchecked(object sender, RoutedEventArgs e)
        {
            player.Stretch = System.Windows.Media.Stretch.Uniform;
        }

        private void Stretch_Click(object sender, RoutedEventArgs e)
        {
            player.Markers.Clear();
            player.Markers.Add(new TimelineMarker() { Time = (player.Position + TimeSpan.FromSeconds(3)) });
        }
    }
}