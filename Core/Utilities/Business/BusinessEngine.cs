using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business {
    public class BusinessEngine {
        public static IResult Run(params IResult[] results) {
            foreach (var result in results) {
                if (!result.Success) {
                    return result;  // Return first error result
                }
            }
            return null;  // No errors
        }
    }
}
