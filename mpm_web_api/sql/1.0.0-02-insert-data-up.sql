INSERT INTO fimp.system_tag_type(type_name_en, type_name_cn, type_name_tw, type_description, id) VALUES ('LightTower', '现场警示灯', '警示燈', '警示燈', 1);
INSERT INTO fimp.system_tag_type(type_name_en, type_name_cn, type_name_tw, type_description, id) VALUES ('Error', '异常类别（安灯类别）', '安燈異常通知類別', '触发异常通知流程类别', 2);
INSERT INTO fimp.system_tag_type(type_name_en, type_name_cn, type_name_tw, type_description, id) VALUES ('CircleTime', '节拍时间类别', '', '节拍时间类别', 3);
INSERT INTO fimp.system_tag_type(type_name_en, type_name_cn, type_name_tw, type_description, id) VALUES ('ProductionSchedule', '工单相关信息', '工单相关信息', '工单相关信息', 4);
INSERT INTO fimp.system_tag_type(type_name_en, type_name_cn, type_name_tw, type_description, id) VALUES ('Other', '其他类别', '其他类别', '其他类别', 5);

INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (1,'red_light_on','红灯输出','红灯输出','红灯输出',1);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (1,'yellow_light_on','黄灯输出','黄灯输出','黄灯输出',2);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (1,'green_light_on','绿灯输出','绿灯输出','绿灯输出',3);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (1,'blue_light_on','蓝灯输出','蓝灯输出','蓝灯输出',4);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (1,'white_light_on','白色灯输出','白色灯输出','白色灯输出',5);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (2,'quality_error','品质呼叫','品质呼叫','品质呼叫',6);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (2,'material_require','物料呼叫','物料呼叫','物料呼叫',7);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (2,'machine_time_error','设备工时异常','设备工时异常','设备工时异常',8);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (2,'maintain_mode','设备维修模式','设备维修模式','设备维修模式',9);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (2,'machine_error_code','设备运行异常','设备运行异常','设备运行异常',10);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (2,'equipment_tools_abnormal','设备工治具异常','设备工治具异常','设备工治具异常',11);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (2,'staff_error','人员异常','人员异常','人员异常',12);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (3,'cycle_time','节拍时间','节拍时间','节拍时间',13);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (4,'work_order','工单','工单','工单',14);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (4,'part_number','机种信息','机种信息','机种信息',15);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (5,'andon_ack_person','安灯人员确认信息','安灯人员确认信息','安灯人员确认信息',16);
INSERT INTO fimp.system_tag_code(type_id, code_name_en, code_name_cn, code_name_tw, code_description, id) VALUES (5,'andon_ack_code','安灯异常编码','安灯异常编码','安灯异常编码',17);

INSERT INTO oee.utilization_rate_formula(id, run_time_formula, error_time_formula, others_time_formula, boot_time_formula)VALUES (1, 'Run','Down', 'Idle', 'Run+Idle+Down+Break');
