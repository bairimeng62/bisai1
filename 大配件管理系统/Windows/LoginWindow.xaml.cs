using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace 大配件管理系统.Windows
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            pwd.TextDecorations = new TextDecorationCollection(new TextDecoration[] {
                new TextDecoration()
                {
                    Location=TextDecorationLocation.Strikethrough,
                    Pen=new Pen(Brushes.White, 10f)
                    {
                        DashCap=PenLineCap.Round,
                        StartLineCap=PenLineCap.Round,
                        EndLineCap=PenLineCap.Round,
                        DashStyle=new DashStyle(new double[]{ 0.0,1.2},0.6f)
                    }
                }
            });
            
        }

    }
}
