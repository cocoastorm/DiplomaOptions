using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    public class YearTerm
    {
        [Key]
        public int YearTermId { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        public bool IsDefault { get; set; }

        public string getTermString
        {
            get
            {
                switch (this.Term)
                {
                    case 10:
                        return "Winter";
                    case 20:
                        return "Spring/Summer";
                    case 30:
                        return "Fall";
                }
                return "unknown";
            }
        }
    }
}
