using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.BackgroundAudio;
using System.Windows.Media;
using Microsoft.Phone;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;


namespace Zires_Explorer.Player
{
    public partial class Player : PhoneApplicationPage
    {
        Zires_Explorer.Player.AlbumInfo albumInfo = new AlbumInfo();
        public MediaLibrary mediaLib = new MediaLibrary();
        public SongCollection songCollection { get; protected set; }
        public AlbumCollection albumCollection { get; protected set; }

        UserControl selectedMusic = new UserControl();
        delegate void myDelegate();
        
        ObservableCollection<Song> songs = new ObservableCollection<Song>();
        ObservableCollection<AlbumInfo> albums = new ObservableCollection<AlbumInfo>();
        DispatcherTimer timer = new DispatcherTimer();


        public Player()
        {
            try
            {
                InitializeComponent();
                load_Data(false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan pos = MediaPlayer.PlayPosition;

            position.Text = String.Format("{0:D1}:{1:D2}", (int)pos.Minutes, pos.Seconds);
            slider.Value = MediaPlayer.PlayPosition.TotalSeconds;
        }


        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            try
            {
                load_Data(true);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                    

        public void load_Data(bool entry)
        {
            if (entry)
            {
                myDelegate delegate_1 = new myDelegate(this.refresh_Page);
                myDelegate delegate_2 = new myDelegate(this.load_StartUpData);
                delegate_1.Invoke();
                delegate_2.Invoke();
            }
            else
            {
                myDelegate delegate_1 = new myDelegate(this.load_StartUpData);
                delegate_1.Invoke();
            }
        }

        public void load_StartUpData()
        {
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Tick += timer_Tick;

            songCollection = mediaLib.Songs;
            albumCollection = mediaLib.Albums;

            foreach (Song so in songCollection)
            {
                songs.Add(so);
            }
            songsList.ItemsSource = songs;



            foreach (Microsoft.Xna.Framework.Media.Album al in albumCollection)
            {
                BitmapImage bitmimage = new BitmapImage();
                if (al.HasArt)
                    bitmimage.SetSource(al.GetAlbumArt());

                albums.Add(new AlbumInfo()
                {
                    Thumbnail = bitmimage,
                    Artist = al.Artist,
                    AlbumSongs = al.Songs,
                    HasArt = al.HasArt,
                    Name = al.Name
                });
                al.Dispose();
            }
            albumsList.ItemsSource = albums;

                


            if (MediaPlayer.Queue.ActiveSong != null)
            {
                slider.Maximum = MediaPlayer.Queue.ActiveSong.Duration.TotalSeconds;
                timer.Start();
            }

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            MediaPlayer.ActiveSongChanged += MediaPlayer_ActiveSongChanged;
            MediaPlayer_MediaStateChanged(null, EventArgs.Empty);
        }
        
        public void refresh_Page()
        {
            BitmapImage ziresIcon = new BitmapImage();
            ziresIcon.UriSource = new Uri("/Assets/ExplorerIcons/Other/ZiresPlayer.png", UriKind.Relative);
            AlbumArt.Source = ziresIcon;


            if (MediaPlayer.State == MediaState.Playing)
            {
                if (MediaPlayer.Queue.ActiveSong.Album.GetAlbumArt() != null)
                {
                    BitmapImage AlbumArtBitmapImage = new BitmapImage();
                    AlbumArtBitmapImage.SetSource(MediaPlayer.Queue.ActiveSong.Album.GetAlbumArt());
                    AlbumArt.Source = AlbumArtBitmapImage;
                }
                if (MediaPlayer.Queue.ActiveSong.Name != null)
                {
                    sn.Text = MediaPlayer.Queue.ActiveSong.Name;
                    name.Text = MediaPlayer.Queue.ActiveSong.Name;
                }
                if (MediaPlayer.Queue.ActiveSong.Artist.Name != null)
                    sa.Text = MediaPlayer.Queue.ActiveSong.Artist.Name;

                if (MediaPlayer.Queue.ActiveSong.Duration.TotalSeconds > 3599)
                    lenght.Text = string.Format("{0}:{1}:{2:D2}", (int)MediaPlayer.Queue.ActiveSong.Duration.Hours, (int)MediaPlayer.Queue.ActiveSong.Duration.Minutes,
                        (int)MediaPlayer.Queue.ActiveSong.Duration.Seconds);
                else
                {
                    lenght.Text = string.Format("{0}:{1:D2}", (int)MediaPlayer.Queue.ActiveSong.Duration.Minutes,
                        (int)MediaPlayer.Queue.ActiveSong.Duration.Seconds);
                }
            }
        }

        Control findElement(DependencyObject obj)
        {
            var theObj = VisualTreeHelper.GetChild(obj, MediaPlayer.Queue.ActiveSongIndex);
            return (Control)theObj;
        }


        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            switch (MediaPlayer.State)
            {
                case MediaState.Playing:
                    playPause.IsChecked = true;
                    slider.Maximum = MediaPlayer.Queue.ActiveSong.Duration.TotalSeconds;
                    timer.Start();
                    break;
                case MediaState.Paused:
                case MediaState.Stopped:
                    playPause.IsChecked = false;
                    break;
            }
        }


        void MediaPlayer_ActiveSongChanged(object sender, EventArgs e)
        {
            try
            {
                slider.Maximum = MediaPlayer.Queue.ActiveSong.Duration.TotalSeconds;
                BitmapImage ziresIcon = new BitmapImage();
                ziresIcon.UriSource = new Uri("/Assets/ExplorerIcons/Other/ZiresPlayer.png", UriKind.Relative);
                AlbumArt.Source = ziresIcon;

                SongChangedAnimation.Begin();

                timer.Start();

                if (MediaPlayer.State == MediaState.Playing)
                {
                    if (MediaPlayer.Queue.ActiveSong.Album.GetAlbumArt() != null)
                    {
                        BitmapImage AlbumArtBitmapImage = new BitmapImage();
                        AlbumArtBitmapImage.SetSource(MediaPlayer.Queue.ActiveSong.Album.GetAlbumArt());
                        AlbumArt.Source = AlbumArtBitmapImage;
                    }
                    if (MediaPlayer.Queue.ActiveSong.Name != null)
                    {
                        sn.Text = MediaPlayer.Queue.ActiveSong.Name;
                        name.Text = MediaPlayer.Queue.ActiveSong.Name;
                    }
                    if (MediaPlayer.Queue.ActiveSong.Artist.Name != null)
                        sa.Text = MediaPlayer.Queue.ActiveSong.Artist.Name;

                    if (MediaPlayer.Queue.ActiveSong.Duration.TotalSeconds > 3599)
                        lenght.Text = string.Format("{0}:{1}:{2:D2}", (int)MediaPlayer.Queue.ActiveSong.Duration.Hours, (int)MediaPlayer.Queue.ActiveSong.Duration.Minutes,
                            (int)MediaPlayer.Queue.ActiveSong.Duration.Seconds);
                    else
                    {
                        lenght.Text = string.Format("{0}:{1:D2}", (int)MediaPlayer.Queue.ActiveSong.Duration.Minutes,
                            (int)MediaPlayer.Queue.ActiveSong.Duration.Seconds);
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            

        }



        private void playPause_Click(object sender, RoutedEventArgs e)
        {
            if (playPause.IsChecked == true)
            {
                if (MediaPlayer.Queue.ActiveSong != null)
                    MediaPlayer.Resume();
                else
                    MediaPlayer.Play(songCollection);
            }
            else
            {
                MediaPlayer.Pause();
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.MoveNext();
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.MovePrevious();
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }


        private void slider_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
  
        }


        private void vol_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            vol.Foreground = audioBorder.BorderBrush;
        }
        

        private void vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                volumeValue.Text = Math.Ceiling(vol.Value).ToString();
                MediaPlayer.Volume = (float) (vol.Value);
                MediaPlayer.IsMuted = false;
                imageIsMutedNo.Visibility = System.Windows.Visibility.Visible;
                imageIsMutedYes.Visibility = System.Windows.Visibility.Collapsed;

                vol.Foreground = audioBorder.BorderBrush;
            }
            catch(NullReferenceException)
            {

            }
        }

        private void imageIsMutedNo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MediaPlayer.IsMuted = true;
            imageIsMutedNo.Visibility = System.Windows.Visibility.Collapsed;
            imageIsMutedYes.Visibility = System.Windows.Visibility.Visible;
            vol.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void imageIsMutedYes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MediaPlayer.IsMuted = false;
            imageIsMutedNo.Visibility = System.Windows.Visibility.Visible;
            imageIsMutedYes.Visibility = System.Windows.Visibility.Collapsed;
            vol.Foreground = audioBorder.BorderBrush;
        }



        private void player_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (AudioControls.Visibility == System.Windows.Visibility.Visible)
                hidecontrols.Begin();
            else
                showcontrols.Begin();
        }



        private void volumeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Audio.Visibility == System.Windows.Visibility.Collapsed)
                showAudio.Begin();
            else
                hideAudio.Begin();
        }
        

        private void songsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MediaPlayer.Play(songCollection, songsList.ItemsSource.IndexOf((Song)songsList.SelectedItem));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void goToNowPlaying_Click(object sender, RoutedEventArgs e)
        {
            all.SelectedIndex = 0;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            MediaPlayer.IsShuffled = true;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            MediaPlayer.IsShuffled = false;
        }

        private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
        {
            MediaPlayer.IsRepeating = true;
        }

        private void ToggleButton_Unchecked_1(object sender, RoutedEventArgs e)
        {
            MediaPlayer.IsRepeating = false;
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (Application.Current as App).selectedsong = MediaPlayer.Queue.ActiveSong;
                NavigationService.Navigate(new Uri("/Properties.xaml?From=player", UriKind.Relative));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Context Menu buttonsject sender, RoutedEventArgs e)
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Song g; Microsoft.Xna.Framework.Media.PhoneExtensions.SongMetadata n;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Properties.xaml?From=player", UriKind.Relative));
        }



        private void ContentPanel_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            if (Audio.Visibility == System.Windows.Visibility.Visible)
                hideAudio.Begin();
        }

        private void albumsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void RoundButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Play(albumCollection.Single(al => al.Name == (((sender as Button).Parent as Grid).Children[3] as TextBlock).Text).Songs);
            albumsList.SelectedItem = null;
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Point point = e.GetPosition(sender as Grid);

                if (point.X > 46 || point.Y > 63)
                    NavigationService.Navigate(new Uri(string.Format("/Player/Album.xaml?albumName={0}",
                        (((sender as Grid).Children[3]) as TextBlock).Text), UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                albumsList.SelectedItem = null;
            }
        }

        private void Item_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                VisualStateManager.GoToState(selectedMusic, "Normal", true);
                selectedMusic = (sender as UserControl);

                VisualStateManager.GoToState(sender as UserControl, "Selected", true);

                songsList.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void slider_Loaded(object sender, RoutedEventArgs e)
        {
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 0) as Rectangle).Height = 2;
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 1) as Rectangle).Height = 7;

            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).Width = (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).Height = 20;
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).RadiusX = 100;
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(slider, 0), 0), 2) as Rectangle).RadiusY = 100;
        }

        private void songsList_Loaded(object sender, RoutedEventArgs e)
        {
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(songsList, 0), 0), 1) as ScrollBar).Background = new SolidColorBrush(Colors.White);
            
        }

        private void all_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }

    public class ListPictureTemplate
    {
        public BitmapImage bi { get; set; }
        public string name { get; set; }
    }
}