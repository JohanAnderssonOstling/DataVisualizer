using DataVisualizerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace DataVisualizerLibrary.FileIO
{
    public class CSVParser : IParser
    {
        public DataModel Parse(string path)
        {

            DataModel DataModel = new DataModel();
            TextReader TextReader = File.OpenText(path);
            string FirstLine = TextReader.ReadLine();
            DataModel.DataCategoryModels = FindDataCategoryModels(FirstLine);

            foreach(DataCategoryModel dataCategoryModel in DataModel.DataCategoryModels)
            {
                dataCategoryModel.DataSetModels = FindDataSetModels(FirstLine, dataCategoryModel.Unit);
            }

            TextReader = File.OpenText(path);
            CsvReader CSVReader = new CsvReader(TextReader);
            CSVReader.Configuration.MissingFieldFound = null;
            CSVReader.Configuration.Delimiter = ",";
        
            CSVReader.Read();
            CSVReader.ReadHeader();
            while (CSVReader.Read())
            {
                foreach(DataCategoryModel dataCategoryModel in DataModel.DataCategoryModels)
                {
                    
                    foreach(DataSetModel dataSetModel in dataCategoryModel.DataSetModels)
                    {
                        
                        
                        DateTime Date = DateTime.Parse(CSVReader.GetField("Date"));
                        
                        double Value = 0;
                        try
                        {
                            Value = CSVReader.GetField<double>(dataSetModel.Name);
                        }
                        catch (CsvHelperException)
                        {
                            Value = double.NaN;
                        }
                        DataPointModel pointModel = new DataPointModel(Date, Value);
                        dataSetModel.DataPointModels.Add(pointModel);
                        
                    }
                }
            }
            
            
            return DataModel;
        }
        private List<DataCategoryModel> FindDataCategoryModels(string line)
        {
            List<DataCategoryModel> DataCategoryModels = new List<DataCategoryModel>();
            
            string[] SplitLine = line.Split(',');
            for(int i = 1; i < SplitLine.Length; i++)
            {
                string Unit = "";
                if(SplitLine[i].Contains("(") && SplitLine[i].Contains(")"))
                    Unit = SplitLine[i].Substring(SplitLine[i].IndexOf("(") + 1, SplitLine[i].IndexOf(")") - SplitLine[i].IndexOf("(") - 1);
                if(!IsDataCategory(DataCategoryModels, Unit))
                {
                    DataCategoryModels.Add(new DataCategoryModel(Unit));
                }

            }
            return DataCategoryModels;
        }
        private List<DataSetModel> FindDataSetModels(string line, string unit)
        {
            List<DataSetModel> DataSetModels = new List<DataSetModel>();
            string[] SplitLine = line.Split(',');
            for(int i = 1; i < SplitLine.Length; i++)
            {
                string Unit = "";
                if(SplitLine[i].Contains("(") && SplitLine[i].Contains(")"))
                     Unit = SplitLine[i].Substring(SplitLine[i].IndexOf("(") + 1, SplitLine[i].IndexOf(")") - SplitLine[i].IndexOf("(") - 1);
                if (Unit.Equals(unit))
                {
                    DataSetModels.Add(new DataSetModel(SplitLine[i]));
                    
                }
            }
            return DataSetModels;
        }
        private bool IsDataCategory(List<DataCategoryModel> dataCategoryModels, string unit)
        {
            foreach(DataCategoryModel dataCategoryModel in dataCategoryModels)
            {
                if (dataCategoryModel.Unit.Equals(unit))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
