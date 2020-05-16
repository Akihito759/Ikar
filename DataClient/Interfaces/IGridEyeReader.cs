using DataClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataClient.Interfaces
{
    public interface IGridEyeReader
    {
        GridEyeDataModel GetCurrentState();
    }
}
