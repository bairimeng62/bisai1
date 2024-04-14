using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Input;
using 大配件管理系统.models;
using 大配件管理系统.View;
using 大配件管理系统.EditView;
using System.Data.Entity.Validation;

namespace 大配件管理系统.ViewModel
{
    public class PartsInforViewModel:ViewModelBase
    {
        private PartsInforProvider partsInforProvider = new PartsInforProvider();

        public PartsInforViewModel()
        {
            partsInfor = partsInforProvider.Select();
            FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
            FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
            FilteredPartsInforPartsType = partsInfor.Select(x => x.PartsType).Distinct().OrderBy(x => x).ToList();
            FilteredPartsInforPosition = partsInfor.Select(x => x.Position).Distinct().OrderBy(x => x).ToList();
            FilteredPartsInforManufacturer = partsInfor.Select(x => x.Manufacturer).Distinct().OrderBy(x => x).ToList();
        }


        private List<PartsInfor> partsInfor;
        public List<PartsInfor> PartsInfor
        {
            get { return partsInfor; }
            set
            {
                partsInfor = value;
                FilteredPartsID = partsInfor.Select(x => x.PartsID).Distinct().OrderBy(x => x).ToList();
                FilteredPartsName = partsInfor.Select(x => x.PartsName).Distinct().OrderBy(x => x).ToList();
                FilteredPartsInforPartsType = partsInfor.Select(x => x.PartsType).Distinct().OrderBy(x => x).ToList();
                FilteredPartsInforPosition = partsInfor.Select(x => x.Position).Distinct().OrderBy(x => x).ToList();
                FilteredPartsInforManufacturer = partsInfor.Select(x => x.Manufacturer).Distinct().OrderBy(x => x).ToList();
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

        //ComboBox输入框的PartsType筛选
        private List<string> filteredPartsInforPartsType;
        public List<string> FilteredPartsInforPartsType
        {
            get { return filteredPartsInforPartsType; }
            set
            {
                filteredPartsInforPartsType = value;
                RaisePropertyChanged();
                
            }
        }
        
        //ComboBox输入框的Position筛选
        private List<string> filteredPartsInforPosition;
        public List<string> FilteredPartsInforPosition
        {
            get { return filteredPartsInforPosition; }
            set
            {
                filteredPartsInforPosition = value;
                RaisePropertyChanged();
            }
        }

        //ComboBox输入框的Manufacturer筛选
        private List<string> filteredPartsInforManufacturer;
        public List<string> FilteredPartsInforManufacturer
        {
            get { return filteredPartsInforManufacturer; }
            set
            {
                filteredPartsInforManufacturer = value;
                RaisePropertyChanged();
            }
        }





        //添加新的配件
        public RelayCommand InsertPartsInforCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(partsInfor1.PartsID) ||
                    string.IsNullOrEmpty(partsInfor1.PartsName) ||
                    string.IsNullOrEmpty(partsInfor1.PartsType) ||
                    string.IsNullOrEmpty(partsInfor1.Position) ||
                    string.IsNullOrEmpty(partsInfor1.Life) ||
                    string.IsNullOrEmpty(partsInfor1.Manufacturer))
                    {
                        MessageBox.Show("请将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Question);
                        return;
                    }
                    
                    
                    var count = new PartsInforProvider().Insert(partsInfor1);
                    if (count == 0)
                    {
                        MessageBox.Show("该编号已经存在！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("添加成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        PartsInfor = partsInforProvider.Select();
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
                    var properties = partsInfor1.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(partsInfor1, string.Empty);
                            
                        }
                    }
                    AppData.Instance.MainWindow.container.Content = new PartsInforView();
                    PartsInfor = partsInforProvider.Select();

                });
            }
        }



        /// <summary>u
        /// 打开修改窗口
        /// </summary>
        public RelayCommand<PartsInfor> OpenPartsInforEditCommand
        {
            get
            {
                return new RelayCommand<PartsInfor>((partsInfor) =>
                {
                    var vm = SimpleIoc.Default.GetInstance<Edit_PartsInforViewModel>();
                    if (vm == null) return;
                    vm.PartsInfor1 = partsInfor;
                    var window = new PartsInforEdit1();
                    window.ShowDialog();
                    PartsInfor = partsInforProvider.Select();//数据刷新

                });
            }
        }


        /// <summary>
        /// 删除功能
        /// </summary>
        public RelayCommand<PartsInfor> DelectPartsInforCommand
        {
            get
            {
                return new RelayCommand< PartsInfor > ((arg) =>
                {
                    MessageBoxResult dr = MessageBox.Show("是否删除？删除后不可恢复", "删除提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        if (!(arg is PartsInfor model)) return;
                        var count = new PartsInforProvider().Delete(model);
                    }
                    
                    PartsInfor = partsInforProvider.Select();
                });
            }
        }


        public ICollectionView FilteredItem => CollectionViewSource.GetDefaultView(PartsInfor);
        /// <summary>
        /// ID搜索框内容获取
        /// </summary>
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

        /// <summary>
        /// ID查询功能
        /// </summary>
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
                            var item = obj as PartsInfor;
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


        /// <summary>
        /// Name搜索框内容获取
        /// </summary>
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

        /// <summary>
        /// Name查询功能
        /// </summary>
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
                            var item = obj as PartsInfor;
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
