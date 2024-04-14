using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 大配件管理系统.models;

namespace 大配件管理系统
{
    public class AppData: ObservableObject
    {
        public static AppData Instance = new Lazy<AppData>(() => new AppData()).Value;

        public string systemName = "铁路车辆大配件管理系统";

        public string SystemName
        {
            get { return systemName; }
            set { systemName = value; RaisePropertyChanged(); }
        }

        private UserInfor userInfor = new UserInfor();

        public UserInfor CurrentUser
        {
            get { return userInfor; }
            set { userInfor = value; 
                RaisePropertyChanged("CurrentUser"); }
        }

        public MainWindow MainWindow { get; set; } = null;
    }
}
