using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DiplomaDataModel.Models
{
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        public int YearTermId { get; set; }
        [ForeignKey("YearTermId")]
        public YearTerm YearTerm { get; set; }

        [ReadOnly(true)]
        [StringLength(9,
        ErrorMessage = "The {0} must be between {2} and {1} characters.")]
        public string StudentId { get; set; }

        [Required]
        [StringLength(40,
        ErrorMessage = "The {0} must be between {2} and {1} characters.",
        MinimumLength = 5)]
        [Display(Name = "First Name: ")]
        public string StudentFirstName { get; set; }

        [Required]
        [StringLength(40,
        ErrorMessage = "The {0} must be between {2} and {1} characters.",
        MinimumLength = 5)]
        [Display(Name = "Last Name: ")]
        public string StudentLastName { get; set; }

        [Display(Name = "First Choice: ")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }

        [ForeignKey("FirstChoiceOptionId")]
        public Option FirstOption { get; set; }

        [Display(Name = "Second Choice: ")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }

        [ForeignKey("SecondChoiceOptionId")]
        public Option SecondOption { get; set; }

        [Display(Name = "Third Choice: ")]
        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }

        [ForeignKey("ThirdChoiceOptionId")]
        public Option ThirdOption { get; set; }

        [Display(Name = "Fourth Choice: ")]
        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }

        [ForeignKey("FourthChoiceOptionId")]
        public Option FourthOption { get; set; }

        private DateTime _SelectionDate = DateTime.MinValue;

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        public DateTime SelectionDate
        {
            get
            {
                return (_SelectionDate == DateTime.MinValue) ? DateTime.Now : _SelectionDate;
            }
            set { _SelectionDate = value; }
        }
    }
}
