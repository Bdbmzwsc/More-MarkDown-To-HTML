using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using System.IO;
using System.Runtime.InteropServices;//DllImport

namespace WindowsFormsApp1
{


    public partial class Form1 : Form
    {
        //API引用
        /// <summary>
        /// 传递消息给记事本
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);

        /// <summary>
        /// 查找句柄
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="lpszClass"></param>
        /// <param name="lpszWindow"></param>
        /// <returns></returns>
        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 记事本需要的常量
        /// </summary>
        public const uint WM_SETTEXT = 0x000C;

        public Form1()
        {
            InitializeComponent();
            chu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml(str, pipeline);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html","<!DOCTYPE html>\r\n<html lang=\"zh-CN\">\r\n" + result + "</html>");
            webBrowser1.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
        }
        public void chu()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"simple.html"))
            {
                return;
            }
            else
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"simple.html", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process Proc;
            try
            {
                // 启动记事本
                Proc = new System.Diagnostics.Process();
                Proc.StartInfo.FileName = "notepad.exe";
                Proc.StartInfo.UseShellExecute = false;
                Proc.StartInfo.RedirectStandardInput = true;
                Proc.StartInfo.RedirectStandardOutput = true;

                Proc.Start();
            }
            catch
            {
                Proc = null;
            }
            string htmltext = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"simple.html");
            if (Proc != null)
            {
                // 调用 API, 传递数据
                while (Proc.MainWindowHandle == IntPtr.Zero)
                {
                    Proc.Refresh();
                }

                IntPtr vHandle = FindWindowEx(Proc.MainWindowHandle, IntPtr.Zero, "Edit", null);

                // 传递数据给记事本
                SendMessage(vHandle, WM_SETTEXT, 0,htmltext);
            }
        }
    }
}
