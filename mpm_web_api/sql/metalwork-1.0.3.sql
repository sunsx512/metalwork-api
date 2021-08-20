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

ALTER TABLE ONLY andon.capacity_alert
    ADD CONSTRAINT capacity_alert_date_key1 UNIQUE (date);

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

ALTER TABLE ONLY andon.quality_alert
    ADD CONSTRAINT quality_alert_work_order_id_key1 UNIQUE (work_order_id);

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

ALTER TABLE ONLY oee.status_setting
    ADD CONSTRAINT status_setting_status_name_key UNIQUE (status_name);


--
-- Name: status_setting status_setting_value_key; Type: CONSTRAINT; Schema: oee; Owner: postgres
--

ALTER TABLE ONLY oee.status_setting
    ADD CONSTRAINT status_setting_value_key UNIQUE (value);

ALTER TABLE ONLY work_order.machine_capacity_log
    ADD CONSTRAINT machine_capacity_log_machine_id_month_key UNIQUE (machine_id, month);

ALTER TABLE ONLY work_order.virtual_line
    ADD CONSTRAINT virtual_line_name_cn_key1 UNIQUE (name_cn);

ALTER TABLE ONLY work_order.wo_config
    ADD CONSTRAINT wo_config_work_order_key1 UNIQUE (work_order);

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