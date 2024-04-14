using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 大配件管理系统.models;
using 大配件管理系统.ViewModel;

namespace 大配件管理系统.View
{
    /// <summary>
    /// IndexView.xaml 的交互逻辑
    /// </summary>
    public partial class IndexView : UserControl
    {
        public IndexView()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                var vm=DataContext as IndexViewModel;

                var warehousingProvider = new WarehousingRecordsProvider();
                var warehousing = warehousingProvider.Select();

                var enterRecordsProvider = new EnterRecordsProvider();
                var enterRecords = enterRecordsProvider.Select();

                var outRecordsProvider = new OutRecordsProvider();
                var outRecords = outRecordsProvider.Select();



                vm.WarehousingLabels.Clear();
                vm.WarehousingValues.Clear();
                vm.PieSeries.Clear();
                vm.LineSeries.Clear();
                vm.LineAxiesX.Clear();
                vm.LineSeries1.Clear();
                vm.LineAxiesX1.Clear();

                warehousing.ForEach(warehouse => 
                {

                    vm.WarehousingLabels.Add(warehouse.PartsName);
                    vm.WarehousingValues.Add(double.Parse(warehouse.Number));
                    vm.PieSeries.Add(new PieSeries() 
                    { Title = warehouse.PartsName, 
                        Values = new ChartValues<double>() 
                        { double.Parse(warehouse.Number) 
                        } 
                    });

                });


                //显示每个配件的Enter图
                warehousing.ForEach(warehouse => 
                {
                    var list = enterRecords.FindAll(item => item.PartsName == warehouse.PartsName);
                    var serise = new LineSeries();
                    serise.Title= warehouse.PartsName;
                    serise.Values = new ChartValues<double>();
                    Axis axis = new Axis();
                    axis.ShowLabels = true;
                    axis.Labels = new List<string>();
                    list.ForEach(enterRecord => 
                    {
                        axis.Labels.Add(enterRecord.EnterTime.ToString());
                        serise.Values.Add(double.Parse(enterRecord.Number));
                    });
                    vm.LineSeries.Add(serise);
                    vm.LineAxiesX.Add(axis);

                });


                //显示每个配件的Out图
                warehousing.ForEach(warehouse1 =>
                {
                    var list = outRecords.FindAll(item => item.PartsName == warehouse1.PartsName);
                    var serise = new LineSeries();
                    serise.Title = warehouse1.PartsName;
                    serise.Values = new ChartValues<double>();
                    Axis axis1 = new Axis();
                    axis1.ShowLabels = true;
                    axis1.Labels = new List<string>();
                    list.ForEach(outRecord =>
                    {
                        axis1.Labels.Add(outRecord.OutTime.ToString());
                        serise.Values.Add(double.Parse(outRecord.Number));
                    });
                    vm.LineSeries1.Add(serise);
                    vm.LineAxiesX1.Add(axis1);

                });
            };
        }
    }
}
