using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPIs.Areas.HelpPage
{
    public class DynamicUriParameterAttribute : Attribute
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
    }
    public class APIRequestAttribute : Attribute
    {
        public string Descrption { get; set; }
    }
}