using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.model.m_wo
{
    [SugarTable("work_order.initial_material_info")]
    public class initial_material_info:base_model
    {
        public int worker_order_id { set; get; }
        public int machine_id { set; get; }
        public string materiel { set; get; }
        public decimal count { set; get; }
    }
}
