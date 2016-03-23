using DiplomaDataModel.Models.CustomValidations;
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
    [OptionsUnique()]
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        public int YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        public YearTerm YearTerm { get; set; }

        [RegularExpression(@"^[Aa]00[0-9]{6}$", ErrorMessage = "Invalid student ID format")]
        [Display(Name = "Student ID")]
        public string StudentId { get; set; }

        [Required]
        [StringLength(40,
        ErrorMessage = "The {0} must be between {2} and {1} characters.",
        MinimumLength = 1)]
        [Display(Name = "First Name ")]
        public string StudentFirstName { get; set; }

        [Required]
        [StringLength(40,
        ErrorMessage = "The {0} must be between {2} and {1} characters.",
        MinimumLength = 1)]
        [Display(Name = "Last Name ")]
        public string StudentLastName { get; set; }

        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }

        [Display(Name = "First Choice")]
        [ForeignKey("FirstChoiceOptionId")]
        public Option FirstOption { get; set; }

        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }

        [Display(Name = "Second Choice")]
        [ForeignKey("SecondChoiceOptionId")]
        public Option SecondOption { get; set; }

        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }

        [Display(Name = "Third Choice")]
        [ForeignKey("ThirdChoiceOptionId")]
        public Option ThirdOption { get; set; }

        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }

        [Display(Name = "Fourth Choice")]
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
