using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualizerLibrary.Models
{
    public class DataCategoryModel
    {
        public string Unit { get; set; } = "Kg";

        public List<DataSetModel> DataSetModels { get; set; } = new List<DataSetModel>();
        public DataCategoryModel()
        {

        }
        public DataCategoryModel(string unit)
        {
            Unit = unit;
        }
    }
    
}
