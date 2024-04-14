using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 大配件管理系统.EditView;
using 大配件管理系统.models;
using 大配件管理系统.View;
using 大配件管理系统.Windows;

namespace 大配件管理系统.ViewModel
{
    public class Edit_PartsInforViewModel: ViewModelBase
    {
        private PartsInfor partsInfor1 = new PartsInfor();

        public PartsInfor PartsInfor1
        {
            get
            {
                return partsInfor1;
            }
            set
            {
                partsInfor1 = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 修改功能
        /// </summary>
        public RelayCommand<Window> EditPartsInforCommand
        {
            get
            {
                return new RelayCommand<Window>((arg) =>
                {
                    if (string.IsNullOrEmpty(partsInfor1.PartsID) ||
                    string.IsNullOrEmpty(partsInfor1.PartsName) ||
                    string.IsNullOrEmpty(partsInfor1.PartsType) ||
                    string.IsNullOrEmpty(partsInfor1.Position) ||
                    string.IsNullOrEmpty(partsInfor1.Life) ||string.IsNullOrEmpty(partsInfor1.Manufacturer))
                    {
                        MessageBox.Show("请将输入栏中信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Question);
                        return;
                    }
                    
                    var count = new PartsInforProvider().Update(partsInfor1);
                    if (count == 0)
                    {
                        MessageBox.Show("修改失败");
                    }
                    else
                    {
                        arg.Close();
                    }

                });
            }
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        public RelayCommand<Window> ExitCommand
        {
            get
            {
                return new RelayCommand<Window>((arg) =>
                {
                    if (arg == null) return;
                    arg.Close();
                });
            }
        }

        public PartsInforEdit1 Edit_PartsInfor { get; internal set; }
    }
}
