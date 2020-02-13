using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_common
{
    public class Shift
    {
        public day day { get; set; }
        public night night { get; set; }
    }

    public class day
    {
        public string start { set; get; }

        public string end { set; get; }
    }
    public class night
    {
        public string start { set; get; }

        public string end { set; get; }
    }

}
