using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using 大配件管理系统.models;
using 大配件管理系统.View;

namespace 大配件管理系统.ViewModel
{
    public class OverhaulInforViewModel : ViewModelBase
    {
        private CarPartsProvider carPartsProvider = new CarPartsProvider();
        private OverhaulInforProvider overhaulInforProvider = new OverhaulInforProvider();
        public AppData AppData { get; set; } = AppData.Instance;


        public OverhaulInforViewModel()
        {
            carParts = carPartsProvider.Select();
            overhaulInfor = overhaulInforProvider.Select();
            FilteredPartsID = carParts.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
            FilteredPartsName = carParts.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
            FilteredCarID = carParts.Select(x => x.CarID).Distinct().OrderBy(x => x).ToList();
        }


        private List<OverhaulInfor> overhaulInfor;
        public List<OverhaulInfor> OverhaulInfor
        {
            get { return overhaulInfor; }
            set
            {
                overhaulInfor = value;
                FilteredPartsName = carParts.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
                FilteredPartsID = carParts.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
                FilteredCarID = carParts.Select(x => x.CarID).Distinct().OrderBy(x => x).ToList();
                RaisePropertyChanged(nameof(OverhaulInfor));

            }
        }

        private OverhaulInfor overhaulInfor1 = new OverhaulInfor();
        public OverhaulInfor OverhaulInfor1
        {
            get
            {
                return overhaulInfor1;
            }
            set
            {
                overhaulInfor1 = value;
                RaisePropertyChanged();
            }
        }


        private List<CarParts> carParts;
        public List<CarParts> CarParts
        {
            get { return carParts; }
            set
            {
                carParts = value;
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

        //ComboBox输入框的PartsID筛选
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

        //ComboBox输入框的CarsId筛选
        private List<string> filteredCarID;
        public List<string> FilteredCarID
        {
            get { return filteredCarID; }
            set
            {
                filteredCarID = value;
                RaisePropertyChanged();
            }
        }


        public ICollectionView FilteredItem => CollectionViewSource.GetDefaultView(overhaulInfor);

        // PartsID搜索框内容获取
        private string IDsearchContent;
        public string IDSearchContent
        {
            get
            {
                return IDsearchContent;
            }
            set
            {
                IDsearchContent = value;
                RaisePropertyChanged();
            }
        }


        // PartsID查询功能
        public RelayCommand SearchIDCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        FilteredItem.Filter = obj =>
                        {
                            var item = obj as OverhaulInfor;
                            if (NameSearchContent == null)
                            {
                                return item.PartsID.Contains(IDSearchContent);
                            }
                            else
                            {
                                return item.PartsID.Contains(IDSearchContent) && item.CarID.Contains(NameSearchContent);
                            }
                        };
                    }
                    catch (Exception ex)
                    {

                    }
                });

            }
        }



        // CarID搜索框内容获取
        private string NamesearchContent;
        public string NameSearchContent
        {
            get
            {
                return NamesearchContent;
            }
            set
            {
                NamesearchContent = value;
                RaisePropertyChanged();
            }
        }


        // CarID查询功能
        public RelayCommand SearchNameCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        FilteredItem.Filter = obj =>
                        {
                            var item = obj as OverhaulInfor;
                            if (IDSearchContent == null)
                            {
                                return item.CarID.Contains(NameSearchContent);
                            }
                            else
                            {
                                return item.PartsID.Contains(IDSearchContent) && item.CarID.Contains(NameSearchContent);
                            }
                        };
                    }
                    catch (Exception ex)
                    {

                    }
                });

            }
        }


        //预报警栏文字变化
        private void UpdateEarlyWarning()
        {
            string lifeString = carParts1.Life.Trim();
            DateTime Life = DateTime.ParseExact(lifeString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime systemTime = DateTime.Now;
            TimeSpan difference = Life - systemTime;

            if (difference.TotalDays <= 0)
            {
                carParts1.EarlyWarning = "已超过预检修日期";
                carParts1.EarlyWarningColor = "Red";
            }
            else if (difference.TotalDays < 183 && difference.TotalDays > 0)
            {
                carParts1.EarlyWarning = "接近预检修日期";
                carParts1.EarlyWarningColor = "Orange";
            }
            else
            {
                carParts1.EarlyWarning = "配件正常使用";
                carParts1.EarlyWarningColor = "Green";
            }
        }



        //刷新功能
        public RelayCommand ClearTextBoxesCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var properties = overhaulInfor1.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(overhaulInfor1, string.Empty);
                        }
                    }
                    OverhaulInfor = overhaulInforProvider.Select();
                    AppData.Instance.MainWindow.container.Content = new OverhaulInforView();

                });
            }
        }


        //添加功能
        public RelayCommand InsertCommand
        {
            get
            {
                return new RelayCommand(() =>
                {

                    if (string.IsNullOrEmpty(overhaulInfor1.CarID) ||
                    string.IsNullOrEmpty(overhaulInfor1.PartsID) ||
                    string.IsNullOrEmpty(overhaulInfor1.PartsName) ||
                    string.IsNullOrEmpty(overhaulInfor1.MaintenanceType) ||
                    string.IsNullOrEmpty(overhaulInfor1.MaintenanceOutcome) ||
                    string.IsNullOrEmpty(overhaulInfor1.MaintenancePerformedBy) ||
                    string.IsNullOrEmpty(overhaulInfor1.MaintenanceCost) ||
                    string.IsNullOrEmpty(overhaulInfor1.MaintenanceDate) ||
                    string.IsNullOrEmpty(overhaulInfor1.NextMaintenanceDate))
                    {
                        MessageBox.Show("请将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string dateString = OverhaulInfor1.MaintenanceDate.Trim();
                    DateTime ChangeDate = DateTime.ParseExact(dateString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    string lifeString = overhaulInfor1.NextMaintenanceDate.Trim();
                    DateTime Life = DateTime.ParseExact(lifeString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    TimeSpan difference = Life - ChangeDate;
                    if (difference.TotalDays <= 0)
                    {
                        MessageBox.Show("请正确填写日期", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    carParts1.CarID = overhaulInfor1.CarID;
                    carParts1.PartsID = overhaulInfor1.PartsID;
                    carParts1.PartsName = overhaulInfor1.PartsName;
                    carParts1.Life = overhaulInfor1.NextMaintenanceDate;
                    carParts1.NextChangeDate = null;
                    UpdateEarlyWarning();
                    var count = new OverhaulInforProvider().Insert(overhaulInfor1);
                    var ad = new CarPartsProvider().Update(carParts1);

                    if (count == 0)
                    {
                        MessageBox.Show("操作失败", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    if (count == -1)
                    {
                        MessageBox.Show("系统未记录此车辆", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else 
                    {
                        MessageBox.Show("添加成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    OverhaulInfor = overhaulInforProvider.Select();
                    CarParts = carPartsProvider.Select();

                });
            }
        }


        //删除功能
        public RelayCommand<OverhaulInfor> DelectCommand
        {
            get
            {
                return new RelayCommand<OverhaulInfor>((arg) =>
                {
                    MessageBoxResult dr = MessageBox.Show("是否删除？删除后不可恢复", "删除提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        if (!(arg is OverhaulInfor model)) return;
                        var count = new OverhaulInforProvider().Delete(model);
                    }

                    OverhaulInfor = overhaulInforProvider.Select();
                });
            }
        }
    }
}
