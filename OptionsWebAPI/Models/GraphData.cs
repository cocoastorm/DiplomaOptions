using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptionsWebAPI.Models
{
    public class GraphData
    {
        public List<ChoiceData> ChoicesData { get; set; }
        public List<String> OptionsTitles { get; set; }
    }
}