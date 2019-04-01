using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualizerLibrary.Models
{
    public class DataPointModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public DataPointModel(DateTime dateTime, double value)
        {
            DateTime = dateTime;
            Value = value;
        }
        public DataPointModel()
        {

        }
    }
}
