using BudgetApp.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Models
{
    public class TypeAccount
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The {0} field is requierd.")]
        [FirstLetterUppercase]
        [Remote(action: "VerifyExistAccountType", controller: "TypesAccount")]
        public string Name { get; set; }
        public int UserId { get; set; }
        public int OrderBy { get; set; }


        #region Other Validation
        /* Other validations*/

        //[Required(ErrorMessage = "The {0} field is requierd.")]
        //[EmailAddress(ErrorMessage = "The email must be a valid one.")]
        //public string Email { get; set; }

        //[Range(minimum:18, maximum:130, ErrorMessage = "The value must be between {1} and {2}" )]
        //public int Age { get; set; }

        //[Url(ErrorMessage = "The field must be a URL valid.")]
        //public string URL { get; set; }

        //[CreditCard(ErrorMessage = "The credit card is not valid.")]
        //[Display(Name = "Credit Card")]
        //public string CreditCard { get; set; }
        #endregion
    }
}
