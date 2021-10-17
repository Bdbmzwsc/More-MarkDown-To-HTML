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
using System.Runtime.InteropServices;

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
            /*
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html", mth(MT.Text));
            web1.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
            */
            string str = "";
            System.Diagnostics.Process Proc=new System.Diagnostics.Process();
            try
            {
                str = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
                // 启动记事本
                //  Proc = new System.Diagnostics.Process();
                Proc.StartInfo.FileName = "notepad.exe";
                Proc.StartInfo.UseShellExecute = false;
                Proc.StartInfo.RedirectStandardInput = true;
                Proc.StartInfo.RedirectStandardOutput = true;

                Proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:\r\n" + ex.Message);
            }
            if (Proc != null)
            {
                // 调用 API, 传递数据
                while (Proc.MainWindowHandle == IntPtr.Zero)
                {
                    Proc.Refresh();
                }
                
                IntPtr vHandle = API.FindWindowEx(Proc.MainWindowHandle, IntPtr.Zero, "Edit", null);

                // 传递数据给记事本
                API.SendMessage(vHandle, API.WM_SETTEXT, 0, str);
            }

        }
        //初始化
        public void chu()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"simple.html"))
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html", "<h1>Welcome to MMTH</h1>");
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

        private void MT_TextChanged(object sender, TextChangedEventArgs e)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html", mth(MT.Text));
            web1.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
        }
    }
}
