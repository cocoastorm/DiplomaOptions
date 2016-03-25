using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptionsWebAPI.Models
{
    public class ChoiceData
    {
        public int YearTermId { get; set; }
        public int[] FirstChoice { get; set; }
        public int[] SecondChoice { get; set; }
        public int[] ThirdChoice { get; set; }
        public int[] FourthChoice { get; set; }
    }
}