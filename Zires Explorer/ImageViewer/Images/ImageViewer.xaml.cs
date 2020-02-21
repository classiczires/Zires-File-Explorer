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


namespace Zires_Explorer.ImageViewer
{
    public partial class ImageViewer : PhoneApplicationPage
    {
        public ImageViewer()
        {
            InitializeComponent();
        }

        System.Windows.Media.Imaging.BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
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

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {

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
                MessageBox.Show(ex.GetBaseException().Message);
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
                 NavigationService.GoBack();
             }
        }
    }
}