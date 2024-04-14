using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 大配件管理系统.models;

namespace 大配件管理系统.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public AppData AppData { get; private set; } = AppData.Instance;

        public LoginViewModel()
        {
           
        }

        /// <summary>
        /// 登录命令
        /// </summary>
        public RelayCommand<Window> LoginCommand
        {
            get
            {
                return new RelayCommand<Window>((arg) =>
                {
                    UserInforProvider userInforProvider = new UserInforProvider();
                    var list = userInforProvider.Select();
                    var user = list.FirstOrDefault(item => item.UserID == AppData.CurrentUser.UserID
                     && item.Password == AppData.CurrentUser.Password);

                    if (user == null)
                    {
                        MessageBox.Show("用户名或密码错误。", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        arg.Close();
                    }
                });
            }
        }
    }
}
