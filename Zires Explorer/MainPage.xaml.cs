using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Zires_Explorer.Resources;
using Zires_Explorer.ViewModels;
using System.Windows.Media;
using System.Windows.Threading;
using Windows.Storage;
using System.IO;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Phone.Info;
using Microsoft.Phone.BackgroundAudio;
using Zires_Explorer.Player;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Windows.Phone.Speech.Synthesis;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Storage;
using Microsoft.Phone.Tasks;
using System.Collections.ObjectModel;
using Windows.Phone.Management.Deployment;
using Windows.ApplicationModel;
using Windows.Phone.System.UserProfile;
using Coding4Fun.Phone.Controls;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;


namespace Zires_Explorer
{
    public partial class MainPage : PhoneApplicationPage
    {
        MainViewModel mainViewModel = new MainViewModel();
        MainViewModel mainViewModel_2 = new MainViewModel();
        ItemViewModel itemViewModel = new ItemViewModel();
        HomeItems homeItems = new HomeItems();
        SelextionList selection = new SelextionList();

        ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>();

        ProgressIndicator progressIndicator = new ProgressIndicator();

        // Constructor

        public MainPage()
        {
            try
            {
                InitializeComponent();
                SystemTray.SetProgressIndicator(this, progressIndicator);

                this.list.ItemsSource = mainViewModel;
                this.list_2.ItemsSource = mainViewModel_2;
                this.selectionList.ItemsSource = selection;
                this.start.ItemsSource = homeItems;
                // Sample code to localize the ApplicationBar
                //BuildLocalizedApplicationBar();
                numbersOfItem.Text = (mainViewModel.Count).ToString() + " items";
                currentPath = "D:\\";
                mainViewModel.storages();
                list.DataContext = mainViewModel;
                list_2.DataContext = mainViewModel;
                Menu.ItemsSource = menuItems;

                Close_HoldMenu.Completed += Close_HoldMenu_Completed;

                //Set Main Menu Position
                MainMenu.Margin = new Thickness(App.Current.Host.Content.ActualHeight, 0, -App.Current.Host.Content.ActualHeight, 0);
                easingDoubleKeyFrame_1.Value = -App.Current.Host.Content.ActualHeight;
                easingDoubleKeyFrame_2.Value = -App.Current.Host.Content.ActualHeight;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        int selectedIndex = 0;
        public string currentPath, selectedPath;
        string selectedButtonInHoldMenu;



        private void menu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Close_MainMenu.Begin();
        }



        private void search_Click(object sender, RoutedEventArgs e)
        {
            Open_Start.Begin();
        }


        private void new_folder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox nameOfNewItem = new TextBox();
                nameOfNewItem.TextChanged += nameOfNewItem_TextChanged;
                CustomMessageBox newFolderMessageBox = new CustomMessageBox() { Caption = "Create New Folder", LeftButtonContent = "Create", RightButtonContent = "Cancel" };

                nameOfNewItem.Text = "New Folder";
                int number = 2;
                while (Directory.Exists(mainViewModel.currentDirectory + "\\" + nameOfNewItem.Text))
                {
                    nameOfNewItem.Text = "New Folder " + "(" + number + ")";
                    number += 1;
                }

                newFolderMessageBox.Content = nameOfNewItem;
                newFolderMessageBox.Show();
                nameOfNewItem.Focus();
                nameOfNewItem.SelectAll();
                newFolderMessageBox.Dismissed += newFolderMessageBox_Dismissed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void nameOfNewItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                char[] characrers = (sender as TextBox).Text.ToCharArray();


                    for (int indexCounter = 0; indexCounter < characrers.Length; indexCounter++)
                    {
                        if (characrers[indexCounter] == '\\' || characrers[indexCounter] == '/' || characrers[indexCounter] == ':' || characrers[indexCounter] == '*' 
                            || characrers[indexCounter] == '?'
                            || characrers[indexCounter] == '"' || characrers[indexCounter] == '<' || characrers[indexCounter] == '>')
                        {
                            (sender as TextBox).Text.Remove(indexCounter, 1);
                            ((sender as TextBox).Parent as CustomMessageBox).Message = "A file name can't contine any of the following characters:\n\t\t\t\t\t" + string.Format(@"\ / : * ? {0} < > |", '"');
                        }
                        else
                        {
                            ((sender as TextBox).Parent as CustomMessageBox).Message = "";
                        }
                    }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void newFolderMessageBox_Dismissed(object sender, DismissedEventArgs e)
        {
            try
            {
                if (e.Result == CustomMessageBoxResult.LeftButton)
                {
                    if (Directory.Exists(mainViewModel.currentDirectory + "\\" + ((sender as CustomMessageBox).Content as TextBox).Text))
                    {
                        MessageBox.Show("Directory with this name already exist please change name of the folder.");
                    }
                    else
                    {
                        Directory.CreateDirectory(mainViewModel.currentDirectory + "\\" + ((sender as CustomMessageBox).Content as TextBox).Text);

                        mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, false);

                        if ((Application.Current as App).isGrid)
                            list.ScrollTo(mainViewModel.Single(item => item.Size == mainViewModel.currentDirectory + "\\" + ((sender as CustomMessageBox).Content as TextBox).Text));
                        else
                            list.ScrollTo(mainViewModel.Single(item => item.Path == mainViewModel.currentDirectory + "\\" + ((sender as CustomMessageBox).Content as TextBox).Text));

                        numbersOfItem.Text = mainViewModel.Count + "items";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cut_Click(object sender, RoutedEventArgs e)
        {
            selectedItem.Text = Math.Round(DeviceStatus.ApplicationCurrentMemoryUsage / 1048576.0, 2) + "  " + Math.Round(DeviceStatus.DeviceTotalMemory / 1048576.0, 2);
        }

        private void copy_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Package> packages = Windows.Phone.Management.Deployment.InstallationManager.FindPackages();
            Package theNeed = packages.Single(item => item.Id.Name == "ex1");
            theNeed.Launch("/MainPage.xaml");
        }

        private void paste_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Application.Current as App).isCut == true)
                {
                    if ((Application.Current as App).typeClipboard == "File folder")
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo((Application.Current as App).onePathClipboard);

                        Directory.Move((Application.Current as App).onePathClipboard, (Application.Current as App).path + "\\" + directoryInfo.Name);
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo((Application.Current as App).onePathClipboard);
                        File.Move((Application.Current as App).onePathClipboard, (Application.Current as App).path + "\\" + fileInfo.Name);
                    }
                }
                else
                {
                    if ((Application.Current as App).typeClipboard == "File folder")
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo((Application.Current as App).onePathClipboard);
                        Directory.CreateDirectory((Application.Current as App).path + "\\" + directoryInfo.Name);

                        foreach (FileInfo fi in directoryInfo.GetFiles())
                        {
                            fi.CopyTo((Application.Current as App).path + "\\" + directoryInfo.Name);
                        }
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo((Application.Current as App).onePathClipboard);
                        File.Copy((Application.Current as App).onePathClipboard, (Application.Current as App).path + "\\" + fileInfo.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectionList.SelectedItems.Count > 0)
            {
                CustomMessageBox multipleItemDeleteMessageBox = new CustomMessageBox() { Caption = "Delete Multiple Items", Message = "Are you sure you want to delete these " + selectionList.SelectedItems.Count + " items?", LeftButtonContent = "Delete", RightButtonContent = "Cancel" };
                multipleItemDeleteMessageBox.Dismissed += multiItemDeleteMessageBox_Dismissed;
                multipleItemDeleteMessageBox.Show();
            }

        }

        void multiItemDeleteMessageBox_Dismissed(object sender, DismissedEventArgs e)
        {
            try
            {
                foreach (ItemViewModel ivm in selectionList.SelectedItems)
                {
                    if (ivm.Type == "File folder")
                    {
                        Directory.Delete(ivm.Path, true);
                    }
                    else
                    {
                        File.Delete(ivm.Path);
                    }
                }
            }
            catch (UnauthorizedAccessException uex)
            {
                MessageBox.Show(uex.Message, "Can't delete", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can't delete", MessageBoxButton.OK);
            }



            Size size = new Size();
            if ((Application.Current as App).isGrid == true)
            {
                list.LayoutMode = LongListSelectorLayoutMode.Grid;
                size.Width = 116;
                size.Height = 146;
                list.GridCellSize = size;
            }
            else
            {
                list.GridCellSize = Size.Empty;
                list.LayoutMode = LongListSelectorLayoutMode.List;
            }

            selectionList.Visibility = System.Windows.Visibility.Collapsed;
            mainViewModel.Clear();
            selection.Clear();
            list.Visibility = System.Windows.Visibility.Visible;

            mainViewModel.Clear();

            mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, false);

            if (mainViewModel.Count > 0)
                list.ScrollTo(mainViewModel[0]);

            selectedItem.Text = "0 item selected";
        }




        ExternalStorageDevice externalStorageDevice;
        public async void readHeader()
        {
            ExternalStorageFile externalStorageFile = await externalStorageDevice.GetFileAsync(itemViewModel.Path);
            Stream memoryStream = await externalStorageFile.OpenForReadAsync();
            int mems = memoryStream.ReadByte();
            selectedItem.Text = mems.ToString();
        }

        int sss = 0;

        public async void readHeaderInternal()
        {
            byte[] data = new byte[401];


            StorageFile sf = await StorageFile.GetFileFromPathAsync(itemViewModel.Path);

            Stream stream = await sf.OpenStreamForReadAsync();
            stream.Seek(sss, SeekOrigin.End);
            stream.Read(data, 0, 400);
            asd.Text = "";

            for (int c = 0; c <= 400; c++)
            {
                asd.Text += "  " + Convert.ToChar(data[c]); /*System.Text.Encoding.UTF8.GetString(data,0,400);*/
            }

        }

        private void rename_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Thread t1 = new Thread(new ThreadStart(swq));
                t1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void swq()
        {
            try
            {
                MessageBox.Show("Hello");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selectall_Click(object sender, RoutedEventArgs e)
        {
            selectionList.SelectedItems.Clear();

            list.Visibility = System.Windows.Visibility.Collapsed;

            selection.Clear();

            Size size = new Size();
            if ((Application.Current as App).isGrid == true)
            {
                selectionList.LayoutMode = LongListSelectorLayoutMode.Grid;
                size.Width = 116;
                size.Height = 146;
                selectionList.GridCellSize = size;
            }
            else
            {
                selectionList.GridCellSize = Size.Empty;
                selectionList.LayoutMode = LongListSelectorLayoutMode.List;
            }


            for (int count = 0; count < mainViewModel.Count; count++)
            {
                selection.Add(mainViewModel[count]);
            }

            mainViewModel.Clear();
            selectionList.Visibility = System.Windows.Visibility.Visible;
        }

        private void selectnone_Click(object sender, RoutedEventArgs e)
        {
            selectionList.IsSelectionEnabled = false;
            mainViewModel.Clear();
            list.SelectedItem = null;

            selectionList.Visibility = System.Windows.Visibility.Collapsed;

            for (int count = 0; count < selection.Count; count++)
            {
                mainViewModel.Add(selection[count]);
            }
            selectionList.SelectedItems.Clear();
            selection.Clear();
            list.Visibility = System.Windows.Visibility.Visible;
        }


        private void Thumbnails_view_Click(object sender, RoutedEventArgs e)
        {
            Thumbnails_view.Visibility = System.Windows.Visibility.Collapsed;
            Details_view.Visibility = System.Windows.Visibility.Visible;

            Size size = new Size();
            list.LayoutMode = LongListSelectorLayoutMode.Grid;
            size.Width = 116;
            size.Height = 155;
            list.GridCellSize = size;

            (Application.Current as App).isGrid = true;
            mainViewModel.Clear();
            mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, false);
        }

        private void Details_view_Click(object sender, RoutedEventArgs e)
        {
            Details_view.Visibility = System.Windows.Visibility.Collapsed;
            Thumbnails_view.Visibility = System.Windows.Visibility.Visible;

            list.GridCellSize = Size.Empty;
            list.LayoutMode = LongListSelectorLayoutMode.List;

            (Application.Current as App).isGrid = false;
            mainViewModel.Clear();
            mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, false);
        }



        private void properties_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pintostart_Click(object sender, RoutedEventArgs e)
        {
            itemViewModel = (ItemViewModel)list.SelectedItem;
            homeItems.Add(itemViewModel);
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.Clear();
            mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, false);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MBresult = MessageBox.Show("Do you want exit now?", "Exit", MessageBoxButton.OKCancel);
            if (MBresult == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (planeProjectionMainMenu.GlobalOffsetX == -(App.Current.Host.Content.ActualHeight))
                Close_MainMenu.Begin();
            else if (openWithList.Visibility == System.Windows.Visibility.Visible)
                Close_OpenWithAnimation.Begin();
            else if (menuControl.Visibility == System.Windows.Visibility.Visible)
                Close_HoldMenu.Begin();
            else 
            {
                if (seperatorControler.IsChecked == false)
                {
                    if (mainViewModel.start != 1)
                    {
                        if (mainViewModel.currentDirectory != "D:\\" && mainViewModel.currentDirectory != "C:\\")
                        {
                            mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, true);
                            if (mainViewModel.Count > 0)
                                list.ScrollTo(mainViewModel[0]);
                        }
                        else
                        {
                            mainViewModel.start = 1;
                            mainViewModel.storages();
                        }
                        numbersOfItem.Text = (mainViewModel.Count()).ToString() + " items";
                        pathPanel.Text = mainViewModel.currentDirectory;
                    }
                    else
                    {
                        CustomMessageBox cmb = new CustomMessageBox() { Caption = "Exit now?", LeftButtonContent = "Yes", RightButtonContent = "No"};
                        cmb.Dismissed += cmb_Dismissed;
                        cmb.Show();
                        mainViewModel.start = 0;
                    }
                }
                else
                {
                    if (mainViewModel_2.start != 1)
                    {
                        if (mainViewModel_2.currentDirectory != "D:\\" && mainViewModel_2.currentDirectory != "C:\\")
                        {
                            mainViewModel_2.load_Data(mainViewModel_2.currentDirectory, (Application.Current as App).isGrid, true);
                            if (mainViewModel_2.Count > 0)
                                list.ScrollTo(mainViewModel_2[0]);
                        }
                        else
                        {
                            mainViewModel_2.start = 1;
                            mainViewModel_2.storages();
                        }
                        numbersOfItem.Text = (mainViewModel.Count()).ToString() + " items";
                        pathPanel_2.Text = mainViewModel.currentDirectory;
                    }
                    else
                    {
                        CustomMessageBox cmb = new CustomMessageBox() { Caption = "Exit now?", LeftButtonContent = "Yes", RightButtonContent = "No" };
                        cmb.Dismissed += cmb_Dismissed;
                        cmb.Show();
                        mainViewModel_2.start = 0;
                    }
                }
            }

            e.Cancel = true;
        }

        void cmb_Dismissed(object sender, DismissedEventArgs e)
        {
            if (e.Result == CustomMessageBoxResult.LeftButton)
            {
                Application.Current.Terminate();
            }
        }



        private async void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                progressIndicator.Text = "Loading...";
                progressIndicator.IsVisible = true;
                progressIndicator.IsIndeterminate = true;


                itemViewModel = (ItemViewModel)list.SelectedItem;
                list.SelectedItem = null;

                if ((Application.Current as App).isGrid)
                {
                    selectedPath = itemViewModel.Size;
                    (Application.Current as App).itemName = itemViewModel.Path;
                }
                else
                {
                    selectedPath = itemViewModel.Path;
                    (Application.Current as App).itemName = itemViewModel.Name;
                }

                (Application.Current as App).path = selectedPath;
                (Application.Current as App).type = itemViewModel.Type;


                if (itemViewModel.Type == "File folder")
                {
                    mainViewModel.load_Data(selectedPath, (Application.Current as App).isGrid, false);
                }
                else if (itemViewModel.Type == "")
                {
                    mainViewModel.load_Data(selectedPath, (Application.Current as App).isGrid, false);
                }
                else if (itemViewModel.Type == "File folder (.zip)")
                {
                    mainViewModel.load_Data(selectedPath, (Application.Current as App).isGrid, false);
                }
                else
                {
                    switch (itemViewModel.Type.ToLower())
                    {
                        case "sound (.mp3)":
                        case "sound (.wav)":
                        case "sound (.ac3)":
                        case "sound (.wma)":
                        case "sound (.aac)":
                            Open_OpenWithAnimation.Begin();
                            break;
                        case "video (.mp4)":
                        case "video (.3gp)":
                        case "video (.avi)":
                        case "video (.mkv)":
                        case "video (.ts)":
                        case "video (.mpg)":
                        case "video (.wmv)":
                        case "video (.vob)":
                            NavigationService.Navigate(new Uri("/Player/VideoPlayer.xaml", UriKind.Relative));
                            break;
                        case "image (.jpg)": /*NavigationService.Navigate(new Uri("/ImageViewer/ImageViewer.xaml", UriKind.Relative));*/
                        case "image (.png)":
                        case "image (.ico)":
                        case "image (.raw)":
                        case "document (.txt)":
                        case "document (.docx)":
                        case "document (.dotx)":
                        case "microsoft excel (.xlsx)":
                        case "microsoft excel (.xlsm)":
                        case "microsoft excel (.xls)":
                        case "microsoft excel (.xltx)":
                        case "microsoft excel (.xlt)":
                        case "pdf file (.pdf)":
                        case "html document (.html)":
                            await Windows.System.Launcher.LaunchFileAsync(await StorageFile.GetFileFromPathAsync((Application.Current as App).path));
                            break;
                    }
                }
                progressIndicator.Text = null;
                progressIndicator.IsVisible = false;
                progressIndicator.IsIndeterminate = false;

                numbersOfItem.Text = (mainViewModel.Count()).ToString() + " items";
                pathPanel.Text = mainViewModel.currentDirectory;

            }
        }




        private void selectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numbersOfItem.Text = (selection.Count).ToString() + " items";
            selectedItem.Text = selectionList.SelectedItems.Count.ToString() + " item selected";
        }






        bool canContinue = true;
        Point point = new Point();
        double x, y;

        private void Grid_ManipulationStarted_1(object sender, ManipulationStartedEventArgs e)
        {
            if (menuControl.Visibility == System.Windows.Visibility.Visible)
                menuControl.Visibility = System.Windows.Visibility.Collapsed;

            x = e.ManipulationOrigin.X;
            y = e.ManipulationOrigin.Y;
            point = e.ManipulationOrigin;
        }

        private void Grid_ManipulationDelta_1(object sender, ManipulationDeltaEventArgs e)
        {
            if (canContinue == true)
            {
                if (e.ManipulationOrigin.Y < y - 30 || e.ManipulationOrigin.Y > y + 30)
                    canContinue = false;
            }
        }

        private void Grid_ManipulationCompleted_1(object sender, ManipulationCompletedEventArgs e)
        {
            try
            {
                if (MainMenu.Margin.Left > 0)
                {
                    if (canContinue == true)
                    {
                        if (e.ManipulationOrigin.X < x - 20)
                        {
                            Open_MainMenu.Begin();
                        }
                    }
                }
                canContinue = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Grid_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {

        }

        private void Grid_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {

        }

        private void Grid_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

        }


        //OpenWithList Events:
        private void ziresPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            Close_OpenWithAnimation.Begin();

            (Application.Current as App).path = selectedPath;
            NavigationService.Navigate(new Uri("/Player/Player.xaml", UriKind.Relative));
        }

        private void phonePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            Close_OpenWithAnimation.Begin();

            MediaPlayerLauncher mediaplayerluncher = new MediaPlayerLauncher();
            mediaplayerluncher.Media = new Uri(selectedPath, UriKind.Relative);
            mediaplayerluncher.Show();
        }

        private void playInBackground_Click(object sender, RoutedEventArgs e)
        {
            Close_OpenWithAnimation.Begin();

            Uri fileUri = new Uri(selectedPath, UriKind.Absolute);
            BackgroundAudioPlayer.Instance.Track = new AudioTrack(fileUri, itemViewModel.Name, "Artist", null, null);
            BackgroundAudioPlayer.Instance.Play();
        }

        private void openWithClose_Click(object sender, RoutedEventArgs e)
        {
            Close_OpenWithAnimation.Begin();
        }






        private void list_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (list.LayoutMode == LongListSelectorLayoutMode.Grid)
            {
                (Application.Current as App).isGrid = true;
            }
            else
            {
                (Application.Current as App).isGrid = false;
            }

        }


        private void settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }




        private void Zires_Loaded(object sender, RoutedEventArgs e)
        {

            Size size = new Size();
            if ((Application.Current as App).isGrid == true)
            {
                list.LayoutMode = LongListSelectorLayoutMode.Grid;
                size.Width = 116;
                size.Height = 155;
                list.GridCellSize = size;
            }
            else
            {
                list.GridCellSize = Size.Empty;
                list.LayoutMode = LongListSelectorLayoutMode.List;
            }
        }



        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {

        }



        //Menu Actions
        private void Item_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            itemViewModel = (ItemViewModel)mainViewModel[int.Parse((sender as StackPanel).Tag.ToString())];
            selectedIndex = int.Parse((sender as StackPanel).Tag.ToString());

            menuItems.Clear();

            if ((Application.Current as App).isGrid)
            {
                selectedPath = itemViewModel.Size;
                (Application.Current as App).itemName = itemViewModel.Path;
            }
            else
            {
                selectedPath = itemViewModel.Path;
                (Application.Current as App).itemName = itemViewModel.Name;
            }


            (Application.Current as App).path = selectedPath;
            (Application.Current as App).type = itemViewModel.Type;
            (Application.Current as App).iconSource = itemViewModel.Source;

            switch (itemViewModel.Type.ToLower())
            {
                case "sound (.mp3)":
                case "sound (.amr)":
                case "sound (.wav)":
                case "sound (.ac3)":
                case "sound (.wma)":
                case "sound (.aac)":
                case "video (.mp4)":
                case "video (.3gp)":
                case "video (.avi)":
                case "video (.mkv)":
                case "video (.ts)":
                case "video (.mpg)":
                case "video (.wmv)":
                case "video (.vob)":
                    menuItems.Add(new MenuItems() { itemContent = "Open with" });
                    menuItems.Add(new MenuItems() { itemContent = "Share", icon = "/Assets/HoldMenuIcons/Share.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Cut", icon = "/Assets/HoldMenuIcons/Cut.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Copy", icon = "/Assets/HoldMenuIcons/Copy.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Delete", icon = "/Assets/HoldMenuIcons/Delete.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Rename" });
                    menuItems.Add(new MenuItems() { itemContent = "Pin to Start", icon = "/Assets/HoldMenuIcons/Pin.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Properties", icon = "/Assets/HoldMenuIcons/Info.png" });

                    break;
                case "image (.jpg)":
                case "image (.png)":
                case "image (.bmp)":
                case "image (.gif)":
                case "image (.jpeg)":
                    menuItems.Add(new MenuItems() { itemContent = "Share", icon = "/Assets/HoldMenuIcons/Share.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Cut", icon = "/Assets/HoldMenuIcons/Cut.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Copy", icon = "/Assets/HoldMenuIcons/Copy.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Delete", icon = "/Assets/HoldMenuIcons/Delete.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Rename" });
                    menuItems.Add(new MenuItems() { itemContent = "Pin to Start", icon = "/Assets/HoldMenuIcons/Pin.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Properties", icon = "/Assets/HoldMenuIcons/Info.png" });
                    break;
                case "file folder":
                case "file folder (.zip)":
                    menuItems.Add(new MenuItems() { itemContent = "Share", icon = "/Assets/HoldMenuIcons/Share.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Cut", icon = "/Assets/HoldMenuIcons/Cut.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Copy", icon = "/Assets/HoldMenuIcons/Copy.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Paste" });
                    menuItems.Add(new MenuItems() { itemContent = "Delete", icon = "/Assets/HoldMenuIcons/Delete.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Rename" });
                    menuItems.Add(new MenuItems() { itemContent = "Pin to Start", icon = "/Assets/HoldMenuIcons/Pin.png" });
                    menuItems.Add(new MenuItems() { itemContent = "Properties", icon = "/Assets/HoldMenuIcons/Info.png" });
                    break;

            }

            if (menuItems.Count == 0)
            {
                menuItems.Add(new MenuItems() { itemContent = "Share", icon = "/Assets/HoldMenuIcons/Share.png" });
                menuItems.Add(new MenuItems() { itemContent = "Cut", icon = "/Assets/HoldMenuIcons/Cut.png" });
                menuItems.Add(new MenuItems() { itemContent = "Copy", icon = "/Assets/HoldMenuIcons/Copy.png" });
                menuItems.Add(new MenuItems() { itemContent = "Delete", icon = "/Assets/HoldMenuIcons/Delete.png" });
                menuItems.Add(new MenuItems() { itemContent = "Rename" });
                menuItems.Add(new MenuItems() { itemContent = "Pin to Start", icon = "/Assets/HoldMenuIcons/Pin.png" });
                menuItems.Add(new MenuItems() { itemContent = "Properties", icon = "/Assets/HoldMenuIcons/Info.png" });
            }
            menuControl.Height = (menuItems.Count * 65) + 10;



            Point touched = e.GetPosition(Zires);

            if (touched.X > 250)
                touched.X = 250 - (480 - touched.X);

            if (768 < ((menuItems.Count * 65) + 10) + touched.Y)
            {
                if (touched.Y >= ((menuItems.Count * 65) + 10))
                {
                    touched.Y = (768 - ((menuItems.Count * 65) + 10)) - (768 - touched.Y);
                }
                else
                {
                    touched.Y = 0;
                }
            }


            planeprojectionHoldMenu.GlobalOffsetX = touched.X;
            planeprojectionHoldMenu.GlobalOffsetY = touched.Y;

            Open_HoldMenu.Begin();
        }




        private void Item_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {

        }

        private void Item_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

        }

        private void Item_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {

        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Close_HoldMenu.Begin();
        }

        private void ContentPanel_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            if (menuControl.Visibility == System.Windows.Visibility.Visible)
                menuControl.Visibility = System.Windows.Visibility.Collapsed;
        }





        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            selectedButtonInHoldMenu = (sender as Button).Content.ToString();
        }

        void Close_HoldMenu_Completed(object sender, EventArgs e)
        {
            try
            {
                string title, kind;

                if ((Application.Current as App).type == "File folder")
                {
                    title = "Folder";
                    kind = "folder";
                }
                else if ((Application.Current as App).type == "File folder (.zip)")
                {
                    title = "Folder";
                    kind = "folder";
                }
                else
                {
                    title = "File";
                    kind = "file";
                }


                switch (selectedButtonInHoldMenu)
                {
                    case "Open with":
                        Open_OpenWithAnimation.Begin();
                        break;
                    case "Share":
                        try
                        {
                            ShareMediaTask share = new ShareMediaTask();
                            share.FilePath = (Application.Current as App).path;
                            share.Show();
                        }
                        catch (Exception ex)
                        {
                            if ((uint)ex.HResult == 0x8007048F)
                            {
                                var result = MessageBox.Show("Bluetooth is turned off, do you want to turn on bluetooth?", "Bluetooth Off", MessageBoxButton.OKCancel);
                                if (result == MessageBoxResult.OK)
                                {
                                    ShowBluetoothcControlPanel();
                                }
                            }
                            else
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                    case "Cut":
                        (Application.Current as App).onePathClipboard = (Application.Current as App).path;
                        (Application.Current as App).isCut = true;

                        if (kind == "folder")
                            (Application.Current as App).typeClipboard = "File folder";
                        else
                            (Application.Current as App).typeClipboard = "File";
                        break;
                    case "Copy":
                        (Application.Current as App).onePathClipboard = (Application.Current as App).path;
                        (Application.Current as App).isCut = false;
                        break;
                    case "Paste":
                        if ((Application.Current as App).isCut == true)
                        {
                            if ((Application.Current as App).typeClipboard == "File folder")
                            {
                                DirectoryInfo directoryInfo = new DirectoryInfo((Application.Current as App).onePathClipboard);

                                Directory.Move((Application.Current as App).onePathClipboard, (Application.Current as App).path + "\\" + directoryInfo.Name);
                            }
                            else
                            {
                                FileInfo fileInfo = new FileInfo((Application.Current as App).onePathClipboard);
                                File.Move((Application.Current as App).onePathClipboard, (Application.Current as App).path + "\\" + fileInfo.Name);
                            }
                        }
                        else
                        {
                            if ((Application.Current as App).typeClipboard == "File folder")
                            {
                                DirectoryInfo directoryInfo = new DirectoryInfo((Application.Current as App).onePathClipboard);
                                Directory.CreateDirectory((Application.Current as App).path + "\\" + directoryInfo.Name);

                                foreach (FileInfo fi in directoryInfo.GetFiles())
                                {
                                    fi.CopyTo((Application.Current as App).path + "\\" + directoryInfo.Name);
                                }
                            }
                            else
                            {
                                FileInfo fileInfo = new FileInfo((Application.Current as App).onePathClipboard);
                                File.Copy((Application.Current as App).onePathClipboard, (Application.Current as App).path + "\\" + fileInfo.Name);
                            }
                        }
                        break;
                    case "Delete":
                        CustomMessageBox deleteMessageBox = new CustomMessageBox() { Caption = "Delete " + title, Message = "Are you sure you want to delete this " + kind + "?\n", LeftButtonContent = "Delete", RightButtonContent = "Cancel" };


                        Grid DeleteMessageBoxContentTemplate = new Grid() { Height = 100 };

                        Image DeleteMessageBoxContentTemplateItemIcon = new Image()
                        {
                            Width = 100,
                            Height = 100,
                            Source = new System.Windows.Media.Imaging.BitmapImage() { UriSource = new Uri((Application.Current as App).iconSource, UriKind.Relative) },
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                            VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                            Margin = new Thickness(10, 0, 0, 0)
                        };

                        TextBlock DeleteMessageBoxContentTemplateItemName = new TextBlock()
                        {
                            Text = (Application.Current as App).itemName,
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                            VerticalAlignment = System.Windows.VerticalAlignment.Top,
                            Margin = new Thickness(125, 10, 0, 0),
                            Width = 345,
                            FontSize = 25
                        };

                        TextBlock DeleteMessageBoxContentTemplateItemType = new TextBlock()
                        {
                            Text = (Application.Current as App).type,
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                            VerticalAlignment = System.Windows.VerticalAlignment.Top,
                            Margin = new Thickness(125, 57, 0, 0),
                            Width = 345,
                            FontSize = 25
                        };


                        DeleteMessageBoxContentTemplate.Children.Add(DeleteMessageBoxContentTemplateItemIcon);
                        DeleteMessageBoxContentTemplate.Children.Add(DeleteMessageBoxContentTemplateItemName);
                        DeleteMessageBoxContentTemplate.Children.Add(DeleteMessageBoxContentTemplateItemType);


                        deleteMessageBox.Content = DeleteMessageBoxContentTemplate;

                        deleteMessageBox.Dismissed += deleteMessageBox_Dismissed;
                        deleteMessageBox.Show();
                        break;
                    case "Rename":
                        CustomMessageBox renameMessageBox = new CustomMessageBox() { Caption = "Rename " + title, LeftButtonContent = "Rename", RightButtonContent = "Cancel" };
                        TextBox textbox = new TextBox() { Text = (Application.Current as App).itemName };
                        textbox.GotFocus += textbox_GotFocus;

                        renameMessageBox.Content = textbox;

                        renameMessageBox.Dismissed += renameMessageBox_Dismissed;
                        renameMessageBox.Show();


                        textbox.Focus();
                        break;
                    case "Properties":
                        NavigationService.Navigate(new Uri("/Properties.xaml?From=explorer", UriKind.Relative));
                        break;
                }
                selectedButtonInHoldMenu = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            FileInfo fi = new FileInfo((Application.Current as App).path);

            (sender as TextBox).Select(0, (sender as TextBox).Text.Length - fi.Extension.Count());
        }


        void deleteMessageBox_Dismissed(object sender, DismissedEventArgs e)
        {
            if (e.Result == CustomMessageBoxResult.LeftButton)
            {
                try
                {
                    if ((Application.Current as App).type == "File folder")
                    {
                        Directory.Delete((Application.Current as App).path, true);
                    }
                    else
                    {
                        File.Delete((Application.Current as App).path);
                    }

                    mainViewModel.Clear();
                    mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, false);

                    if (selectedIndex > 1)
                        list.ScrollTo(mainViewModel[selectedIndex - 1]);
                }
                catch (UnauthorizedAccessException uex)
                {
                    MessageBox.Show(uex.Message, "Can't delete", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Can't delete", MessageBoxButton.OK);
                }
            }
        }

        private void ShowBluetoothcControlPanel()
        {
            ConnectionSettingsTask connectionSettingsTask = new ConnectionSettingsTask();
            connectionSettingsTask.ConnectionSettingsType = ConnectionSettingsType.Bluetooth;
            connectionSettingsTask.Show();
        }

        private void renameMessageBox_Dismissed(object sender, DismissedEventArgs e)
        {
            if (e.Result == CustomMessageBoxResult.LeftButton)
            {
                try
                {
                    (Application.Current as App).itemName = ((sender as CustomMessageBox).Content as TextBox).Text;

                    FileInfo file = new FileInfo((Application.Current as App).path);

                    if ((Application.Current as App).type == "File folder")
                    {
                        Directory.Move((Application.Current as App).path, file.DirectoryName + "\\" + (Application.Current as App).itemName);
                    }
                    else
                    {
                        File.Move((Application.Current as App).path, file.DirectoryName + "\\" + (Application.Current as App).itemName);
                    }


                    mainViewModel.load_Data(mainViewModel.currentDirectory, (Application.Current as App).isGrid, false);

                    if (mainViewModel.Count > 0)
                        list.ScrollTo(mainViewModel[selectedIndex - 1]);
                }
                catch (UnauthorizedAccessException uex)
                {
                    MessageBox.Show(uex.Message, "Can't rename", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Can't rename", MessageBoxButton.OK);
                }
            }
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.start = 1;
            mainViewModel.storages();
        }

        private void ContentElement_LayoutUpdated(object sender, EventArgs e)
        {
        }

        private void ContentElement_TextInputUpdate(object sender, TextCompositionEventArgs e)
        {
            if ((sender as ScrollViewer).ScrollableWidth > 0)
                (sender as ScrollViewer).ScrollToHorizontalOffset((sender as ScrollViewer).ScrollableWidth);

        }

        private void ToggleButton_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (seperator.Height.Value > e.DeltaManipulation.Translation.Y)
                seperator.Height = new GridLength(seperator.Height.Value + (e.DeltaManipulation.Translation.Y * -1));
        }

        private void list_GotFocus(object sender, RoutedEventArgs e)
        {
            seperatorControler.IsChecked = false;
        }

        private void list_2_GotFocus(object sender, RoutedEventArgs e)
        {
            seperatorControler.IsChecked = true;
        }

        private async void list_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list_2.SelectedItem != null)
            {
                itemViewModel = (ItemViewModel)list_2.SelectedItem;
                list_2.SelectedItem = null;

                if ((Application.Current as App).isGrid)
                {
                    selectedPath = itemViewModel.Size;
                    (Application.Current as App).itemName = itemViewModel.Path;
                }
                else
                {
                    selectedPath = itemViewModel.Path;
                    (Application.Current as App).itemName = itemViewModel.Name;
                }

                (Application.Current as App).path = selectedPath;
                (Application.Current as App).type = itemViewModel.Type;


                if (itemViewModel.Type == "File folder")
                {
                    mainViewModel_2.load_Data(selectedPath, (Application.Current as App).isGrid, false);
                }
                else if (itemViewModel.Type == "")
                {
                    mainViewModel_2.load_Data(selectedPath, (Application.Current as App).isGrid, false);
                }
                else if (itemViewModel.Type == "File folder (.zip)")
                {
                    mainViewModel_2.load_Data(selectedPath, (Application.Current as App).isGrid, false);
                }
                else
                {
                    switch (itemViewModel.Type.ToLower())
                    {
                        case "sound (.mp3)":
                        case "sound (.wav)":
                        case "sound (.ac3)":
                        case "sound (.wma)":
                        case "sound (.aac)":
                            Open_OpenWithAnimation.Begin();
                            break;
                        case "video (.mp4)":
                        case "video (.3gp)":
                        case "video (.avi)":
                        case "video (.mkv)":
                        case "video (.ts)":
                        case "video (.mpg)":
                        case "video (.wmv)":
                        case "video (.vob)":
                            NavigationService.Navigate(new Uri("/Player/VideoPlayer.xaml", UriKind.Relative));
                            break;
                        case "image (.jpg)": /*NavigationService.Navigate(new Uri("/ImageViewer/ImageViewer.xaml", UriKind.Relative));*/
                        case "image (.png)":
                        case "image (.ico)":
                        case "image (.raw)":
                        case "document (.txt)":
                        case "document (.docx)":
                        case "document (.dotx)":
                        case "microsoft excel (.xlsx)":
                        case "microsoft excel (.xlsm)":
                        case "microsoft excel (.xls)":
                        case "microsoft excel (.xltx)":
                        case "microsoft excel (.xlt)":
                        case "pdf file (.pdf)":
                        case "html document (.html)":
                            await Windows.System.Launcher.LaunchFileAsync(await StorageFile.GetFileFromPathAsync((Application.Current as App).path));
                            break;
                    }
                }
            }
            numbersOfItem.Text = (mainViewModel.Count()).ToString() + " items";
            pathPanel_2.Text = mainViewModel.currentDirectory;
        }

        private void list_2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void list_Loaded(object sender, RoutedEventArgs e)
        {
            (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(list, 0), 0), 0) as ViewportControl).Padding = new Thickness(0, 0, 0, 40);
        }

    }
}
