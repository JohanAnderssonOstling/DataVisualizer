using Caliburn.Micro;
using DataVisualizerLibrary.FileIO;
using DataVisualizerLibrary.Models;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DataVisualizerUIMVVM.ViewModels
{
    public class LineChartViewModel : Screen
    {
        public AxesCollection AxesY { get; set; } = new AxesCollection();
        
        private DataModel dataModel;
        public DataModel DataModel
        {
            get { return dataModel; }
            set { dataModel = value; GenerateLineChart(dataModel); }
        }
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();

        public LineChartViewModel()
        {
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
            for (int i = 0; i < dataCategoryModels.Count; i++)
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
            for (int i = 0; i < dataCategoryModels.Count; i++)
            {
                for (int j = 0; j < dataCategoryModels[i].DataSetModels.Count; j++)
                {
                    LineSeries Series = new LineSeries
                    {
                        Title = dataCategoryModels[i].DataSetModels[j].Name,
                        ScalesYAt = i,
                        Values = new ChartValues<DataPointModel>(dataCategoryModels[i].DataSetModels[j].DataPointModels)
                    };
                    SeriesCollection.Add(Series);
                }
            }
        }
        public void OpenFile()
        {
            OpenFileDialog FileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv"
            };

            Nullable<bool> Result = FileDialog.ShowDialog();
            if (Result == true)
            {
                CSVParser Parser = new CSVParser();
                DataModel = Parser.Parse(FileDialog.FileName);
            }
        }
        public bool CanOpenFile()
        {
            return true;
        }
    }
}
