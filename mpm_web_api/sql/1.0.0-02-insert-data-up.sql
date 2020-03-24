INSERT INTO "common"."area_layer" VALUES (1, '区域', 'area', '區域', '所属区域');

INSERT INTO "common"."tag_type" VALUES (1, '设备', 'Machine', '設備', '设备标签类型');
INSERT INTO "common"."tag_type" VALUES (2, '节拍时间', 'Cycle_Time', '節拍時間', '节拍时间类标签类型');
INSERT INTO "common"."tag_type" VALUES (3, '异常', 'Error', '异常', '异常标签类型');
INSERT INTO "common"."tag_type" VALUES (4, '预警', 'Alert', '預警', '预警标签类型');
INSERT INTO "common"."tag_type" VALUES (5, '其他', 'Other', '其他', '其他标签类型');

INSERT INTO "common"."tag_type_sub_fixed" VALUES (1, 1, '设备状态', 'machine_status', '設備狀態', '设备状态');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (2, 1, '设备状态灯颜色', 'lamp_color', '設備狀態', '設備狀態燈顏色');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (3, 2, '设备总节拍时间', 'total_cycle_time', '設備总節拍時間', '设备总节拍时间');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (4, 3, '设备异常', 'equipment_error', '設備异常', '设备异常');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (5, 3, '品质异常', 'quality_error', '品質异常', '品质异常');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (6, 3, '物料呼叫', 'material_require', '物料呼叫', '物料呼叫');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (7, 3, '品质签到', 'quality_sign_in', '品質簽到', '品质签到');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (8, 3, '设备异常签到', 'equipment_sign_in', '設備異常簽到', '设备异常签到');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (9, 3, '品质解除', 'quality_release', '品質解除', '品质解除');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (10, 3, '物料解除', 'material_release', '物料解除', '物料解除');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (11, 3, '设备异常解除', 'equipment_release', '設備異常解除', '设备异常解除');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (12, 3, '异常灯颜色', 'lamp_color', '异常灯颜色', '异常灯颜色');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (13, 4, '设备状态预警', 'machine_status_alert', '設備狀態預警', '设备状态预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (14, 4, '设备状态持续时间预警', 'machine_status_duration_alert', '設備狀態持續時間預警 ', '设备状态持续时间预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (15, 4, '日稼动率预警', 'day_utilization_rate_alert', '日稼動率預警', '日稼动率预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (16, 4, '班稼动率预警', 'shift_utilization_rate_alert', '班稼動率預警', '班稼动率预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (17, 4, '工单稼动率预警', 'work_order_utilization_rate_alert', '工单稼動率預警', '工单稼动率预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (18, 4, '设备费用预警', 'machine_cost_alert', '設備費用預警', '设备费用预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (19, 4, '工单瓶颈站预警', 'work_order_bottleneck_alert', '工單瓶頸站預警', '工单瓶颈站预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (20, 4, '工单结束预警', 'work_order_finish_alert', '工單結束預警', '工单结束预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (21, 4, '工单逾期预警', 'work_order_overdue_alert', '工單逾期預警', '工单逾期预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (22, 4, '工单超过标准工时预警', 'work_order_over_standard_alert', '工單超過標準工時預警', '工单超过标准工时预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (23, 4, '产能预警', 'capacity_alert', '產能預警', '产能预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (24, 4, '设备故障预警', 'machine_fault_alert', '設備故障預警', '设备故障预警');
INSERT INTO "common"."tag_type_sub_fixed" VALUES (25, 4, '品质异常预警', 'quality_error_alert', '品質异常預警', '品质异常预警');


