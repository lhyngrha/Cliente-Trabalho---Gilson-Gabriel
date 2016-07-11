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
using Newtonsoft.Json;

namespace Cliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ip = "http://localhost:53089/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            Compromisso f = new Compromisso
            {

                id = int.Parse(txtId.Text),
                descricao = txtDesc.Text,
                local = txtLocal.Text,
                data = Convert.ToDateTime(datapica.SelectedDate),
                realizado = cehck.IsChecked.Value
                
            };

            List<Compromisso> fl = new List<Compromisso>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/api/compromisso", content);
        }

        private async void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var response = await httpClient.GetAsync("/api/compromisso");

            var str = response.Content.ReadAsStringAsync().Result;

            List<Compromisso> obj = JsonConvert.DeserializeObject<List<Compromisso>>(str);
            listBox.ItemsSource = obj;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Compromisso f = new Compromisso
            {
                id = int.Parse(txtId.Text),
                descricao = txtDesc.Text,
                local = txtLocal.Text,
                data = Convert.ToDateTime(datapica.SelectedDate),
                realizado = cehck.IsChecked.Value
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("api/compromisso/" + f.id, content);
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            //await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);
            await httpClient.DeleteAsync("/api/compromisso/" + txtId.Text);
        }

        private async void btnRealizado_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var miojo = (Compromisso)listBox.SelectedItem;
            Compromisso f = new Compromisso();
            f = miojo;
            f.realizado = true;

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("api/compromisso/" + txtId.Text, content);
        }
    }
}
