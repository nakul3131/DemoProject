using DemoProject.Services.Concrete;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class UniqueInformationNumber : ValidationAttribute
    {
        EFDbContext context1 = new EFDbContext();
        
        //protected bool IsMatch(string uniqueNumber)
        //{
        //    if (context.People.Where(p => p.PersonInformationNumber == uniqueNumber))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                long uniqueNumber = Convert.ToInt64(value);
                //var ss = context.People;
                if (value != null)
                {
                    context1.People.Where(c => c.PersonInformationNumber == uniqueNumber).FirstOrDefault();
                    return new ValidationResult("number is already exist");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }
}
