using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace 大配件管理系统.View
{
    /// <summary>
    /// PartsInforView.xaml 的交互逻辑
    /// </summary>
    public partial class PartsInforView : UserControl
    {

        public PartsInforView()
        {
            InitializeComponent();
        }

        

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true; // 自动打开下拉框
            }
        }

        private void PART_EditableTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    // 确认输入
                    comboBox.IsDropDownOpen = false; // 关闭下拉框
                    comboBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); // 移动焦点，确保数据绑定更新
                }
            }
        }
    }
}
