CREATE SCHEMA IF NOT EXISTS common;
CREATE SCHEMA IF NOT EXISTS andon;
CREATE SCHEMA IF NOT EXISTS oee;
CREATE SCHEMA IF NOT EXISTS work_order;

CREATE TABLE IF NOT EXISTS "andon"."alert_mes" (
  "message_flow" varchar(255) COLLATE "pg_catalog"."default",
  "message_send" bool,
  "insert_time" timestamptz(4),
  "id" serial NOT NULL,
  CONSTRAINT "alert_mes_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."capacity_alert" (
  "id" serial NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "date" date NOT NULL,
  "capacity" numeric(8,2) NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  "tag_name" varchar(200) COLLATE "pg_catalog"."default",
  CONSTRAINT "capacity_alert_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "capacity_alert_date_key1" UNIQUE ("date")
);

CREATE TABLE IF NOT EXISTS "andon"."error_config" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "tag_type_sub_id" int4 NOT NULL,
  "response_person_id" int4 NOT NULL,
  "level1_notification_group_id" int4,
  "level2_notification_group_id" int4,
  "level3_notification_group_id" int4,
  "alert_active" bool,
  "trigger_out_color" int4,
  "timeout_setting" int4,
  "notice_type" int4,
  "logic_type" int4,
  CONSTRAINT "error_config_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."error_log" (
  "id" serial NOT NULL,
  "error_config_id" int4 NOT NULL,
  "tag_type_sub_name" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "error_type_name" varchar(32) COLLATE "pg_catalog"."default",
  "machine_name" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "error_type_detail_name" varchar(255) COLLATE "pg_catalog"."default",
  "responsible_name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "substitutes" varchar(255) COLLATE "pg_catalog"."default",
  "work_order" varchar(50) COLLATE "pg_catalog"."default",
  "part_number" varchar(32) COLLATE "pg_catalog"."default",
  "start_time" timestamptz(4),
  "arrival_time" timestamptz(4),
  "release_time" timestamptz(4),
  "defectives_count" numeric(8,2),
  "description" varchar(255) COLLATE "pg_catalog"."default",
  "cost_time" numeric(8,2),
  CONSTRAINT "error_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."error_log_mes" (
  "message_level" int4,
  "message_flow" varchar(255) COLLATE "pg_catalog"."default",
  "message_send" bool,
  "insert_time" timestamptz(4),
  "error_log_id" int4,
  "id" serial NOT NULL,
  CONSTRAINT "error_log_mes_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."error_type" (
  "id" serial NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "error_type_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "error_type_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "error_type_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "error_type_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "andon"."error_type_details" (
  "id" serial NOT NULL,
  "error_type_id" int4 NOT NULL,
  "code" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "name_cn" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(50) COLLATE "pg_catalog"."default",
  CONSTRAINT "error_type_details_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "error_type_details_code_key" UNIQUE ("code"),
  CONSTRAINT "error_type_details_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "error_type_details_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "error_type_details_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "andon"."machine_cost_alert" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "cost" numeric(8,2) NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  "alert_mode" int4 NOT NULL,
  CONSTRAINT "machine_cost_alert_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."machine_fault_alert" (
  "id" serial NOT NULL,
  "error_type_detail_id" int4 NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "fault_times" int4 NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  CONSTRAINT "machine_fault_alert_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."machine_status_alert" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "machine_status" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  CONSTRAINT "machine_status_alert_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."machine_status_duration_alert" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "machine_status" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "duration" numeric(8,2) NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  CONSTRAINT "machine_status_duration_alert_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."material_request_info" (
  "id" serial NOT NULL,
  "error_config_id" int4 NOT NULL,
  "material_code" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "machine_name" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "request_person_name" varchar(20) COLLATE "pg_catalog"."default",
  "work_order" varchar(50) COLLATE "pg_catalog"."default",
  "part_number" varchar(50) COLLATE "pg_catalog"."default",
  "request_count" int4 NOT NULL,
  "take_person_name" varchar(20) COLLATE "pg_catalog"."default",
  "take_time" timestamptz(4),
  "createtime" timestamptz(4) NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default",
  "cost_time" numeric(8,2),
  CONSTRAINT "material_request_info_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."notification_group" (
  "id" serial NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "notification_group_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "notification_group_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "notification_group_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "notification_group_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "andon"."notification_person" (
  "id" serial NOT NULL,
  "person_id" int4 NOT NULL,
  "notification_group_id" int4 NOT NULL,
  CONSTRAINT "error_config_person_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."quality_alert" (
  "id" serial NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "work_order_id" int4 NOT NULL,
  "defective_number" int4 NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  CONSTRAINT "quality_alert_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "quality_alert_work_order_id_key1" UNIQUE ("work_order_id")
);

CREATE TABLE IF NOT EXISTS "andon"."utilization_rate_alert" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "maximum" numeric(8,2) NOT NULL,
  "minimum" numeric(8,2) NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  "utilization_rate_type" int4 NOT NULL,
  CONSTRAINT "utilization_rate_alert_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "andon"."work_order_alert" (
  "id" serial NOT NULL,
  "virtual_line_id" int4 NOT NULL,
  "notice_group_id" int4 NOT NULL,
  "alert_type" int4 NOT NULL,
  "notice_type" int4 NOT NULL,
  "enable" bool NOT NULL,
  CONSTRAINT "work_order_alert_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."api_exception_log" (
  "id" serial NOT NULL,
  "method" varchar(255) COLLATE "pg_catalog"."default",
  "content" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  "insert_time" timestamptz(6) NOT NULL,
  "path" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "api_exception_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."api_request_log" (
  "id" serial NOT NULL,
  "method" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "path" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "cost_time" int4 NOT NULL,
  "insert_time" timestamptz(6) NOT NULL,
  CONSTRAINT "api_request_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE  IF NOT EXISTS "common"."area_layer" (
  "id" serial NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "area_layer_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "area_layer_name_cn_key" UNIQUE ("name_cn"),
  CONSTRAINT "area_layer_name_en_key" UNIQUE ("name_en"),
  CONSTRAINT "area_layer_name_tw_key" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "common"."area_node" (
  "id" serial NOT NULL,
  "area_layer_id" int4 NOT NULL,
  "upper_id" int4 NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "area_node_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "area_node_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "area_node_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "area_node_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "common"."area_property" (
  "id" serial NOT NULL,
  "area_node_id" int4 NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "format" varchar(1000) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "area_property_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "common"."department" (
  "id" serial NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "department_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "department_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "department_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "department_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "common"."email_server" (
  "id" serial NOT NULL,
  "host" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "port" int4 NOT NULL,
  "user_name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "password" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "email_server_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."machine" (
  "id" serial NOT NULL,
  "area_node_id" int4 NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "mahine_id_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "machine_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "machine_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "machine_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "common"."migration_log" (
  "id" serial NOT NULL,
  "migration_version" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "migration_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."person" (
  "id" serial NOT NULL,
  "dept_id" int4 NOT NULL,
  "id_num" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "user_name" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "user_level" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "email" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "wechart" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "mobile_phone" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "user_position" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "person_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "common"."srp_inner_log" (
  "id" serial NOT NULL,
  "srp_code" varchar(10) COLLATE "pg_catalog"."default",
  "insert_time" timestamptz(6),
  CONSTRAINT "srp_inner_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."srp_log" (
  "content" varchar(255) COLLATE "pg_catalog"."default",
  "create_time" timestamptz(6),
  "id" serial NOT NULL,
  CONSTRAINT "srp_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."tag_info" (
  "id" serial NOT NULL,
  "machine_id" int4,
  "tag_type_sub_id" int4 NOT NULL,
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "tag_info_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."tag_info_extra" (
  "id" serial NOT NULL,
  "tag_type_sub_id" int4 NOT NULL,
  "target_type" int4 NOT NULL,
  "target_id" int4 NOT NULL,
  "tag_name" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "tag_info_extra_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."tag_type" (
  "id" serial NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "tag_type_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "tag_type_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "tag_type_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "tag_type_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "common"."tag_type_sub_custom" (
  "id" serial NOT NULL,
  "tag_type_id" int4 NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "tag_type_sub_pkey_copy_1_copy_1" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."tag_type_sub_fixed" (
  "id" serial NOT NULL,
  "tag_type_id" int4 NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "tag_type_sub_pkey_copy_1" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."variable" (
  "id" serial NOT NULL,
  "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "value" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "variable_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."wechart_server" (
  "id" serial NOT NULL,
  "apply_name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "corp_id" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "apply_agentid" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "apply_secret" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "access_token" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "create_time" timestamptz(6),
  CONSTRAINT "wechart_server_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "common"."wise_paas_user" (
  "id" serial NOT NULL,
  "name" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "password" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "wise_paas_user_pkey" PRIMARY KEY ("name")
);

CREATE TABLE IF NOT EXISTS "oee"."machine_lease" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "unit_price" numeric(24) NOT NULL,
  "start_time" timestamptz(6) NOT NULL,
  "type" int4,
  "total_price" numeric(24),
  CONSTRAINT "machine_lease_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "machine_lease_machine_id_key1" UNIQUE ("machine_id")
);

CREATE TABLE IF NOT EXISTS "oee"."machine_lease_log" (
  "id" serial NOT NULL,
  "machine_id" int4,
  "run_time" float4,
  "consumption_price" numeric(24),
  "insert_time" timestamptz(6),
  CONSTRAINT "machine_lease_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "oee"."status_duration_day" (
  "id" serial NOT NULL,
  "status_name" varchar(10) COLLATE "pg_catalog"."default",
  "upper_id" int4 NOT NULL,
  "duration_time" int4,
  CONSTRAINT "status_duration_day_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "oee"."status_duration_order" (
  "id" serial NOT NULL,
  "status_name" varchar(10) COLLATE "pg_catalog"."default",
  "duration_time" float4,
  "upper_id" int4 NOT NULL,
  CONSTRAINT "status_duration_order_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "oee"."status_duration_shift" (
  "id" serial NOT NULL,
  "status_name" varchar(10) COLLATE "pg_catalog"."default",
  "duration_time" float4,
  "upper_id" int4,
  CONSTRAINT "status_duration_class_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "oee"."status_setting" (
  "id" serial NOT NULL,
  "status_name" varchar(10) COLLATE "pg_catalog"."default" NOT NULL,
  "value" int4 NOT NULL,
  CONSTRAINT "status_setting_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "status_setting_status_name_key" UNIQUE ("status_name"),
  CONSTRAINT "status_setting_value_key" UNIQUE ("value")
);

CREATE TABLE IF NOT EXISTS "oee"."tag_time_day" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "status_name" varchar(255) COLLATE "pg_catalog"."default",
  "date" timestamptz(0),
  "duration_time" float4
);

CREATE TABLE IF NOT EXISTS "oee"."tag_time_shift" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "status_name" varchar(255) COLLATE "pg_catalog"."default",
  "date" timestamptz(4),
  "shift" varchar(255) COLLATE "pg_catalog"."default",
  "duration_time" float4
);

CREATE TABLE IF NOT EXISTS "oee"."tricolor_tag_duration" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "status_name" varchar(255) COLLATE "pg_catalog"."default",
  "duration_time" float4,
  "insert_time" timestamptz(4),
  "away_time" timestamptz(4),
  "whether" bool
);

CREATE TABLE IF NOT EXISTS "oee"."tricolor_tag_status" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "status_name" varchar(255) COLLATE "pg_catalog"."default",
  "insert_time" timestamptz(4),
  "whether" bool,
  CONSTRAINT "tricolor_tag_status_pkey" PRIMARY KEY ("machine_id")
);

CREATE TABLE IF NOT EXISTS "oee"."utilization_rate_day" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "date" date,
  "utilization_rate" float4,
  "insert_time" timestamptz(6),
  CONSTRAINT "utilization_rate_day_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "oee"."utilization_rate_formula" (
  "id" serial NOT NULL,
  "formula" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "utilization_rate_formula_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "oee"."utilization_rate_order" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "work_order" varchar(50) COLLATE "pg_catalog"."default",
  "part_number" varchar(50) COLLATE "pg_catalog"."default",
  "utilization_rate" float4,
  "insert_time" timestamptz(4),
  CONSTRAINT "utilization_rate_order_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "oee"."utilization_rate_shift" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "date" date,
  "shift" varchar(32) COLLATE "pg_catalog"."default",
  "utilization_rate" float4,
  "insert_time" timestamptz(6),
  CONSTRAINT "utilization_rate_class_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."breakpoint_log" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "work_order_id" int4 NOT NULL,
  "insert_time" timestamptz(6) NOT NULL,
  "breakpoint" int4 NOT NULL,
  CONSTRAINT "breakpoint_log_pkey" PRIMARY KEY ("machine_id")
);

CREATE TABLE IF NOT EXISTS "work_order"."capacity_log" (
  "id" serial NOT NULL,
  "year" int4 NOT NULL,
  "month" int4 NOT NULL,
  "count" int4 NOT NULL,
  "date" date NOT NULL,
  CONSTRAINT "capacity_log_pkey" PRIMARY KEY ("date")
);

CREATE TABLE IF NOT EXISTS "work_order"."ct_log" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "work_order_id" int4 NOT NULL,
  "tag_type_sub_id" int4 NOT NULL,
  "cycle_time" numeric(8,2) NOT NULL,
  "insert_time" timestamptz(6) NOT NULL,
  CONSTRAINT "ct_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."overdue_work_order" (
  "id" serial NOT NULL,
  "start_time" timestamptz(0) NOT NULL,
  "end_time" timestamptz(0) NOT NULL,
  "overdue_time" numeric(8,2) NOT NULL,
  "wo_config_id" int4 NOT NULL,
  CONSTRAINT "overdue_work_order_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."virtual_line" (
  "id" serial NOT NULL,
  "name_cn" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_en" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "name_tw" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "description" varchar(255) COLLATE "pg_catalog"."default",
  CONSTRAINT "virtual_line_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "virtual_line_name_cn_key1" UNIQUE ("name_cn"),
  CONSTRAINT "virtual_line_name_en_key1" UNIQUE ("name_en"),
  CONSTRAINT "virtual_line_name_tw_key1" UNIQUE ("name_tw")
);

CREATE TABLE IF NOT EXISTS "work_order"."virtual_line_current_log" (
  "id" serial NOT NULL,
  "virtual_line_id" int4 NOT NULL,
  "wo_config_id" int4 NOT NULL,
  "start_time" timestamptz(6) NOT NULL,
  "balance_rate" numeric(8,2),
  "productivity" numeric(8,2),
  "quantity" numeric(8,2),
  "bad_quantity" numeric(8,2),
  CONSTRAINT "virtual_line_cur_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."virtual_line_log" (
  "id" serial NOT NULL,
  "virtual_line_id" int4 NOT NULL,
  "wo_config_id" int4 NOT NULL,
  "start_time" timestamptz(6) NOT NULL,
  "end_time" timestamptz(6) NOT NULL,
  "balance_rate" numeric(8,2),
  "productivity" numeric(8,2),
  "quantity" numeric(8,2),
  "bad_quantity" numeric(8,2),
  CONSTRAINT "virtual_line_log_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."wo_config" (
  "id" serial NOT NULL,
  "virtual_line_id" int4 NOT NULL,
  "work_order" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "part_num" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "shift" int4 NOT NULL,
  "standard_num" int4 NOT NULL,
  "auto" bool NOT NULL,
  "order_index" int4 NOT NULL,
  "status" int2 NOT NULL,
  "standard_time" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "create_time" timestamptz(6) NOT NULL,
  CONSTRAINT "wo_config_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "wo_config_work_order_key1" UNIQUE ("work_order")
);

CREATE TABLE IF NOT EXISTS "work_order"."wo_machine" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "virtual_line_id" int4 NOT NULL,
  CONSTRAINT "wo_machine_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."wo_machine_current_log" (
  "id" serial NOT NULL,
  "machine_id" int4 NOT NULL,
  "wo_config_id" int4,
  "standard_time" numeric(8,2) NOT NULL,
  "start_time" timestamptz(0),
  "quantity" numeric(8,2) NOT NULL DEFAULT 0,
  "bad_quantity" numeric(8,2) NOT NULL DEFAULT 0,
  "achieving_rate" numeric(8,2),
  "productivity" numeric(8,2),
  "cycle_time" numeric(8,2),
  "cycle_time_average" numeric(8,2),
  "standard_num" int4 NOT NULL,
  CONSTRAINT "machine_cur_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."wo_machine_log" (
  "id" serial NOT NULL,
  "wo_config_id" int4,
  "machine_id" int4 NOT NULL,
  "standard_time" numeric(8,2) NOT NULL,
  "start_time" timestamptz(0),
  "end_time" timestamptz(0),
  "quantity" numeric(8,2) NOT NULL DEFAULT 0,
  "bad_quantity" numeric(8,2) NOT NULL DEFAULT 0,
  "achieving_rate" numeric(8,2),
  "productivity" numeric(8,2),
  "cycle_time" numeric(8,2),
  "cycle_time_average" numeric(8,2),
  "standard_num" int4 NOT NULL,
  CONSTRAINT "machine_pkey" PRIMARY KEY ("id")
);

CREATE TABLE IF NOT EXISTS "work_order"."work_order_log" (
  "id" serial NOT NULL,
  "year" int4 NOT NULL,
  "month" int4 NOT NULL,
  "count" int4 NOT NULL,
  "date" date NOT NULL,
  CONSTRAINT "work_order_log_pkey" PRIMARY KEY ("date")
);

CREATE TABLE IF NOT EXISTS "work_order"."worker_exception_log" (
  "id" serial NOT NULL,
  "function" varchar(255) COLLATE "pg_catalog"."default",
  "content" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  "insert_time" timestamptz(6) NOT NULL,
  CONSTRAINT "worker_exception_log_pkey" PRIMARY KEY ("id")
);

ALTER TABLE "andon"."capacity_alert" ADD CONSTRAINT "capacity_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."error_config" ADD CONSTRAINT "error_config_level1_notification_group_id_fkey" FOREIGN KEY ("level1_notification_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."error_config" ADD CONSTRAINT "error_config_level2_notification_group_id_fkey" FOREIGN KEY ("level2_notification_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."error_config" ADD CONSTRAINT "error_config_level3_notification_group_id_fkey" FOREIGN KEY ("level3_notification_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."error_config" ADD CONSTRAINT "error_config_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."error_config" ADD CONSTRAINT "error_config_response_person_id_fkey" FOREIGN KEY ("response_person_id") REFERENCES "common"."person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."error_log" ADD CONSTRAINT "error_log_error_config_id_fkey" FOREIGN KEY ("error_config_id") REFERENCES "andon"."error_config" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."error_type_details" ADD CONSTRAINT "error_type_details_error_type_id_fkey" FOREIGN KEY ("error_type_id") REFERENCES "andon"."error_type" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_cost_alert" ADD CONSTRAINT "machine_cost_alert_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_cost_alert" ADD CONSTRAINT "machine_cost_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_fault_alert" ADD CONSTRAINT "machine_fault_alert_error_type_detail_id_fkey" FOREIGN KEY ("error_type_detail_id") REFERENCES "andon"."error_type_details" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_fault_alert" ADD CONSTRAINT "machine_fault_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_status_alert" ADD CONSTRAINT "machine_status_alert_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_status_alert" ADD CONSTRAINT "machine_status_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_status_duration_alert" ADD CONSTRAINT "machine_status_duration_alert_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."machine_status_duration_alert" ADD CONSTRAINT "machine_status_duration_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."material_request_info" ADD CONSTRAINT "material_request_info_error_config_id_fkey" FOREIGN KEY ("error_config_id") REFERENCES "andon"."error_config" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."notification_person" ADD CONSTRAINT "notification_person_notification_group_id_fkey" FOREIGN KEY ("notification_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."notification_person" ADD CONSTRAINT "notification_person_person_id_fkey" FOREIGN KEY ("person_id") REFERENCES "common"."person" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."quality_alert" ADD CONSTRAINT "quality_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."quality_alert" ADD CONSTRAINT "quality_alert_work_order_id_fkey" FOREIGN KEY ("work_order_id") REFERENCES "work_order"."wo_config" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."utilization_rate_alert" ADD CONSTRAINT "utilization_rate_alert_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "andon"."utilization_rate_alert" ADD CONSTRAINT "utilization_rate_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."work_order_alert" ADD CONSTRAINT "work_order_alert_notice_group_id_fkey" FOREIGN KEY ("notice_group_id") REFERENCES "andon"."notification_group" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "andon"."work_order_alert" ADD CONSTRAINT "work_order_alert_virtual_line_id_fkey" FOREIGN KEY ("virtual_line_id") REFERENCES "work_order"."virtual_line" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "common"."area_node" ADD CONSTRAINT "area_node_area_layer_id_fkey" FOREIGN KEY ("area_layer_id") REFERENCES "common"."area_layer" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "common"."area_property" ADD CONSTRAINT "area_property_area_node_id_fkey" FOREIGN KEY ("area_node_id") REFERENCES "common"."area_node" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "common"."person" ADD CONSTRAINT "person_dept_id_fkey" FOREIGN KEY ("dept_id") REFERENCES "common"."department" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "common"."tag_info" ADD CONSTRAINT "tag_info_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "common"."tag_type_sub_custom" ADD FOREIGN KEY ("id") REFERENCES "common"."tag_type" ("id") ON DELETE CASCADE ON UPDATE RESTRICT;
ALTER TABLE "common"."tag_type_sub_fixed" ADD FOREIGN KEY ("tag_type_id") REFERENCES "common"."tag_type" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."machine_lease" ADD CONSTRAINT "machine_lease_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."machine_lease_log" ADD CONSTRAINT "machine_lease_log_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."status_duration_day" ADD CONSTRAINT "status_duration_day_upper_id_fkey" FOREIGN KEY ("upper_id") REFERENCES "oee"."utilization_rate_day" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."status_duration_order" ADD CONSTRAINT "status_duration_order_upper_id_fkey" FOREIGN KEY ("upper_id") REFERENCES "oee"."utilization_rate_order" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."status_duration_shift" ADD CONSTRAINT "status_duration_shift_upper_id_fkey" FOREIGN KEY ("upper_id") REFERENCES "oee"."utilization_rate_shift" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."tag_time_day" ADD CONSTRAINT "tag_time_day_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "oee"."tag_time_shift" ADD CONSTRAINT "tag_time_shift_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "oee"."tricolor_tag_duration" ADD CONSTRAINT "tricolor_tag_duration_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "oee"."tricolor_tag_status" ADD CONSTRAINT "tricolor_tag_status_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."utilization_rate_day" ADD CONSTRAINT "utilization_rate_day_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "oee"."utilization_rate_order" ADD CONSTRAINT "utilization_rate_order_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "oee"."utilization_rate_shift" ADD CONSTRAINT "utilization_rate_shift_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "work_order"."breakpoint_log" ADD CONSTRAINT "breakpoint_log_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."breakpoint_log" ADD CONSTRAINT "breakpoint_log_work_order_id_fkey" FOREIGN KEY ("work_order_id") REFERENCES "work_order"."wo_config" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."ct_log" ADD CONSTRAINT "ct_log_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "work_order"."ct_log" ADD CONSTRAINT "ct_log_work_order_id_fkey" FOREIGN KEY ("work_order_id") REFERENCES "work_order"."wo_config" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "work_order"."virtual_line_current_log" ADD CONSTRAINT "virtual_line_current_log_virtual_line_id_fkey" FOREIGN KEY ("virtual_line_id") REFERENCES "work_order"."virtual_line" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "work_order"."virtual_line_current_log" ADD CONSTRAINT "virtual_line_current_log_wo_config_id_fkey" FOREIGN KEY ("wo_config_id") REFERENCES "work_order"."wo_config" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."virtual_line_log" ADD CONSTRAINT "virtual_line_log_virtual_line_id_fkey" FOREIGN KEY ("virtual_line_id") REFERENCES "work_order"."virtual_line" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "work_order"."virtual_line_log" ADD CONSTRAINT "virtual_line_log_wo_config_id_fkey" FOREIGN KEY ("wo_config_id") REFERENCES "work_order"."wo_config" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."wo_config" ADD CONSTRAINT "wo_config_virtual_line_id_fkey" FOREIGN KEY ("virtual_line_id") REFERENCES "work_order"."virtual_line" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."wo_machine" ADD CONSTRAINT "wo_machine_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."wo_machine" ADD CONSTRAINT "wo_machine_virtual_line_id_fkey" FOREIGN KEY ("virtual_line_id") REFERENCES "work_order"."virtual_line" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."wo_machine_current_log" ADD CONSTRAINT "wo_machine_current_log_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "work_order"."wo_machine_current_log" ADD CONSTRAINT "wo_machine_current_log_wo_config_id_fkey" FOREIGN KEY ("wo_config_id") REFERENCES "work_order"."wo_config" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "work_order"."wo_machine_log" ADD CONSTRAINT "wo_machine_log_machine_id_fkey" FOREIGN KEY ("machine_id") REFERENCES "common"."machine" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "work_order"."wo_machine_log" ADD CONSTRAINT "wo_machine_log_wo_config_id_fkey" FOREIGN KEY ("wo_config_id") REFERENCES "work_order"."wo_config" ("id") ON DELETE CASCADE ON UPDATE NO ACTION;

CREATE VIEW "andon"."notification_person_detail" AS  SELECT notification_person.person_id,
    notification_person.notification_group_id,
    person.dept_id,
    person.id_num,
    person.user_name,
    person.user_level,
    person.email,
    person.wechart,
    person.mobile_phone,
    notification_person.id,
    person.user_position
   FROM (andon.notification_person
     JOIN common.person ON ((notification_person.person_id = person.id)));

CREATE VIEW "common"."tag_type_sub" AS  SELECT tag_type_sub_fixed.id,
    tag_type_sub_fixed.tag_type_id,
    tag_type_sub_fixed.name_cn,
    tag_type_sub_fixed.name_en,
    tag_type_sub_fixed.name_tw,
    tag_type_sub_fixed.description
   FROM common.tag_type_sub_fixed 
UNION ALL
 SELECT tag_type_sub_custom.id,
    tag_type_sub_custom.tag_type_id,
    tag_type_sub_custom.name_cn,
    tag_type_sub_custom.name_en,
    tag_type_sub_custom.name_tw,
    tag_type_sub_custom.description
   FROM common.tag_type_sub_custom;

CREATE VIEW "work_order"."wo_machine_detail" AS  SELECT wo_machine.id,
    wo_machine.virtual_line_id,
    wo_machine.machine_id,
    machine.name_cn,
    machine.name_en,
    machine.name_tw,
    machine.description,
    machine.area_node_id
   FROM (work_order.wo_machine
     JOIN common.machine ON ((wo_machine.machine_id = machine.id)));


GRANT ALL ON SCHEMA common TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA common GRANT ALL ON TABLES TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA common GRANT ALL ON SEQUENCES TO "ifactoryMetal";
GRANT ALL ON ALL TABLES IN SCHEMA common TO "ifactoryMetal";
GRANT ALL ON ALL SEQUENCES IN SCHEMA common TO "ifactoryMetal";

GRANT ALL ON SCHEMA andon TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA andon GRANT ALL ON TABLES TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA andon GRANT ALL ON SEQUENCES TO "ifactoryMetal";
GRANT ALL ON ALL TABLES IN SCHEMA andon TO "ifactoryMetal";
GRANT ALL ON ALL SEQUENCES IN SCHEMA andon TO "ifactoryMetal";

GRANT ALL ON SCHEMA oee TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA oee GRANT ALL ON TABLES TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA oee GRANT ALL ON SEQUENCES TO "ifactoryMetal";
GRANT ALL ON ALL TABLES IN SCHEMA oee TO "ifactoryMetal";
GRANT ALL ON ALL SEQUENCES IN SCHEMA oee TO "ifactoryMetal";

GRANT ALL ON SCHEMA work_order TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA work_order GRANT ALL ON TABLES TO "ifactoryMetal";
ALTER DEFAULT PRIVILEGES IN SCHEMA work_order GRANT ALL ON SEQUENCES TO "ifactoryMetal";
GRANT ALL ON ALL TABLES IN SCHEMA work_order TO "ifactoryMetal";
GRANT ALL ON ALL SEQUENCES IN SCHEMA work_order TO "ifactoryMetal";