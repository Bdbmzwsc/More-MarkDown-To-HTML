using System;
using System.Collections.Generic;
using System.Linq;
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
using Markdig;
using System.IO;

namespace MMTH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            chu();
        }
        //转HTML
        public string mth(string m)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(m, pipeline);
        }

        private void yulan_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html", mth(MT.Text));
            web1.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
        }
        //初始化
        public void chu()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"simple"))
            {
                web1.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
                return;
                
            }
            else
            {
                /*
                System.Reflection.Assembly Asmb = System.Reflection.Assembly.GetExecutingAssembly();
                Stream istr = Asmb.GetManifestResourceStream("MMTH.assets.simple.txt");
                System.IO.StreamReader sr = new System.IO.StreamReader(istr);
                string str = sr.ReadToEnd();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html",str);
                web1.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
                */
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html", "<h1>Welcome to MMTH</h1>");
               web1.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");

            }
        }
    }
}
