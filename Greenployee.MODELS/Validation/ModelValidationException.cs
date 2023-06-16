using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.Validation
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException(string error) : base(error)
        {
            
        }

        public static void When(bool hasError, string message)
        {
            if (hasError) { throw new ModelValidationException(message); }
        }

    }
}
