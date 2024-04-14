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
    public class ReplaceInforViewModel : ViewModelBase
    {

        private CarPartsProvider carPartsProvider = new CarPartsProvider();
        private ReplaceInforProvider replaceInforProvider = new ReplaceInforProvider();
        public AppData AppData { get; set; } = AppData.Instance;


        public ReplaceInforViewModel()
        {
            carParts = carPartsProvider.Select();
            replaceInfor = replaceInforProvider.Select();
            FilteredPartsID = carParts.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
            FilteredCarID = carParts.Select(x => x.CarID).Distinct().OrderBy(x => x).ToList();
            FilteredPartsName = carParts.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();

        }

        private List<ReplaceInfor> replaceInfor;
        public List<ReplaceInfor> ReplaceInfor
        {
            get { return replaceInfor; }
            set
            {
                replaceInfor = value;
                FilteredPartsID = carParts.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
                FilteredCarID = carParts.Select(x => x.CarID).Distinct().OrderBy(x => x).ToList();
                FilteredPartsName = carParts.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
                RaisePropertyChanged(nameof(ReplaceInfor));

            }
        }

        private ReplaceInfor replaceInfor1 = new ReplaceInfor();
        public ReplaceInfor ReplaceInfor1
        {
            get
            {
                return replaceInfor1;
            }
            set
            {
                replaceInfor1 = value;
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



        public ICollectionView FilteredItem => CollectionViewSource.GetDefaultView(replaceInfor);

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



        //刷新功能
        public RelayCommand ClearTextBoxesCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var properties = replaceInfor1.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(replaceInfor1, string.Empty);
                        }
                    }
                    ReplaceInfor = replaceInforProvider.Select();
                    AppData.Instance.MainWindow.container.Content = new ReplaceInforView();

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

                    if (string.IsNullOrEmpty(replaceInfor1.CarID) ||
                    string.IsNullOrEmpty(replaceInfor1.PartsID) ||
                    string.IsNullOrEmpty(replaceInfor1.PartsName) ||
                    string.IsNullOrEmpty(replaceInfor1.ReplaceDate) ||
                    string.IsNullOrEmpty(replaceInfor1.Cost) ||
                    string.IsNullOrEmpty(replaceInfor1.NextChangeExpected) ||
                    string.IsNullOrEmpty(replaceInfor1.UserID) ||
                    string.IsNullOrEmpty(replaceInfor1.ReasonForChange))
                    {
                        MessageBox.Show("请将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string dateString = replaceInfor1.ReplaceDate.Trim();
                    DateTime ChangeDate = DateTime.ParseExact(dateString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    string lifeString = replaceInfor1.NextChangeExpected.Trim();
                    DateTime Life = DateTime.ParseExact(lifeString, new[] { "yyyy/M/dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    TimeSpan difference = Life - ChangeDate;
                    if (difference.TotalDays <= 0)
                    {
                        MessageBox.Show("请正确填写日期", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    carParts1.CarID = replaceInfor1.CarID;
                    carParts1.PartsID = replaceInfor1.PartsID;
                    carParts1.PartsName = replaceInfor1.PartsName;
                    carParts1.NextChangeDate = replaceInfor1.NextChangeExpected;
                    carParts1.ChangeDate = replaceInfor1.ReplaceDate;
                    carParts1.Life = null;

                    var count = new ReplaceInforProvider().Insert(replaceInfor1);
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
                    ReplaceInfor = replaceInforProvider.Select();
                    CarParts = carPartsProvider.Select();
                });
            }
        }



        //删除功能
        public RelayCommand<ReplaceInfor> DelectCommand
        {
            get
            {
                return new RelayCommand<ReplaceInfor>((arg) =>
                {
                    MessageBoxResult dr = MessageBox.Show("是否删除？删除后不可恢复", "删除提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        if (!(arg is ReplaceInfor model)) return;
                        var count = new ReplaceInforProvider().Delete(model);
                    }

                    ReplaceInfor = replaceInforProvider.Select();
                });
            }
        }
    }
}
