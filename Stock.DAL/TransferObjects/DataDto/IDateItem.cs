using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public interface IDateItem
    {
        DateTime PriceDate { get; set; }
    }
}
