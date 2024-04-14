using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 大配件管理系统.EditView;
using 大配件管理系统.models;

namespace 大配件管理系统.ViewModel
{
    public class Edit_CarPartsViewMode: ViewModelBase
    {
        

        private CarParts carParts = new CarParts();
        public CarParts CarParts
        {
            get
            {
                return carParts;
            }
            set
            {
                carParts = value;
                RaisePropertyChanged();
            }
        }


        
        
        private void UpdateEarlyWarning()
        {

            
            string lifeString = CarParts.Life.Trim();
            DateTime Life = DateTime.ParseExact(lifeString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime systemTime = DateTime.Now;
            TimeSpan difference = Life - systemTime;

            if (difference.TotalDays <= 0)
                {
                    CarParts.EarlyWarning = "已超过预检修日期";
                    CarParts.EarlyWarningColor = "Red";
                }
                else if (difference.TotalDays <183 && difference.TotalDays > 0)
                {
                    CarParts.EarlyWarning = "接近预检修日期";
                    CarParts.EarlyWarningColor = "Orange";
                }
                else
                {
                    CarParts.EarlyWarning = "配件正常使用";
                    CarParts.EarlyWarningColor = "Green";
                }
            
        }




        private CarParts originalCarParts;  // 添加这个字段
        public CarParts OriginalCarParts
        {
            get { return originalCarParts; }
            set {originalCarParts = value;RaisePropertyChanged();}
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
                    
                    if (string.IsNullOrEmpty(carParts.PartsID) ||
                    string.IsNullOrEmpty(carParts.PartsName) ||
                    string.IsNullOrEmpty(carParts.CarID))
                    {

                        MessageBox.Show("请将输入栏中信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Question);
                        
                        return;
                    }

                    // 保存原始值
                    OriginalCarParts = new CarParts
                    {
                        PartsID = carParts.PartsID,
                        PartsName = carParts.PartsName,
                        CarID = carParts.CarID,
                        // 保存其他属性...
                    };

                    var count = new CarPartsProvider().Update(carParts);
                    if (count == 0)
                    {
                        MessageBox.Show("修改失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        UpdateEarlyWarning();
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

                    // 恢复原始值
                    if (OriginalCarParts != null)
                    {
                        carParts.PartsID = OriginalCarParts.PartsID;
                        carParts.PartsName = OriginalCarParts.PartsName;
                        carParts.CarID = OriginalCarParts.CarID;
                        // 恢复其他属性...
                    }

                    arg.Close();
                });
            }
        }


        


    }
}
