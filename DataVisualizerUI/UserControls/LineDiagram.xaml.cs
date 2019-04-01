using DataVisualizerLibrary.FileIO;
using DataVisualizerLibrary.Models;
using LiveCharts;
using LiveCharts.Configurations;
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

namespace DataVisualizerUI.UserControls
{
    public partial class LineDiagram : UserControl
    {
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();
        public AxesCollection AxesY { get; set; } = new AxesCollection();
       

        private DataModel dataModel;
        public DataModel DataModel
        {
            get { return dataModel; }
            set { dataModel = value; GenerateLineChart(dataModel); }
        }

        public LineDiagram()
        {
            InitializeComponent();
            DataContext = this;

            var dayConfig = Mappers.Xy<DataPointModel>()
            .X(dateModel => dateModel.DateTime.Ticks / TimeSpan.FromDays(1).Ticks)
            .Y(dateModel => dateModel.Value);

            SeriesCollection = new SeriesCollection(dayConfig);
            Formatter = value => new DateTime((long)(Math.Abs(value) * TimeSpan.FromDays(1).Ticks)).ToString("d");
        }
        public void GenerateLineChart(DataModel dataModel)
        {
            SeriesCollection.Clear();
            AxesY.Clear();

            GenerateYAxis(dataModel.DataCategoryModels);
            GenerateSeries(dataModel.DataCategoryModels);
            
        }
        private void GenerateYAxis(List<DataCategoryModel> dataCategoryModels)
        {
            for(int i = 0; i < dataCategoryModels.Count; i++)
            {
                Axis YAxis = new Axis
                {
                    Title = dataCategoryModels[i].Unit,
                };
                if (i % 2 == 0)
                    YAxis.Position = AxisPosition.LeftBottom;
                else
                    YAxis.Position = AxisPosition.RightTop;
                AxesY.Add(YAxis);
            }
        }
        private void GenerateSeries(List<DataCategoryModel> dataCategoryModels)
        {
            for(int i = 0; i < dataCategoryModels.Count; i++)
            {
                for(int j = 0; j < dataCategoryModels[i].DataSetModels.Count; j++)
                {
                    LineSeries Series = new LineSeries();
                    Series.Title=dataCategoryModels[i].DataSetModels[j].Name;
                    Series.ScalesYAt = i;
                    Series.Values = new ChartValues<DataPointModel> (dataCategoryModels[i].DataSetModels[j].DataPointModels);
                    SeriesCollection.Add(Series);
                }
            }
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog FileDialog = new Microsoft.Win32.OpenFileDialog();
            FileDialog.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> Result = FileDialog.ShowDialog();
            if(Result == true)
            {
                CSVParser Parser = new CSVParser();
                Console.WriteLine(FileDialog.FileName);
                DataModel = Parser.Parse(FileDialog.FileName);
                
            }
        }
    }
}
