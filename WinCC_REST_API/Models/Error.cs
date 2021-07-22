using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinCC_REST_API.Models
{
    public class Error
    {
        public string errorValue { get; set; }
    }

    public class Error_example : IExamplesProvider //ns=2i=1
    {
        public object GetExamples()
        {
            return new Error
            {
                errorValue = "Verbindung zu OPC UA Alarms & Conditions Server nicht hergestellt"
            };
        }
    }
}