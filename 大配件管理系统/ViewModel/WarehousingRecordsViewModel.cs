using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using 大配件管理系统.models;
using 大配件管理系统.View;
using System.Collections.ObjectModel;

namespace 大配件管理系统.ViewModel
{
    public class WarehousingRecordsViewModel : ViewModelBase
    {
        private PartsInforProvider partsInforProvider = new PartsInforProvider();
        private WarehousingRecordsProvider warehousingRecordsProvider = new WarehousingRecordsProvider();
        public AppData AppData { get; set; } = AppData.Instance;


        public WarehousingRecordsViewModel()
        {
            partsInfor = partsInforProvider.Select();
            warehousingRecords = warehousingRecordsProvider.Select();
            FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
            FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
            FilteredManufacturer = partsInfor.Select(x => x.Manufacturer).Distinct().OrderBy(x => x).ToList();
        }


        private List<WarehousingRecords> warehousingRecords;
        public List<WarehousingRecords> WarehousingRecords
        {
            get { return warehousingRecords; }
            set
            {
                warehousingRecords = value;
                FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
                FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
                FilteredManufacturer = partsInfor.Select(x => x.Manufacturer).Distinct().OrderBy(x => x).ToList();
                RaisePropertyChanged(nameof(WarehousingRecords));

            }
        }

        private WarehousingRecords warehousingRecords1 = new WarehousingRecords();
        public WarehousingRecords WarehousingRecords1
        {
            get
            {
                return warehousingRecords1;
            }
            set
            {
                warehousingRecords1 = value;
                RaisePropertyChanged();
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

        //ComboBox输入框的Manufacturer筛选
        private List<string> filteredManufacturer;
        public List<string> FilteredManufacturer
        {
            get { return filteredManufacturer; }
            set
            {
                filteredManufacturer = value;
                RaisePropertyChanged();
            }
        }


        public ICollectionView FilteredItem => CollectionViewSource.GetDefaultView(warehousingRecords);

        // ID搜索框内容获取
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


        // ID查询功能
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
                            var item = obj as WarehousingRecords;
                            if (NameSearchContent == null)
                            {
                                return item.PartsID.Contains(IDSearchContent);
                            }
                            else
                            {
                                return item.PartsID.Contains(IDSearchContent) && item.PartsName.Contains(NameSearchContent);
                            }
                        };
                    }
                    catch (Exception ex)
                    {

                    }
                });

            }
        }



        // Name搜索框内容获取
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


        // Name查询功能
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
                            var item = obj as WarehousingRecords;
                            if (IDSearchContent == null)
                            {
                                return item.PartsName.Contains(NameSearchContent);
                            }
                            else
                            {
                                return item.PartsID.Contains(IDSearchContent) && item.PartsName.Contains(NameSearchContent);
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
                    var properties = warehousingRecords1.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(warehousingRecords1, string.Empty);
                        }
                    }
                    AppData.Instance.MainWindow.container.Content = new WarehousingRecordsView();
                    WarehousingRecords = warehousingRecordsProvider.Select();

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
                    string purchasePriceStr = warehousingRecords1.PurchasePrice;
                    if (string.IsNullOrEmpty(warehousingRecords1.PartsID) ||
                    string.IsNullOrEmpty(warehousingRecords1.PartsName) ||
                    string.IsNullOrEmpty(warehousingRecords1.Warehouse) ||
                    string.IsNullOrEmpty(purchasePriceStr))
                    {
                        MessageBox.Show("请将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var count = new WarehousingRecordsProvider().Insert(warehousingRecords1);
                    if (count == 0)
                    {
                        MessageBox.Show("该配件存在,已自动完成添加", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("添加成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    WarehousingRecords = warehousingRecordsProvider.Select();
                });
            }
        }


        //删除功能
        public RelayCommand<WarehousingRecords> DelectCommand
        {
            get
            {
                return new RelayCommand<WarehousingRecords>((arg) =>
                {
                    MessageBoxResult dr = MessageBox.Show("是否删除？删除后不可恢复", "删除提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        if (!(arg is WarehousingRecords model)) return;
                        var count = new WarehousingRecordsProvider().Delete(model);
                    }

                    WarehousingRecords = warehousingRecordsProvider.Select();
                });
            }
        }
    }
}
