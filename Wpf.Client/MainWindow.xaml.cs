using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using Newtonsoft.Json;
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
using UIHelper;
using Wpf.Client.Models.Car.Validation;

namespace Wpf.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string file_selected = string.Empty;
        public string file_name { get; set; }
        public static string New_FileName { get; set; }
        //private readonly IConfiguration _configuration;
        public MainWindow()
        {
            //_configuration = configuration;
            InitializeComponent();
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                New_FileName = openFileDialog.FileName;
            }
        }

        private async void btnSaveChangs_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => PostRequest());
        }

        public async Task<bool> PostRequest()
        {
           
            //New_FileName
            var app = App.Current as IGetConfiguration;
            var serverUrl = app.Configuration.GetSection("ServerUrl").Value;
            WebRequest request = WebRequest.Create($"{serverUrl}api/Cars/add");
            {
                request.Method = "POST";
                request.ContentType = "application/json";
                request.PreAuthenticate = true;
                request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYW1lIjoidXNlckBnbWFpbC5jb20iLCJyb2xlcyI6ImFkbWluIiwiZXhwIjoxNjMxNjA2MDAxfQ.x07kID0pPX73KE9ENNJ5K16ryto6ISI4yJ1qbHBckO0");
            };
            string base64 = ImageHelper.ImageConvertToBase64(New_FileName); 
            string json = JsonConvert.SerializeObject(new
            {
                Mark = "Камез",//tbMark.Text.ToString(),
                Model = "33",//tbModel.Text.ToString(),
                Year = 1972, //int.Parse(tbYear.Text),
                Fuel = "Дизель",//tbFuel.Text.ToString(),
                Capacity = 12.45F, //float.Parse(tbСapacity.Text),
                Image = base64
            });
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                await request.GetResponseAsync();
                return true;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    MessageBox.Show("Error code: " + httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        var errors = JsonConvert.DeserializeObject<AddCarValidation>(text);
                        MessageBox.Show(text);
                        MessageBox.Show(errors.Errors.Mark[0]);
                        MessageBox.Show(errors.Errors.Model[0]);
                        MessageBox.Show(errors.Errors.Year[0]);
                        MessageBox.Show(errors.Errors.Fuel[0]);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private void btnTestLogin_Click(object sender, RoutedEventArgs e)
        {
            //New_FileName
            var app = App.Current as IGetConfiguration;
            var serverUrl = app.Configuration.GetSection("ServerUrl").Value;
            WebRequest request = WebRequest.Create($"{serverUrl}api/Account");
            {
                request.Method = "POST";
                request.ContentType = "application/json";
            };
            string json = JsonConvert.SerializeObject(new
            {
                Email = "user@gmail.com",//tbMark.Text.ToString(),
                Password = "Qwerty1-"
            });
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            using (Stream stream = request.GetRequestStreamAsync().Result)
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                var response = request.GetResponseAsync().Result;
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    MessageBox.Show(stream.ReadToEnd());
                    return;
                }
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    MessageBox.Show("Error code: " + httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        var errors = JsonConvert.DeserializeObject<AddCarValidation>(text);
                        MessageBox.Show(text);
                        MessageBox.Show(errors.Errors.Mark[0]);
                        MessageBox.Show(errors.Errors.Model[0]);
                        MessageBox.Show(errors.Errors.Year[0]);
                        MessageBox.Show(errors.Errors.Fuel[0]);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
    }
}
