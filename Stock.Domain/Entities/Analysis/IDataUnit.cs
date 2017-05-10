using System;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Entities
{
    public interface IDataUnit
    {
        DateTime GetDate();
        int GetIndexNumber();
    }
}
