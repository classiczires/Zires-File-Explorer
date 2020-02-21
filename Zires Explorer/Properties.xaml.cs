using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Zires_Explorer
{
    public partial class Properties : PhoneApplicationPage
    {
        private string source;
        private string itemType;
        private bool load_Data = false;
        delegate void myDelegate();

        FileInfo fileInfo;
        DirectoryInfo directoryInfo;

        public Properties()
        {
            InitializeComponent();
        }

      


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("From"))
            {
                switch (NavigationContext.QueryString["From"])
                {
                    case "explorer":
                        source = (Application.Current as App).path;
                        itemType = (Application.Current as App).type;
                        icon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri((Application.Current as App).iconSource, UriKind.Relative));

                        if (itemType == "File folder")
                        {
                            directoryInfo = new DirectoryInfo(source);

                            myDelegate delegate_1 = new myDelegate(this.load_FolderProperties);
                            delegate_1.Invoke();
                        }
                        else
                        {
                            fileInfo = new FileInfo(source);

                            myDelegate delegate_2 = new myDelegate(this.load_FileProperties);
                            delegate_2.Invoke();
                        }
                        break;
                    case "player":
                        pivot.SelectedIndex = 1;
                        pivot.IsLocked = true;
                        myDelegate delegate_3 = new myDelegate(this.loadSongDetails);
                        delegate_3.Invoke();
                        break;
                }
                NavigationContext.QueryString.Clear();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void pathTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            location.SelectAll();
        }


        public void load_FolderProperties()
        {
            name.Text = directoryInfo.Name;
            type.Text = itemType;
            location.Text = directoryInfo.FullName;
            create.Text = directoryInfo.CreationTime.ToString();
            countFile_Folder();


            switch (directoryInfo.Attributes)
            {
                case FileAttributes.Archive:
                    archive.IsChecked = true;
                    break;
                case FileAttributes.Hidden:
                    hidden.IsChecked = true;
                    break;
                case FileAttributes.ReadOnly:
                    read_only.IsChecked = true;
                    break;
            }


            long DirectorySize = CalculateDirectorySize(directoryInfo, true);

            if (DirectorySize < 1024)
                size.Text = DirectorySize + " byte";
            else if (DirectorySize >= 1024 && DirectorySize < 1048576)
                size.Text = DirectorySize / 1024 + " KB";
            else if (DirectorySize >= 1048576 && DirectorySize < 1073741824)
                size.Text = Math.Round((double)(DirectorySize / 1048576.0), 2) + " MB";
            else if (DirectorySize >= 1073741824)
                size.Text = Math.Round((double)(DirectorySize / 1099511627776), 2) + " GB";
        }





        private void countFile_Folder()
        {
            contines.Text = Directory.GetFiles(source).Count() + " Files, ";
            contines.Text += Directory.GetDirectories(source).Count() + " Folders";
        }


        long totalSize = 0;
        public long CalculateDirectorySize(DirectoryInfo directory, bool includeSubdirectories)
        { 
            try
            {
                FileInfo[] files = directory.GetFiles();

                foreach (FileInfo file in files)
                {

                    totalSize += file.Length;

                }


                if (includeSubdirectories)
                {
                    DirectoryInfo[] dirs = directory.GetDirectories();

                    foreach (DirectoryInfo dir in dirs)
                    {
                        totalSize += CalculateDirectorySize(dir, true);
                    }
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                MessageBox.Show(unauthorizedAccessException.Message);
            } 
            return totalSize;
        }



        public void load_FileProperties()
        {
            name.Text = fileInfo.Name;
            type.Text = itemType;
            location.Text = fileInfo.FullName;
            create.Text = fileInfo.CreationTime.ToString();


            if (fileInfo.Length < 1024)
            {
                size.Text = fileInfo.Length + " byte";
            }
            else if (fileInfo.Length >= 1024 && fileInfo.Length < 1048576)
            {
                size.Text = fileInfo.Length / 1024 + " KB";
            }
            else if (fileInfo.Length >= 1048576)
            {
                size.Text = Math.Round(fileInfo.Length / 1048576.0, 2) + " MB";
            }
            
            switch(fileInfo.Attributes)
            {
                case FileAttributes.Archive:
                    archive.IsChecked = true;
                    break;
                case FileAttributes.Hidden:
                    hidden.IsChecked = true;
                    break;
                case FileAttributes.ReadOnly:
                    read_only.IsChecked = true;
                    break;
            }

        }



        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }


        private void loadSongDetails()
        {
            Microsoft.Xna.Framework.Media.Song song = (Application.Current as App).selectedsong;

            songTitle.Text = song.Name;
            albumArtist.Text = song.Album.Artist.Name;

            if (song.Album.HasArt)
            {
                BitmapImage albumArtSource = new BitmapImage();
                albumArtSource.SetSource(song.Album.GetAlbumArt());
                albumArt.Source = albumArtSource;
            }
            else
            {
                remove.Visibility = System.Windows.Visibility.Collapsed;
                changeOrSet.Content = "Set";
            }

            songDuration.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", song.Duration.Hours, song.Duration.Minutes, song.Duration.Seconds);
            albumDuration.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", song.Album.Duration.Hours, song.Album.Duration.Minutes, song.Album.Duration.Seconds);
            album.Text = song.Album.Name;
            artist.Text = song.Artist.Name;
            genre.Text = song.Genre.Name;
            track.Text = song.TrackNumber.ToString();

            song.Dispose();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void changeOrSet_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.PhotoChooserTask phcht = new Microsoft.Phone.Tasks.PhotoChooserTask();
            phcht.PixelHeight = 512;
            phcht.PixelWidth = 512;
            
            phcht.Show();
            phcht.Completed += new EventHandler<Microsoft.Phone.Tasks.PhotoResult>(phcht_Completed);
        }

        void phcht_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult e)
        {
            try
            {
                if (e.TaskResult == Microsoft.Phone.Tasks.TaskResult.OK && e.ChosenPhoto != null)
                {
                    albumArt.Source = null;
                    albumArt.Source = Microsoft.Phone.PictureDecoder.DecodeJpeg(e.ChosenPhoto);
                    e.ChosenPhoto.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            Microsoft.Xna.Framework.Media.PhoneExtensions.SongMetadata songMetadata = new Microsoft.Xna.Framework.Media.PhoneExtensions.SongMetadata();
            
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void PivotItem_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}