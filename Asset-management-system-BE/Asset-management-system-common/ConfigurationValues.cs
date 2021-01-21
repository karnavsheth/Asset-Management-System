using System;
using System.Collections.Generic;

namespace Asset_management_system_common
{
    public class ConfigurationValues
    {
        public List<Variant> ImageVariants { get; set; }
    }

    public class Variant
    {
        public Int32 w { get; set; }
        public Int32 h { get; set; }

    }
}
