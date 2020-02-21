using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zires_Explorer.Resources;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Windows.Storage;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Info;
using Microsoft.Phone.Storage;
using Microsoft.Xna.Framework.Media;
using System.Windows;
using System.Text.RegularExpressions;
using System.Threading;



namespace Zires_Explorer.ViewModels
{
    public class MainViewModel : ObservableCollection<ItemViewModel>
    {
        public string currentDirectory;
        delegate void myDelegate();

        public MainViewModel()
        {
            currentDirectory = "D:\\";
            storages();
        }


        public string backPath;
        public int start = 1;
        
        IsolatedStorageFile v = IsolatedStorageFile.GetUserStoreForApplication(); 


        public void storages()
        {
            start = 0;
            this.Clear();
            
            this.Items.Add(new ItemViewModel() { Name = "Phone", Size = v.AvailableFreeSpace.ToString(), Type = "", Source = "/Assets/ExplorerIcons/Drives/Drive(C).PNG", Path = "ms-appx/Assets" });

            if ((Application.Current as App).isGrid == true)
                this.Items.Add(new ViewModels.ItemViewModel() { Path = "External Storage", Type = "File folder", Size = "D:\\", Source = "/Assets/ExplorerIcons/Drives/Drive(D).PNG" });
            else
                this.Items.Add(new ItemViewModel() { Name = "External Storage", Size = "00000", Type = "", Source = "/Assets/ExplorerIcons/Drives/Drive(D).PNG", Path = "D:\\" });
        }





        string length;
        int index;

        public void load_Data(string currentDir, bool isGrid, bool isParent)
        {

            try
            {
                this.Clear();

                if (isGrid)
                {
                    longListIsGrid_LoadData(currentDir, isParent);
                }
                else
                {
                    longListIsList_LoadData(currentDir, isParent);
                }
                
            }
            catch (IOException)
            {

            }
        }
        


        private async void longListIsGrid_LoadData(string currentDir, bool isParent)
        {
            /*
            IEnumerable<ExternalStorageDevice> fil = await ExternalStorage.GetExternalStorageDevicesAsync();

                ExternalStorageDevice esd = fil.FirstOrDefault();
                ExternalStorageFolder exStorageFolder = await esd.GetFolderAsync("D:\\Eftekhare");
                ExternalStorageFile exStorageFile = await esd.GetFileAsync("D:\\");

                this.Items.Add(new ViewModels.ItemViewModel() { Path = exStorageFolder.Name, Source = "/Assets/ExplorerIcons/Folders/Folder(Fill).PNG", Size = exStorageFolder.Path, Type = "File folder", Index = index });

                this.Items.Add(new ItemViewModel() { Path = exStorageFile.Name, Size = exStorageFile.Path, Source = "/Assets/ExplorerIcons/Files/File.PNG", Type = "File (" + exStorageFile.DateModified + ")", Tag = index });
           
          */
            try
            {
                index = 0;
                DirectoryInfo di = new DirectoryInfo(currentDir);

                if (isParent)
                    currentDirectory = di.Parent.FullName;
                else
                    currentDirectory = currentDir;


                DirectoryInfo[] directory = new DirectoryInfo(currentDirectory).GetDirectories();
                FileInfo[] files = new DirectoryInfo(currentDirectory).GetFiles();



                foreach (DirectoryInfo dir in directory)
                {
                    this.Items.Add(new ViewModels.ItemViewModel() { Path = dir.Name, Source = "/Assets/ExplorerIcons/Folders/Folder(Fill).PNG", Size = dir.FullName, Type = "File folder", Tag = index });

                    index += 1;
                }


                foreach (FileInfo file in files)
                {
                    switch (file.Extension.ToLower())
                    {
                        case ".zip":
                            this.Items.Add(new ViewModels.ItemViewModel() { Path = file.Name, Size = file.FullName, Source = "/Assets/ExplorerIcons/Files/Zip.PNG", Type = "File folder (" + file.Extension + ")", Tag = index });
                            break;
                        case ".mp3":
                        case ".wav":
                        case ".ac3":
                        case ".wma":
                        case ".aac":
                            this.Items.Add(new ItemViewModel() { Path = file.Name, Size = file.FullName, Source = "/Assets/ExplorerIcons/Files/Music.PNG", Type = "Sound (" + file.Extension + ")", Tag = index });
                            break;
                        case ".txt":
                        case ".docx":
                        case ".dotx":
                            this.Items.Add(new ItemViewModel() { Path = file.Name, Size = file.FullName, Source = "/Assets/ExplorerIcons/Files/Word.PNG", Type = "Document (" + file.Extension + ")", Tag = index });
                            break;
                        case ".xlsx":
                        case ".xlsm":
                        case ".xls":
                        case ".xltx":
                        case ".xlt":
                            this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Microsoft Excel (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/Excel.PNG", Path = file.FullName, Tag = index });
                            break;
                        case ".mp4":
                        case ".3gp":
                        case ".avi":
                        case ".mkv":
                        case ".ts":
                        case ".mpg":
                        case ".wmv":
                            this.Items.Add(new ItemViewModel() { Path = file.Name, Size = file.FullName, Source = "/Assets/ExplorerIcons/Files/Video.png", Type = "Video (" + file.Extension + ")", Tag = index });
                            break;
                        case ".jpg":
                        case ".png":
                        case ".ico":
                        case ".raw":
                            this.Items.Add(new ItemViewModel() { Path = file.Name, Size = file.FullName, Source = "/Assets/ExplorerIcons/Files/Picture(JPG).PNG", Type = "Image (" + file.Extension + ")", Tag = index });
                            break;
                        case ".dll":
                            this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Application extension (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/File(dll).PNG", Path = file.FullName, Tag = index });
                            break;
                        default:
                            this.Items.Add(new ItemViewModel() { Path = file.Name, Size = file.FullName, Source = "/Assets/ExplorerIcons/Files/File.PNG", Type = "File (" + file.Extension + ")", Tag = index });
                            break;
                    }
                    index += 1;

                }
                this.OrderBy(items => items.Type);
            }
            catch(UnauthorizedAccessException unauthorizedAccessException)
            {
                MessageBox.Show(unauthorizedAccessException.Message);
            }
        }


        private void longListIsList_LoadData(string currentDir, bool isParent)
        {
            try
            {
                index = 0;
                DirectoryInfo di = new DirectoryInfo(currentDir);

                if (isParent)
                    currentDirectory = di.Parent.FullName;
                else
                    currentDirectory = currentDir;

                myDelegate delegate_1 = new myDelegate(this.load_Directories);
                myDelegate delegate_2 = new myDelegate(this.load_Files);
                delegate_1.Invoke();
                delegate_2.Invoke();
                         
                this.OrderBy(items => items.Type);
            }
            catch(UnauthorizedAccessException unauthorizedAccessException)
            {
                MessageBox.Show(unauthorizedAccessException.Message);
            }
        }



        void load_Directories()
        {
            DirectoryInfo[] directory = new DirectoryInfo(currentDirectory).GetDirectories();


            foreach (DirectoryInfo dir in directory)
            {
                this.Items.Add(new ViewModels.ItemViewModel() { Name = dir.Name, Type = "File folder", Size = dir.CreationTime.ToShortDateString(), Source = "/Assets/ExplorerIcons/Folders/Folder(Fill).PNG", Path = dir.FullName, Tag = index });

                index += 1;
            }
        }

        void load_Files()
        {
            FileInfo[] files = new DirectoryInfo(currentDirectory).GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Length < 1024)
                {
                    length = file.Length + " byte";
                }
                else if (file.Length >= 1024 && file.Length < 1048576)
                {
                    length = file.Length / 1024 + " KB";
                }
                else if (file.Length >= 1048576)
                {
                    length = Math.Round(file.Length / 1048576.0, 2) + " MB";
                }


                switch (file.Extension.ToLower())
                {
                    case ".zip":
                        this.Items.Add(new ViewModels.ItemViewModel() { Name = file.Name, Type = "File folder (" + file.Extension + ")", Size = file.CreationTime.ToShortDateString(), Source = "/Assets/ExplorerIcons/Files/Zip.PNG", Path = file.FullName, Tag = index });
                        break;
                    case ".mp3":
                    case ".wav":
                    case ".ac3":
                    case ".wma":
                    case ".aac":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Sound (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/Music.PNG", Path = file.FullName, Tag = index });
                        break;
                    case ".txt":
                    case ".docx":
                    case ".dotx":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Document (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/Word.PNG", Path = file.FullName, Tag = index });
                        break;
                    case ".pdf":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "PDF File (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Applications/AdobeAcrobatReader.png", Path = file.FullName, Tag = index });
                        break;
                    case ".xlsx":
                    case ".xlsm":
                    case ".xls":
                    case ".xltx":
                    case ".xlt":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Microsoft Excel (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/Excel.PNG", Path = file.FullName, Tag = index });
                        break;
                    case ".mp4":
                    case ".3gp":
                    case ".avi":
                    case ".mkv":
                    case ".ts":
                    case ".mpg":
                    case ".wmv":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Video (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/Video.png", Path = file.FullName, Tag = index });
                        break;
                    case ".jpg":
                    case ".png":
                    case ".ico":
                    case ".raw":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Image (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/Picture(JPG).PNG", Path = file.FullName, Tag = index });
                        break;
                    case ".dll":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "Application extension (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/File(dll).PNG", Path = file.FullName, Tag = index });
                        break;
                    case ".html":
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "HTML Document (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/File.PNG", Path = file.FullName, Tag = index });
                        break;
                    default:
                        this.Items.Add(new ItemViewModel() { Name = file.Name, Size = length, Type = "File (" + file.Extension + ")", Source = "/Assets/ExplorerIcons/Files/File.PNG", Path = file.FullName, Tag = index });
                        break;
                }
                index += 1;
            }
        }
    }
}
