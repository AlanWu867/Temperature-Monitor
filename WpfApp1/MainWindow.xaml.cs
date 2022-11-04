using Ownskit.Utils;
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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using IniParser;
using IniParser.Model;


namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyboardListener KListener = new KeyboardListener(); // linsten keyboaed
        public MainWindow()
        {
            KListener.KeyDown += new RawKeyEventHandler(KListener_KeyDown);
            InitializeComponent();
        }

        void init(object sender, EventArgs e) //初始化，視窗置中與隱藏
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            this.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
        

        void KListener_KeyDown(object sender, RawKeyEventArgs args)
        {
            
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Settings.ini");
            bool check_value = Convert.ToBoolean(data["open_frame"]["check"]);
            string top = data["Position"]["Top"];
            string left = data["Position"]["LEFT"];
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (args.Key.ToString()=="Q")
                {
                    if (this.Visibility == Visibility.Hidden)
                    {
                        this.Visibility = Visibility.Visible;

                        var hwnd = new WindowInteropHelper(Page1.win2).Handle; // 點擊穿透
                        WindowsServices.SetWindowExTransparent2(hwnd);    // 點擊穿透
                        Page1.win2.Opacity = 1;
                        Page1.win2.IsHitTestVisible = true;
                        if (check_value == false)
                        {
                            Page1.win2.Visibility = Visibility.Visible;
                        }


                        this.Activate();
                    }
                    else
                    {

                        this.Visibility = Visibility.Hidden;
                        if (check_value==true)
                        {
                            Page1.win2.Opacity = 0.6;
                            Page1.win2.IsHitTestVisible = false;
                            var hwnd = new WindowInteropHelper(Page1.win2).Handle; // 點擊穿透
                            WindowsServices.SetWindowExTransparent(hwnd);    // 點擊穿透
                        }
                        else
                        {
                            Page1.win2.Visibility = Visibility.Hidden; ;
                        }

                    }
                }
                
            }

        }
        public static class WindowsServices // 點擊穿透
        {
            const int WS_EX_TRANSPARENT = 0x00000020;
            const int GWL_EXSTYLE = (-20);

            [DllImport("user32.dll")]
            static extern int GetWindowLong(IntPtr hwnd, int index);

            [DllImport("user32.dll")]
            static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

            public static void SetWindowExTransparent(IntPtr hwnd)     //點擊穿透
            {
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            }
            public static void SetWindowExTransparent2(IntPtr hwnd)    //取消點擊穿透
            {
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
            }
        }

        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        private void CloseWindow(object sender, RoutedEventArgs args)
        {
            //this.ShowInTaskbar = false;
            //this.WindowState = WindowState.Minimized;
            this.Visibility = Visibility.Hidden;
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Settings.ini");
            bool check_value = Convert.ToBoolean(data["open_frame"]["check"]);
            if (check_value == true)
            {
                Page1.win2.Opacity = 0.6;
                Page1.win2.IsHitTestVisible = false;
                var hwnd = new WindowInteropHelper(Page1.win2).Handle; // 點擊穿透
                WindowsServices.SetWindowExTransparent(hwnd);    // 點擊穿透
            }
            else
            {
                Page1.win2.Visibility = Visibility.Hidden; ;
            }
        }
        private void DoubleClick(object sender, RoutedEventArgs args)
        {
            //this.WindowState = WindowState.Normal;
            this.Visibility = Visibility.Visible;
            this.Activate();
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Settings.ini");
            bool check_value = Convert.ToBoolean(data["open_frame"]["check"]);
            var hwnd = new WindowInteropHelper(Page1.win2).Handle; // 點擊穿透
            WindowsServices.SetWindowExTransparent2(hwnd);    // 點擊穿透
            Page1.win2.Opacity = 1;
            Page1.win2.IsHitTestVisible = true;
            if (check_value == false)
            {
                Page1.win2.Visibility = Visibility.Visible;
            }
            //this.ShowInTaskbar = true;

        }
        private void Change_page(object sender, RoutedEventArgs e) //換頁
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btn1":
                    btn1.Background = Brushes.White;
                    btn2.Background = Brushes.Black;
                    //btn3.Background = Brushes.Black;
                    //btn4.Background = Brushes.Black;
                    //btn5.Background = Brushes.Black;
                    btn1.Foreground = Brushes.Black;
                    btn2.Foreground = Brushes.White;
                    pagechange.Source = new Uri("page1.xaml", UriKind.Relative);
                    //btn3.Foreground = Brushes.White;
                    //btn4.Foreground = Brushes.White;
                    //btn5.Foreground = Brushes.White;
                    break;
                case "btn2":
                    btn1.Background = Brushes.Black;
                    btn2.Background = Brushes.White;
                    pagechange.Source = new Uri("page2.xaml", UriKind.Relative);
                    //btn3.Background = Brushes.Black;
                    //btn4.Background = Brushes.Black;
                    //btn5.Background = Brushes.Black;
                    btn1.Foreground = Brushes.White;
                    btn2.Foreground = Brushes.Black;
                    //btn3.Foreground = Brushes.White;
                    //btn4.Foreground = Brushes.White;
                    //btn5.Foreground = Brushes.White;
                    break;
                //case "btn3":
                //    btn1.Background = Brushes.Black;
                //    btn2.Background = Brushes.Black;
                //    btn3.Background = Brushes.White;
                //    btn4.Background = Brushes.Black;
                //    btn5.Background = Brushes.Black;

                //    btn1.Foreground = Brushes.White;
                //    btn2.Foreground = Brushes.White;
                //    btn3.Foreground = Brushes.Black;
                //    btn4.Foreground = Brushes.White;
                //    btn5.Foreground = Brushes.White;
                //    break;
                //case "btn4":
                //    btn1.Background = Brushes.Black;
                //    btn2.Background = Brushes.Black;
                //    btn3.Background = Brushes.Black;
                //    btn4.Background = Brushes.White;
                //    btn5.Background = Brushes.Black;

                //    btn1.Foreground = Brushes.White;
                //    btn2.Foreground = Brushes.White;
                //    btn3.Foreground = Brushes.White;
                //    btn4.Foreground = Brushes.Black;
                //    btn5.Foreground = Brushes.White;
                //    break;
                //case "btn5":
                //    btn1.Background = Brushes.Black;
                //    btn2.Background = Brushes.Black;
                //    btn3.Background = Brushes.Black;
                //    btn4.Background = Brushes.Black;
                //    btn5.Background = Brushes.White;

                //    btn1.Foreground = Brushes.White;
                //    btn2.Foreground = Brushes.White;
                //    btn3.Foreground = Brushes.White;
                //    btn4.Foreground = Brushes.White;
                //    btn5.Foreground = Brushes.Black;
                //    break;
                default:
                    break;
            }
        }


    }
}
