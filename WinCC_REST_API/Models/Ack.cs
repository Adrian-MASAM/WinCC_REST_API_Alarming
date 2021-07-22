using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinCC_REST_API.Models
{
    public class Ack
    {
        public string ConditionName { get; set; }
    }

    public class Ack_example : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Ack
            {
                ConditionName = "1"
            };
        }
    }
}