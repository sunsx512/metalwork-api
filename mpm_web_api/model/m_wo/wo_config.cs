using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model
{
    [SugarTable("work_order.wo_config")]
    public class wo_config:base_model
    {
        /// <summary>
        /// 工单名
        /// </summary>
        public string work_order { get; set; }
        /// <summary>
        /// 机种号
        /// </summary>
        public string part_num { get; set; }
        /// <summary>
        /// 班别 0:白班 1:夜班
        /// </summary>
        public int shift { get; set; }
        /// <summary>
        /// 标准数量
        /// </summary>
        public int standard_num { get; set; }
        /// <summary>
        /// 是否自动完结
        /// </summary>
        public bool auto { get; set; }
        /// <summary>
        /// 执行序号
        /// </summary>
        public int order_index { get; set; }
        /// <summary>
        /// 状态 0:创建 1:排产 2:执行中  3:完成
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 虚拟线id
        /// </summary>
        public int virtual_line_id { get; set; }
        /// <summary>
        /// 标准时间  格式为 200;100;100
        /// </summary>
        public string standard_time { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_time { get; set; }
        /// <summary>
        /// 平衡率公式
        /// </summary>
        public string lbr_formula { get; set; }

    }

    public class wo_config_detail : wo_config
    {
        /// <summary>
        /// 虚拟线信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual_line virtual_Line {set; get;}

    }

    public class wo_config_excute : wo_config_detail
    {
        /// <summary>
        /// 虚拟线日志信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual_line_log virtual_Line_log { set; get; }

    }

}
