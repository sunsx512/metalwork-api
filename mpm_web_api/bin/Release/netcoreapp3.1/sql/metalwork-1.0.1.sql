INSERT INTO "common"."wechart_server" VALUES (1, '', '', '', '','2020-05-05 00:00:00');
INSERT INTO "common"."email_server" VALUES (1, '', 163, '', '');

INSERT INTO "common"."area_layer" (name_cn, name_en, name_tw, description, calculate_avail) VALUES ('群组', 'Group', '群组','群组', false);
INSERT INTO "common"."area_node" (area_layer_id, upper_id, name_cn, name_en, name_tw, description) VALUES (1,0,'群组01', 'group01', '群组01', '群组01');
INSERT INTO "common"."area_property" (area_node_id, name_cn, name_en, name_tw, format,description) VALUES (1,'时区','time_zone', '时区', 8, '时区');

INSERT INTO "common"."tag_type" (name_cn, name_en, name_tw, description) VALUES ('设备', 'Machine', '設備', '设备标签类型');
INSERT INTO "common"."tag_type" (name_cn, name_en, name_tw, description) VALUES ('节拍时间', 'Cycle_Time', '節拍時間', '节拍时间类标签类型');
INSERT INTO "common"."tag_type" (name_cn, name_en, name_tw, description) VALUES ('异常', 'Error', '异常', '异常标签类型');
INSERT INTO "common"."tag_type" (name_cn, name_en, name_tw, description) VALUES ('预警', 'Alert', '預警', '预警标签类型');
INSERT INTO "common"."tag_type" (name_cn, name_en, name_tw, description) VALUES ('环境', 'Environment', '環境', '环境标签类型');
INSERT INTO "common"."tag_type" (name_cn, name_en, name_tw, description) VALUES ('其他', 'Other', '其他', '其他标签类型');

INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (1, 1, '设备状态', 'machine_status', '設備狀態', '设备状态');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (2, 2, '节拍时间开始信号', 'cycle_time_start_signal', '節拍時間開始訊號', '节拍时间开始信号');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (3, 2, '节拍时间结束信号', 'cycle_time_end_signal', '節拍時間結束訊號 ', '节拍时间结束信号');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (4, 3, '设备异常', 'equipment_error', '設備异常', '设备异常');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (5, 3, '品质异常', 'quality_error', '品質异常', '品质异常');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (6, 3, '物料呼叫', 'material_require', '物料呼叫', '物料呼叫');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (12, 3, '异常灯颜色', 'lamp_color', '异常灯颜色', '异常灯颜色');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (13, 4, '设备状态预警', 'machine_status_alert', '設備狀態預警', '设备状态预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (14, 4, '设备状态持续时间预警', 'machine_status_duration_alert', '設備狀態持續時間預警 ', '设备状态持续时间预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (15, 4, '日稼动率预警', 'day_utilization_rate_alert', '日稼動率預警', '日稼动率预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (16, 4, '班稼动率预警', 'shift_utilization_rate_alert', '班稼動率預警', '班稼动率预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (17, 4, '工单稼动率预警', 'work_order_utilization_rate_alert', '工单稼動率預警', '工单稼动率预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (18, 4, '设备费用预警', 'machine_cost_alert', '設備費用預警', '设备费用预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (19, 4, '平衡率预警', 'work_order_balancing_rate_alert', '平衡率預警', '平衡率预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (20, 4, '工单结束预警', 'work_order_finish_alert', '工單結束預警', '工单结束预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (21, 4, '工单逾期预警', 'work_order_overdue_alert', '工單逾期預警', '工单逾期预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (22, 4, '工单超过标准工时预警', 'work_order_over_standard_alert', '工單超過標準工時預警', '工单超过标准工时预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (23, 4, '产能预警', 'capacity_alert', '產能預警', '产能预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (24, 4, '设备故障预警', 'machine_fault_alert', '設備故障預警', '设备故障预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (25, 4, '品质异常预警', 'quality_error_alert', '品質异常預警', '品质异常预警');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (26, 5, '温度', 'temperature', '溫度', '温度');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (27, 5, '湿度', 'humidity', '濕度', '湿度');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (28, 5, '二氧化碳', 'carbon_dioxide', '二氧化碳', '二氧化碳');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (29, 5, '挥发性有机化合物', 'volatile_organic_compounds', '揮發性有機化合物', '挥发性有机化合物');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (30, 5, 'pH', 'pH', 'pH', 'pH');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (31, 5, '粉尘', 'dust', '粉塵', '粉尘');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (32, 5, '噪音', 'noise', '譟音 ', '噪音');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (33, 5, '照明度', 'Illuminance', '照明度', '照明度');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (34, 5, '压力', 'pressure', '压力', '压力');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (35, 5, '流量', 'flow', '流量', '流量');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (36, 5, '重量', 'weight', '重量', '重量');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (37, 5, '电压', 'voltage', '電壓', '电压');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (38, 5, '电流', 'current', '電流', '电流');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (39, 5, '浓度', 'consistence', '濃度', '浓度');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (40, 5, '硬度', 'hardness', '硬度', '硬度');
INSERT INTO common.tag_type_sub_fixed (id, tag_type_id, name_cn, name_en, name_tw, description) VALUES (41, 1, '设备异常解除信号', 'machine_error_release', '設備异常解除訊號', '设备异常解除信号');


INSERT INTO "oee"."status_setting" (status_name, value) VALUES ('Idle', 0);
INSERT INTO "oee"."status_setting" (status_name, value) VALUES ('Run', 1);
INSERT INTO "oee"."status_setting" (status_name, value) VALUES ('Off', 2);
INSERT INTO "oee"."status_setting" (status_name, value) VALUES ('Error', 3);

INSERT INTO "oee"."utilization_rate_formula" VALUES (1, '$Run/($Run+$Off+$Idle+$Error)');



INSERT INTO lpm.performance_formula (name, ratio, enable) VALUES ('productivity', 20, true);
INSERT INTO lpm.performance_formula (name, ratio, enable) VALUES ('attendance', 20, true);
INSERT INTO lpm.performance_formula (name, ratio, enable) VALUES ('leave', 20, false);
INSERT INTO lpm.performance_formula (name, ratio, enable) VALUES ('proposal', 20, true);
INSERT INTO lpm.performance_formula (name, ratio, enable) VALUES ('quality', 20, true);
