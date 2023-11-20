using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var text = Text_Block1.Text;

            var parts = text.Split('*', '/', '+', '-');

            var res = new List<int>();

            for (int i = 0; i < parts.Length; i++)
            {
                var num = new StringBuilder();

                for (int j = 0; j < parts[i].Length; j++)
                {
                    
                    var ch = parts[i][j].ToString();

                    bool success = int.TryParse(ch, out var number);
                    if (success)
                    {
                        num.Append(ch);
                    }
                }

                res.Add(Convert.ToInt32(num.ToString()));
            }

            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '*':
                        Text_Block2.Text = (res[0] * res[1]).ToString();
                        break;
                    case '/':
                        Text_Block2.Text = (res[0] / res[1]).ToString();
                        break;
                    case '-':
                        Text_Block2.Text = (res[0] - res[1]).ToString();
                        break;
                    case '+':
                        Text_Block2.Text = (res[0] + res[1]).ToString();
                        break;
                }
            }

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Source source = JsonConvert.DeserializeObject<Source>(File.ReadAllText(@"json1.json"));

            MailAddress from = new MailAddress("mail@gmail.com", "Tom");
            MailAddress to = new MailAddress("mail@gmail.com");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = $"{source.outage_start}, {Message(source)}";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("mail@gmail.com", "password");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        public string Message(Source source)
        {
            var res = new StringBuilder();

            foreach (var item in source.affected_areas)
            {
                res.Append($"\n {item.area_name} + {item.affected_customers} + {item.estimated_recovery_time}");
            }
            
            return res.ToString();
        }

        public BindingList<Source> listModels;

        private void List_Loaded(object sender, RoutedEventArgs e)
        {
            Source source = JsonConvert.DeserializeObject<Source>(File.ReadAllText(@"json1.json"));

            listModels.Add(source);

            /*listModels = new BindingList<ListModel>()
            {
                new ListModel(){
                    outage_start = source.outage_start,
                    outage_end = source.outage_end,
                    area_name = source.affected_areas[0].area_name,
                    affected_customers = source.affected_areas[0].affected_customers,
                    reason = source.affected_areas[0].reason
                },
                new ListModel(){
                    outage_start = source.outage_start,
                    outage_end = source.outage_end,
                    area_name = source.affected_areas[1].area_name,
                    affected_customers = source.affected_areas[1].affected_customers,
                    reason = source.affected_areas[1].reason
                }
            };*/

            List.ItemsSource = listModels;
        }
    }
}
