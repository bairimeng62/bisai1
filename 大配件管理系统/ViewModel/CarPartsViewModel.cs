using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using 大配件管理系统.EditView;
using 大配件管理系统.models;
using 大配件管理系统.View;

namespace 大配件管理系统.ViewModel
{
    public class CarPartsViewModel : ViewModelBase
    {
        public AppData AppData { get; set; } = AppData.Instance;
        private PartsInforProvider partsInforProvider = new PartsInforProvider();
        private CarPartsProvider carPartsProvider = new CarPartsProvider();


        public CarPartsViewModel()
        {
            carParts = carPartsProvider.Select(); 
            partsInfor = partsInforProvider.Select();
            FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
            FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();

            //预报警栏文字变化
            UpdateEarlyWarning();
            

        }

        private List<CarParts> carParts;
        public List<CarParts> CarParts
        {
            get { return carParts; }
            set
            {
                carParts = value;
                FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
                FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
                UpdateEarlyWarning();
                RaisePropertyChanged(nameof(CarParts));

            }
        }

        private CarParts carParts1 = new CarParts();
        public CarParts CarParts1
        {
            get
            {
                return carParts1;
            }
            set
            {
                carParts1 = value;
                //预报警栏文字变化
                UpdateEarlyWarning();
                RaisePropertyChanged(nameof(CarParts1));
            }
        }

        private List<PartsInfor> partsInfor;
        public List<PartsInfor> PartsInfor
        {
            get { return partsInfor; }
            set
            {
                partsInfor = value;
                RaisePropertyChanged(nameof(PartsInfor));

            }
        }

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


        private Edit_CarPartsViewMode edit_CarParts = new Edit_CarPartsViewMode();
        public Edit_CarPartsViewMode Edit_CarParts
        {
            get
            {
                return edit_CarParts;
            }
            set
            {
                edit_CarParts = value;
                RaisePropertyChanged();
            }
        }




        //ComboBox输入框的PartsID筛选
        private List<string> filteredPartsID;
        public List<string> FilteredPartsID
        {
            get { return filteredPartsID; }
            set
            {
                filteredPartsID = value;
                RaisePropertyChanged();
            }
        }

        //ComboBox输入框的PartsName筛选
        private List<string> filteredPartsName;
        public List<string> FilteredPartsName
        {
            get { return filteredPartsName; }
            set
            {
                filteredPartsName = value;
                RaisePropertyChanged();
            }
        }

        


        

        //预报警栏文字变化
        private void UpdateEarlyWarning()
        {
            foreach (var part in CarParts)
            {
                
                string lifeString = part.Life.Trim();
                DateTime Life = DateTime.ParseExact(lifeString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                DateTime systemTime = DateTime.Now;

                TimeSpan difference = Life - systemTime;

                if (difference.TotalDays <= 0)
                {
                    part.EarlyWarning = "已超过预检修日期";
                    part.EarlyWarningColor = "Red";
                }
                else if (difference.TotalDays < 183 && difference.TotalDays > 0)
                {
                    part.EarlyWarning = "接近预检修日期";
                    part.EarlyWarningColor = "Orange";
                }
                else
                {
                    part.EarlyWarning = "配件正常使用";
                    part.EarlyWarningColor = "Green";
                }
            }
        }



        //添加功能
        public RelayCommand InsertCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string dateString = CarParts1.ChangeDate.Trim();
                    DateTime ChangeDate = DateTime.ParseExact(dateString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    string lifeString = CarParts1.Life.Trim();
                    DateTime Life = DateTime.ParseExact(lifeString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    string nextChangeDate = CarParts1.NextChangeDate.Trim();
                    DateTime NextChangeDate = DateTime.ParseExact(nextChangeDate, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    TimeSpan difference = Life - ChangeDate;
                    TimeSpan differenc1 = NextChangeDate - ChangeDate;

                    if (difference.TotalDays <= 0 || differenc1.TotalDays <= 0)
                    {
                        MessageBox.Show("请正确填写日期", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(carParts1.PartsID) ||
                    string.IsNullOrEmpty(carParts1.PartsName) ||
                    string.IsNullOrEmpty(carParts1.CarID))
                    {
                        MessageBox.Show("请将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var count = new CarPartsProvider().Insert(carParts1);
                    if (count == 0)
                    {
                        MessageBox.Show("该配件编号已经存在！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        CarParts = carPartsProvider.Select();
                        MessageBox.Show("添加成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        

                    }
                    
                });
            }
        }



        /// <summary>
        /// 刷新功能
        /// </summary>
        public RelayCommand ClearTextBoxesCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var properties = carParts1.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(carParts1, string.Empty);

                        }
                    }
                    AppData.Instance.MainWindow.container.Content = new CarPartsView(); 
                    CarParts = carPartsProvider.Select();


                });
            }
        }

        

        /// <summary>
        /// 删除功能
        /// </summary>
        public RelayCommand<CarParts> DelectCommand
        {
            get
            {
                return new RelayCommand<CarParts>((arg) =>
                {
                    MessageBoxResult dr = MessageBox.Show("是否删除？删除后不可恢复", "删除提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        if (!(arg is CarParts model)) return;
                        var count = new CarPartsProvider().Delete(model);
                    }
                    CarParts = carPartsProvider.Select();
                });
            }
        }




        


        /// <summary>
        /// 打开修改窗口
        /// </summary>
        public RelayCommand<CarParts> OpenEditCommand
        {
            get
            {
                return new RelayCommand<CarParts>((carparts) =>
                {
                    var vm = SimpleIoc.Default.GetInstance<Edit_CarPartsViewMode>();
                    if (vm == null) return;
                    vm.CarParts = carparts;

                    var window = new CarPartsEdit1();
                    window.ShowDialog();
                    CarParts = carPartsProvider.Select();//数据刷新

                });
            }
        }






        public ICollectionView FilteredItem => CollectionViewSource.GetDefaultView(CarParts);

        /// <summary>
        /// CarID搜索框内容获取
        /// </summary>
        private string carIDSearchContent;
        public string CarIDSearchContent
        {
            get
            {
                return carIDSearchContent;
            }
            set
            {
                carIDSearchContent = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// CarID查询功能
        /// </summary>
        public RelayCommand CarIDSearchCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        FilteredItem.Filter = obj =>
                        {
                            var item = obj as CarParts;
                            if (CarPartsIDSearchContent == null)
                            {
                                return item.CarID.Contains(CarIDSearchContent);
                            }
                            else
                            {
                                return item.CarID.Contains(CarIDSearchContent) && item.PartsID.Contains(CarPartsIDSearchContent);
                            }
                        };
                    }
                    catch (Exception ex)
                    {

                    }
                });

            }
        }

        /// <summary>
        /// CarPartsID搜索框内容获取
        /// </summary>
        private string carPartsIDSearchContent;
        public string CarPartsIDSearchContent
        {
            get
            {
                return carPartsIDSearchContent;
            }
            set
            {
                carPartsIDSearchContent = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// CarPartsID查询功能
        /// </summary>
        public RelayCommand CarPartsIDSearchCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        FilteredItem.Filter = obj =>
                        {
                            var item = obj as CarParts;
                            if (CarIDSearchContent == null)
                            {
                                return item.PartsID.Contains(CarPartsIDSearchContent);
                            }
                            else
                            {
                                return item.CarID.Contains(CarIDSearchContent) && item.PartsID.Contains(CarPartsIDSearchContent);
                            }
                        };
                    }
                    catch (Exception ex)
                    {

                    }
                });

            }
        }



    }
}
