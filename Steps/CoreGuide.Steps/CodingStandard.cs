using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class CodingStandard
    {
        #region Naming convetion

        /// 1) Add suffix "Async" at the end of every asynchronous method ex: ResgiterAsync
        /// 2) Add suffix "Input" to the model recieved by our endpoints
        /// 3) Add suffix "Output" to the model return from our Endpoints
        /// 4) Add suffix "Request" to the request sent to third party
        /// 5) Add suffix "Response" to the request recieved from the third party
        /// 6) Add suffix "Validator" to the class validating the input
        /// 7) Add suffix "Manager" to the class responsible for handling business logic
        /// 8) Add suffix "Service" to the class responsible for handling business utilities
        /// 9) Add suffix "Configurations" to the class responsible for getting configuration section

        #endregion

        #region Do
        /// 1) Seperate cross cutting concerns logic (ex: "logging" - "filling output" - "mapping") from the manager to thier own utilities
        /// 2) Use access modifier "public" for interface
        /// 3) Use access modifier "Internal" for classes that implement and interface
        /// 4) Use access modifier "Internal" for both utilities interfaces and classes
        /// 5) Use access modifier "Internal" for validation classes
        #endregion

        #region Avoid
        /// 1) Global variables 
        //Race Condition
        #endregion


    }
}
