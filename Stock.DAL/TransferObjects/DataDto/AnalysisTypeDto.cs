using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.TransferObjects
{
    public class AnalysisTypeDto
    {
        [Key]
        [Column("AnalysisTypeId")]
        public int Id { get; set; }

        [Column("AnalysisTypeName")]
        public string Name { get; set; }


        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(AnalysisTypeDto)) return false;

            AnalysisTypeDto compared = (AnalysisTypeDto)obj;
            if ((compared.Id) != Id) return false;
            if (!compared.Name.Equals(Name)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
