CREATE SCHEMA IF NOT EXISTS andon;
CREATE SCHEMA IF NOT EXISTS fimp;
CREATE SCHEMA IF NOT EXISTS oee;


CREATE TABLE IF NOT EXISTS andon.error_config
(
    id serial NOT NULL,
    node_id integer,
    error_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    system_tag_code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    error_active integer NOT NULL,
    trigger_count integer,
    trigger_out_color integer NOT NULL,
    trigger_message_type integer NOT NULL,
    message_multilevel integer,
    check_ack integer,
    timeout_setting integer,
    wechat_type integer,
    check_arrival integer,
    arrival_message_type integer,
    arrival_out_color integer,
    part_num character varying(200) COLLATE pg_catalog."default",
    description character varying(200) COLLATE pg_catalog."default",
    CONSTRAINT error_config_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.error_config_person
(
    id serial NOT NULL,
    error_config_id integer,
    class_no character varying(50) COLLATE pg_catalog."default",
    person_level integer,
    person_id integer,
    CONSTRAINT error_config_person_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.error_config_pn
(
	id serial NOT NULL,
    error_config_id integer,
    class_no character varying(50) COLLATE pg_catalog."default",
    part_num character varying(50) COLLATE pg_catalog."default",
    description character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT error_config_pn_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.error_log
(
	id serial NOT NULL,
    error_name character varying(50) COLLATE pg_catalog."default",
    system_tag_code character varying(100) COLLATE pg_catalog."default" NOT NULL,
    ack_person_id integer,
    node_id integer,
    machine_code character varying(50) COLLATE pg_catalog."default",
    pn character varying(100) COLLATE pg_catalog."default",
    work_order character varying(100) COLLATE pg_catalog."default",
    arrival_person_id integer,
    error_type_id integer,
    remark character varying(255) COLLATE pg_catalog."default",
    start_time timestamp without time zone NOT NULL DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    release_time timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    arrival_time timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    maintenance_time timestamp(6) without time zone,
    line_id integer,
    defectives_count real,
    downtime_min real,
    CONSTRAINT error_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.error_log_person
(
	id serial NOT NULL,
    error_log_id integer NOT NULL,
    person_id integer,
    message_level integer,
    insert_time timestamp(6) without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    message_flow character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT error_log_person_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.error_quality_log
(
	id serial NOT NULL,
    error_log_id integer,
    error_type_id integer,
    apm_node_id integer,
    machine_code character varying(20) COLLATE pg_catalog."default",
    defectives_count real,
    quality_description character varying(200) COLLATE pg_catalog."default",
    quality_reason character varying(200) COLLATE pg_catalog."default",
    improve_plan character varying(200) COLLATE pg_catalog."default",
    plan_date timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    responsible_person character varying(100) COLLATE pg_catalog."default",
    remark character varying(200) COLLATE pg_catalog."default",
    insert_time timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    CONSTRAINT error_quality_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.error_type
(
	id serial NOT NULL,
    type_code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    type_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    remark character varying(500) COLLATE pg_catalog."default" NOT NULL,
    createtime timestamp without time zone,
    CONSTRAINT error_type_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.error_type_details
(
	id serial NOT NULL,
    type_id integer NOT NULL,
    code_no character varying(50) COLLATE pg_catalog."default" NOT NULL,
    code_name_en character varying(50) COLLATE pg_catalog."default" NOT NULL,
    code_name_cn character varying(50) COLLATE pg_catalog."default",
    code_name_tw character varying(50) COLLATE pg_catalog."default",
    remark character varying(100) COLLATE pg_catalog."default",
    createtime timestamp without time zone,
    CONSTRAINT error_type_details_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.machine_error_code
(
	id serial NOT NULL,
    machine_type integer,
    code_no character varying(50) COLLATE pg_catalog."default" NOT NULL,
    code_name_en character varying(50) COLLATE pg_catalog."default",
    code_name_cn character varying(255) COLLATE pg_catalog."default",
    code_name_tw character varying(50) COLLATE pg_catalog."default",
    tag_value integer NOT NULL,
    require_andon integer,
    desciption character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT machine_error_code_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.machine_error_code_log
(
	id serial NOT NULL,
    error_code_id integer,
    machine_code character varying(100) COLLATE pg_catalog."default",
    insert_time timestamp(6) without time zone,
    away_time timestamp(6) without time zone,
    value integer
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE IF NOT EXISTS andon.material_request
(
	id serial NOT NULL,
    material_id integer NOT NULL,
    station_id integer NOT NULL,
    request_person_id integer NOT NULL,
    work_order character varying(50) COLLATE pg_catalog."default" NOT NULL,
    part_num character varying(50) COLLATE pg_catalog."default" NOT NULL,
    request_count bigint NOT NULL,
    take_person_id bigint NOT NULL,
    take_time time(6) without time zone NOT NULL,
    remark character varying(500) COLLATE pg_catalog."default" NOT NULL,
    createtime time(6) without time zone NOT NULL,
    CONSTRAINT material_request_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS andon.material_request_info
(
	id serial NOT NULL,
    material_id integer,
    station_id integer,
    request_person_id integer DEFAULT '-1'::integer,
    work_order character varying(20) COLLATE pg_catalog."default",
    part_num character varying(30) COLLATE pg_catalog."default",
    request_count integer,
    take_person_id integer DEFAULT '-1'::integer,
    take_time timestamp(6) without time zone,
    remark character varying(50) COLLATE pg_catalog."default",
    createtime timestamp(6) without time zone,
    depot_ack_time timestamp(6) without time zone,
    request_ack_time timestamp(6) without time zone,
    CONSTRAINT material_request_info_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.apm_config
(
	id serial NOT NULL,
    user_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    password character varying(50) COLLATE pg_catalog."default" NOT NULL,
    api_url character varying(100) COLLATE pg_catalog."default" NOT NULL,
    group_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT apm_config_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.area_config
(
    id integer NOT NULL,
    group_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    layer character varying(255) COLLATE pg_catalog."default" NOT NULL,
    parent_id integer NOT NULL,
    CONSTRAINT area_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;



CREATE TABLE IF NOT EXISTS fimp.ct
(
	id serial NOT NULL,
    pn character varying(50) COLLATE pg_catalog."default",
    wo character varying(20) COLLATE pg_catalog."default",
    value integer NOT NULL,
    station_id integer,
    start_time timestamp(6) without time zone NOT NULL DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    end_time timestamp(6) without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    tag_code character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT ct_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.ct_averaged
(
	id serial NOT NULL,
    device_id integer,
    device_code character varying(255) COLLATE pg_catalog."default",
    work_order character varying(255) COLLATE pg_catalog."default",
    part_number character varying COLLATE pg_catalog."default",
    averaged_min real,
    tag_code character varying(255) COLLATE pg_catalog."default",
    insert_time timestamp(6) without time zone,
    last_time timestamp(6) without time zone,
    CONSTRAINT ct_averaged_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.ct_log
(
	id serial NOT NULL,
    machine_code character varying(50) COLLATE pg_catalog."default",
    pn character varying(50) COLLATE pg_catalog."default",
    wo character varying(50) COLLATE pg_catalog."default",
    tag_code character varying(100) COLLATE pg_catalog."default" NOT NULL,
    station_id integer,
    insert_time timestamp(6) without time zone NOT NULL DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    value integer,
    CONSTRAINT ct_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.dept_info
(
	id serial NOT NULL,
    plant_id integer,
    dept_no character varying(50) COLLATE pg_catalog."default",
    dept_name_en character varying(50) COLLATE pg_catalog."default",
    dept_name_tw character varying(50) COLLATE pg_catalog."default",
    dept_name_cn character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT dept_info_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.machine_working_time
(
	id serial NOT NULL,
    machine_id integer,
    plant_id integer NOT NULL,
    unit_no character varying(50) COLLATE pg_catalog."default" NOT NULL,
    line_id integer,
    station_id integer,
    part_num character varying(50) COLLATE pg_catalog."default" NOT NULL,
    standard_time numeric(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT machine_working_time_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.mail_config
(
	id serial NOT NULL,
    host character varying(255) COLLATE pg_catalog."default" NOT NULL,
    port integer NOT NULL,
    user_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    password character varying(255) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT mail_config_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.material_category
(
	id serial NOT NULL,
    type_code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    type_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    unit_no character varying(50) COLLATE pg_catalog."default" NOT NULL,
    line_id integer NOT NULL,
    remark character varying(500) COLLATE pg_catalog."default" NOT NULL,
    createtime timestamp(6) without time zone NOT NULL,
    CONSTRAINT material_category_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.material_info
(
	id serial NOT NULL,
    category_id integer NOT NULL,
    material_code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    material_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    material_type character varying(50) COLLATE pg_catalog."default" NOT NULL,
    material_inventory bigint NOT NULL,
    remark character varying(500) COLLATE pg_catalog."default" NOT NULL,
    createtime timestamp(6) without time zone,
    CONSTRAINT material_info_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.person
(
	id serial NOT NULL,
    dept_id integer NOT NULL,
    id_num character varying(50) COLLATE pg_catalog."default",
    user_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    user_level character varying(50) COLLATE pg_catalog."default",
    user_email character varying(50) COLLATE pg_catalog."default",
    card_num character varying(50) COLLATE pg_catalog."default",
    mobile_phone character varying(50) COLLATE pg_catalog."default",
    other_contact character varying(50) COLLATE pg_catalog."default",
    user_position character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT person_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.pro_schedule_config
(
	id serial NOT NULL,
    line_id integer NOT NULL,
    work_order character varying(30) COLLATE pg_catalog."default" NOT NULL,
    part_num character varying(30) COLLATE pg_catalog."default" NOT NULL,
    order_status integer NOT NULL,
    order_index integer NOT NULL,
    standard_workinghour integer NOT NULL,
    class_name character varying(30) COLLATE pg_catalog."default" NOT NULL,
    insert_time timestamp with time zone NOT NULL,
    schedule_time timestamp with time zone,
    partmeter_option integer NOT NULL,
    standard_num integer,
    CONSTRAINT pro_schedule_config_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.pro_schedule_device
(
	id serial NOT NULL,
    pro_schedule_id integer NOT NULL,
    line_id integer NOT NULL,
    station_id integer NOT NULL,
    start_time timestamp with time zone,
    end_time timestamp with time zone,
    quantity integer,
    bad_quantity integer,
    yield numeric(18,2),
    achieving_rate numeric(18,2),
    productivity numeric(18,2),
    order_status integer NOT NULL,
    CONSTRAINT pro_schedule_device_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.pro_schedule_device_log
(
	id serial NOT NULL,
    work_order character varying(30) COLLATE pg_catalog."default" NOT NULL,
    part_num character varying(30) COLLATE pg_catalog."default" NOT NULL,
    machine_code character varying(30) COLLATE pg_catalog."default" NOT NULL,
    quantity integer NOT NULL,
    bad_quantity integer NOT NULL,
    type integer NOT NULL,
    ts timestamp with time zone NOT NULL,
    CONSTRAINT pro_schedule_device_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.pro_schedule_line
(
	id serial NOT NULL,
    pro_schedule_id integer NOT NULL,
    line_id integer NOT NULL,
    start_time timestamp with time zone,
    end_time timestamp with time zone,
    quantity integer,
    bad_quantity integer,
    yield numeric(18,2),
    achieving_rate numeric(18,2),
    productivity numeric(18,2),
    order_status integer NOT NULL,
    CONSTRAINT pro_schedule_line_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.productivity_daily
(
	id serial NOT NULL,
    year integer NOT NULL,
    month integer NOT NULL,
    day integer NOT NULL,
    real_num real NOT NULL,
    real_workinghour real NOT NULL,
    product_workinghour real NOT NULL,
    line_id integer NOT NULL,
    createtime timestamp(6) without time zone NOT NULL DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    CONSTRAINT productivity_daily_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.rest_time
(
	id serial NOT NULL,
    unit_no character varying(50) COLLATE pg_catalog."default" NOT NULL,
    start_time character varying(10) COLLATE pg_catalog."default",
    end_time character varying(10) COLLATE pg_catalog."default",
    state character varying(10) COLLATE pg_catalog."default",
    CONSTRAINT "RestTime_pkey" PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.rest_time_row
(
	id serial NOT NULL,
    machine_code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    start_time character varying(50) COLLATE pg_catalog."default",
    end_time character varying(50) COLLATE pg_catalog."default",
    tag_code character varying(50) COLLATE pg_catalog."default",
    remarks character varying(500) COLLATE pg_catalog."default",
    CONSTRAINT rest_time_rows_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.srp_inner_log
(
	id serial NOT NULL,
    srp_code character varying(50) COLLATE pg_catalog."default",
    srp_description character varying(100) COLLATE pg_catalog."default",
    create_time timestamp(6) without time zone,
    last_time character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT srp_inner_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.srp_log
(
	id serial NOT NULL,
    content character varying(500) COLLATE pg_catalog."default",
    create_time timestamp(6) without time zone,
    CONSTRAINT srp_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.system_configs
(
	id serial NOT NULL,
    config_code character varying(255) COLLATE pg_catalog."default",
    config_value character varying(255) COLLATE pg_catalog."default",
    desciption character varying(255) COLLATE pg_catalog."default",
    type_code character varying(100) COLLATE pg_catalog."default",
    CONSTRAINT system_config_parameter_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.system_tag_code
(
	id serial NOT NULL,
    type_id integer NOT NULL,
    code_name_en character varying(255) COLLATE pg_catalog."default" NOT NULL,
    code_name_cn character varying(255) COLLATE pg_catalog."default",
    code_name_tw character varying(255) COLLATE pg_catalog."default",
    code_description character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT system_tag_code1_pkey PRIMARY KEY (code_name_en)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.system_tag_type
(
	id serial NOT NULL,
    type_name_en character varying(100) COLLATE pg_catalog."default" NOT NULL,
    type_name_cn character varying(100) COLLATE pg_catalog."default",
    type_name_tw character varying(100) COLLATE pg_catalog."default",
    type_description character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT system_tag_type1_pkey PRIMARY KEY (type_name_en)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.tag_time_class
(
	id serial NOT NULL,
    machine_code character varying(30) COLLATE pg_catalog."default" NOT NULL,
    tag_code character varying(30) COLLATE pg_catalog."default" NOT NULL,
    year integer NOT NULL,
    month integer NOT NULL,
    day integer NOT NULL,
    class_no character varying(50) COLLATE pg_catalog."default" NOT NULL,
    duration real NOT NULL,
    station_id integer,
    date date
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.tag_time_day
(
	id serial NOT NULL,
    machine_code character varying(30) COLLATE pg_catalog."default",
    tag_code character varying(20) COLLATE pg_catalog."default" NOT NULL,
    year integer NOT NULL,
    month integer NOT NULL,
    day integer NOT NULL,
    duration real NOT NULL,
    station_id integer,
    date date,
    CONSTRAINT tag_time_day_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE IF NOT EXISTS fimp.tag_value_log
(
	id serial NOT NULL,
    tag_code character varying(255) COLLATE pg_catalog."default",
    srp_code character varying(255) COLLATE pg_catalog."default",
    system_tag_code character varying(255) COLLATE pg_catalog."default",
    tag_value character varying(50) COLLATE pg_catalog."default",
    insert_time timestamp(6) without time zone,
    CONSTRAINT tag_value_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.tag_value_trend
(
	id serial NOT NULL,
    tag_code character varying(200) COLLATE pg_catalog."default",
    tag_value character varying(50) COLLATE pg_catalog."default",
    insert_time timestamp(6) with time zone,
    CONSTRAINT tag_value_trend_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.tricolor_tag
(
	id serial NOT NULL,
    machine_code character varying(20) COLLATE pg_catalog."default",
    tag_code character varying(20) COLLATE pg_catalog."default" NOT NULL,
    apm_node_id integer,
    insert_time timestamp(6) without time zone,
    whether boolean,
    tag_status boolean,
    CONSTRAINT tricolor_tag_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.tricolor_tag_duration
(
	id serial NOT NULL,
    machine_code character varying(255) COLLATE pg_catalog."default",
    apm_node_id integer,
    tag_code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    duration_second integer NOT NULL,
    insert_time timestamp(6) without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    away_time timestamp(6) without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    tag_status boolean DEFAULT false,
    whether boolean,
    CONSTRAINT tricolor_tag_duration_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.tricolor_tag_log
(
	id serial NOT NULL,
    machine_code character varying(50) COLLATE pg_catalog."default",
    tag_code character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apm_node_id integer,
    insert_time timestamp(6) without time zone,
    CONSTRAINT tricolor_tag_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp."user"
(
	id serial NOT NULL,
    user_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    pass_word character varying(50) COLLATE pg_catalog."default" NOT NULL,
    user_level integer NOT NULL,
    dept_id integer,
    create_time timestamp(6) without time zone,
    CONSTRAINT user_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS fimp.webaccess_tag_info
(
	id serial NOT NULL,
    tag_code character varying(100) COLLATE pg_catalog."default",
    system_type_code character varying(100) COLLATE pg_catalog."default",
    tag_description character varying(200) COLLATE pg_catalog."default",
    system_tag_code character varying(100) COLLATE pg_catalog."default",
    scada_id character varying(50) COLLATE pg_catalog."default",
    apm_node_id integer,
    CONSTRAINT tag_info_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS oee.line_balance_rate
(
	id serial NOT NULL,
    area_id integer,
    city_id integer,
    plant_id integer,
    unit_no character varying(50) COLLATE pg_catalog."default",
    line_id integer,
    balance_rate real,
    pn character varying(50) COLLATE pg_catalog."default",
    wo character varying(50) COLLATE pg_catalog."default",
    insert_time timestamp(6) without time zone DEFAULT '0001-01-01 07:36:42+07:36:42'::timestamp with time zone,
    CONSTRAINT line_balance_rate_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS oee.utilization_rate_class
(
	id serial NOT NULL,
    machine_code character varying(100) COLLATE pg_catalog."default",
    year integer,
    month integer,
    day integer,
    date character varying(10) COLLATE pg_catalog."default",
    class_no character varying(5) COLLATE pg_catalog."default",
    run_time real,
    error_time real,
    others_time real,
    rest_time real,
    boot_time real,
    utilization_rate real,
    CONSTRAINT utilization_rate_class_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS oee.utilization_rate_day
(
	id serial NOT NULL,
    machine_code character varying(50) COLLATE pg_catalog."default",
    year integer,
    month integer,
    day integer,
    date character varying(10) COLLATE pg_catalog."default",
    run_time real,
    error_time real,
    others_time real,
    rest_time real,
    boot_time real,
    utilization_rate real,
    CONSTRAINT utilization_rate_day_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS oee.utilization_rate_formula
(
    id integer NOT NULL,
    run_time_formula character varying(200) COLLATE pg_catalog."default",
    error_time_formula character varying(200) COLLATE pg_catalog."default",
    others_time_formula character varying(200) COLLATE pg_catalog."default",
    boot_time_formula character varying(200) COLLATE pg_catalog."default"
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


CREATE TABLE IF NOT EXISTS oee.utilization_rate_order
(
	id serial NOT NULL,
    work_order character varying(50) COLLATE pg_catalog."default",
    part_number character varying(50) COLLATE pg_catalog."default",
    run_time double precision,
    error_time double precision,
    others_time double precision,
    rest_time double precision,
    boot_time double precision,
    utilization_rate double precision,
    insert_time timestamp(6) without time zone,
    line_id integer,
    CONSTRAINT utilization_rate_order_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

GRANT ALL ON SCHEMA fimp TO ifactorytmp;
ALTER DEFAULT PRIVILEGES IN SCHEMA fimp GRANT ALL ON TABLES TO ifactorytmp;
ALTER DEFAULT PRIVILEGES IN SCHEMA fimp GRANT ALL ON SEQUENCES TO ifactorytmp;
GRANT ALL ON ALL TABLES IN SCHEMA fimp TO ifactorytmp;
GRANT ALL ON ALL SEQUENCES IN SCHEMA fimp TO ifactorytmp;

GRANT ALL ON SCHEMA andon TO ifactorytmp;
ALTER DEFAULT PRIVILEGES IN SCHEMA andon GRANT ALL ON TABLES TO ifactorytmp;
ALTER DEFAULT PRIVILEGES IN SCHEMA andon GRANT ALL ON SEQUENCES TO ifactorytmp;
GRANT ALL ON ALL TABLES IN SCHEMA andon TO ifactorytmp;
GRANT ALL ON ALL SEQUENCES IN SCHEMA andon TO ifactorytmp;

GRANT ALL ON SCHEMA oee TO ifactorytmp;
ALTER DEFAULT PRIVILEGES IN SCHEMA oee GRANT ALL ON TABLES TO ifactorytmp;
ALTER DEFAULT PRIVILEGES IN SCHEMA oee GRANT ALL ON SEQUENCES TO ifactorytmp;
GRANT ALL ON ALL TABLES IN SCHEMA oee TO ifactorytmp;
GRANT ALL ON ALL SEQUENCES IN SCHEMA oee TO ifactorytmp;