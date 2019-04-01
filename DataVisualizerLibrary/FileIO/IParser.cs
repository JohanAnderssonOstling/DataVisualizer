using DataVisualizerLibrary.Models;

namespace DataVisualizerLibrary.FileIO
{
    public interface IParser
    {
        DataModel Parse(string path);
    }
}