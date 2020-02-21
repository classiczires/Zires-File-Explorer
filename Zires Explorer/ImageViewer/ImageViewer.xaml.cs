using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone;
using System.IO;
using Windows.Storage;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Zires_Explorer.ImageViewer
{
    public partial class ImageViewer : PhoneApplicationPage
    {
        public ImageViewer()
        {
            InitializeComponent();
            Touch.FrameReported += Touch_FrameReported;
            bitmapImage.DownloadProgress += bitmapImage_DownloadProgress;
        }

        void bitmapImage_DownloadProgress(object sender, System.Windows.Media.Imaging.DownloadProgressEventArgs e)
        {
            try
            {
                SystemTray.IsVisible = true;

                SystemTray.SetProgressIndicator(imageViewer, new ProgressIndicator() { IsIndeterminate = true, Text = "Loading the Picture", Value = e.Progress });
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Point onePoint = new Point();
        Point towPoint = new Point();
        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            TouchPointCollection touches = e.GetTouchPoints(viewPort);

            if (touches.Count == 2)
            {
                if ((onePoint.X - e.GetTouchPoints(viewPort)[0].Position.X) < (towPoint.X - e.GetTouchPoints(viewPort)[1].Position.X))
                {
                    viewPort.Bounds = new Rect(0, 0, viewPort.Bounds.Width, viewPort.Bounds.Height + 10);
                    viewPort.Bounds = new Rect(0, 0, viewPort.Bounds.Width + 10, viewPort.Bounds.Height);
                }
                else if ((towPoint.X - onePoint.X) < (e.GetTouchPoints(viewPort)[1].Position.X - e.GetTouchPoints(viewPort)[0].Position.X))
                {
                    if (image.Height > 50)
                    {
                        viewPort.Bounds = new Rect(0, 0, viewPort.Bounds.Width, viewPort.Bounds.Height - 10);
                        viewPort.Bounds = new Rect(0, 0, viewPort.Bounds.Width - 10, viewPort.Bounds.Height);
                    }
                }

                onePoint = touches[0].Position;
                towPoint = touches[1].Position;
            }
        }

        System.Windows.Media.Imaging.BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            bitmapImage.UriSource = new Uri((Application.Current as App).path);
            /*
            StorageFile imagefile = await StorageFile.GetFileFromApplicationUriAsync(bitmapImage.UriSource);
            Windows.Storage.FileProperties.ImageProperties imageDetails = await imagefile.Properties.GetImagePropertiesAsync();
            


            button.Content = (Math.Round((double) (imageDetails.Width * imageDetails.Height) / 1000000)).ToString() + " MP | "
                + imageDetails.Width + " × " + imageDetails.Height;
            
             */


            button.Content = (Math.Round((double)(image.Width * image.Height) / 1000000)).ToString() + " MP | "
                + image.Width + " × " + image.Height;



            image.Source = bitmapImage;
        }


        private void image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (imageViewer.ApplicationBar.IsVisible == true)
            {
                imageViewer.ApplicationBar.IsVisible = false;
            }
            else
            {
                imageViewer.ApplicationBar.IsVisible = true;
            }
        }




        //AppBar Events
        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Windows.Phone.System.UserProfile.LockScreen.SetImageUri(new Uri((Application.Current as App).path, UriKind.Absolute));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplicationBarMenuItem_Click_2(object sender, EventArgs e)
        {

        }


        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            CustomMessageBox deleteMessageBox = new CustomMessageBox() { Caption = "Delete Picture?", Message = "This picture will be deleted.", LeftButtonContent = "Delete", RightButtonContent = "Cancel" };
            deleteMessageBox.Dismissed += deleteMessageBox_Dismissed;
            deleteMessageBox.Show();
        }

        void deleteMessageBox_Dismissed(object sender, DismissedEventArgs e)
        {
             if (e.Result == CustomMessageBoxResult.LeftButton)
             {
                 File.Delete((Application.Current as App).path);
             }
        }

        private void imageViewer_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bitmapImage= null;
            image.Source = null;
        }
    }
}