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

namespace 大配件管理系统.View
{
    /// <summary>
    /// OutRecordsView.xaml 的交互逻辑
    /// </summary>
    public partial class OutRecordsView : UserControl
    {
        public OutRecordsView()
        {
            InitializeComponent();
        }


        //输入框自动匹配
        private void PART_EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var comboBox = textBox?.TemplatedParent as ComboBox;

            if (textBox != null && comboBox != null)
            {
                // 获取输入文本
                string enteredText = textBox.Text;

                // 查找匹配项
                var match = comboBox.Items.Cast<string>().FirstOrDefault(item => item.StartsWith(enteredText, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrEmpty(match))
                {
                    // 设置匹配项
                    textBox.Text = match;
                    textBox.CaretIndex = enteredText.Length; // 设置光标位置到输入文本的末尾
                }
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

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true; // 自动打开下拉框
            }
        }

        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
