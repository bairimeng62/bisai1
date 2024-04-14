using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 大配件管理系统.ViewModel
{
    public class IndexViewModel : ViewModelBase
    {
        private List<string> warehousingLabels = new List<string>();

        //warehousing的X轴标签
        public List<string> WarehousingLabels
        {
            get { return warehousingLabels; }
            set { warehousingLabels = value;RaisePropertyChanged(); }
        }

        private IChartValues warehousingValues = new ChartValues<double>();

        //warehousing的报表值
        public IChartValues WarehousingValues
        {
            get { return warehousingValues; }
            set { warehousingValues = value; RaisePropertyChanged(); }
        }


        //warehousing的饼状图
        public SeriesCollection PieSeries { get; set; } = new SeriesCollection();


        //Enter的曲线报表
        public SeriesCollection LineSeries { get; set; } = new SeriesCollection();
        //Enter的曲线报表标签
        public AxesCollection LineAxiesX { get; set; } = new AxesCollection();


        //Out的曲线报表
        public SeriesCollection LineSeries1 { get; set; } = new SeriesCollection();
        //Out的曲线报表标签
        public AxesCollection LineAxiesX1 { get; set; } = new AxesCollection();
    }
}
