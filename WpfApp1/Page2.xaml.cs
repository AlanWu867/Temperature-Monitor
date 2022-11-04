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

namespace WpfApp1
{
    /// <summary>
    /// Page2.xaml 的互動邏輯
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }
        private void open__youtube(object sender, RoutedEventArgs e)
        {
            string webpageUrl = "https://www.youtube.com/channel/UCaeZiPGrBR4pSXBy8louyWw";
            System.Diagnostics.Process.Start(webpageUrl);
        }
        private void open__github(object sender, RoutedEventArgs e)
        {
            string webpageUrl = "https://github.com/AlanWu867/temperature-gadget";
            System.Diagnostics.Process.Start(webpageUrl);
        }
        private void open__paypal(object sender, RoutedEventArgs e)
        {
            string webpageUrl = "https://www.paypal.com/paypalme/BaLaG867?country.x=TW&locale.x=zh_TW";
            System.Diagnostics.Process.Start(webpageUrl);
        }
    }
}
