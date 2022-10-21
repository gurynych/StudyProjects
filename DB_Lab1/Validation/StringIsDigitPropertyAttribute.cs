using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DB_Lab1.Validation
{
    public class StringIsDigitPropertyAttribute : ValidationAttribute
    {        
        public override bool IsValid(object value)
        {
            if (value is string stringProperty)
            {
                if (stringProperty.Any(c => !char.IsDigit(c)))
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
