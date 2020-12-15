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

namespace CalDdvTdv
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        double a;
        double x;
        double y;
        double asemi;
        double bsemi;
        double vx;
        double vy;
        double aoffset;
        double boffset;
        private void Calculate()
        {
            double A1 = Math.Cos(a) * Math.Cos(a) / asemi / asemi + Math.Sin(a) * Math.Sin(a) / bsemi / bsemi;
            double B1 = 2 * Math.Sin(a) * Math.Cos(a) * ((1 / asemi / asemi) - (1 / bsemi / bsemi));
            double C1 = Math.Cos(a) * Math.Cos(a) / bsemi / bsemi + Math.Sin(a) * Math.Sin(a) / asemi / asemi;
            double h = aoffset * Math.Cos(a) + boffset * Math.Sin(a);
            double k = aoffset * Math.Sin(a) - boffset * Math.Cos(a);
            double XE = x + h;
            double YE = y + k;

            double A2 = A1 * h * h + B1 * h * k + C1 * k * k - 1;
            double B21 = (2 * A1 * x + B1 * y) * h + (2 * C1 * y + B1 * x) * k;
            double B22 = (2 * A1 * vx + B1 * vy) * h + (2 * C1 * vy + B1 * vx) * k;
            double C21 = A1 * x * x + B1 * x * y + C1 * y * y;
            double C22 = 2 * (A1 * x * vx + C1 * y * vy) + B1 * (x * vy + y * vx);
            double C23 = A1 * vx * vx + B1 * vx * vy + C1 * vy * vy;

            double D1 = B22 * B22 - 4 * A2 * C23;
            double E1 = 2 * B21 * B22 - 4 * A2 * C22;
            double F1 = B21 * B21 - 4 * A2 * C21;

            double D2 = D1 * (B22 * B22 - D1);
            double E2 = E1 * (B22 * B22 - D1);
            double F2 = F1 * B22 * B22 - E1 * E1;

            //double tmin1 = (-E2 - Math.Sqrt(E2 * E2 - 4 * D2 * F2)) / (2 * D2);
            //double tmin2 = (-E2 + Math.Sqrt(E2 * E2 - 4 * D2 * F2)) / (2 * D2);

            double tmin1 = (-E2 - Math.Sqrt(4 * D2 * F2 - E2 * E2)) / (2 * D2);
            double tmin2 = (-E2 + Math.Sqrt(4 * D2 * F2 - E2 * E2)) / (2 * D2);
            //DDV
            if (tmin1 < tmin2)
            {
                double ft11 = (-(B21 + B22 * tmin1) - Math.Sqrt(D1 * tmin1 * tmin1 + E1 * tmin1 + F1)) / (2 * A2);
                double ft21 = (-(B21 + B22 * tmin1) + Math.Sqrt(D1 * tmin1 * tmin1 + E1 * tmin1 + F1)) / (2 * A2);
            }
            else
            {
                double ft12 = (-(B21 + B22 * tmin2) - Math.Sqrt(D1 * tmin2 * tmin2 + E1 * tmin2 + F1)) / (2 * A2);
                double ft22 = (-(B21 + B22 * tmin2) + Math.Sqrt(D1 * tmin2 * tmin2 + E1 * tmin2 + F1)) / (2 * A2);
            }
            //TDV
            double A3 = A1 * vx * vx + B1 * vx * vy + C1 * vy * vy;
            double B3 = 2 * (A1 * XE * vx + C1 * YE * vy) + B1 * (XE * vy + YE * vx);
            double C3 = A1 * XE * XE + B1 * XE * YE + C1 * YE * YE - 1;
            double t1 = (-B3 - Math.Sqrt(B3 * B3 - 4 * A3 * C3)) / (2 * A3);
            double t2 = (-B3 + Math.Sqrt(B3 * B3 - 4 * A3 * C3)) / (2 * A3);
        }
        private void ParseParmeter()
        {
            double jiaodua;
            double.TryParse(tbx_a.Text, out jiaodua);
            a = jiaodua / 180 * Math.PI;
            double.TryParse(tbx_x.Text, out x);
            double.TryParse(tbx_y.Text, out y);
            double.TryParse(tbx_asemi.Text, out asemi);
            double.TryParse(tbx_bsemi.Text, out bsemi);
            double.TryParse(tbx_vx.Text, out vx);
            double.TryParse(tbx_vy.Text, out vy);
            double.TryParse(tbx_xoffset.Text, out aoffset);
            double.TryParse(tbx_yoffset.Text, out boffset);
        }

        private void btn_calculate_Click(object sender, RoutedEventArgs e)
        {
            ParseParmeter();
            Calculate();
        }
    }
}
