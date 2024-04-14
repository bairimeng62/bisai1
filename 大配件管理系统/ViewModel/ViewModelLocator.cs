/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:大配件管理系统"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;


namespace 大配件管理系统.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<CarPartsViewModel>();
            SimpleIoc.Default.Register<EnterRecordsViewModel>();
            SimpleIoc.Default.Register<IndexViewModel>();
            SimpleIoc.Default.Register<OutRecordsViewModel>();
            SimpleIoc.Default.Register<OverhaulInforViewModel>();
            SimpleIoc.Default.Register<PartsInforViewModel>();
            SimpleIoc.Default.Register<ReplaceInforViewModel>();
            SimpleIoc.Default.Register<UserInforViewModel>();
            SimpleIoc.Default.Register<WarehousingRecordsViewModel>();
            SimpleIoc.Default.Register<Edit_PartsInforViewModel>();
            SimpleIoc.Default.Register<Edit_CarPartsViewMode>();


        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public CarPartsViewModel CarParts
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CarPartsViewModel>();
            }
        }

        public EnterRecordsViewModel EnterRecords
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EnterRecordsViewModel>();
            }
        }

        public IndexViewModel Index
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IndexViewModel>();
            }
        }

        public OutRecordsViewModel OutRecords
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OutRecordsViewModel>();
            }
        }

        public OverhaulInforViewModel OverhaulInfor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OverhaulInforViewModel>();
            }
        }

        public PartsInforViewModel PartsInfor 
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PartsInforViewModel>();
            }
        }

        public ReplaceInforViewModel ReplaceInfor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReplaceInforViewModel>();
            }
        }

        public UserInforViewModel UserInfor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserInforViewModel>();
            }
        }

        public WarehousingRecordsViewModel WarehousingRecords
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WarehousingRecordsViewModel>();
            }
        }

        public Edit_PartsInforViewModel Edit_PartsInfor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Edit_PartsInforViewModel>();
            }
        }

        public Edit_CarPartsViewMode Edit_CarParts
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Edit_CarPartsViewMode>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}