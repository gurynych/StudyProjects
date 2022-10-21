using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DB_Lab1.Validation
{
    public class StringIsLetterPropertyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string stringProperty)
            {
                if (stringProperty.Any(c => !char.IsLetter(c)))
                {
                    ErrorMessage = "Некорректно заполненное поле";
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
