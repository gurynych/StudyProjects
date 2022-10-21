using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DB_Lab1.Validation
{
    public class StringIsLetterOrDigitPropertyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string stringProperty)
            {
                if (stringProperty.Any(c => !char.IsLetterOrDigit(c)))
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
