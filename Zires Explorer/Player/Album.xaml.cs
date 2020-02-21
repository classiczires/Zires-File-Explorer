using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace Zires_Explorer.Player
{
    public partial class Album : PhoneApplicationPage
    {
        public Album()
        {
            InitializeComponent();
            songsOfAlbum.ItemsSource = songs;
        }

        ObservableCollection<Song> songs = new ObservableCollection<Song>();
        SongCollection songCollection;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                string _albumName = NavigationContext.QueryString["albumName"];

                MediaLibrary medialib = new MediaLibrary();
                AlbumCollection albumCollection = medialib.Albums;
                Microsoft.Xna.Framework.Media.Album album = albumCollection.Single(al => al.Name == _albumName);
                songCollection = album.Songs;



                if (album.HasArt)
                {
                    BitmapImage bmpImage = new BitmapImage();
                    bmpImage.SetSource(album.GetAlbumArt());
                    albumArt.Source = bmpImage;
                }


                albumName.Text = album.Name;
                AlbumArtist.Text = album.Artist.Name;
                if (album.Duration.Days > 0)
                {
                    albumDuration.Text = string.Format("Duration: {0:D1}:{1:D2}:{2:D2}:{3:D2}", album.Duration.Days
                        , album.Duration.Hours, album.Duration.Minutes, album.Duration.Minutes);
                }
                else
                {
                    albumDuration.Text = string.Format("Duration: {0:D2}:{1:D2}:{2:D2}", album.Duration.Hours, album.Duration.Minutes, album.Duration.Minutes);
                }
                songsNumbers.Text = "Songs numbers: " + album.Songs.Count;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void songsOfAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MediaPlayer.Play(songCollection, songsOfAlbum.ItemsSource.IndexOf((Song)songsOfAlbum.SelectedItem));
            NavigationService.GoBack();
        }

        private void AlbumPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void songsOfAlbum_Loaded(object sender, RoutedEventArgs e)
        {
            string _albumName = NavigationContext.QueryString["albumName"];

            MediaLibrary medialib = new MediaLibrary();
            AlbumCollection albumCollection = medialib.Albums;
            Microsoft.Xna.Framework.Media.Album album = albumCollection.Single(al => al.Name == _albumName);
            songCollection = album.Songs;


            songs.Clear();

            foreach (Song so in album.Songs)
            {
                songs.Add(so);
            }

            loadList.Begin();
        }

        private void loadList_Completed(object sender, EventArgs e)
        {

        }
    }
}