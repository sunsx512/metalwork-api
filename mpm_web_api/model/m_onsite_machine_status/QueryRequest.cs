using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnsiteStatusWorker.Models
{
    public class QueryRequest
    {
        /// <summary>
        /// 时间序列类型
        /// </summary>
        public class request
        {
            public List<content> targets { get; set; }
            public range range { get; set; }
        }

        public class content {
            public string target { get; set; }
            public string type { get; set; }
        }

        public class range {
            public string from { get; set; }
            public string to { get; set; }
        }
    }
}
