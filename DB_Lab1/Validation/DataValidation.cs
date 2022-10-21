using DB_Lab1.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DB_Lab1.Validation
{
    public abstract class DataValidation : ViewModelBase, IDataErrorInfo
    {
        protected List<string> errors = new List<string>();        

        public string this[string columnName]
        {
            get
            {
                Type viewModel = GetType();
                PropertyInfo property = viewModel.GetProperty(columnName);
                errors.Remove(columnName);

                List<ValidationResult> results = new List<ValidationResult>();
                ValidationContext context = new ValidationContext(this) { MemberName = columnName };
                string error = string.Empty;

                object val = property.GetValue(this);

                if (!Validator.TryValidateProperty(val, context, results))
                {                    
                    foreach(ValidationResult err in results)
                    {
                        error += err.ErrorMessage + "\n";                        
                    }

                    errors.Add(columnName);
                }               
                return error;
            }
        }

        public string Error => throw new NotImplementedException();
    }
}
