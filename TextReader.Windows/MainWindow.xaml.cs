using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
using TextReader.Managers;

namespace TextReader.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://localhost:44384/api/text/sort");

            var textRange = new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd);

            int option;
            if (rbNone.IsChecked == true)
            {
                option = 0;
            }
            else if (rbAscending.IsChecked == true)
            {
                option = 1;
            }
            else
            {
                option = 2;
            }

            var data = JsonConvert.SerializeObject(new { SortOption = option, textRange.Text });
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");

            // Post the JSON and wait for a response.
            var response = client.PostAsync(uri, content);

            Task.WaitAll(response);

            if (response.Result.IsSuccessStatusCode)
            {
                var result = response.Result.Content.ReadAsStringAsync();
                result.Wait();
                tbResult.Text = result.Result;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://localhost:44384/api/text/statistics");

            var textRange = new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd);

            var data = JsonConvert.SerializeObject(new { textRange.Text });
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");

            // Post the JSON and wait for a response.
            var response = client.PostAsync(uri, content);

            Task.WaitAll(response);

            if (response.Result.IsSuccessStatusCode)
            {
                var result = response.Result.Content.ReadAsStringAsync();
                result.Wait();
                data = result.Result;

                var stats = JsonConvert.DeserializeObject<TextStatistics>(data);
                data = $"Spaces: {stats.Spaces}\nHyphens: {stats.Hyphens}\nWords: {stats.Words}";
                tbResult.Text = data;
            }
        }
    }
}
