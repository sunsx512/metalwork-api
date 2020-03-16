INSERT INTO "common"."tag_type" VALUES (1, '设备', 'Machine', '設備', '设备标签类型');
INSERT INTO "common"."tag_type" VALUES (2, '节拍时间', 'Cycle_Time', '節拍時間', '节拍时间类标签类型');
INSERT INTO "common"."tag_type" VALUES (3, '异常', 'Error', '异常', '异常标签类型');
INSERT INTO "common"."tag_type" VALUES (4, '其他', 'Other', '其他', '其他标签类型');

INSERT INTO "common"."tag_type_sub" VALUES (1, 1, '设备状态', 'machine_status', '設備狀態', '设备状态');
INSERT INTO "common"."tag_type_sub" VALUES (2, 1, '设备状态灯颜色', 'lamp_color', '設備狀態', '設備狀態燈顏色');
INSERT INTO "common"."tag_type_sub" VALUES (3, 1, '设备状态w', '设备状态w', '设备状态w', '设备状态w');
INSERT INTO "common"."tag_type_sub" VALUES (4, 2, '设备总节拍时间', 'total_cycle_time', '設備总節拍時間', '设备总节拍时间');
INSERT INTO "common"."tag_type_sub" VALUES (5, 3, '设备异常', 'equipment_error', '設備异常', '设备异常');
INSERT INTO "common"."tag_type_sub" VALUES (6, 3, '品质异常', 'quality_error', '品質异常', '品质异常');
INSERT INTO "common"."tag_type_sub" VALUES (7, 3, '物料呼叫', 'material_require', '物料呼叫', '物料呼叫');
INSERT INTO "common"."tag_type_sub" VALUES (8, 3, '品质签到', 'quality_sign_in', '品質簽到', '品质签到');
INSERT INTO "common"."tag_type_sub" VALUES (9, 3, '设备异常签到', 'equipment_sign_in', '設備異常簽到', '设备异常签到');
INSERT INTO "common"."tag_type_sub" VALUES (10, 3, '品质解除', 'quality_release', '品質解除', '品质解除');
INSERT INTO "common"."tag_type_sub" VALUES (11, 3, '物料解除', 'material_release', '物料解除', '物料解除');
INSERT INTO "common"."tag_type_sub" VALUES (12, 3, '设备异常解除', 'equipment_release', '設備異常解除', '设备异常解除');
INSERT INTO "common"."tag_type_sub" VALUES (13, 3, '异常灯颜色', 'lamp_color', '异常灯颜色', '异常灯颜色');


