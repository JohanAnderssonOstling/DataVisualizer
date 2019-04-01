using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace LineDiagram
{
    class DiagramDataSet
    {
        public DiagramDataSet(Color GraphColor, List<double> Data, string Name)
        {
            this.GraphColor = GraphColor;
            this.Data = Data;
            this.Name = Name;
        }
        public Color GraphColor { get; set; }
        public List<double> Data { get; set; }
        public string Name { get; set; }
    }
}
