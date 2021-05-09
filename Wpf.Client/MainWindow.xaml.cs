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

namespace Wpf.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.Sleep(3000);
            //String URI = "https://vpu911.ga/api/Girls/search";
            //WebClient webClient = new WebClient();
            //string reply = webClient.DownloadString(URI);

            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/api/cars/add");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    //string json = "{\"user\":\"test\"," +
            //    //              "\"password\":\"bla\"}";
            //    string json = JsonConvert.SerializeObject(new 
            //    {
            //        Mark = "Ford",
            //        Model = "Биток із США",
            //        Year = 2020,
            //        Fuel = "Бензин-газ",
            //        Сapacity = 5.7F,
            //        Image = "2021-Ford-Thunderbird-Rebord.jpg"
            //    });

            //    streamWriter.Write(json);
            //}

            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();
            //}



            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create("http://localhost:5000/api/cars/add");
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = JsonConvert.SerializeObject(new
            {
                Mark = "Ford",
                Model = "Биток із США",
                Year = 2020,
                Fuel = "Брикет РУФ",
                Сapacity = 5.7F,
                Image = "2021-Ford-Th999underbird-Rebord.jpg"
            });
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.
            //request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            try
            {
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Console.WriteLine(responseFromServer);
                }

                // Close the response.
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            InitializeComponent();
        }
    }
}
