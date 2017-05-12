using System;
using Stock.DAL.TransferObjects;
using Stock.Core;

namespace Stock.Domain.Entities
{
    public interface IDataUnit
    {
        DateTime GetDate();
        int GetIndexNumber();
        int GetAssetId();
        int GetTimeframeId();
        AnalysisType GetAnalysisType();
    }
}
