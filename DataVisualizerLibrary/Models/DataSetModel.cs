using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualizerLibrary.Models
{
    public class DataSetModel
    {

        public List<DataPointModel> DataPointModels { get; set; } = new List<DataPointModel>();
        public string Name { get; set; }

        public DataSetModel()
        {

        }
        public DataSetModel(string name)
        {
            Name = name;
        }
        
    }
}
