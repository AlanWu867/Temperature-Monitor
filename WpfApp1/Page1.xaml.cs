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
using System.Windows.Interop;
using System.Runtime.InteropServices;
using IniParser;
using IniParser.Model;
using System.Timers;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Page1.xaml 的互動邏輯
    /// </summary>
    public partial class Page1 : Page
    {
        public static int aa = 0;
        public Page1()
        {
            InitializeComponent();
        }
        public static Window1 win2;
        public static string check;
        public void UpdateText()
        {

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Settings.ini");
            check=data["open_frame"]["check"];
            while (true)
            {
                try
                {
                    Dispatcher.Invoke((Action)delegate ()
                    {
                        var top = win2.Top.ToString();
                        var left = win2.Left.ToString();
                        data["Position"]["Top"] = top;
                        data["Position"]["LEFT"] = left;
                        data["open_frame"]["check"] = check;
                        data["window"]["color"] = color_pick.SelectedColor.ToString();
                        parser.WriteFile("Settings.ini", data);
                    });
                    Thread.Sleep(200);
                }
                catch
                {
                    break;
                }


            }


        }
        public static bool check_value;
        private void init_checkbox(object sender, EventArgs e)
        {
            if (aa==0)
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("Settings.ini");
                check_value = Convert.ToBoolean(data["open_frame"]["check"]);
                open_frame.IsChecked = check_value;
                win2 = new Window1();
                win2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(data["window"]["color"]);
                color_pick.SelectedColor = ((Color)ColorConverter.ConvertFromString(data["window"]["color"]));
                string top = data["Position"]["Top"];
                string left = data["Position"]["LEFT"];
                string frame_static = data["Position"]["static"];
                string frame_Opacity = data["Position"]["Opacity"];
                if (open_frame.IsChecked ?? false)
                {



                    if (top == null)
                    {
                        data["Position"]["LEFT"] = "0";
                        data["Position"]["Top"] = "0";
                        parser.WriteFile("Settings.ini", data);
                        top = data["Position"]["Top"];
                        left = data["Position"]["LEFT"];
                        win2.Left = Convert.ToDouble(left);
                        win2.Top = Convert.ToDouble(top);
                    }
                    else
                    {
                        win2.Left = Convert.ToDouble(left);
                        win2.Top = Convert.ToDouble(top);
                    }
                    win2.Opacity = 0.6;
                    win2.IsHitTestVisible = false;
                    win2.Show();
                    var hwnd = new WindowInteropHelper(win2).Handle; // 點擊穿透
                    WindowsServices.SetWindowExTransparent(hwnd);    // 點擊穿透


                }
                else
                {
                    win2.Left = Convert.ToDouble(left);
                    win2.Top = Convert.ToDouble(top);
                    win2.Visibility = Visibility.Hidden;
                    //win2.Show();

                }
                Thread t = new Thread(new ThreadStart(UpdateText));
                t.Start();
                aa = 1;
            }
            else
            {
                open_frame.IsChecked = check_value;
            }

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Settings.ini");
            if (open_frame.IsChecked ?? false)
            {


                string top = data["Position"]["Top"];
                string left = data["Position"]["LEFT"];

                if (top == null)
                {
                    data["Position"]["LEFT"]="0";
                    data["Position"]["Top"]="0";
                    parser.WriteFile("Settings.ini", data);
                    top = data["Position"]["Top"];
                    left = data["Position"]["LEFT"];
                    win2.Left = Convert.ToDouble(left);
                    win2.Top = Convert.ToDouble(top);
                }
                else
                {
                    //win2.Left = Convert.ToDouble(left);
                    //win2.Top = Convert.ToDouble(top);
                }
                


            }
            else
            {

            }
            check= open_frame.IsChecked.ToString();
            data["open_frame"]["check"] = open_frame.IsChecked.ToString();
            parser.WriteFile("Settings.ini", data);
        }
        public static class WindowsServices // 點擊穿透
        {
            const int WS_EX_TRANSPARENT = 0x00000020;
            const int GWL_EXSTYLE = (-20);

            [DllImport("user32.dll")]
            static extern int GetWindowLong(IntPtr hwnd, int index);

            [DllImport("user32.dll")]
            static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

            public static void SetWindowExTransparent(IntPtr hwnd)
            {
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //try 
            //{
            //    win2.Opacity = 0.6;
            //    //win2.Topmost = true;
            //    win2.IsHitTestVisible = false;
            //    var top = win2.Top.ToString();
            //    var left = win2.Left.ToString();
            //    var parser = new FileIniDataParser();
            //    IniData data = parser.ReadFile("Settings.ini");
            //    data["Position"]["Top"]=top;
            //    data["Position"]["LEFT"]=left;
            //    data["Position"]["static"] = "1";
            //    data["Position"]["Opacity"] = "0.6";
            //    data["open_frame"]["check"] = open_frame.IsChecked.ToString();
            //    parser.WriteFile("Settings.ini", data);
            //    var hwnd = new WindowInteropHelper(win2).Handle; // 點擊穿透
            //    WindowsServices.SetWindowExTransparent(hwnd);    // 點擊穿透
            //}
            //catch 
            //{ 

            //}
        }

        private void open_frame_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void color_changed(object sender, RoutedEventArgs e)
        {
            win2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(color_pick.SelectedColor.ToString());
        }
    }
}
