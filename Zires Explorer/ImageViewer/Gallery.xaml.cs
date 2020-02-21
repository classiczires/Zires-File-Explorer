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

namespace Zires_Explorer.ImageViewer
{
    public partial class Gallery : PhoneApplicationPage
    {
        ObservableCollection<allListItemTemplete> thumbnailCollection = new ObservableCollection<allListItemTemplete>();
        MediaLibrary mediaLib = new MediaLibrary();
        PictureCollection pictures;



        public Gallery()
        {
            InitializeComponent();
            pictures = mediaLib.Pictures;

            foreach(Microsoft.Xna.Framework.Media.Picture picture in pictures)
            {
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.SetSource(picture.GetThumbnail());

                thumbnailCollection.Add(new allListItemTemplete() { name = picture.Name, source = bmpImage });
            }
            allList.ItemsSource = thumbnailCollection;
        }

        private void allList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("");
        }
    }

    public class allListItemTemplete
    {
        public BitmapSource source { get; set; }
        public string name { get; set; }
    }
}