using System;
using System.Collections.Generic;

namespace Asset_management_system_commonLibarary
{
    public class ConfigurationValues
    {
        public List<Variant> ImageVariants { get; set; }
    }

    public class Variant
    {
        //Height and Width of image variant
        public Int32 w { get; set; }
        public Int32 h { get; set; }

    }
}
