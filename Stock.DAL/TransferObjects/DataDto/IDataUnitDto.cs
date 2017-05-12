using System;
using Stock.Core;

namespace Stock.DAL.TransferObjects
{
    public interface IDataUnitDto
    {
        DateTime GetDate();
        int GetIndexNumber();
        int GetAssetId();
        int GetTimeframeId();
        AnalysisType GetAnalysisType();
    }
}
