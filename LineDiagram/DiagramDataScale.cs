using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDiagram
{
    class DiagramDataScale
    {

        public DiagramDataScale(double LowestValue, double HighestVale, List<DiagramDataSet> DiagramDatasets, string Unit)
        {
            this.LowestValue = LowestValue;
            this.HighestValue = HighestValue;
            this.DiagramDataSets = DiagramDataSets;
            this.Unit = Unit;
        }
        public double LowestValue { get; set; }
        public double HighestValue { get; set; }
        public List<DiagramDataSet> DiagramDataSets { get; set; }
        public string Unit { get; set; }
        


    }
}
