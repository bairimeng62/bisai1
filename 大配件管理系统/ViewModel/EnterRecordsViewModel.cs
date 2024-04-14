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

namespace 大配件管理系统.ViewModel
{
    public class EnterRecordsViewModel : ViewModelBase
    {
        private PartsInforProvider partsInforProvider = new PartsInforProvider();
        private EnterRecordsProvider enterRecordsProvider = new EnterRecordsProvider();
        private WarehousingRecordsProvider warehousingRecordsProvider = new WarehousingRecordsProvider();
        public AppData AppData { get; set; } = AppData.Instance;


        public EnterRecordsViewModel()
        {
            partsInfor = partsInforProvider.Select();
            enterRecords = enterRecordsProvider.Select();
            FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
            FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
            FilteredManufacturer = partsInfor.Select(x => x.Manufacturer).Distinct().OrderBy(x => x).ToList();
            RaisePropertyChanged(nameof(WarehousingRecords));

        }


        private List<EnterRecords> enterRecords;
        public List<EnterRecords> EnterRecords
        {
            get { return enterRecords; }
            set
            {
                enterRecords = value;
                FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
                FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
                FilteredManufacturer = partsInfor.Select(x => x.Manufacturer).Distinct().OrderBy(x => x).ToList();
                RaisePropertyChanged(nameof(WarehousingRecords));
                RaisePropertyChanged(nameof(EnterRecords));

            }
        }

        private EnterRecords enterRecords1 = new EnterRecords();
        public EnterRecords EnterRecords1
        {
            get
            {
                return enterRecords1;
            }
            set
            {
                enterRecords1 = value;
                RaisePropertyChanged(nameof(WarehousingRecords));
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


        private List<WarehousingRecords> warehousingRecords;
        public List<WarehousingRecords> WarehousingRecords
        {
            get { return warehousingRecords; }
            set
            {
                warehousingRecords = value;
                RaisePropertyChanged(nameof(warehousingRecords));

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
                RaisePropertyChanged(nameof(warehousingRecords1));
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



        

        //添加功能
        public RelayCommand InsertCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string purchasePriceStr = enterRecords1.PurchasePrice;
                    if (string.IsNullOrEmpty(enterRecords1.PartsID) ||
                    string.IsNullOrEmpty(enterRecords1.PartsName) ||
                    string.IsNullOrEmpty(enterRecords1.Warehouse)||
                    string.IsNullOrEmpty(purchasePriceStr))
                    {
                        MessageBox.Show("请将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    enterRecords1.UserID = AppData.Instance.CurrentUser.UserID;
                    var count = new EnterRecordsProvider().Insert(enterRecords1);
                    if (count == 0)
                    {
                        MessageBox.Show("操作失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    warehousingRecords1.PartsID = enterRecords1.PartsID;
                    warehousingRecords1.PartsName = enterRecords1.PartsName;
                    warehousingRecords1.Warehouse = enterRecords1.Warehouse;
                    warehousingRecords1.Number = enterRecords1.Number;
                    warehousingRecords1.PurchasePrice = enterRecords1.PurchasePrice;
                    warehousingRecords1.WarehouseLocation = enterRecords1.WarehouseLocation;
                    warehousingRecords1.SafetyStockLevel = enterRecords1.SafetyStockLevel;
                    warehousingRecords1.Remarks = enterRecords1.Remarks;
                    var ad = new WarehousingRecordsProvider().Insert(warehousingRecords1);
                    MessageBox.Show("添加成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    EnterRecords = enterRecordsProvider.Select();
                    WarehousingRecords = warehousingRecordsProvider.Select();
                    

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
                    var properties = enterRecords1.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(enterRecords1, string.Empty);
                        }
                    }
                    AppData.Instance.MainWindow.container.Content = new EnterRecordsView();
                    EnterRecords = enterRecordsProvider.Select();

                });
            }
        }


        //删除功能
        public RelayCommand<EnterRecords> DelectCommand
        {
            get
            {
                return new RelayCommand<EnterRecords>((arg) =>
                {
                    MessageBoxResult dr = MessageBox.Show("是否删除？删除后不可恢复", "删除提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        if (!(arg is EnterRecords model)) return;
                        var count = new EnterRecordsProvider().Delete(model);
                    }

                    EnterRecords = enterRecordsProvider.Select();
                });
            }
        }



        public ICollectionView FilteredItem => CollectionViewSource.GetDefaultView(enterRecords);

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
                            var item = obj as EnterRecords;
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
                            var item = obj as EnterRecords;
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
    }
}
