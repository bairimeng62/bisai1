using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;
using 大配件管理系统.View;
using 大配件管理系统.Windows;

namespace 大配件管理系统.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public AppData AppData { get; private set; } = AppData.Instance;

        public MainViewModel()
        {
           
        }


        public RelayCommand<RadioButton> SelectViewCommand
        {
            get
            {
                return new RelayCommand<RadioButton>((arg) =>
                {
                    if (!(arg is RadioButton button)) return;
                    if (string.IsNullOrEmpty(button.Content.ToString())) return;
                    switch (button.Content.ToString())
                    {
                        case "首页": AppData.Instance.MainWindow.container.Content = new IndexView(); break;
                        case "车辆配件": AppData.Instance.MainWindow.container.Content = new CarPartsView(); break;
                        case "配件总览": AppData.Instance.MainWindow.container.Content = new PartsInforView(); break;
                        case "配件出库": AppData.Instance.MainWindow.container.Content = new OutRecordsView(); break;
                        case "配件入库": AppData.Instance.MainWindow.container.Content = new EnterRecordsView(); break;
                        case "库存信息": AppData.Instance.MainWindow.container.Content = new WarehousingRecordsView(); break;
                        case "更换记录": AppData.Instance.MainWindow.container.Content = new ReplaceInforView(); break;
                        case "检修记录": AppData.Instance.MainWindow.container.Content = new OverhaulInforView(); break;
                        case "用户信息": AppData.Instance.MainWindow.container.Content = new UserInforView(); break;
                        default: 
                            break;
                    }
                });
            }
        }


        /// <summary>
        /// 退出事件
        /// </summary>
        public RelayCommand<Window> ExitCommand
        {
            get
            {
                return new RelayCommand<Window>((arg) =>
                {
                    AppData.CurrentUser.UserID = string.Empty;
                    AppData.CurrentUser.Password = string.Empty;
                    LoginWindow mainWindow = new LoginWindow();
                    mainWindow.Show();
                    arg.Close();
                });
            }
        }
    }
}