using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class ExtremumGroup
    {
        public DataItem master { get; set; }
        public DataItem slave { get; set; }
        public ExtremumType type { get; set; }
    }
}
