CREATE SCHEMA IF NOT EXISTS fimp;

CREATE TABLE IF NOT EXISTS fimp.migration_log
(
    id serial NOT NULL,
    migration_id character varying(255) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT migration_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
