--
-- PostgreSQL database dump
--

-- Dumped from database version 11.9
-- Dumped by pg_dump version 12.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: andon; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA andon;


ALTER SCHEMA andon OWNER TO postgres;

--
-- Name: common; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA common;


ALTER SCHEMA common OWNER TO postgres;

--
-- Name: ehs; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA ehs;


ALTER SCHEMA ehs OWNER TO postgres;

--
-- Name: lpm; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA lpm;


ALTER SCHEMA lpm OWNER TO postgres;

--
-- Name: oee; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA oee;


ALTER SCHEMA oee OWNER TO postgres;


--
-- Name: work_order; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA work_order;


ALTER SCHEMA work_order OWNER TO postgres;

--
-- Name: get_month_first_hour(timestamp without time zone); Type: FUNCTION; Schema: common; Owner: postgres
--

CREATE FUNCTION common.get_month_first_hour(in_date timestamp without time zone, OUT out_date text) RETURNS text
    LANGUAGE plpgsql
    AS $$
BEGIN
  SELECT to_char(in_date, 'YYYY_MM_dd')||' 01:00:00' INTO out_date;
END;
$$;


ALTER FUNCTION common.get_month_first_hour(in_date timestamp without time zone, OUT out_date text) OWNER TO postgres;

--
-- Name: sub_table_trigger(); Type: FUNCTION; Schema: common; Owner: postgres
--

CREATE FUNCTION common.sub_table_trigger() RETURNS trigger
    LANGUAGE plpgsql
    AS $_$
DECLARE month_text TEXT;
        this_month_first_day_text TEXT;
        next_month_first_day_text TEXT;
        insert_statement TEXT;
BEGIN
  SELECT to_char(NEW.insert_time, 'YYYY_MM_dd') INTO month_text;
  SELECT common.get_month_first_hour(NEW.insert_time) INTO this_month_first_day_text;
  SELECT to_char(to_timestamp(this_month_first_day_text,'YYYY-MM-DD hh:oo:ss') + interval '1 day', 'YYYY-MM-DD hh:00:ss') INTO next_month_first_day_text;
  insert_statement := 'INSERT INTO common.raw_date_custom_'
                      || month_text||' VALUES ($1.*)';
  EXECUTE insert_statement USING NEW;
  RETURN NULL;
  EXCEPTION
  WHEN UNDEFINED_TABLE
    THEN
      EXECUTE
      'CREATE TABLE IF NOT EXISTS common.raw_date_custom_'
      || month_text
      || '(CHECK (insert_time >= '''
      || this_month_first_day_text
      || ''' and insert_time<'''
      || next_month_first_day_text
      || ''')) INHERITS (common.raw_date_custom)';
      RAISE NOTICE 'CREATE NON-EXISTANT TABLE common.raw_date_custom_%', month_text;
      EXECUTE
      'CREATE INDEX raw_date_custom_insert_time_'
      || month_text
      || ' ON common.raw_date_custom_'
      || month_text
      || '(insert_time)';
      EXECUTE insert_statement USING NEW;
      RETURN NULL;
END;
$_$;


ALTER FUNCTION common.sub_table_trigger() OWNER TO postgres;

SET default_tablespace = '';

--
-- Name: alert_mes; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.alert_mes (
    message_flow character varying(255),
    message_des character varying(255),
    insert_time timestamp(4) without time zone,
    target_id integer,
    target_type integer,
    id integer NOT NULL
);


ALTER TABLE andon.alert_mes OWNER TO postgres;

--
-- Name: alert_mes_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.alert_mes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.alert_mes_id_seq OWNER TO postgres;

--
-- Name: alert_mes_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.alert_mes_id_seq OWNED BY andon.alert_mes.id;


--
-- Name: andon_logic; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.andon_logic (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    level1_notification_group_id integer NOT NULL,
    level2_notification_group_id integer NOT NULL,
    level3_notification_group_id integer NOT NULL,
    notice_type integer NOT NULL,
    timeout_setting integer
);


ALTER TABLE andon.andon_logic OWNER TO postgres;

--
-- Name: andon_logic_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.andon_logic_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.andon_logic_id_seq OWNER TO postgres;

--
-- Name: andon_logic_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.andon_logic_id_seq OWNED BY andon.andon_logic.id;


--
-- Name: capacity_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.capacity_alert (
    id integer NOT NULL,
    notice_group_id integer NOT NULL,
    date date NOT NULL,
    capacity numeric(8,2) NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL
);


ALTER TABLE andon.capacity_alert OWNER TO postgres;

--
-- Name: capacity_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.capacity_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.capacity_alert_id_seq OWNER TO postgres;

--
-- Name: capacity_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.capacity_alert_id_seq OWNED BY andon.capacity_alert.id;


--
-- Name: error_config; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.error_config (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    tag_type_sub_id integer NOT NULL,
    response_person_id integer NOT NULL,
    alert_active boolean,
    trigger_out_color integer,
    sign_in_color integer,
    logic_type integer,
    andon_logic_id integer
);


ALTER TABLE andon.error_config OWNER TO postgres;

--
-- Name: error_config_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.error_config_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.error_config_id_seq OWNER TO postgres;

--
-- Name: error_config_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.error_config_id_seq OWNED BY andon.error_config.id;


--
-- Name: error_log; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.error_log (
    id integer NOT NULL,
    error_config_id integer NOT NULL,
    tag_type_sub_name character varying(50) NOT NULL,
    error_type_name character varying(32),
    machine_name character varying(32) NOT NULL,
    error_type_detail_name character varying(255),
    work_order character varying(50),
    part_number character varying(32),
    start_time timestamp(4) without time zone,
    arrival_time timestamp(4) without time zone,
    release_time timestamp(4) without time zone,
    defectives_count numeric(8,2),
    description character varying(255),
    cost_time numeric(8,2),
    substitutes integer,
    responsible_name integer NOT NULL,
    status integer NOT NULL,
    error_level integer
);


ALTER TABLE andon.error_log OWNER TO postgres;

--
-- Name: error_log_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.error_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.error_log_id_seq OWNER TO postgres;

--
-- Name: error_log_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.error_log_id_seq OWNED BY andon.error_log.id;


--
-- Name: error_log_mes; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.error_log_mes (
    id integer NOT NULL,
    message_level integer,
    message_flow character varying(255),
    message_send boolean,
    insert_time timestamp(4) without time zone,
    error_log_id integer
);


ALTER TABLE andon.error_log_mes OWNER TO postgres;

--
-- Name: error_log_mes_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.error_log_mes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.error_log_mes_id_seq OWNER TO postgres;

--
-- Name: error_log_mes_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.error_log_mes_id_seq OWNED BY andon.error_log_mes.id;


--
-- Name: error_month; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.error_month (
    id smallint NOT NULL,
    machine_id integer,
    error_frequency smallint,
    insert_time timestamp(4) without time zone,
    error_time real,
    month integer,
    mtbf real
);


ALTER TABLE andon.error_month OWNER TO postgres;

--
-- Name: error_month_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.error_month_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.error_month_id_seq OWNER TO postgres;

--
-- Name: error_month_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.error_month_id_seq OWNED BY andon.error_month.id;


--
-- Name: error_type; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.error_type (
    id integer NOT NULL,
    name_en character varying(255) NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE andon.error_type OWNER TO postgres;

--
-- Name: error_type_details; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.error_type_details (
    id integer NOT NULL,
    error_type_id integer NOT NULL,
    code character varying(20) NOT NULL,
    name_en character varying(50) NOT NULL,
    name_cn character varying(50) NOT NULL,
    name_tw character varying(50) NOT NULL,
    description character varying(50)
);


ALTER TABLE andon.error_type_details OWNER TO postgres;

--
-- Name: error_type_details_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.error_type_details_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.error_type_details_id_seq OWNER TO postgres;

--
-- Name: error_type_details_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.error_type_details_id_seq OWNED BY andon.error_type_details.id;


--
-- Name: error_type_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.error_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.error_type_id_seq OWNER TO postgres;

--
-- Name: error_type_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.error_type_id_seq OWNED BY andon.error_type.id;


--
-- Name: error_week; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.error_week (
    id smallint NOT NULL,
    machine_id integer,
    error_time numeric(8,2),
    error_frequency integer,
    insert_time timestamp(4) without time zone,
    week integer,
    mtbf real
);


ALTER TABLE andon.error_week OWNER TO postgres;

--
-- Name: error_week_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.error_week_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.error_week_id_seq OWNER TO postgres;

--
-- Name: error_week_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.error_week_id_seq OWNED BY andon.error_week.id;


--
-- Name: machine_cost_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.machine_cost_alert (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    notice_group_id integer NOT NULL,
    cost numeric(8,2) NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL,
    alert_mode integer NOT NULL
);


ALTER TABLE andon.machine_cost_alert OWNER TO postgres;

--
-- Name: machine_cost_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.machine_cost_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.machine_cost_alert_id_seq OWNER TO postgres;

--
-- Name: machine_cost_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.machine_cost_alert_id_seq OWNED BY andon.machine_cost_alert.id;


--
-- Name: machine_fault_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.machine_fault_alert (
    id integer NOT NULL,
    error_type_detail_id integer NOT NULL,
    notice_group_id integer NOT NULL,
    fault_times integer NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL
);


ALTER TABLE andon.machine_fault_alert OWNER TO postgres;

--
-- Name: machine_fault_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.machine_fault_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.machine_fault_alert_id_seq OWNER TO postgres;

--
-- Name: machine_fault_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.machine_fault_alert_id_seq OWNED BY andon.machine_fault_alert.id;


--
-- Name: machine_status_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.machine_status_alert (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    notice_group_id integer NOT NULL,
    machine_status character varying(50) NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL
);


ALTER TABLE andon.machine_status_alert OWNER TO postgres;

--
-- Name: machine_status_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.machine_status_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.machine_status_alert_id_seq OWNER TO postgres;

--
-- Name: machine_status_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.machine_status_alert_id_seq OWNED BY andon.machine_status_alert.id;


--
-- Name: machine_status_duration_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.machine_status_duration_alert (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    notice_group_id integer NOT NULL,
    machine_status character varying(50) NOT NULL,
    duration numeric(8,2) NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL
);


ALTER TABLE andon.machine_status_duration_alert OWNER TO postgres;

--
-- Name: machine_status_duration_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.machine_status_duration_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.machine_status_duration_alert_id_seq OWNER TO postgres;

--
-- Name: machine_status_duration_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.machine_status_duration_alert_id_seq OWNED BY andon.machine_status_duration_alert.id;


--
-- Name: material_request_info; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.material_request_info (
    id integer NOT NULL,
    error_config_id integer NOT NULL,
    material_code character varying(20) NOT NULL,
    machine_name character varying(50) NOT NULL,
    request_person_name character varying(20),
    work_order character varying(50),
    part_number character varying(50),
    request_count numeric(6,2) NOT NULL,
    take_person_name character varying(20),
    take_time timestamp(4) without time zone,
    createtime timestamp(4) without time zone NOT NULL,
    description character varying(255),
    cost_time numeric(8,2),
    status integer NOT NULL,
    error_level integer
);


ALTER TABLE andon.material_request_info OWNER TO postgres;

--
-- Name: material_request_info_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.material_request_info_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.material_request_info_id_seq OWNER TO postgres;

--
-- Name: material_request_info_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.material_request_info_id_seq OWNED BY andon.material_request_info.id;


--
-- Name: notification_group; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.notification_group (
    id integer NOT NULL,
    name_en character varying(255) NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE andon.notification_group OWNER TO postgres;

--
-- Name: notification_group_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.notification_group_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.notification_group_id_seq OWNER TO postgres;

--
-- Name: notification_group_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.notification_group_id_seq OWNED BY andon.notification_group.id;


--
-- Name: notification_person; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.notification_person (
    id integer NOT NULL,
    person_id integer NOT NULL,
    notification_group_id integer NOT NULL
);


ALTER TABLE andon.notification_person OWNER TO postgres;

--
-- Name: person; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.person (
    id integer NOT NULL,
    dept_id integer NOT NULL,
    id_num character varying(50) NOT NULL,
    user_name character varying(50) NOT NULL,
    user_level character varying(50) NOT NULL,
    email character varying(50) NOT NULL,
    wechart character varying(50) NOT NULL,
    mobile_phone character varying(50) NOT NULL,
    user_position character varying(255),
    photo_path character varying(255)
);


ALTER TABLE common.person OWNER TO postgres;

--
-- Name: notification_person_detail; Type: VIEW; Schema: andon; Owner: postgres
--

CREATE VIEW andon.notification_person_detail AS
 SELECT notification_person.person_id,
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


ALTER TABLE andon.notification_person_detail OWNER TO postgres;

--
-- Name: notification_person_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.notification_person_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.notification_person_id_seq OWNER TO postgres;

--
-- Name: notification_person_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.notification_person_id_seq OWNED BY andon.notification_person.id;


--
-- Name: quality_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.quality_alert (
    id integer NOT NULL,
    notice_group_id integer NOT NULL,
    work_order_id integer NOT NULL,
    defective_number integer NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL
);


ALTER TABLE andon.quality_alert OWNER TO postgres;

--
-- Name: quality_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.quality_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.quality_alert_id_seq OWNER TO postgres;

--
-- Name: quality_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.quality_alert_id_seq OWNED BY andon.quality_alert.id;


--
-- Name: utilization_rate_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.utilization_rate_alert (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    notice_group_id integer NOT NULL,
    maximum numeric(8,2) NOT NULL,
    minimum numeric(8,2) NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL,
    utilization_rate_type integer NOT NULL
);


ALTER TABLE andon.utilization_rate_alert OWNER TO postgres;

--
-- Name: utilization_rate_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.utilization_rate_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.utilization_rate_alert_id_seq OWNER TO postgres;

--
-- Name: utilization_rate_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.utilization_rate_alert_id_seq OWNED BY andon.utilization_rate_alert.id;


--
-- Name: work_order_alert; Type: TABLE; Schema: andon; Owner: postgres
--

CREATE TABLE andon.work_order_alert (
    id integer NOT NULL,
    virtual_line_id integer NOT NULL,
    notice_group_id integer NOT NULL,
    alert_type integer NOT NULL,
    notice_type integer NOT NULL,
    enable boolean NOT NULL,
    maximum numeric(8,2),
    minimum numeric(8,2)
);


ALTER TABLE andon.work_order_alert OWNER TO postgres;

--
-- Name: work_order_alert_id_seq; Type: SEQUENCE; Schema: andon; Owner: postgres
--

CREATE SEQUENCE andon.work_order_alert_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE andon.work_order_alert_id_seq OWNER TO postgres;

--
-- Name: work_order_alert_id_seq; Type: SEQUENCE OWNED BY; Schema: andon; Owner: postgres
--

ALTER SEQUENCE andon.work_order_alert_id_seq OWNED BY andon.work_order_alert.id;


--
-- Name: api_exception_log; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.api_exception_log (
    id integer NOT NULL,
    method character varying(255),
    content character varying(2000) NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL,
    path character varying(255)
);


ALTER TABLE common.api_exception_log OWNER TO postgres;

--
-- Name: api_exception_log_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.api_exception_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.api_exception_log_id_seq OWNER TO postgres;

--
-- Name: api_exception_log_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.api_exception_log_id_seq OWNED BY common.api_exception_log.id;


--
-- Name: api_request_log; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.api_request_log (
    id integer NOT NULL,
    method character varying(20) NOT NULL,
    path character varying(200) NOT NULL,
    cost_time integer NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE common.api_request_log OWNER TO postgres;

--
-- Name: api_request_log_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.api_request_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.api_request_log_id_seq OWNER TO postgres;

--
-- Name: api_request_log_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.api_request_log_id_seq OWNED BY common.api_request_log.id;


--
-- Name: area_layer; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.area_layer (
    id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255),
    calculate_avail boolean
);


ALTER TABLE common.area_layer OWNER TO postgres;

--
-- Name: area_layer_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.area_layer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.area_layer_id_seq OWNER TO postgres;

--
-- Name: area_layer_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.area_layer_id_seq OWNED BY common.area_layer.id;


--
-- Name: area_node; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.area_node (
    id integer NOT NULL,
    area_layer_id integer NOT NULL,
    upper_id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.area_node OWNER TO postgres;

--
-- Name: area_node_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.area_node_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.area_node_id_seq OWNER TO postgres;

--
-- Name: area_node_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.area_node_id_seq OWNED BY common.area_node.id;


--
-- Name: area_property; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.area_property (
    id integer NOT NULL,
    area_node_id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    format character varying(1000) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.area_property OWNER TO postgres;

--
-- Name: area_property_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.area_property_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.area_property_id_seq OWNER TO postgres;

--
-- Name: area_property_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.area_property_id_seq OWNED BY common.area_property.id;


--
-- Name: department; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.department (
    id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.department OWNER TO postgres;

--
-- Name: department_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.department_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.department_id_seq OWNER TO postgres;

--
-- Name: department_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.department_id_seq OWNED BY common.department.id;


--
-- Name: email_server; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.email_server (
    id integer NOT NULL,
    host character varying(50) NOT NULL,
    port integer NOT NULL,
    user_name character varying(255) NOT NULL,
    password character varying(255) NOT NULL
);


ALTER TABLE common.email_server OWNER TO postgres;

--
-- Name: email_server_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.email_server_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.email_server_id_seq OWNER TO postgres;

--
-- Name: email_server_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.email_server_id_seq OWNED BY common.email_server.id;


--
-- Name: machine; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.machine (
    id integer NOT NULL,
    area_node_id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.machine OWNER TO postgres;

--
-- Name: machine_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.machine_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.machine_id_seq OWNER TO postgres;

--
-- Name: machine_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.machine_id_seq OWNED BY common.machine.id;


--
-- Name: migration_log; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.migration_log (
    id integer NOT NULL,
    migration_version character varying(2000) NOT NULL
);


ALTER TABLE common.migration_log OWNER TO postgres;

--
-- Name: migration_log_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.migration_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.migration_log_id_seq OWNER TO postgres;

--
-- Name: migration_log_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.migration_log_id_seq OWNED BY common.migration_log.id;


--
-- Name: mqtt_log; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.mqtt_log (
    content character varying(255),
    create_time timestamp(6) without time zone,
    id integer NOT NULL
);


ALTER TABLE common.mqtt_log OWNER TO postgres;

--
-- Name: mqtt_log_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.mqtt_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.mqtt_log_id_seq OWNER TO postgres;

--
-- Name: mqtt_log_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.mqtt_log_id_seq OWNED BY common.mqtt_log.id;


--
-- Name: person_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.person_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.person_id_seq OWNER TO postgres;

--
-- Name: person_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.person_id_seq OWNED BY common.person.id;


--
-- Name: raw_date_custom; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.raw_date_custom (
    id integer NOT NULL,
    tag_type_sub_id integer NOT NULL,
    machine_id integer NOT NULL,
    value numeric(8,2) NOT NULL,
    insert_time timestamp(6) without time zone
);


ALTER TABLE common.raw_date_custom OWNER TO postgres;


--
-- Name: raw_date_custom_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.raw_date_custom_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.raw_date_custom_id_seq OWNER TO postgres;

--
-- Name: raw_date_custom_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.raw_date_custom_id_seq OWNED BY common.raw_date_custom.id;


--
-- Name: srp_inner_log; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.srp_inner_log (
    id integer NOT NULL,
    srp_code character varying(10),
    insert_time timestamp(6) without time zone
);


ALTER TABLE common.srp_inner_log OWNER TO postgres;

--
-- Name: srp_inner_log_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.srp_inner_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.srp_inner_log_id_seq OWNER TO postgres;

--
-- Name: srp_inner_log_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.srp_inner_log_id_seq OWNED BY common.srp_inner_log.id;


--
-- Name: srp_log; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.srp_log (
    content character varying(255),
    create_time timestamp(6) without time zone,
    id integer NOT NULL
);


ALTER TABLE common.srp_log OWNER TO postgres;

--
-- Name: srp_log_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.srp_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.srp_log_id_seq OWNER TO postgres;

--
-- Name: srp_log_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.srp_log_id_seq OWNED BY common.srp_log.id;


--
-- Name: tag_info; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.tag_info (
    id integer NOT NULL,
    machine_id integer,
    tag_type_sub_id integer NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.tag_info OWNER TO postgres;

--
-- Name: tag_info_extra; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.tag_info_extra (
    id integer NOT NULL,
    tag_type_sub_id integer NOT NULL,
    target_type integer NOT NULL,
    target_id integer NOT NULL,
    name character varying(200) NOT NULL,
    description character varying(200)
);


ALTER TABLE common.tag_info_extra OWNER TO postgres;

--
-- Name: tag_info_extra_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.tag_info_extra_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.tag_info_extra_id_seq OWNER TO postgres;

--
-- Name: tag_info_extra_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.tag_info_extra_id_seq OWNED BY common.tag_info_extra.id;


--
-- Name: tag_info_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.tag_info_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.tag_info_id_seq OWNER TO postgres;

--
-- Name: tag_info_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.tag_info_id_seq OWNED BY common.tag_info.id;


--
-- Name: tag_type; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.tag_type (
    id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.tag_type OWNER TO postgres;

--
-- Name: tag_type_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.tag_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.tag_type_id_seq OWNER TO postgres;

--
-- Name: tag_type_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.tag_type_id_seq OWNED BY common.tag_type.id;


--
-- Name: tag_type_sub_custom_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.tag_type_sub_custom_id_seq
    START WITH 50
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 2147483647
    CACHE 1;


ALTER TABLE common.tag_type_sub_custom_id_seq OWNER TO postgres;

--
-- Name: tag_type_sub_custom; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.tag_type_sub_custom (
    id integer DEFAULT nextval('common.tag_type_sub_custom_id_seq'::regclass) NOT NULL,
    tag_type_id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.tag_type_sub_custom OWNER TO postgres;

--
-- Name: tag_type_sub_fixed; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.tag_type_sub_fixed (
    id integer NOT NULL,
    tag_type_id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE common.tag_type_sub_fixed OWNER TO postgres;

--
-- Name: tag_type_sub; Type: VIEW; Schema: common; Owner: postgres
--

CREATE VIEW common.tag_type_sub AS
 SELECT tag_type_sub_fixed.id,
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


ALTER TABLE common.tag_type_sub OWNER TO postgres;

--
-- Name: tag_type_sub_fixed_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.tag_type_sub_fixed_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.tag_type_sub_fixed_id_seq OWNER TO postgres;

--
-- Name: tag_type_sub_fixed_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.tag_type_sub_fixed_id_seq OWNED BY common.tag_type_sub_fixed.id;


--
-- Name: wechart_server; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.wechart_server (
    id integer NOT NULL,
    apply_name character varying(255) NOT NULL,
    corp_id character varying(255) NOT NULL,
    apply_agentid character varying(255) NOT NULL,
    apply_secret character varying(255) NOT NULL,
    access_token character varying(255) NOT NULL,
    create_time timestamp(6) without time zone
);


ALTER TABLE common.wechart_server OWNER TO postgres;

--
-- Name: wechart_server_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.wechart_server_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.wechart_server_id_seq OWNER TO postgres;

--
-- Name: wechart_server_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.wechart_server_id_seq OWNED BY common.wechart_server.id;


--
-- Name: wise_paas_user; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.wise_paas_user (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    password character varying(100) NOT NULL,
    role character varying(100) NOT NULL
);


ALTER TABLE common.wise_paas_user OWNER TO postgres;

--
-- Name: wise_paas_user_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

CREATE SEQUENCE common.wise_paas_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE common.wise_paas_user_id_seq OWNER TO postgres;

--
-- Name: wise_paas_user_id_seq; Type: SEQUENCE OWNED BY; Schema: common; Owner: postgres
--

ALTER SEQUENCE common.wise_paas_user_id_seq OWNED BY common.wise_paas_user.id;


--
-- Name: current_state; Type: TABLE; Schema: ehs; Owner: postgres
--

CREATE TABLE ehs.current_state (
    tag_id integer NOT NULL,
    state character varying(255) NOT NULL,
    value numeric(255,0) NOT NULL,
    update_time timestamp(6) without time zone NOT NULL,
    id integer NOT NULL,
    description character varying(1024)
);


ALTER TABLE ehs.current_state OWNER TO postgres;

--
-- Name: current_state_id_seq; Type: SEQUENCE; Schema: ehs; Owner: postgres
--

CREATE SEQUENCE ehs.current_state_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE ehs.current_state_id_seq OWNER TO postgres;

--
-- Name: current_state_id_seq; Type: SEQUENCE OWNED BY; Schema: ehs; Owner: postgres
--

ALTER SEQUENCE ehs.current_state_id_seq OWNED BY ehs.current_state.id;


--
-- Name: di_tu; Type: TABLE; Schema: ehs; Owner: postgres
--

CREATE TABLE ehs.di_tu (
    id integer NOT NULL,
    value double precision
);


ALTER TABLE ehs.di_tu OWNER TO postgres;

--
-- Name: exception_log; Type: TABLE; Schema: ehs; Owner: postgres
--

CREATE TABLE ehs.exception_log (
    id integer NOT NULL,
    function character varying(255) NOT NULL,
    content text NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE ehs.exception_log OWNER TO postgres;

--
-- Name: exception_log_id_seq; Type: SEQUENCE; Schema: ehs; Owner: postgres
--

CREATE SEQUENCE ehs.exception_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE ehs.exception_log_id_seq OWNER TO postgres;

--
-- Name: exception_log_id_seq; Type: SEQUENCE OWNED BY; Schema: ehs; Owner: postgres
--

ALTER SEQUENCE ehs.exception_log_id_seq OWNED BY ehs.exception_log.id;


--
-- Name: notice_logic; Type: TABLE; Schema: ehs; Owner: postgres
--

CREATE TABLE ehs.notice_logic (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    normal_notification_group_id integer,
    abnormal_notification_group_id integer,
    serious_notification_group_id integer,
    notice_type integer NOT NULL
);


ALTER TABLE ehs.notice_logic OWNER TO postgres;

--
-- Name: notice_logic_id_seq; Type: SEQUENCE; Schema: ehs; Owner: postgres
--

CREATE SEQUENCE ehs.notice_logic_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE ehs.notice_logic_id_seq OWNER TO postgres;

--
-- Name: notice_logic_id_seq; Type: SEQUENCE OWNED BY; Schema: ehs; Owner: postgres
--

ALTER SEQUENCE ehs.notice_logic_id_seq OWNED BY ehs.notice_logic.id;


--
-- Name: raw_data; Type: TABLE; Schema: ehs; Owner: postgres
--

CREATE TABLE ehs.raw_data (
    id integer NOT NULL,
    tag_id integer NOT NULL,
    value numeric(6,2) NOT NULL,
    insert_time timestamp without time zone NOT NULL
);


ALTER TABLE ehs.raw_data OWNER TO postgres;

--
-- Name: raw_data_id_seq; Type: SEQUENCE; Schema: ehs; Owner: postgres
--

CREATE SEQUENCE ehs.raw_data_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE ehs.raw_data_id_seq OWNER TO postgres;

--
-- Name: raw_data_id_seq; Type: SEQUENCE OWNED BY; Schema: ehs; Owner: postgres
--

ALTER SEQUENCE ehs.raw_data_id_seq OWNED BY ehs.raw_data.id;


--
-- Name: standard; Type: TABLE; Schema: ehs; Owner: postgres
--

CREATE TABLE ehs.standard (
    id integer NOT NULL,
    tag_id integer NOT NULL,
    tag_type_sub_id integer NOT NULL,
    normal_min numeric(6,2),
    normal_max numeric(6,2),
    abnormal_min numeric(6,2),
    abnormal_max numeric(6,2),
    serious_min numeric(6,2),
    serious_max numeric(6,2),
    notice_logic_id integer
);


ALTER TABLE ehs.standard OWNER TO postgres;

--
-- Name: standard_id_seq; Type: SEQUENCE; Schema: ehs; Owner: postgres
--

CREATE SEQUENCE ehs.standard_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE ehs.standard_id_seq OWNER TO postgres;

--
-- Name: standard_id_seq; Type: SEQUENCE OWNED BY; Schema: ehs; Owner: postgres
--

ALTER SEQUENCE ehs.standard_id_seq OWNED BY ehs.standard.id;


--
-- Name: state_statistics; Type: TABLE; Schema: ehs; Owner: postgres
--

CREATE TABLE ehs.state_statistics (
    tag_id integer NOT NULL,
    date date NOT NULL,
    normal_count integer NOT NULL,
    abnormal_count integer NOT NULL,
    serious_count integer NOT NULL,
    update_time timestamp(6) without time zone NOT NULL,
    normal_duration numeric(6,2) NOT NULL,
    abnormal_duration numeric(6,2) NOT NULL,
    serious_duration numeric(6,2) NOT NULL,
    id integer NOT NULL
);


ALTER TABLE ehs.state_statistics OWNER TO postgres;

--
-- Name: state_trigger_statistics_id_seq; Type: SEQUENCE; Schema: ehs; Owner: postgres
--

CREATE SEQUENCE ehs.state_trigger_statistics_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE ehs.state_trigger_statistics_id_seq OWNER TO postgres;

--
-- Name: state_trigger_statistics_id_seq; Type: SEQUENCE OWNED BY; Schema: ehs; Owner: postgres
--

ALTER SEQUENCE ehs.state_trigger_statistics_id_seq OWNED BY ehs.state_statistics.id;



--
-- Name: attendance_statistics; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.attendance_statistics (
    id integer NOT NULL,
    date date NOT NULL,
    shift integer NOT NULL,
    person_id integer NOT NULL,
    is_attend boolean NOT NULL
);


ALTER TABLE lpm.attendance_statistics OWNER TO postgres;

--
-- Name: attendance_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.attendance_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.attendance_id_seq OWNER TO postgres;

--
-- Name: attendance_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.attendance_id_seq OWNED BY lpm.attendance_statistics.id;


--
-- Name: attendance_log; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.attendance_log (
    person_id integer NOT NULL,
    work_overtime_hours real NOT NULL,
    actual_duty_hours real NOT NULL,
    insert_time date NOT NULL,
    id smallint NOT NULL,
    leave_hours real NOT NULL,
    standard_duty_hours real NOT NULL
);


ALTER TABLE lpm.attendance_log OWNER TO postgres;

--
-- Name: attendance_log_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.attendance_log_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.attendance_log_id_seq OWNER TO postgres;

--
-- Name: attendance_log_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.attendance_log_id_seq OWNED BY lpm.attendance_log.id;


--
-- Name: attendance_summary; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.attendance_summary (
    id smallint NOT NULL,
    person_id integer NOT NULL,
    work_overtime_hours real NOT NULL,
    actual_duty_hours real NOT NULL,
    attendance numeric(24,2) NOT NULL,
    date date NOT NULL,
    leave_hours real NOT NULL,
    standard_duty_hours real NOT NULL
);


ALTER TABLE lpm.attendance_summary OWNER TO postgres;

--
-- Name: attendance_summary_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.attendance_summary_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.attendance_summary_id_seq OWNER TO postgres;

--
-- Name: attendance_summary_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.attendance_summary_id_seq OWNED BY lpm.attendance_summary.id;


--
-- Name: department_efficiency; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.department_efficiency (
    id smallint NOT NULL,
    dept_id integer,
    output_minutes numeric(32,2),
    effective_minutes numeric(32,2),
    efficiency_rate numeric(32,2),
    date date
);


ALTER TABLE lpm.department_efficiency OWNER TO postgres;

--
-- Name: department_efficiency_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.department_efficiency_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.department_efficiency_id_seq OWNER TO postgres;

--
-- Name: department_efficiency_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.department_efficiency_id_seq OWNED BY lpm.department_efficiency.id;


--
-- Name: department_handle; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.department_handle (
    id smallint NOT NULL,
    department_id integer NOT NULL,
    handle_num integer NOT NULL,
    date date NOT NULL
);


ALTER TABLE lpm.department_handle OWNER TO postgres;

--
-- Name: department_handle_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.department_handle_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.department_handle_id_seq OWNER TO postgres;

--
-- Name: department_handle_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.department_handle_id_seq OWNED BY lpm.department_handle.id;


--
-- Name: department_performance; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.department_performance (
    id smallint NOT NULL,
    dept_id integer NOT NULL,
    performance real NOT NULL,
    date date NOT NULL
);


ALTER TABLE lpm.department_performance OWNER TO postgres;

--
-- Name: department_performance_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.department_performance_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.department_performance_id_seq OWNER TO postgres;

--
-- Name: department_performance_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.department_performance_id_seq OWNED BY lpm.department_performance.id;


--
-- Name: department_proposal; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.department_proposal (
    id smallint NOT NULL,
    dept_id integer,
    proposal_num integer,
    date date
);


ALTER TABLE lpm.department_proposal OWNER TO postgres;

--
-- Name: department_proposal_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.department_proposal_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.department_proposal_id_seq OWNER TO postgres;

--
-- Name: department_proposal_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.department_proposal_id_seq OWNED BY lpm.department_proposal.id;


--
-- Name: department_quality_record; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.department_quality_record (
    id smallint NOT NULL,
    dept_id integer NOT NULL,
    pass_num numeric(32,0) NOT NULL,
    total_num numeric(32,0) NOT NULL,
    quality_rate numeric(32,2) NOT NULL,
    date date NOT NULL
);


ALTER TABLE lpm.department_quality_record OWNER TO postgres;

--
-- Name: department_quality_record_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.department_quality_record_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.department_quality_record_id_seq OWNER TO postgres;

--
-- Name: department_quality_record_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.department_quality_record_id_seq OWNED BY lpm.department_quality_record.id;


--
-- Name: efficiencydetail; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.efficiencydetail (
    id smallint NOT NULL,
    person_id integer NOT NULL,
    wo_id integer NOT NULL,
    wo_starttime timestamp(6) without time zone,
    wo_endtime timestamp(6) without time zone,
    wo_count numeric(32,0),
    wo_precount numeric(32,0),
    standard_time integer,
    iserror boolean,
    insert_time timestamp(6) without time zone NOT NULL,
    wo_errorcount numeric(32,0),
    wo_preerrorcount numeric(32,0),
    machine_id integer,
    error_log_id integer,
    error_time numeric(32,0),
    error_end boolean,
    break_time numeric(32,0)
);


ALTER TABLE lpm.efficiencydetail OWNER TO postgres;

--
-- Name: leave_statistics; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.leave_statistics (
    id integer NOT NULL,
    person_id integer NOT NULL,
    start_time timestamp without time zone NOT NULL,
    end_time timestamp without time zone NOT NULL,
    duration numeric(6,2) NOT NULL,
    substitutes integer
);


ALTER TABLE lpm.leave_statistics OWNER TO postgres;

--
-- Name: leave_statistics_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.leave_statistics_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.leave_statistics_id_seq OWNER TO postgres;

--
-- Name: leave_statistics_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.leave_statistics_id_seq OWNED BY lpm.leave_statistics.id;


--
-- Name: waitingtime_log; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.waitingtime_log (
    id smallint NOT NULL,
    machine_id integer,
    waiting_time numeric(24,2),
    insert_time timestamp(6) without time zone
);


ALTER TABLE lpm.waitingtime_log OWNER TO postgres;

--
-- Name: machine_waitingtime_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.machine_waitingtime_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.machine_waitingtime_id_seq OWNER TO postgres;

--
-- Name: machine_waitingtime_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.machine_waitingtime_id_seq OWNED BY lpm.waitingtime_log.id;


--
-- Name: overtime_statistics; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.overtime_statistics (
    id integer NOT NULL,
    start_time timestamp(6) without time zone NOT NULL,
    end_time timestamp(6) without time zone NOT NULL,
    person_id integer NOT NULL,
    duration numeric(6,2) NOT NULL
);


ALTER TABLE lpm.overtime_statistics OWNER TO postgres;

--
-- Name: overtime_statistics_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.overtime_statistics_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.overtime_statistics_id_seq OWNER TO postgres;

--
-- Name: overtime_statistics_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.overtime_statistics_id_seq OWNED BY lpm.overtime_statistics.id;


--
-- Name: performance_formula; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.performance_formula (
    id integer NOT NULL,
    name character varying NOT NULL,
    ratio numeric NOT NULL,
    enable boolean NOT NULL
);


ALTER TABLE lpm.performance_formula OWNER TO postgres;

--
-- Name: person_efficiency; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.person_efficiency (
    id smallint NOT NULL,
    person_id integer,
    dept_id integer,
    output_minutes numeric(32,2),
    effective_minutes numeric(32,2),
    efficiency_rate numeric(32,2),
    date date
);


ALTER TABLE lpm.person_efficiency OWNER TO postgres;

--
-- Name: person_efficiency_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.person_efficiency_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.person_efficiency_id_seq OWNER TO postgres;

--
-- Name: person_efficiency_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.person_efficiency_id_seq OWNED BY lpm.person_efficiency.id;


--
-- Name: person_handle; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.person_handle (
    id smallint NOT NULL,
    person_id integer NOT NULL,
    error_log_id integer NOT NULL,
    date date NOT NULL,
    shift integer,
    id_num character varying(50) NOT NULL,
    dept_id integer NOT NULL
);


ALTER TABLE lpm.person_handle OWNER TO postgres;

--
-- Name: person_handle_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.person_handle_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.person_handle_id_seq OWNER TO postgres;

--
-- Name: person_handle_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.person_handle_id_seq OWNED BY lpm.person_handle.id;


--
-- Name: person_performance; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.person_performance (
    id smallint NOT NULL,
    person_id integer NOT NULL,
    dept_id integer NOT NULL,
    performance real NOT NULL,
    date date NOT NULL
);


ALTER TABLE lpm.person_performance OWNER TO postgres;

--
-- Name: person_performance_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.person_performance_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.person_performance_id_seq OWNER TO postgres;

--
-- Name: person_performance_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.person_performance_id_seq OWNED BY lpm.person_performance.id;


--
-- Name: person_proposal; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.person_proposal (
    id smallint NOT NULL,
    person_id integer NOT NULL,
    proposal_num integer NOT NULL,
    dept_id integer NOT NULL,
    date date
);


ALTER TABLE lpm.person_proposal OWNER TO postgres;

--
-- Name: person_proposal_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.person_proposal_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.person_proposal_id_seq OWNER TO postgres;

--
-- Name: person_proposal_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.person_proposal_id_seq OWNED BY lpm.person_proposal.id;


--
-- Name: person_quality_record; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.person_quality_record (
    id smallint NOT NULL,
    person_id integer NOT NULL,
    shift smallint NOT NULL,
    pass_num numeric(16,0) NOT NULL,
    total_num numeric(32,0) NOT NULL,
    date date NOT NULL,
    quality_rate numeric(24,2) NOT NULL,
    dept_id integer NOT NULL
);


ALTER TABLE lpm.person_quality_record OWNER TO postgres;

--
-- Name: person_shift; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.person_shift (
    id integer NOT NULL,
    person_id integer NOT NULL,
    schedule_id integer NOT NULL,
    shift integer NOT NULL,
    machine_id integer NOT NULL
);


ALTER TABLE lpm.person_shift OWNER TO postgres;

--
-- Name: person_shift_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.person_shift_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.person_shift_id_seq OWNER TO postgres;

--
-- Name: person_shift_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.person_shift_id_seq OWNED BY lpm.person_shift.id;


--
-- Name: productivity_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.productivity_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.productivity_id_seq OWNER TO postgres;

--
-- Name: productivity_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.productivity_id_seq OWNED BY lpm.efficiencydetail.id;


--
-- Name: proposal; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.proposal (
    id integer NOT NULL,
    person_id integer NOT NULL,
    title character varying(255) NOT NULL,
    content text,
    date date
);


ALTER TABLE lpm.proposal OWNER TO postgres;

--
-- Name: proposal_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.proposal_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.proposal_id_seq OWNER TO postgres;

--
-- Name: proposal_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.proposal_id_seq OWNED BY lpm.proposal.id;


--
-- Name: proposal_person_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.proposal_person_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.proposal_person_id_seq OWNER TO postgres;

--
-- Name: proposal_person_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.proposal_person_id_seq OWNED BY lpm.proposal.person_id;


--
-- Name: quality_record_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.quality_record_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.quality_record_id_seq OWNER TO postgres;

--
-- Name: quality_record_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.quality_record_id_seq OWNED BY lpm.person_quality_record.id;


--
-- Name: schedule; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.schedule (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    start_time timestamp(6) without time zone NOT NULL,
    end_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE lpm.schedule OWNER TO postgres;

--
-- Name: schedule_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.schedule_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.schedule_id_seq OWNER TO postgres;

--
-- Name: schedule_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.schedule_id_seq OWNED BY lpm.schedule.id;


--
-- Name: test_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.test_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.test_id_seq OWNER TO postgres;

--
-- Name: test_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.test_id_seq OWNED BY lpm.performance_formula.id;


--
-- Name: waitingtime_day; Type: TABLE; Schema: lpm; Owner: postgres
--

CREATE TABLE lpm.waitingtime_day (
    id smallint NOT NULL,
    machine_id integer NOT NULL,
    waiting_time numeric(24,2) NOT NULL,
    date date NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE lpm.waitingtime_day OWNER TO postgres;

--
-- Name: waitingtime_day_id_seq; Type: SEQUENCE; Schema: lpm; Owner: postgres
--

CREATE SEQUENCE lpm.waitingtime_day_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE lpm.waitingtime_day_id_seq OWNER TO postgres;

--
-- Name: waitingtime_day_id_seq; Type: SEQUENCE OWNED BY; Schema: lpm; Owner: postgres
--

ALTER SEQUENCE lpm.waitingtime_day_id_seq OWNED BY lpm.waitingtime_day.id;


--
-- Name: layer_status_duration_day; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_status_duration_day (
    id smallint NOT NULL,
    status_name character varying(10) NOT NULL,
    upper_id integer NOT NULL,
    duration_time real NOT NULL,
    date date NOT NULL,
    area_layer_id integer NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL,
    area_node_id integer NOT NULL
);


ALTER TABLE oee.layer_status_duration_day OWNER TO postgres;

--
-- Name: layer_status_duration_day_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_status_duration_day_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_status_duration_day_id_seq OWNER TO postgres;

--
-- Name: layer_status_duration_day_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_status_duration_day_id_seq OWNED BY oee.layer_status_duration_day.id;


--
-- Name: layer_status_duration_week; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_status_duration_week (
    id smallint NOT NULL,
    status_name character varying(10) NOT NULL,
    upper_id integer NOT NULL,
    duration_time real NOT NULL,
    week integer NOT NULL,
    area_layer_id integer NOT NULL,
    area_node_id integer NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE oee.layer_status_duration_week OWNER TO postgres;

--
-- Name: layer_status_duration_week_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_status_duration_week_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_status_duration_week_id_seq OWNER TO postgres;

--
-- Name: layer_status_duration_week_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_status_duration_week_id_seq OWNED BY oee.layer_status_duration_week.id;


--
-- Name: layer_status_duration_month; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_status_duration_month (
    id smallint DEFAULT nextval('oee.layer_status_duration_week_id_seq'::regclass) NOT NULL,
    status_name character varying(10) NOT NULL,
    upper_id integer NOT NULL,
    duration_time real NOT NULL,
    month integer NOT NULL,
    area_layer_id integer NOT NULL,
    area_node_id integer NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE oee.layer_status_duration_month OWNER TO postgres;

--
-- Name: workorder_status_duration; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.workorder_status_duration (
    id smallint NOT NULL,
    status_name character varying(50),
    duration_time real,
    upper_id integer,
    work_order character varying(50),
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.workorder_status_duration OWNER TO postgres;

--
-- Name: layer_status_duration_order_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_status_duration_order_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_status_duration_order_id_seq OWNER TO postgres;

--
-- Name: layer_status_duration_order_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_status_duration_order_id_seq OWNED BY oee.workorder_status_duration.id;


--
-- Name: layer_status_duration_shift; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_status_duration_shift (
    id smallint NOT NULL,
    area_layer_id integer,
    area_node_id integer,
    status_name character varying(10),
    duration_time real,
    upper_id integer,
    shift character varying(10),
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.layer_status_duration_shift OWNER TO postgres;

--
-- Name: layer_utilization_rate_shift; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_utilization_rate_shift (
    id smallint NOT NULL,
    area_layer_id integer,
    area_node_id integer,
    shift character varying(10),
    utilization_rate real,
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.layer_utilization_rate_shift OWNER TO postgres;

--
-- Name: layer_status_duration_shift_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_status_duration_shift_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_status_duration_shift_id_seq OWNER TO postgres;

--
-- Name: layer_status_duration_shift_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_status_duration_shift_id_seq OWNED BY oee.layer_utilization_rate_shift.id;


--
-- Name: layer_status_duration_shift_id_seq1; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_status_duration_shift_id_seq1
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_status_duration_shift_id_seq1 OWNER TO postgres;

--
-- Name: layer_status_duration_shift_id_seq1; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_status_duration_shift_id_seq1 OWNED BY oee.layer_status_duration_shift.id;


--
-- Name: layer_utilization_rate_day; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_utilization_rate_day (
    id smallint NOT NULL,
    area_layer_id integer NOT NULL,
    utilization_rate real,
    date date NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL,
    area_node_id integer NOT NULL
);


ALTER TABLE oee.layer_utilization_rate_day OWNER TO postgres;

--
-- Name: layer_utilization_rate_day_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_utilization_rate_day_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_utilization_rate_day_id_seq OWNER TO postgres;

--
-- Name: layer_utilization_rate_day_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_utilization_rate_day_id_seq OWNED BY oee.layer_utilization_rate_day.id;


--
-- Name: layer_utilization_rate_week; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_utilization_rate_week (
    id smallint NOT NULL,
    area_layer_id integer NOT NULL,
    area_node_id integer NOT NULL,
    utilization_rate real NOT NULL,
    week integer NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE oee.layer_utilization_rate_week OWNER TO postgres;

--
-- Name: layer_utilization_rate_week_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_utilization_rate_week_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_utilization_rate_week_id_seq OWNER TO postgres;

--
-- Name: layer_utilization_rate_week_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_utilization_rate_week_id_seq OWNED BY oee.layer_utilization_rate_week.id;


--
-- Name: layer_utilization_rate_month; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.layer_utilization_rate_month (
    id smallint DEFAULT nextval('oee.layer_utilization_rate_week_id_seq'::regclass) NOT NULL,
    area_layer_id integer NOT NULL,
    area_node_id integer NOT NULL,
    utilization_rate real NOT NULL,
    month integer NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE oee.layer_utilization_rate_month OWNER TO postgres;

--
-- Name: workorder_utilization_rate; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.workorder_utilization_rate (
    id smallint NOT NULL,
    work_order character varying(50),
    utilization_rate real,
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.workorder_utilization_rate OWNER TO postgres;

--
-- Name: layer_utilization_rate_order_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.layer_utilization_rate_order_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.layer_utilization_rate_order_id_seq OWNER TO postgres;

--
-- Name: layer_utilization_rate_order_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.layer_utilization_rate_order_id_seq OWNED BY oee.workorder_utilization_rate.id;


--
-- Name: machine_lease; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.machine_lease (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    unit_price numeric(24,0) NOT NULL,
    start_time timestamp(6) without time zone NOT NULL,
    type integer,
    total_price numeric(24,0)
);


ALTER TABLE oee.machine_lease OWNER TO postgres;

--
-- Name: machine_lease_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.machine_lease_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.machine_lease_id_seq OWNER TO postgres;

--
-- Name: machine_lease_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.machine_lease_id_seq OWNED BY oee.machine_lease.id;


--
-- Name: machine_lease_log; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.machine_lease_log (
    id integer NOT NULL,
    machine_id integer,
    run_time real,
    consumption_price numeric(24,0),
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.machine_lease_log OWNER TO postgres;

--
-- Name: machine_lease_log_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.machine_lease_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.machine_lease_log_id_seq OWNER TO postgres;

--
-- Name: machine_lease_log_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.machine_lease_log_id_seq OWNED BY oee.machine_lease_log.id;


--
-- Name: maintenance_records; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.maintenance_records (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    "time" timestamp without time zone NOT NULL,
    description character varying(1000)
);


ALTER TABLE oee.maintenance_records OWNER TO postgres;

--
-- Name: maintenance_records_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.maintenance_records_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.maintenance_records_id_seq OWNER TO postgres;

--
-- Name: maintenance_records_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.maintenance_records_id_seq OWNED BY oee.maintenance_records.id;


--
-- Name: status_duration_day; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.status_duration_day (
    id integer NOT NULL,
    status_name character varying(10),
    upper_id integer NOT NULL,
    duration_time real,
    date date,
    insert_time timestamp(6) without time zone,
    machine_id integer
);


ALTER TABLE oee.status_duration_day OWNER TO postgres;

--
-- Name: status_duration_day_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.status_duration_day_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.status_duration_day_id_seq OWNER TO postgres;

--
-- Name: status_duration_day_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.status_duration_day_id_seq OWNED BY oee.status_duration_day.id;


--
-- Name: status_duration_month; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.status_duration_month (
    id smallint NOT NULL,
    machine_id integer,
    status_name character varying(10),
    upper_id integer,
    duration_time real,
    month smallint,
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.status_duration_month OWNER TO postgres;

--
-- Name: status_duration_month_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.status_duration_month_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.status_duration_month_id_seq OWNER TO postgres;

--
-- Name: status_duration_month_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.status_duration_month_id_seq OWNED BY oee.status_duration_month.id;


--
-- Name: status_duration_order; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.status_duration_order (
    id integer NOT NULL,
    status_name character varying(10),
    duration_time real,
    upper_id integer NOT NULL,
    work_order character varying(255),
    insert_time timestamp(6) without time zone,
    machine_id integer
);


ALTER TABLE oee.status_duration_order OWNER TO postgres;

--
-- Name: status_duration_order_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.status_duration_order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.status_duration_order_id_seq OWNER TO postgres;

--
-- Name: status_duration_order_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.status_duration_order_id_seq OWNED BY oee.status_duration_order.id;


--
-- Name: status_duration_shift; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.status_duration_shift (
    id integer NOT NULL,
    status_name character varying(10),
    duration_time real,
    upper_id integer,
    shift character varying(10),
    insert_time timestamp(6) without time zone,
    machine_id integer
);


ALTER TABLE oee.status_duration_shift OWNER TO postgres;

--
-- Name: status_duration_shift_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.status_duration_shift_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.status_duration_shift_id_seq OWNER TO postgres;

--
-- Name: status_duration_shift_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.status_duration_shift_id_seq OWNED BY oee.status_duration_shift.id;


--
-- Name: status_duration_week; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.status_duration_week (
    id integer NOT NULL,
    machine_id integer,
    status_name character varying(10),
    upper_id integer,
    duration_time real,
    week integer,
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.status_duration_week OWNER TO postgres;

--
-- Name: status_duration_week_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.status_duration_week_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.status_duration_week_id_seq OWNER TO postgres;

--
-- Name: status_duration_week_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.status_duration_week_id_seq OWNED BY oee.status_duration_week.id;


--
-- Name: status_setting; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.status_setting (
    id integer NOT NULL,
    status_name character varying(100) NOT NULL,
    value integer NOT NULL
);


ALTER TABLE oee.status_setting OWNER TO postgres;

--
-- Name: status_setting_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.status_setting_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.status_setting_id_seq OWNER TO postgres;

--
-- Name: status_setting_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.status_setting_id_seq OWNED BY oee.status_setting.id;


--
-- Name: tag_time_day; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.tag_time_day (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    status_name character varying(255),
    date timestamp(0) without time zone,
    duration_time real
);


ALTER TABLE oee.tag_time_day OWNER TO postgres;

--
-- Name: tag_time_day_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.tag_time_day_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.tag_time_day_id_seq OWNER TO postgres;

--
-- Name: tag_time_day_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.tag_time_day_id_seq OWNED BY oee.tag_time_day.id;


--
-- Name: tag_time_shift; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.tag_time_shift (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    status_name character varying(255),
    date timestamp(4) without time zone,
    shift character varying(255),
    duration_time real
);


ALTER TABLE oee.tag_time_shift OWNER TO postgres;

--
-- Name: tag_time_shift_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.tag_time_shift_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.tag_time_shift_id_seq OWNER TO postgres;

--
-- Name: tag_time_shift_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.tag_time_shift_id_seq OWNED BY oee.tag_time_shift.id;


--
-- Name: tricolor_tag_duration; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.tricolor_tag_duration (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    status_name character varying(255),
    duration_time real,
    insert_time timestamp(4) without time zone,
    away_time timestamp(4) without time zone,
    whether boolean
);


ALTER TABLE oee.tricolor_tag_duration OWNER TO postgres;

--
-- Name: tricolor_tag_duration_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.tricolor_tag_duration_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.tricolor_tag_duration_id_seq OWNER TO postgres;

--
-- Name: tricolor_tag_duration_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.tricolor_tag_duration_id_seq OWNED BY oee.tricolor_tag_duration.id;


--
-- Name: tricolor_tag_status; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.tricolor_tag_status (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    status_name character varying(255),
    insert_time timestamp(4) without time zone,
    whether boolean
);


ALTER TABLE oee.tricolor_tag_status OWNER TO postgres;

--
-- Name: tricolor_tag_status_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.tricolor_tag_status_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.tricolor_tag_status_id_seq OWNER TO postgres;

--
-- Name: tricolor_tag_status_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.tricolor_tag_status_id_seq OWNED BY oee.tricolor_tag_status.id;


--
-- Name: utilization_rate_day; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.utilization_rate_day (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    date date,
    utilization_rate real,
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.utilization_rate_day OWNER TO postgres;

--
-- Name: utilization_rate_day_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.utilization_rate_day_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.utilization_rate_day_id_seq OWNER TO postgres;

--
-- Name: utilization_rate_day_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.utilization_rate_day_id_seq OWNED BY oee.utilization_rate_day.id;


--
-- Name: utilization_rate_formula; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.utilization_rate_formula (
    id integer NOT NULL,
    formula character varying(255)
);


ALTER TABLE oee.utilization_rate_formula OWNER TO postgres;

--
-- Name: utilization_rate_formula_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.utilization_rate_formula_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.utilization_rate_formula_id_seq OWNER TO postgres;

--
-- Name: utilization_rate_formula_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.utilization_rate_formula_id_seq OWNED BY oee.utilization_rate_formula.id;


--
-- Name: utilization_rate_month; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.utilization_rate_month (
    machine_id integer NOT NULL,
    utilization_rate real,
    insert_time timestamp(6) without time zone,
    month integer,
    id smallint NOT NULL
);


ALTER TABLE oee.utilization_rate_month OWNER TO postgres;

--
-- Name: utilization_rate_month_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.utilization_rate_month_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.utilization_rate_month_id_seq OWNER TO postgres;

--
-- Name: utilization_rate_month_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.utilization_rate_month_id_seq OWNED BY oee.utilization_rate_month.id;


--
-- Name: utilization_rate_order; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.utilization_rate_order (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    work_order character varying(50),
    part_number character varying(50),
    utilization_rate real,
    insert_time timestamp(4) without time zone
);


ALTER TABLE oee.utilization_rate_order OWNER TO postgres;

--
-- Name: utilization_rate_order_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.utilization_rate_order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.utilization_rate_order_id_seq OWNER TO postgres;

--
-- Name: utilization_rate_order_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.utilization_rate_order_id_seq OWNED BY oee.utilization_rate_order.id;


--
-- Name: utilization_rate_shift; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.utilization_rate_shift (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    date date,
    shift character varying(32),
    utilization_rate real,
    insert_time timestamp(6) without time zone
);


ALTER TABLE oee.utilization_rate_shift OWNER TO postgres;

--
-- Name: utilization_rate_shift_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.utilization_rate_shift_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.utilization_rate_shift_id_seq OWNER TO postgres;

--
-- Name: utilization_rate_shift_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.utilization_rate_shift_id_seq OWNED BY oee.utilization_rate_shift.id;


--
-- Name: utilization_rate_week; Type: TABLE; Schema: oee; Owner: postgres
--

CREATE TABLE oee.utilization_rate_week (
    machine_id integer NOT NULL,
    utilization_rate real NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL,
    week integer,
    id smallint NOT NULL
);


ALTER TABLE oee.utilization_rate_week OWNER TO postgres;

--
-- Name: utilization_rate_week_id_seq; Type: SEQUENCE; Schema: oee; Owner: postgres
--

CREATE SEQUENCE oee.utilization_rate_week_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE oee.utilization_rate_week_id_seq OWNER TO postgres;

--
-- Name: utilization_rate_week_id_seq; Type: SEQUENCE OWNED BY; Schema: oee; Owner: postgres
--

ALTER SEQUENCE oee.utilization_rate_week_id_seq OWNED BY oee.utilization_rate_week.id;


--
-- Name: breakpoint_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.breakpoint_log (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    work_order_id integer NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL,
    breakpoint integer NOT NULL
);


ALTER TABLE work_order.breakpoint_log OWNER TO postgres;

--
-- Name: breakpoint_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.breakpoint_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.breakpoint_log_id_seq OWNER TO postgres;

--
-- Name: breakpoint_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.breakpoint_log_id_seq OWNED BY work_order.breakpoint_log.id;


--
-- Name: capacity_config; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.capacity_config (
    id integer NOT NULL,
    date date NOT NULL,
    capacity numeric(8,2) NOT NULL,
    utilization numeric(8,2)
);


ALTER TABLE work_order.capacity_config OWNER TO postgres;

--
-- Name: capacity_config_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.capacity_config_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.capacity_config_id_seq OWNER TO postgres;

--
-- Name: capacity_config_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.capacity_config_id_seq OWNED BY work_order.capacity_config.id;


--
-- Name: capacity_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.capacity_log (
    id integer NOT NULL,
    year integer NOT NULL,
    month integer NOT NULL,
    count integer NOT NULL,
    date date NOT NULL
);


ALTER TABLE work_order.capacity_log OWNER TO postgres;

--
-- Name: capacity_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.capacity_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.capacity_log_id_seq OWNER TO postgres;

--
-- Name: capacity_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.capacity_log_id_seq OWNED BY work_order.capacity_log.id;


--
-- Name: ct_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.ct_log (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    work_order_id integer NOT NULL,
    tag_type_sub_id integer NOT NULL,
    cycle_time numeric(8,2) NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE work_order.ct_log OWNER TO postgres;

--
-- Name: ct_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.ct_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.ct_log_id_seq OWNER TO postgres;

--
-- Name: ct_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.ct_log_id_seq OWNED BY work_order.ct_log.id;


--
-- Name: initial_material_info; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.initial_material_info (
    id integer NOT NULL,
    worker_order_id integer NOT NULL,
    machine_id integer NOT NULL,
    materiel character varying(100) NOT NULL,
    count numeric(6,2) NOT NULL
);


ALTER TABLE work_order.initial_material_info OWNER TO postgres;

--
-- Name: initial_material_info_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.initial_material_info_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.initial_material_info_id_seq OWNER TO postgres;

--
-- Name: initial_material_info_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.initial_material_info_id_seq OWNED BY work_order.initial_material_info.id;


--
-- Name: machine_capacity_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.machine_capacity_log (
    id integer NOT NULL,
    year integer NOT NULL,
    count integer NOT NULL,
    machine_id integer NOT NULL,
    month integer NOT NULL,
    date date NOT NULL
);


ALTER TABLE work_order.machine_capacity_log OWNER TO postgres;

--
-- Name: machine_capacity_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.machine_capacity_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.machine_capacity_log_id_seq OWNER TO postgres;

--
-- Name: machine_capacity_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.machine_capacity_log_id_seq OWNED BY work_order.machine_capacity_log.id;


--
-- Name: overdue_work_order; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.overdue_work_order (
    id integer NOT NULL,
    start_time timestamp(0) without time zone NOT NULL,
    end_time timestamp(0) without time zone NOT NULL,
    overdue_time numeric(16,2) NOT NULL,
    wo_config_id integer NOT NULL,
    responsible_unit character varying(255),
    reason character varying(1000)
);


ALTER TABLE work_order.overdue_work_order OWNER TO postgres;

--
-- Name: overdue_work_order_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.overdue_work_order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.overdue_work_order_id_seq OWNER TO postgres;

--
-- Name: overdue_work_order_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.overdue_work_order_id_seq OWNED BY work_order.overdue_work_order.id;


--
-- Name: virtual_line; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.virtual_line (
    id integer NOT NULL,
    name_cn character varying(255) NOT NULL,
    name_en character varying(255) NOT NULL,
    name_tw character varying(255) NOT NULL,
    description character varying(255)
);


ALTER TABLE work_order.virtual_line OWNER TO postgres;

--
-- Name: virtual_line_current_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.virtual_line_current_log (
    id integer NOT NULL,
    virtual_line_id integer NOT NULL,
    wo_config_id integer NOT NULL,
    start_time timestamp(6) without time zone NOT NULL,
    balance_rate numeric(8,2),
    productivity numeric(8,2),
    quantity numeric(8,2),
    bad_quantity numeric(8,2),
    change_over numeric(10,2),
    production_efficiency numeric(8,2)
);


ALTER TABLE work_order.virtual_line_current_log OWNER TO postgres;

--
-- Name: virtual_line_current_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.virtual_line_current_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.virtual_line_current_log_id_seq OWNER TO postgres;

--
-- Name: virtual_line_current_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.virtual_line_current_log_id_seq OWNED BY work_order.virtual_line_current_log.id;


--
-- Name: virtual_line_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.virtual_line_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.virtual_line_id_seq OWNER TO postgres;

--
-- Name: virtual_line_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.virtual_line_id_seq OWNED BY work_order.virtual_line.id;


--
-- Name: virtual_line_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.virtual_line_log (
    id integer NOT NULL,
    virtual_line_id integer NOT NULL,
    wo_config_id integer NOT NULL,
    start_time timestamp(6) without time zone NOT NULL,
    end_time timestamp(6) without time zone NOT NULL,
    balance_rate numeric(8,2),
    productivity numeric(8,2),
    quantity numeric(8,2),
    bad_quantity numeric(8,2),
    change_over numeric(10,2),
    production_efficiency numeric(8,2)
);


ALTER TABLE work_order.virtual_line_log OWNER TO postgres;

--
-- Name: virtual_line_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.virtual_line_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.virtual_line_log_id_seq OWNER TO postgres;

--
-- Name: virtual_line_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.virtual_line_log_id_seq OWNED BY work_order.virtual_line_log.id;


--
-- Name: wo_config; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.wo_config (
    id integer NOT NULL,
    virtual_line_id integer NOT NULL,
    work_order character varying(255) NOT NULL,
    part_num character varying(255) NOT NULL,
    shift integer NOT NULL,
    standard_num integer NOT NULL,
    auto boolean NOT NULL,
    order_index integer NOT NULL,
    status smallint NOT NULL,
    standard_time character varying(255) NOT NULL,
    create_time timestamp(6) without time zone NOT NULL,
    lbr_formula character varying(255)
);


ALTER TABLE work_order.wo_config OWNER TO postgres;

--
-- Name: wo_config_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.wo_config_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.wo_config_id_seq OWNER TO postgres;

--
-- Name: wo_config_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.wo_config_id_seq OWNED BY work_order.wo_config.id;


--
-- Name: wo_machine; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.wo_machine (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    virtual_line_id integer NOT NULL
);


ALTER TABLE work_order.wo_machine OWNER TO postgres;

--
-- Name: wo_machine_current_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.wo_machine_current_log (
    id integer NOT NULL,
    machine_id integer NOT NULL,
    wo_config_id integer,
    standard_time numeric(8,2) NOT NULL,
    start_time timestamp(0) without time zone,
    quantity numeric(8,2) DEFAULT 0 NOT NULL,
    bad_quantity numeric(8,2) DEFAULT 0 NOT NULL,
    achieving_rate numeric(8,2),
    productivity numeric(8,2),
    cycle_time numeric(8,2),
    cycle_time_average numeric(8,2),
    standard_num integer NOT NULL,
    production_efficiency numeric(8,2)
);


ALTER TABLE work_order.wo_machine_current_log OWNER TO postgres;

--
-- Name: wo_machine_current_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.wo_machine_current_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.wo_machine_current_log_id_seq OWNER TO postgres;

--
-- Name: wo_machine_current_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.wo_machine_current_log_id_seq OWNED BY work_order.wo_machine_current_log.id;


--
-- Name: wo_machine_detail; Type: VIEW; Schema: work_order; Owner: postgres
--

CREATE VIEW work_order.wo_machine_detail AS
 SELECT wo_machine.id,
    wo_machine.virtual_line_id,
    wo_machine.machine_id,
    machine.name_cn,
    machine.name_en,
    machine.name_tw,
    machine.description,
    machine.area_node_id
   FROM (work_order.wo_machine
     JOIN common.machine ON ((wo_machine.machine_id = machine.id)));


ALTER TABLE work_order.wo_machine_detail OWNER TO postgres;

--
-- Name: wo_machine_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.wo_machine_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.wo_machine_id_seq OWNER TO postgres;

--
-- Name: wo_machine_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.wo_machine_id_seq OWNED BY work_order.wo_machine.id;


--
-- Name: wo_machine_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.wo_machine_log (
    id integer NOT NULL,
    wo_config_id integer,
    machine_id integer NOT NULL,
    standard_time numeric(8,2) NOT NULL,
    start_time timestamp(0) without time zone,
    end_time timestamp(0) without time zone,
    quantity numeric(8,2) DEFAULT 0 NOT NULL,
    bad_quantity numeric(8,2) DEFAULT 0 NOT NULL,
    achieving_rate numeric(8,2),
    productivity numeric(8,2),
    cycle_time numeric(8,2),
    cycle_time_average numeric(8,2),
    standard_num integer NOT NULL,
    production_efficiency numeric(8,2)
);


ALTER TABLE work_order.wo_machine_log OWNER TO postgres;

--
-- Name: wo_machine_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.wo_machine_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.wo_machine_log_id_seq OWNER TO postgres;

--
-- Name: wo_machine_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.wo_machine_log_id_seq OWNED BY work_order.wo_machine_log.id;


--
-- Name: work_order_detail; Type: VIEW; Schema: work_order; Owner: postgres
--

CREATE VIEW work_order.work_order_detail AS
 SELECT wo_config.id,
    wo_config.virtual_line_id,
    wo_config.work_order,
    wo_config.part_num,
    wo_config.shift,
    wo_config.auto,
    wo_config.status,
    wo_config.lbr_formula,
    wo_machine_current_log.machine_id,
    wo_machine_current_log.standard_time,
    wo_machine_current_log.start_time,
    NULL::timestamp with time zone AS end_time,
    wo_machine_current_log.quantity,
    wo_machine_current_log.bad_quantity,
    wo_machine_current_log.cycle_time,
    wo_machine_current_log.cycle_time_average,
    wo_machine_current_log.standard_num
   FROM (work_order.wo_config
     JOIN work_order.wo_machine_current_log ON ((wo_machine_current_log.wo_config_id = wo_config.id)))
UNION ALL
 SELECT wo_config.id,
    wo_config.virtual_line_id,
    wo_config.work_order,
    wo_config.part_num,
    wo_config.shift,
    wo_config.auto,
    wo_config.status,
    wo_config.lbr_formula,
    wo_machine_log.machine_id,
    wo_machine_log.standard_time,
    wo_machine_log.start_time,
    wo_machine_log.end_time,
    wo_machine_log.quantity,
    wo_machine_log.bad_quantity,
    wo_machine_log.cycle_time,
    wo_machine_log.cycle_time_average,
    wo_machine_log.standard_num
   FROM (work_order.wo_config
     JOIN work_order.wo_machine_log ON ((wo_machine_log.wo_config_id = wo_config.id)));


ALTER TABLE work_order.work_order_detail OWNER TO postgres;

--
-- Name: work_order_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.work_order_log (
    id integer NOT NULL,
    year integer NOT NULL,
    month integer NOT NULL,
    count integer NOT NULL,
    date date NOT NULL
);


ALTER TABLE work_order.work_order_log OWNER TO postgres;

--
-- Name: work_order_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.work_order_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.work_order_log_id_seq OWNER TO postgres;

--
-- Name: work_order_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.work_order_log_id_seq OWNED BY work_order.work_order_log.id;


--
-- Name: worker_exception_log; Type: TABLE; Schema: work_order; Owner: postgres
--

CREATE TABLE work_order.worker_exception_log (
    id integer NOT NULL,
    function character varying(255),
    content character varying(2000) NOT NULL,
    insert_time timestamp(6) without time zone NOT NULL
);


ALTER TABLE work_order.worker_exception_log OWNER TO postgres;

--
-- Name: worker_exception_log_id_seq; Type: SEQUENCE; Schema: work_order; Owner: postgres
--

CREATE SEQUENCE work_order.worker_exception_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE work_order.worker_exception_log_id_seq OWNER TO postgres;

--
-- Name: worker_exception_log_id_seq; Type: SEQUENCE OWNED BY; Schema: work_order; Owner: postgres
--

ALTER SEQUENCE work_order.worker_exception_log_id_seq OWNED BY work_order.worker_exception_log.id;


--
-- Name: alert_mes id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.alert_mes ALTER COLUMN id SET DEFAULT nextval('andon.alert_mes_id_seq'::regclass);


--
-- Name: andon_logic id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.andon_logic ALTER COLUMN id SET DEFAULT nextval('andon.andon_logic_id_seq'::regclass);


--
-- Name: capacity_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.capacity_alert ALTER COLUMN id SET DEFAULT nextval('andon.capacity_alert_id_seq'::regclass);


--
-- Name: error_config id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_config ALTER COLUMN id SET DEFAULT nextval('andon.error_config_id_seq'::regclass);


--
-- Name: error_log id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_log ALTER COLUMN id SET DEFAULT nextval('andon.error_log_id_seq'::regclass);


--
-- Name: error_log_mes id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_log_mes ALTER COLUMN id SET DEFAULT nextval('andon.error_log_mes_id_seq'::regclass);


--
-- Name: error_month id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_month ALTER COLUMN id SET DEFAULT nextval('andon.error_month_id_seq'::regclass);


--
-- Name: error_type id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type ALTER COLUMN id SET DEFAULT nextval('andon.error_type_id_seq'::regclass);


--
-- Name: error_type_details id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type_details ALTER COLUMN id SET DEFAULT nextval('andon.error_type_details_id_seq'::regclass);


--
-- Name: error_week id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_week ALTER COLUMN id SET DEFAULT nextval('andon.error_week_id_seq'::regclass);


--
-- Name: machine_cost_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_cost_alert ALTER COLUMN id SET DEFAULT nextval('andon.machine_cost_alert_id_seq'::regclass);


--
-- Name: machine_fault_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_fault_alert ALTER COLUMN id SET DEFAULT nextval('andon.machine_fault_alert_id_seq'::regclass);


--
-- Name: machine_status_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_alert ALTER COLUMN id SET DEFAULT nextval('andon.machine_status_alert_id_seq'::regclass);


--
-- Name: machine_status_duration_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_duration_alert ALTER COLUMN id SET DEFAULT nextval('andon.machine_status_duration_alert_id_seq'::regclass);


--
-- Name: material_request_info id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.material_request_info ALTER COLUMN id SET DEFAULT nextval('andon.material_request_info_id_seq'::regclass);


--
-- Name: notification_group id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_group ALTER COLUMN id SET DEFAULT nextval('andon.notification_group_id_seq'::regclass);


--
-- Name: notification_person id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_person ALTER COLUMN id SET DEFAULT nextval('andon.notification_person_id_seq'::regclass);


--
-- Name: quality_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.quality_alert ALTER COLUMN id SET DEFAULT nextval('andon.quality_alert_id_seq'::regclass);


--
-- Name: utilization_rate_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.utilization_rate_alert ALTER COLUMN id SET DEFAULT nextval('andon.utilization_rate_alert_id_seq'::regclass);


--
-- Name: work_order_alert id; Type: DEFAULT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.work_order_alert ALTER COLUMN id SET DEFAULT nextval('andon.work_order_alert_id_seq'::regclass);


--
-- Name: api_exception_log id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.api_exception_log ALTER COLUMN id SET DEFAULT nextval('common.api_exception_log_id_seq'::regclass);


--
-- Name: api_request_log id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.api_request_log ALTER COLUMN id SET DEFAULT nextval('common.api_request_log_id_seq'::regclass);


--
-- Name: area_layer id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_layer ALTER COLUMN id SET DEFAULT nextval('common.area_layer_id_seq'::regclass);


--
-- Name: area_node id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_node ALTER COLUMN id SET DEFAULT nextval('common.area_node_id_seq'::regclass);


--
-- Name: area_property id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_property ALTER COLUMN id SET DEFAULT nextval('common.area_property_id_seq'::regclass);


--
-- Name: department id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.department ALTER COLUMN id SET DEFAULT nextval('common.department_id_seq'::regclass);


--
-- Name: email_server id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.email_server ALTER COLUMN id SET DEFAULT nextval('common.email_server_id_seq'::regclass);


--
-- Name: machine id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.machine ALTER COLUMN id SET DEFAULT nextval('common.machine_id_seq'::regclass);


--
-- Name: migration_log id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.migration_log ALTER COLUMN id SET DEFAULT nextval('common.migration_log_id_seq'::regclass);


--
-- Name: mqtt_log id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.mqtt_log ALTER COLUMN id SET DEFAULT nextval('common.mqtt_log_id_seq'::regclass);


--
-- Name: person id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.person ALTER COLUMN id SET DEFAULT nextval('common.person_id_seq'::regclass);


--
-- Name: raw_date_custom id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.raw_date_custom ALTER COLUMN id SET DEFAULT nextval('common.raw_date_custom_id_seq'::regclass);


--
-- Name: srp_inner_log id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.srp_inner_log ALTER COLUMN id SET DEFAULT nextval('common.srp_inner_log_id_seq'::regclass);


--
-- Name: srp_log id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.srp_log ALTER COLUMN id SET DEFAULT nextval('common.srp_log_id_seq'::regclass);


--
-- Name: tag_info id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_info ALTER COLUMN id SET DEFAULT nextval('common.tag_info_id_seq'::regclass);


--
-- Name: tag_info_extra id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_info_extra ALTER COLUMN id SET DEFAULT nextval('common.tag_info_extra_id_seq'::regclass);


--
-- Name: tag_type id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type ALTER COLUMN id SET DEFAULT nextval('common.tag_type_id_seq'::regclass);


--
-- Name: tag_type_sub_fixed id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type_sub_fixed ALTER COLUMN id SET DEFAULT nextval('common.tag_type_sub_fixed_id_seq'::regclass);


--
-- Name: wechart_server id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.wechart_server ALTER COLUMN id SET DEFAULT nextval('common.wechart_server_id_seq'::regclass);


--
-- Name: wise_paas_user id; Type: DEFAULT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.wise_paas_user ALTER COLUMN id SET DEFAULT nextval('common.wise_paas_user_id_seq'::regclass);



--
-- Name: current_state id; Type: DEFAULT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.current_state ALTER COLUMN id SET DEFAULT nextval('ehs.current_state_id_seq'::regclass);


--
-- Name: exception_log id; Type: DEFAULT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.exception_log ALTER COLUMN id SET DEFAULT nextval('ehs.exception_log_id_seq'::regclass);


--
-- Name: notice_logic id; Type: DEFAULT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.notice_logic ALTER COLUMN id SET DEFAULT nextval('ehs.notice_logic_id_seq'::regclass);


--
-- Name: raw_data id; Type: DEFAULT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.raw_data ALTER COLUMN id SET DEFAULT nextval('ehs.raw_data_id_seq'::regclass);


--
-- Name: standard id; Type: DEFAULT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.standard ALTER COLUMN id SET DEFAULT nextval('ehs.standard_id_seq'::regclass);


--
-- Name: state_statistics id; Type: DEFAULT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.state_statistics ALTER COLUMN id SET DEFAULT nextval('ehs.state_trigger_statistics_id_seq'::regclass);


--
-- Name: attendance_log id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.attendance_log ALTER COLUMN id SET DEFAULT nextval('lpm.attendance_log_id_seq'::regclass);


--
-- Name: attendance_statistics id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.attendance_statistics ALTER COLUMN id SET DEFAULT nextval('lpm.attendance_id_seq'::regclass);


--
-- Name: attendance_summary id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.attendance_summary ALTER COLUMN id SET DEFAULT nextval('lpm.attendance_summary_id_seq'::regclass);


--
-- Name: department_efficiency id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_efficiency ALTER COLUMN id SET DEFAULT nextval('lpm.department_efficiency_id_seq'::regclass);


--
-- Name: department_handle id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_handle ALTER COLUMN id SET DEFAULT nextval('lpm.department_handle_id_seq'::regclass);


--
-- Name: department_performance id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_performance ALTER COLUMN id SET DEFAULT nextval('lpm.department_performance_id_seq'::regclass);


--
-- Name: department_proposal id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_proposal ALTER COLUMN id SET DEFAULT nextval('lpm.department_proposal_id_seq'::regclass);


--
-- Name: department_quality_record id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_quality_record ALTER COLUMN id SET DEFAULT nextval('lpm.department_quality_record_id_seq'::regclass);


--
-- Name: efficiencydetail id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.efficiencydetail ALTER COLUMN id SET DEFAULT nextval('lpm.productivity_id_seq'::regclass);


--
-- Name: leave_statistics id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.leave_statistics ALTER COLUMN id SET DEFAULT nextval('lpm.leave_statistics_id_seq'::regclass);


--
-- Name: overtime_statistics id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.overtime_statistics ALTER COLUMN id SET DEFAULT nextval('lpm.overtime_statistics_id_seq'::regclass);


--
-- Name: performance_formula id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.performance_formula ALTER COLUMN id SET DEFAULT nextval('lpm.test_id_seq'::regclass);


--
-- Name: person_efficiency id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_efficiency ALTER COLUMN id SET DEFAULT nextval('lpm.person_efficiency_id_seq'::regclass);


--
-- Name: person_handle id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_handle ALTER COLUMN id SET DEFAULT nextval('lpm.person_handle_id_seq'::regclass);


--
-- Name: person_performance id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_performance ALTER COLUMN id SET DEFAULT nextval('lpm.person_performance_id_seq'::regclass);


--
-- Name: person_proposal id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_proposal ALTER COLUMN id SET DEFAULT nextval('lpm.person_proposal_id_seq'::regclass);


--
-- Name: person_quality_record id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_quality_record ALTER COLUMN id SET DEFAULT nextval('lpm.quality_record_id_seq'::regclass);


--
-- Name: person_shift id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_shift ALTER COLUMN id SET DEFAULT nextval('lpm.person_shift_id_seq'::regclass);


--
-- Name: proposal id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.proposal ALTER COLUMN id SET DEFAULT nextval('lpm.proposal_id_seq'::regclass);


--
-- Name: proposal person_id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.proposal ALTER COLUMN person_id SET DEFAULT nextval('lpm.proposal_person_id_seq'::regclass);


--
-- Name: schedule id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.schedule ALTER COLUMN id SET DEFAULT nextval('lpm.schedule_id_seq'::regclass);


--
-- Name: waitingtime_day id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.waitingtime_day ALTER COLUMN id SET DEFAULT nextval('lpm.waitingtime_day_id_seq'::regclass);


--
-- Name: waitingtime_log id; Type: DEFAULT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.waitingtime_log ALTER COLUMN id SET DEFAULT nextval('lpm.machine_waitingtime_id_seq'::regclass);


--
-- Name: layer_status_duration_day id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_status_duration_day ALTER COLUMN id SET DEFAULT nextval('oee.layer_status_duration_day_id_seq'::regclass);


--
-- Name: layer_status_duration_shift id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_status_duration_shift ALTER COLUMN id SET DEFAULT nextval('oee.layer_status_duration_shift_id_seq1'::regclass);


--
-- Name: layer_status_duration_week id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_status_duration_week ALTER COLUMN id SET DEFAULT nextval('oee.layer_status_duration_week_id_seq'::regclass);


--
-- Name: layer_utilization_rate_day id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_utilization_rate_day ALTER COLUMN id SET DEFAULT nextval('oee.layer_utilization_rate_day_id_seq'::regclass);


--
-- Name: layer_utilization_rate_shift id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_utilization_rate_shift ALTER COLUMN id SET DEFAULT nextval('oee.layer_status_duration_shift_id_seq'::regclass);


--
-- Name: layer_utilization_rate_week id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_utilization_rate_week ALTER COLUMN id SET DEFAULT nextval('oee.layer_utilization_rate_week_id_seq'::regclass);


--
-- Name: machine_lease id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.machine_lease ALTER COLUMN id SET DEFAULT nextval('oee.machine_lease_id_seq'::regclass);


--
-- Name: machine_lease_log id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.machine_lease_log ALTER COLUMN id SET DEFAULT nextval('oee.machine_lease_log_id_seq'::regclass);


--
-- Name: maintenance_records id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.maintenance_records ALTER COLUMN id SET DEFAULT nextval('oee.maintenance_records_id_seq'::regclass);


--
-- Name: status_duration_day id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_day ALTER COLUMN id SET DEFAULT nextval('oee.status_duration_day_id_seq'::regclass);


--
-- Name: status_duration_month id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_month ALTER COLUMN id SET DEFAULT nextval('oee.status_duration_month_id_seq'::regclass);


--
-- Name: status_duration_order id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_order ALTER COLUMN id SET DEFAULT nextval('oee.status_duration_order_id_seq'::regclass);


--
-- Name: status_duration_shift id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_shift ALTER COLUMN id SET DEFAULT nextval('oee.status_duration_shift_id_seq'::regclass);


--
-- Name: status_duration_week id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_week ALTER COLUMN id SET DEFAULT nextval('oee.status_duration_week_id_seq'::regclass);


--
-- Name: status_setting id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_setting ALTER COLUMN id SET DEFAULT nextval('oee.status_setting_id_seq'::regclass);


--
-- Name: tag_time_day id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tag_time_day ALTER COLUMN id SET DEFAULT nextval('oee.tag_time_day_id_seq'::regclass);


--
-- Name: tag_time_shift id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tag_time_shift ALTER COLUMN id SET DEFAULT nextval('oee.tag_time_shift_id_seq'::regclass);


--
-- Name: tricolor_tag_duration id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tricolor_tag_duration ALTER COLUMN id SET DEFAULT nextval('oee.tricolor_tag_duration_id_seq'::regclass);


--
-- Name: tricolor_tag_status id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tricolor_tag_status ALTER COLUMN id SET DEFAULT nextval('oee.tricolor_tag_status_id_seq'::regclass);


--
-- Name: utilization_rate_day id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_day ALTER COLUMN id SET DEFAULT nextval('oee.utilization_rate_day_id_seq'::regclass);


--
-- Name: utilization_rate_formula id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_formula ALTER COLUMN id SET DEFAULT nextval('oee.utilization_rate_formula_id_seq'::regclass);


--
-- Name: utilization_rate_month id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_month ALTER COLUMN id SET DEFAULT nextval('oee.utilization_rate_month_id_seq'::regclass);


--
-- Name: utilization_rate_order id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_order ALTER COLUMN id SET DEFAULT nextval('oee.utilization_rate_order_id_seq'::regclass);


--
-- Name: utilization_rate_shift id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_shift ALTER COLUMN id SET DEFAULT nextval('oee.utilization_rate_shift_id_seq'::regclass);


--
-- Name: utilization_rate_week id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_week ALTER COLUMN id SET DEFAULT nextval('oee.utilization_rate_week_id_seq'::regclass);


--
-- Name: workorder_status_duration id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.workorder_status_duration ALTER COLUMN id SET DEFAULT nextval('oee.layer_status_duration_order_id_seq'::regclass);


--
-- Name: workorder_utilization_rate id; Type: DEFAULT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.workorder_utilization_rate ALTER COLUMN id SET DEFAULT nextval('oee.layer_utilization_rate_order_id_seq'::regclass);


--
-- Name: breakpoint_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.breakpoint_log ALTER COLUMN id SET DEFAULT nextval('work_order.breakpoint_log_id_seq'::regclass);


--
-- Name: capacity_config id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.capacity_config ALTER COLUMN id SET DEFAULT nextval('work_order.capacity_config_id_seq'::regclass);


--
-- Name: capacity_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.capacity_log ALTER COLUMN id SET DEFAULT nextval('work_order.capacity_log_id_seq'::regclass);


--
-- Name: ct_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.ct_log ALTER COLUMN id SET DEFAULT nextval('work_order.ct_log_id_seq'::regclass);


--
-- Name: initial_material_info id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.initial_material_info ALTER COLUMN id SET DEFAULT nextval('work_order.initial_material_info_id_seq'::regclass);


--
-- Name: machine_capacity_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.machine_capacity_log ALTER COLUMN id SET DEFAULT nextval('work_order.machine_capacity_log_id_seq'::regclass);


--
-- Name: overdue_work_order id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.overdue_work_order ALTER COLUMN id SET DEFAULT nextval('work_order.overdue_work_order_id_seq'::regclass);


--
-- Name: virtual_line id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line ALTER COLUMN id SET DEFAULT nextval('work_order.virtual_line_id_seq'::regclass);


--
-- Name: virtual_line_current_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_current_log ALTER COLUMN id SET DEFAULT nextval('work_order.virtual_line_current_log_id_seq'::regclass);


--
-- Name: virtual_line_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_log ALTER COLUMN id SET DEFAULT nextval('work_order.virtual_line_log_id_seq'::regclass);


--
-- Name: wo_config id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_config ALTER COLUMN id SET DEFAULT nextval('work_order.wo_config_id_seq'::regclass);


--
-- Name: wo_machine id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine ALTER COLUMN id SET DEFAULT nextval('work_order.wo_machine_id_seq'::regclass);


--
-- Name: wo_machine_current_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_current_log ALTER COLUMN id SET DEFAULT nextval('work_order.wo_machine_current_log_id_seq'::regclass);


--
-- Name: wo_machine_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_log ALTER COLUMN id SET DEFAULT nextval('work_order.wo_machine_log_id_seq'::regclass);


--
-- Name: work_order_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.work_order_log ALTER COLUMN id SET DEFAULT nextval('work_order.work_order_log_id_seq'::regclass);


--
-- Name: worker_exception_log id; Type: DEFAULT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.worker_exception_log ALTER COLUMN id SET DEFAULT nextval('work_order.worker_exception_log_id_seq'::regclass);


--
-- Name: alert_mes alert_mes_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.alert_mes
    ADD CONSTRAINT alert_mes_pkey PRIMARY KEY (id);


--
-- Name: andon_logic andon_logic_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.andon_logic
    ADD CONSTRAINT andon_logic_pkey PRIMARY KEY (id);


--
-- Name: capacity_alert capacity_alert_date_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.capacity_alert
    ADD CONSTRAINT capacity_alert_date_key1 UNIQUE (date);


--
-- Name: capacity_alert capacity_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.capacity_alert
    ADD CONSTRAINT capacity_alert_pkey PRIMARY KEY (id);


--
-- Name: notification_person error_config_person_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_person
    ADD CONSTRAINT error_config_person_pkey PRIMARY KEY (id);


--
-- Name: error_config error_config_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_config
    ADD CONSTRAINT error_config_pkey PRIMARY KEY (id);


--
-- Name: error_log_mes error_log_mes_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_log_mes
    ADD CONSTRAINT error_log_mes_pkey PRIMARY KEY (id);


--
-- Name: error_log error_log_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_log
    ADD CONSTRAINT error_log_pkey PRIMARY KEY (id);


--
-- Name: error_month error_month_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_month
    ADD CONSTRAINT error_month_pkey PRIMARY KEY (id);


--
-- Name: error_type_details error_type_details_code_key; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type_details
    ADD CONSTRAINT error_type_details_code_key UNIQUE (code);


--
-- Name: error_type_details error_type_details_name_cn_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type_details
    ADD CONSTRAINT error_type_details_name_cn_key1 UNIQUE (name_cn);


--
-- Name: error_type_details error_type_details_name_en_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type_details
    ADD CONSTRAINT error_type_details_name_en_key1 UNIQUE (name_en);


--
-- Name: error_type_details error_type_details_name_tw_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type_details
    ADD CONSTRAINT error_type_details_name_tw_key1 UNIQUE (name_tw);


--
-- Name: error_type_details error_type_details_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type_details
    ADD CONSTRAINT error_type_details_pkey PRIMARY KEY (id);


--
-- Name: error_type error_type_name_cn_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type
    ADD CONSTRAINT error_type_name_cn_key1 UNIQUE (name_cn);


--
-- Name: error_type error_type_name_en_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type
    ADD CONSTRAINT error_type_name_en_key1 UNIQUE (name_en);


--
-- Name: error_type error_type_name_tw_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type
    ADD CONSTRAINT error_type_name_tw_key1 UNIQUE (name_tw);


--
-- Name: error_type error_type_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type
    ADD CONSTRAINT error_type_pkey PRIMARY KEY (id);


--
-- Name: machine_cost_alert machine_cost_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_cost_alert
    ADD CONSTRAINT machine_cost_alert_pkey PRIMARY KEY (id);


--
-- Name: machine_fault_alert machine_fault_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_fault_alert
    ADD CONSTRAINT machine_fault_alert_pkey PRIMARY KEY (id);


--
-- Name: machine_status_alert machine_status_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_alert
    ADD CONSTRAINT machine_status_alert_pkey PRIMARY KEY (id);


--
-- Name: machine_status_duration_alert machine_status_duration_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_duration_alert
    ADD CONSTRAINT machine_status_duration_alert_pkey PRIMARY KEY (id);


--
-- Name: material_request_info material_request_info_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.material_request_info
    ADD CONSTRAINT material_request_info_pkey PRIMARY KEY (id);


--
-- Name: notification_group notification_group_name_cn_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_group
    ADD CONSTRAINT notification_group_name_cn_key1 UNIQUE (name_cn);


--
-- Name: notification_group notification_group_name_en_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_group
    ADD CONSTRAINT notification_group_name_en_key1 UNIQUE (name_en);


--
-- Name: notification_group notification_group_name_tw_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_group
    ADD CONSTRAINT notification_group_name_tw_key1 UNIQUE (name_tw);


--
-- Name: notification_group notification_group_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_group
    ADD CONSTRAINT notification_group_pkey PRIMARY KEY (id);


--
-- Name: quality_alert quality_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.quality_alert
    ADD CONSTRAINT quality_alert_pkey PRIMARY KEY (id);


--
-- Name: quality_alert quality_alert_work_order_id_key1; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.quality_alert
    ADD CONSTRAINT quality_alert_work_order_id_key1 UNIQUE (work_order_id);


--
-- Name: utilization_rate_alert utilization_rate_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.utilization_rate_alert
    ADD CONSTRAINT utilization_rate_alert_pkey PRIMARY KEY (id);


--
-- Name: work_order_alert work_order_alert_pkey; Type: CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.work_order_alert
    ADD CONSTRAINT work_order_alert_pkey PRIMARY KEY (id);


--
-- Name: api_exception_log api_exception_log_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.api_exception_log
    ADD CONSTRAINT api_exception_log_pkey PRIMARY KEY (id);


--
-- Name: api_request_log api_request_log_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.api_request_log
    ADD CONSTRAINT api_request_log_pkey PRIMARY KEY (id);


--
-- Name: area_layer area_layer_name_cn_key; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_layer
    ADD CONSTRAINT area_layer_name_cn_key UNIQUE (name_cn);


--
-- Name: area_layer area_layer_name_en_key; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_layer
    ADD CONSTRAINT area_layer_name_en_key UNIQUE (name_en);


--
-- Name: area_layer area_layer_name_tw_key; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_layer
    ADD CONSTRAINT area_layer_name_tw_key UNIQUE (name_tw);


--
-- Name: area_layer area_layer_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_layer
    ADD CONSTRAINT area_layer_pkey PRIMARY KEY (id);


--
-- Name: area_node area_node_name_cn_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_node
    ADD CONSTRAINT area_node_name_cn_key1 UNIQUE (name_cn);


--
-- Name: area_node area_node_name_en_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_node
    ADD CONSTRAINT area_node_name_en_key1 UNIQUE (name_en);


--
-- Name: area_node area_node_name_tw_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_node
    ADD CONSTRAINT area_node_name_tw_key1 UNIQUE (name_tw);


--
-- Name: area_node area_node_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_node
    ADD CONSTRAINT area_node_pkey PRIMARY KEY (id);


--
-- Name: area_property area_property_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_property
    ADD CONSTRAINT area_property_pkey PRIMARY KEY (id);


--
-- Name: department department_name_cn_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.department
    ADD CONSTRAINT department_name_cn_key1 UNIQUE (name_cn);


--
-- Name: department department_name_en_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.department
    ADD CONSTRAINT department_name_en_key1 UNIQUE (name_en);


--
-- Name: department department_name_tw_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.department
    ADD CONSTRAINT department_name_tw_key1 UNIQUE (name_tw);


--
-- Name: department department_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.department
    ADD CONSTRAINT department_pkey PRIMARY KEY (id);


--
-- Name: email_server email_server_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.email_server
    ADD CONSTRAINT email_server_pkey PRIMARY KEY (id);


--
-- Name: machine machine_name_cn_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.machine
    ADD CONSTRAINT machine_name_cn_key1 UNIQUE (name_cn);


--
-- Name: machine machine_name_en_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.machine
    ADD CONSTRAINT machine_name_en_key1 UNIQUE (name_en);


--
-- Name: machine machine_name_tw_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.machine
    ADD CONSTRAINT machine_name_tw_key1 UNIQUE (name_tw);


--
-- Name: machine mahine_id_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.machine
    ADD CONSTRAINT mahine_id_pkey PRIMARY KEY (id);


--
-- Name: migration_log migration_log_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.migration_log
    ADD CONSTRAINT migration_log_pkey PRIMARY KEY (id);


--
-- Name: mqtt_log mqtt_log_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.mqtt_log
    ADD CONSTRAINT mqtt_log_pkey PRIMARY KEY (id);


--
-- Name: person person_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.person
    ADD CONSTRAINT person_pkey PRIMARY KEY (id);


--
-- Name: raw_date_custom raw_date_custom_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.raw_date_custom
    ADD CONSTRAINT raw_date_custom_pkey PRIMARY KEY (id);


--
-- Name: srp_inner_log srp_inner_log_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.srp_inner_log
    ADD CONSTRAINT srp_inner_log_pkey PRIMARY KEY (id);


--
-- Name: srp_log srp_log_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.srp_log
    ADD CONSTRAINT srp_log_pkey PRIMARY KEY (id);


--
-- Name: tag_info_extra tag_info_extra_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_info_extra
    ADD CONSTRAINT tag_info_extra_pkey PRIMARY KEY (id);


--
-- Name: tag_info tag_info_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_info
    ADD CONSTRAINT tag_info_pkey PRIMARY KEY (id);


--
-- Name: tag_type tag_type_name_cn_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type
    ADD CONSTRAINT tag_type_name_cn_key1 UNIQUE (name_cn);


--
-- Name: tag_type tag_type_name_en_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type
    ADD CONSTRAINT tag_type_name_en_key1 UNIQUE (name_en);


--
-- Name: tag_type tag_type_name_tw_key1; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type
    ADD CONSTRAINT tag_type_name_tw_key1 UNIQUE (name_tw);


--
-- Name: tag_type tag_type_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type
    ADD CONSTRAINT tag_type_pkey PRIMARY KEY (id);


--
-- Name: tag_type_sub_custom tag_type_sub_custom_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type_sub_custom
    ADD CONSTRAINT tag_type_sub_custom_pkey PRIMARY KEY (id);


--
-- Name: tag_type_sub_fixed tag_type_sub_fixed_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type_sub_fixed
    ADD CONSTRAINT tag_type_sub_fixed_pkey PRIMARY KEY (id);


--
-- Name: wechart_server wechart_server_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.wechart_server
    ADD CONSTRAINT wechart_server_pkey PRIMARY KEY (id);


--
-- Name: wise_paas_user wise_paas_user_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.wise_paas_user
    ADD CONSTRAINT wise_paas_user_pkey PRIMARY KEY (name);


--
-- Name: current_state current_state_pkey; Type: CONSTRAINT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.current_state
    ADD CONSTRAINT current_state_pkey PRIMARY KEY (tag_id);


--
-- Name: di_tu di_tu_pkey; Type: CONSTRAINT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.di_tu
    ADD CONSTRAINT di_tu_pkey PRIMARY KEY (id);


--
-- Name: exception_log exception_log_pkey; Type: CONSTRAINT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.exception_log
    ADD CONSTRAINT exception_log_pkey PRIMARY KEY (id);


--
-- Name: notice_logic notice_logic_pkey; Type: CONSTRAINT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.notice_logic
    ADD CONSTRAINT notice_logic_pkey PRIMARY KEY (id);


--
-- Name: raw_data raw_data_pkey; Type: CONSTRAINT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.raw_data
    ADD CONSTRAINT raw_data_pkey PRIMARY KEY (id);


--
-- Name: standard standard_pkey; Type: CONSTRAINT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.standard
    ADD CONSTRAINT standard_pkey PRIMARY KEY (id);


--
-- Name: state_statistics state_statistics_pkey; Type: CONSTRAINT; Schema: ehs; Owner: postgres
--

ALTER TABLE ONLY ehs.state_statistics
    ADD CONSTRAINT state_statistics_pkey PRIMARY KEY (id);



--
-- Name: attendance_statistics attendance_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.attendance_statistics
    ADD CONSTRAINT attendance_pkey PRIMARY KEY (id);


--
-- Name: attendance_summary attendance_summary_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.attendance_summary
    ADD CONSTRAINT attendance_summary_pkey PRIMARY KEY (id);


--
-- Name: department_efficiency department_efficiency_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_efficiency
    ADD CONSTRAINT department_efficiency_pkey PRIMARY KEY (id);


--
-- Name: department_handle department_handle_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_handle
    ADD CONSTRAINT department_handle_pkey PRIMARY KEY (id);


--
-- Name: department_performance department_performance_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_performance
    ADD CONSTRAINT department_performance_pkey PRIMARY KEY (id);


--
-- Name: department_proposal department_proposal_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_proposal
    ADD CONSTRAINT department_proposal_pkey PRIMARY KEY (id);


--
-- Name: department_quality_record department_quality_record_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.department_quality_record
    ADD CONSTRAINT department_quality_record_pkey PRIMARY KEY (id);


--
-- Name: leave_statistics leave_statistics_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.leave_statistics
    ADD CONSTRAINT leave_statistics_pkey PRIMARY KEY (id);


--
-- Name: waitingtime_log machine_waitingtime_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.waitingtime_log
    ADD CONSTRAINT machine_waitingtime_pkey PRIMARY KEY (id);


--
-- Name: overtime_statistics overtime_statistics_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.overtime_statistics
    ADD CONSTRAINT overtime_statistics_pkey PRIMARY KEY (id);


--
-- Name: person_efficiency person_efficiency_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_efficiency
    ADD CONSTRAINT person_efficiency_pkey PRIMARY KEY (id);


--
-- Name: person_handle person_handle_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_handle
    ADD CONSTRAINT person_handle_pkey PRIMARY KEY (id);


--
-- Name: person_performance person_performance_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_performance
    ADD CONSTRAINT person_performance_pkey PRIMARY KEY (id);


--
-- Name: person_proposal person_proposal_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_proposal
    ADD CONSTRAINT person_proposal_pkey PRIMARY KEY (id);


--
-- Name: person_shift person_shift_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_shift
    ADD CONSTRAINT person_shift_pkey PRIMARY KEY (id);


--
-- Name: efficiencydetail productivity_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.efficiencydetail
    ADD CONSTRAINT productivity_pkey PRIMARY KEY (id);


--
-- Name: proposal proposal_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.proposal
    ADD CONSTRAINT proposal_pkey PRIMARY KEY (id);


--
-- Name: person_quality_record quality_record_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.person_quality_record
    ADD CONSTRAINT quality_record_pkey PRIMARY KEY (id);


--
-- Name: schedule schedule_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.schedule
    ADD CONSTRAINT schedule_pkey PRIMARY KEY (id);


--
-- Name: performance_formula test_pkey; Type: CONSTRAINT; Schema: lpm; Owner: postgres
--

ALTER TABLE ONLY lpm.performance_formula
    ADD CONSTRAINT test_pkey PRIMARY KEY (id);


--
-- Name: layer_status_duration_day layer_status_duration_day_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_status_duration_day
    ADD CONSTRAINT layer_status_duration_day_pkey PRIMARY KEY (id);


--
-- Name: workorder_status_duration layer_status_duration_order_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.workorder_status_duration
    ADD CONSTRAINT layer_status_duration_order_pkey PRIMARY KEY (id);


--
-- Name: layer_utilization_rate_shift layer_status_duration_shift_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_utilization_rate_shift
    ADD CONSTRAINT layer_status_duration_shift_pkey PRIMARY KEY (id);


--
-- Name: layer_status_duration_shift layer_status_duration_shift_pkey1; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_status_duration_shift
    ADD CONSTRAINT layer_status_duration_shift_pkey1 PRIMARY KEY (id);


--
-- Name: layer_status_duration_month layer_status_duration_week_copy1_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_status_duration_month
    ADD CONSTRAINT layer_status_duration_week_copy1_pkey PRIMARY KEY (id);


--
-- Name: layer_status_duration_week layer_status_duration_week_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_status_duration_week
    ADD CONSTRAINT layer_status_duration_week_pkey PRIMARY KEY (id);


--
-- Name: layer_utilization_rate_day layer_utilization_rate_day_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_utilization_rate_day
    ADD CONSTRAINT layer_utilization_rate_day_pkey PRIMARY KEY (id);


--
-- Name: workorder_utilization_rate layer_utilization_rate_order_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.workorder_utilization_rate
    ADD CONSTRAINT layer_utilization_rate_order_pkey PRIMARY KEY (id);


--
-- Name: layer_utilization_rate_month layer_utilization_rate_week_copy1_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_utilization_rate_month
    ADD CONSTRAINT layer_utilization_rate_week_copy1_pkey PRIMARY KEY (id);


--
-- Name: layer_utilization_rate_week layer_utilization_rate_week_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.layer_utilization_rate_week
    ADD CONSTRAINT layer_utilization_rate_week_pkey PRIMARY KEY (id);


--
-- Name: machine_lease_log machine_lease_log_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.machine_lease_log
    ADD CONSTRAINT machine_lease_log_pkey PRIMARY KEY (id);


--
-- Name: machine_lease machine_lease_machine_id_key1; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.machine_lease
    ADD CONSTRAINT machine_lease_machine_id_key1 UNIQUE (machine_id);


--
-- Name: machine_lease machine_lease_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.machine_lease
    ADD CONSTRAINT machine_lease_pkey PRIMARY KEY (id);


--
-- Name: maintenance_records maintenance_records_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.maintenance_records
    ADD CONSTRAINT maintenance_records_pkey PRIMARY KEY (id);


--
-- Name: status_duration_shift status_duration_class_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_shift
    ADD CONSTRAINT status_duration_class_pkey PRIMARY KEY (id);


--
-- Name: status_duration_day status_duration_day_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_day
    ADD CONSTRAINT status_duration_day_pkey PRIMARY KEY (id);


--
-- Name: status_duration_month status_duration_month_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_month
    ADD CONSTRAINT status_duration_month_pkey PRIMARY KEY (id);


--
-- Name: status_duration_order status_duration_order_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_order
    ADD CONSTRAINT status_duration_order_pkey PRIMARY KEY (id);


--
-- Name: status_duration_week status_duration_week_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_week
    ADD CONSTRAINT status_duration_week_pkey PRIMARY KEY (id);


--
-- Name: status_setting status_setting_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_setting
    ADD CONSTRAINT status_setting_pkey PRIMARY KEY (id);


--
-- Name: status_setting status_setting_status_name_key; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_setting
    ADD CONSTRAINT status_setting_status_name_key UNIQUE (status_name);


--
-- Name: status_setting status_setting_value_key; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_setting
    ADD CONSTRAINT status_setting_value_key UNIQUE (value);


--
-- Name: tag_time_day tag_time_day_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tag_time_day
    ADD CONSTRAINT tag_time_day_pkey PRIMARY KEY (id);


--
-- Name: tag_time_shift tag_time_shift_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tag_time_shift
    ADD CONSTRAINT tag_time_shift_pkey PRIMARY KEY (id);


--
-- Name: tricolor_tag_duration tricolor_tag_duration_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tricolor_tag_duration
    ADD CONSTRAINT tricolor_tag_duration_pkey PRIMARY KEY (id);


--
-- Name: tricolor_tag_status tricolor_tag_status_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tricolor_tag_status
    ADD CONSTRAINT tricolor_tag_status_pkey PRIMARY KEY (machine_id);


--
-- Name: utilization_rate_shift utilization_rate_class_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_shift
    ADD CONSTRAINT utilization_rate_class_pkey PRIMARY KEY (id);


--
-- Name: utilization_rate_day utilization_rate_day_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_day
    ADD CONSTRAINT utilization_rate_day_pkey PRIMARY KEY (id);


--
-- Name: utilization_rate_formula utilization_rate_formula_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_formula
    ADD CONSTRAINT utilization_rate_formula_pkey PRIMARY KEY (id);


--
-- Name: utilization_rate_order utilization_rate_order_pkey; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_order
    ADD CONSTRAINT utilization_rate_order_pkey PRIMARY KEY (id);


--
-- Name: breakpoint_log breakpoint_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.breakpoint_log
    ADD CONSTRAINT breakpoint_log_pkey PRIMARY KEY (machine_id);


--
-- Name: capacity_config capacity_config_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.capacity_config
    ADD CONSTRAINT capacity_config_pkey PRIMARY KEY (date);


--
-- Name: capacity_log capacity_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.capacity_log
    ADD CONSTRAINT capacity_log_pkey PRIMARY KEY (date);


--
-- Name: ct_log ct_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.ct_log
    ADD CONSTRAINT ct_log_pkey PRIMARY KEY (id);


--
-- Name: initial_material_info initial_material_info_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.initial_material_info
    ADD CONSTRAINT initial_material_info_pkey PRIMARY KEY (id);


--
-- Name: machine_capacity_log machine_capacity_log_machine_id_month_key; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.machine_capacity_log
    ADD CONSTRAINT machine_capacity_log_machine_id_month_key UNIQUE (machine_id, month);


--
-- Name: machine_capacity_log machine_capacity_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.machine_capacity_log
    ADD CONSTRAINT machine_capacity_log_pkey PRIMARY KEY (id);


--
-- Name: wo_machine_current_log machine_cur_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_current_log
    ADD CONSTRAINT machine_cur_pkey PRIMARY KEY (id);


--
-- Name: wo_machine_log machine_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_log
    ADD CONSTRAINT machine_pkey PRIMARY KEY (id);


--
-- Name: overdue_work_order overdue_work_order_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.overdue_work_order
    ADD CONSTRAINT overdue_work_order_pkey PRIMARY KEY (id);


--
-- Name: virtual_line_current_log virtual_line_cur_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_current_log
    ADD CONSTRAINT virtual_line_cur_log_pkey PRIMARY KEY (id);


--
-- Name: virtual_line_log virtual_line_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_log
    ADD CONSTRAINT virtual_line_log_pkey PRIMARY KEY (id);


--
-- Name: virtual_line virtual_line_name_cn_key1; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line
    ADD CONSTRAINT virtual_line_name_cn_key1 UNIQUE (name_cn);


--
-- Name: virtual_line virtual_line_name_en_key1; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line
    ADD CONSTRAINT virtual_line_name_en_key1 UNIQUE (name_en);


--
-- Name: virtual_line virtual_line_name_tw_key1; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line
    ADD CONSTRAINT virtual_line_name_tw_key1 UNIQUE (name_tw);


--
-- Name: virtual_line virtual_line_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line
    ADD CONSTRAINT virtual_line_pkey PRIMARY KEY (id);


--
-- Name: wo_config wo_config_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_config
    ADD CONSTRAINT wo_config_pkey PRIMARY KEY (id);


--
-- Name: wo_config wo_config_work_order_key1; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_config
    ADD CONSTRAINT wo_config_work_order_key1 UNIQUE (work_order);


--
-- Name: wo_machine wo_machine_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine
    ADD CONSTRAINT wo_machine_pkey PRIMARY KEY (id);


--
-- Name: work_order_log work_order_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.work_order_log
    ADD CONSTRAINT work_order_log_pkey PRIMARY KEY (date);


--
-- Name: worker_exception_log worker_exception_log_pkey; Type: CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.worker_exception_log
    ADD CONSTRAINT worker_exception_log_pkey PRIMARY KEY (id);


--
-- Name: raw_date_custom insert_sub_table_trigger; Type: TRIGGER; Schema: common; Owner: postgres
--

CREATE TRIGGER insert_sub_table_trigger BEFORE INSERT ON common.raw_date_custom FOR EACH ROW EXECUTE PROCEDURE common.sub_table_trigger();


--
-- Name: capacity_alert capacity_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.capacity_alert
    ADD CONSTRAINT capacity_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: error_config error_config_machine_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_config
    ADD CONSTRAINT error_config_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: error_config error_config_response_person_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_config
    ADD CONSTRAINT error_config_response_person_id_fkey FOREIGN KEY (response_person_id) REFERENCES common.person(id) ON DELETE CASCADE;


--
-- Name: error_log error_log_error_config_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_log
    ADD CONSTRAINT error_log_error_config_id_fkey FOREIGN KEY (error_config_id) REFERENCES andon.error_config(id) ON DELETE CASCADE;


--
-- Name: error_type_details error_type_details_error_type_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.error_type_details
    ADD CONSTRAINT error_type_details_error_type_id_fkey FOREIGN KEY (error_type_id) REFERENCES andon.error_type(id) ON DELETE CASCADE;


--
-- Name: machine_cost_alert machine_cost_alert_machine_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_cost_alert
    ADD CONSTRAINT machine_cost_alert_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: machine_cost_alert machine_cost_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_cost_alert
    ADD CONSTRAINT machine_cost_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: machine_fault_alert machine_fault_alert_error_type_detail_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_fault_alert
    ADD CONSTRAINT machine_fault_alert_error_type_detail_id_fkey FOREIGN KEY (error_type_detail_id) REFERENCES andon.error_type_details(id) ON DELETE CASCADE;


--
-- Name: machine_fault_alert machine_fault_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_fault_alert
    ADD CONSTRAINT machine_fault_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: machine_status_alert machine_status_alert_machine_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_alert
    ADD CONSTRAINT machine_status_alert_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: machine_status_alert machine_status_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_alert
    ADD CONSTRAINT machine_status_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: machine_status_duration_alert machine_status_duration_alert_machine_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_duration_alert
    ADD CONSTRAINT machine_status_duration_alert_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: machine_status_duration_alert machine_status_duration_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.machine_status_duration_alert
    ADD CONSTRAINT machine_status_duration_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: material_request_info material_request_info_error_config_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.material_request_info
    ADD CONSTRAINT material_request_info_error_config_id_fkey FOREIGN KEY (error_config_id) REFERENCES andon.error_config(id) ON DELETE CASCADE;


--
-- Name: notification_person notification_person_notification_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_person
    ADD CONSTRAINT notification_person_notification_group_id_fkey FOREIGN KEY (notification_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: notification_person notification_person_person_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.notification_person
    ADD CONSTRAINT notification_person_person_id_fkey FOREIGN KEY (person_id) REFERENCES common.person(id) ON DELETE CASCADE;


--
-- Name: quality_alert quality_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.quality_alert
    ADD CONSTRAINT quality_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: quality_alert quality_alert_work_order_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.quality_alert
    ADD CONSTRAINT quality_alert_work_order_id_fkey FOREIGN KEY (work_order_id) REFERENCES work_order.wo_config(id) ON DELETE CASCADE;


--
-- Name: utilization_rate_alert utilization_rate_alert_machine_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.utilization_rate_alert
    ADD CONSTRAINT utilization_rate_alert_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: utilization_rate_alert utilization_rate_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.utilization_rate_alert
    ADD CONSTRAINT utilization_rate_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: work_order_alert work_order_alert_notice_group_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.work_order_alert
    ADD CONSTRAINT work_order_alert_notice_group_id_fkey FOREIGN KEY (notice_group_id) REFERENCES andon.notification_group(id) ON DELETE CASCADE;


--
-- Name: work_order_alert work_order_alert_virtual_line_id_fkey; Type: FK CONSTRAINT; Schema: andon; Owner: postgres
--

ALTER TABLE ONLY andon.work_order_alert
    ADD CONSTRAINT work_order_alert_virtual_line_id_fkey FOREIGN KEY (virtual_line_id) REFERENCES work_order.virtual_line(id) ON DELETE CASCADE;


--
-- Name: area_node area_node_area_layer_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_node
    ADD CONSTRAINT area_node_area_layer_id_fkey FOREIGN KEY (area_layer_id) REFERENCES common.area_layer(id) ON DELETE CASCADE;


--
-- Name: area_property area_property_area_node_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.area_property
    ADD CONSTRAINT area_property_area_node_id_fkey FOREIGN KEY (area_node_id) REFERENCES common.area_node(id) ON DELETE CASCADE;


--
-- Name: person person_dept_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.person
    ADD CONSTRAINT person_dept_id_fkey FOREIGN KEY (dept_id) REFERENCES common.department(id) ON DELETE CASCADE;


--
-- Name: tag_info tag_info_machine_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_info
    ADD CONSTRAINT tag_info_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: tag_type_sub_custom tag_type_sub_custom_tag_type_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type_sub_custom
    ADD CONSTRAINT tag_type_sub_custom_tag_type_id_fkey FOREIGN KEY (tag_type_id) REFERENCES common.tag_type(id) ON UPDATE RESTRICT ON DELETE CASCADE;


--
-- Name: tag_type_sub_custom tag_type_sub_custom_tag_type_id_fkey1; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type_sub_custom
    ADD CONSTRAINT tag_type_sub_custom_tag_type_id_fkey1 FOREIGN KEY (tag_type_id) REFERENCES common.tag_type(id) ON UPDATE RESTRICT ON DELETE CASCADE;


--
-- Name: tag_type_sub_fixed tag_type_sub_fixed_tag_type_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type_sub_fixed
    ADD CONSTRAINT tag_type_sub_fixed_tag_type_id_fkey FOREIGN KEY (tag_type_id) REFERENCES common.tag_type(id) ON DELETE CASCADE;


--
-- Name: tag_type_sub_fixed tag_type_sub_fixed_tag_type_id_fkey1; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.tag_type_sub_fixed
    ADD CONSTRAINT tag_type_sub_fixed_tag_type_id_fkey1 FOREIGN KEY (tag_type_id) REFERENCES common.tag_type(id) ON DELETE CASCADE;


--
-- Name: machine_lease_log machine_lease_log_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.machine_lease_log
    ADD CONSTRAINT machine_lease_log_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: machine_lease machine_lease_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.machine_lease
    ADD CONSTRAINT machine_lease_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: status_duration_day status_duration_day_upper_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_day
    ADD CONSTRAINT status_duration_day_upper_id_fkey FOREIGN KEY (upper_id) REFERENCES oee.utilization_rate_day(id) ON DELETE CASCADE;


--
-- Name: status_duration_order status_duration_order_upper_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_order
    ADD CONSTRAINT status_duration_order_upper_id_fkey FOREIGN KEY (upper_id) REFERENCES oee.utilization_rate_order(id) ON DELETE CASCADE;


--
-- Name: status_duration_shift status_duration_shift_upper_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_duration_shift
    ADD CONSTRAINT status_duration_shift_upper_id_fkey FOREIGN KEY (upper_id) REFERENCES oee.utilization_rate_shift(id) ON DELETE CASCADE;


--
-- Name: tag_time_day tag_time_day_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tag_time_day
    ADD CONSTRAINT tag_time_day_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: tag_time_shift tag_time_shift_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tag_time_shift
    ADD CONSTRAINT tag_time_shift_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: tricolor_tag_duration tricolor_tag_duration_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tricolor_tag_duration
    ADD CONSTRAINT tricolor_tag_duration_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: tricolor_tag_status tricolor_tag_status_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.tricolor_tag_status
    ADD CONSTRAINT tricolor_tag_status_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: utilization_rate_week utilization_rate_day_copy1_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_week
    ADD CONSTRAINT utilization_rate_day_copy1_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: utilization_rate_month utilization_rate_day_copy2_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_month
    ADD CONSTRAINT utilization_rate_day_copy2_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: utilization_rate_day utilization_rate_day_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_day
    ADD CONSTRAINT utilization_rate_day_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: utilization_rate_order utilization_rate_order_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_order
    ADD CONSTRAINT utilization_rate_order_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: utilization_rate_shift utilization_rate_shift_machine_id_fkey; Type: FK CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.utilization_rate_shift
    ADD CONSTRAINT utilization_rate_shift_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: breakpoint_log breakpoint_log_machine_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.breakpoint_log
    ADD CONSTRAINT breakpoint_log_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: breakpoint_log breakpoint_log_work_order_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.breakpoint_log
    ADD CONSTRAINT breakpoint_log_work_order_id_fkey FOREIGN KEY (work_order_id) REFERENCES work_order.wo_config(id) ON DELETE CASCADE;


--
-- Name: ct_log ct_log_machine_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.ct_log
    ADD CONSTRAINT ct_log_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: ct_log ct_log_work_order_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.ct_log
    ADD CONSTRAINT ct_log_work_order_id_fkey FOREIGN KEY (work_order_id) REFERENCES work_order.wo_config(id) ON DELETE CASCADE;


--
-- Name: virtual_line_current_log virtual_line_current_log_virtual_line_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_current_log
    ADD CONSTRAINT virtual_line_current_log_virtual_line_id_fkey FOREIGN KEY (virtual_line_id) REFERENCES work_order.virtual_line(id) ON DELETE CASCADE;


--
-- Name: virtual_line_current_log virtual_line_current_log_wo_config_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_current_log
    ADD CONSTRAINT virtual_line_current_log_wo_config_id_fkey FOREIGN KEY (wo_config_id) REFERENCES work_order.wo_config(id) ON DELETE CASCADE;


--
-- Name: virtual_line_log virtual_line_log_virtual_line_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_log
    ADD CONSTRAINT virtual_line_log_virtual_line_id_fkey FOREIGN KEY (virtual_line_id) REFERENCES work_order.virtual_line(id) ON DELETE CASCADE;


--
-- Name: virtual_line_log virtual_line_log_wo_config_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.virtual_line_log
    ADD CONSTRAINT virtual_line_log_wo_config_id_fkey FOREIGN KEY (wo_config_id) REFERENCES work_order.wo_config(id) ON DELETE CASCADE;


--
-- Name: wo_config wo_config_virtual_line_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_config
    ADD CONSTRAINT wo_config_virtual_line_id_fkey FOREIGN KEY (virtual_line_id) REFERENCES work_order.virtual_line(id) ON DELETE CASCADE;


--
-- Name: wo_machine_current_log wo_machine_current_log_machine_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_current_log
    ADD CONSTRAINT wo_machine_current_log_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: wo_machine_current_log wo_machine_current_log_wo_config_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_current_log
    ADD CONSTRAINT wo_machine_current_log_wo_config_id_fkey FOREIGN KEY (wo_config_id) REFERENCES work_order.wo_config(id) ON DELETE CASCADE;


--
-- Name: wo_machine_log wo_machine_log_machine_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_log
    ADD CONSTRAINT wo_machine_log_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: wo_machine_log wo_machine_log_wo_config_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine_log
    ADD CONSTRAINT wo_machine_log_wo_config_id_fkey FOREIGN KEY (wo_config_id) REFERENCES work_order.wo_config(id) ON DELETE CASCADE;


--
-- Name: wo_machine wo_machine_machine_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine
    ADD CONSTRAINT wo_machine_machine_id_fkey FOREIGN KEY (machine_id) REFERENCES common.machine(id) ON DELETE CASCADE;


--
-- Name: wo_machine wo_machine_virtual_line_id_fkey; Type: FK CONSTRAINT; Schema: work_order; Owner: postgres
--

ALTER TABLE ONLY work_order.wo_machine
    ADD CONSTRAINT wo_machine_virtual_line_id_fkey FOREIGN KEY (virtual_line_id) REFERENCES work_order.virtual_line(id) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

--
-- PostgreSQL database dump complete
--
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