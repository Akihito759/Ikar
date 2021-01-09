using DataClient.Models;

namespace DataClient.Interfaces
{
    public interface IGridEyeReader
    {
        GridEyeDataModel GetCurrentState();
    }
}
