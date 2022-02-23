using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using WpfAnimatedGif;
using XamlAnimatedGif;
using static Google.Apis.Drive.v3.DriveService;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DriveService service;

        List<string> textsList = new List<string>
            { "This cat loves you (probably)", "Good boi\u2122", "Pspspspsps", "You are now blessed by this cat", "What a pretty cat", "I meow you", 
            "You look purfect", "Have a meowgical day", "I think we should get meow-rried someday", "I’m just kitten around", "I’m so fur-tunate",
            "That’s paw-some", "Whisker me away", "I’m feline good", "I’ve got cattitude"};

        int before = -1; //save last index so we don't repeat


        public MainWindow()
        {
            InitializeComponent();

            App.Current.Properties["Boxes"] = 0;
            App.Current.Properties["Clothes"] = 0;
            App.Current.Properties["Hats"] = 0;
            App.Current.Properties["Sinks"] = 0;
            App.Current.Properties["Space"] = 0;
            App.Current.Properties["Sunglasses"] = 0;
            App.Current.Properties["Ties"] = 0;
        }

        

        private String CreateRequest()
        {
            String request = "https://api.thecatapi.com/v1/images/search";
            List<String> options = new List<String>();

            options.Add(App.Current.Properties["Boxes"].ToString());
            options.Add(App.Current.Properties["Clothes"].ToString());
            options.Add(App.Current.Properties["Hats"].ToString());
            options.Add(App.Current.Properties["Sinks"].ToString());
            options.Add(App.Current.Properties["Space"].ToString());
            options.Add(App.Current.Properties["Sunglasses"].ToString());
            options.Add(App.Current.Properties["Ties"].ToString());

            foreach (String cat in options)
            {
                if (cat != "0")
                {
                    request = request + "?category_ids=" + cat;
                    break;
                }
            }

            return request;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RandomCatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var random = new Random();
            int index;

            String req = CreateRequest();

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var client = new RestClient(req);
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", "7187213e-ecce-432d-912d-f91c0967c659");
                IRestResponse response = client.Execute(request);

                int statusCode = (int)response.StatusCode;

                if (statusCode != 200)
                {
                    Console.Write("Invalid Search");
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);


                    CatPic.Source = obj[0].url;

                    App.Current.Properties["ImageURL"] = obj[0].url;


                    index = random.Next(0, textsList.Count);
                    while (before == index)
                    {
                        index = random.Next(0, textsList.Count);
                    }
                    TextUnderCat.Text = textsList[index];
                }
            }
        }

        private void SpecificCatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            choice choiceWindow = new choice();
            choiceWindow.Owner = this;
            choiceWindow.Show();
        }

        private void SaveButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Image Files(*.jpg, *.gif) | *.jpg; *.gif";

            if (saveFileDialog.ShowDialog() == true)
            {

                WebClient webClient = new WebClient();
                webClient.DownloadFile(App.Current.Properties["ImageURL"].ToString(), saveFileDialog.FileName);
            }

        }

        private void UploadButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose cat picture to upload";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == true)
            {
                var req = "https://api.thecatapi.com/v1/images/upload";

                var client = new RestClient(req);
                var request = new RestRequest(Method.POST);
                request.AddHeader("x-api-key", "7187213e-ecce-432d-912d-f91c0967c659");
                request.AddHeader("content-type", "multipart/form-data;");
                request.AddFile("image", fdlg.FileName, "image/jpeg");
                IRestResponse response = client.Execute(request);

                confirmWindow confirm = new confirmWindow();
                confirm.Owner = this;
                confirm.Show();


            }
        }
    }
}
