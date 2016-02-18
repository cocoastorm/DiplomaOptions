using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models.CustomValidations
{
    class OptionsUnique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HashSet<int> options = new HashSet<int>();
            // add dropdown selections to the hashset
            Choice c = (Choice) value;
            options.Add((int)c.FirstChoiceOptionId);
            options.Add((int)c.SecondChoiceOptionId);
            options.Add((int)c.ThirdChoiceOptionId);
            options.Add((int)c.FourthChoiceOptionId);
            // check to see if the four options chosen are unique
            if(options.Count == 4)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("You cannot select duplicate options");
            }
        }
    }
}
