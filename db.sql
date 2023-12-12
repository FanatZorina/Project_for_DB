--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.0

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
-- Name: delcascade(integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.delcascade(IN delid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN

DELETE FROM audit WHERE id_door IN (
SELECT l.id
FROM lock l
JOIN adress a ON L.id_street = a.id
WHERE l.id_street = delid
);


DELETE FROM lock WHERE id_street = delid;

DELETE FROM adress WHERE id = delid;
END;
$$;


ALTER PROCEDURE public.delcascade(IN delid integer) OWNER TO postgres;

--
-- Name: recdel(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.recdel(aid integer) RETURNS void
    LANGUAGE plpgsql
    AS $$
declare
	lock_row record;
	audit_row record;
    lock_id integer;
    audit_number integer;
BEGIN
    FOR lock_row IN (SELECT id FROM lock WHERE id_street = aid)
    LOOP
        lock_id := lock_row.id;
        	FOR audit_row IN (SELECT number FROM audit WHERE id_door = lock_id)
    		LOOP
        		audit_number := audit_row.number;
        		DELETE FROM audit WHERE number = audit_number;
    		END LOOP;
        DELETE FROM lock WHERE id = lock_id;
    END LOOP;


    DELETE FROM adress WHERE id = aid;
END;
$$;


ALTER FUNCTION public.recdel(aid integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Name: adress; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.adress (
    id integer NOT NULL,
    street character varying(100) NOT NULL,
    number integer NOT NULL,
    building integer
);


ALTER TABLE public.adress OWNER TO postgres;

--
-- Name: adress_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.adress_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.adress_id_seq OWNER TO postgres;

--
-- Name: adress_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.adress_id_seq OWNED BY public.adress.id;


--
-- Name: audit; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.audit (
    number integer NOT NULL,
    date timestamp without time zone NOT NULL,
    id_door integer NOT NULL,
    id_street integer NOT NULL,
    login character varying(100) NOT NULL,
    closed boolean NOT NULL
);


ALTER TABLE public.audit OWNER TO postgres;

--
-- Name: audit_number_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.audit_number_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.audit_number_seq OWNER TO postgres;

--
-- Name: audit_number_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.audit_number_seq OWNED BY public.audit.number;


--
-- Name: departament; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.departament (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    number integer NOT NULL
);


ALTER TABLE public.departament OWNER TO postgres;

--
-- Name: departament_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.departament_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.departament_id_seq OWNER TO postgres;

--
-- Name: departament_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.departament_id_seq OWNED BY public.departament.id;


--
-- Name: lock; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.lock (
    id integer NOT NULL,
    id_street integer NOT NULL,
    level integer NOT NULL,
    closed boolean NOT NULL
);


ALTER TABLE public.lock OWNER TO postgres;

--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    login character varying(100) NOT NULL,
    password character varying(100) NOT NULL,
    name character varying(100) NOT NULL,
    sname character varying(100) NOT NULL,
    level integer NOT NULL,
    departament_id integer NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: adress id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adress ALTER COLUMN id SET DEFAULT nextval('public.adress_id_seq'::regclass);


--
-- Name: audit number; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.audit ALTER COLUMN number SET DEFAULT nextval('public.audit_number_seq'::regclass);


--
-- Name: departament id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departament ALTER COLUMN id SET DEFAULT nextval('public.departament_id_seq'::regclass);


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
\.


--
-- Data for Name: adress; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.adress (id, street, number, building) FROM stdin;
1	9мая	110	1
2	9мая	110	2
3	9мая	120	\N
4	8марта	75	1
5	8марта	75	2
6	8марта	75	3
7	Мира	14	\N
8	Мира	15	\N
9	Ленина	113	\N
10	Волкова	123	\N
\.


--
-- Data for Name: audit; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.audit (number, date, id_door, id_street, login, closed) FROM stdin;
3	2014-04-13 14:00:00	130	3	qwe3	t
4	2014-04-13 14:00:00	140	4	qwe4	t
5	2014-04-13 14:00:00	150	5	qwe5	t
6	2014-04-13 14:00:00	160	6	qwe6	t
7	2014-04-13 14:00:00	170	7	qwe7	t
8	2014-04-13 14:00:00	180	8	qwe8	t
9	2014-04-13 14:00:00	190	9	qwe9	t
10	2014-04-13 14:00:00	200	10	qwe10	t
21	2014-04-13 14:00:00	110	1	qwe1	t
0	2023-12-11 19:29:58.931385	110	1	qwe3	f
22	2023-12-11 20:23:07.207794	110	1	qwe3	t
23	2023-12-12 17:14:29.587799	110	1	qwe3	f
\.


--
-- Data for Name: departament; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.departament (id, name, number) FROM stdin;
1	Отдел1	10
2	Отдел2	20
3	Отдел3	30
4	Отдел4	40
5	Отдел5	50
6	Отдел6	60
7	Отдел7	70
8	Отдел8	80
9	Отдел9	90
10	Отдел10	100
\.


--
-- Data for Name: lock; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.lock (id, id_street, level, closed) FROM stdin;
120	2	2	t
130	3	3	t
140	4	4	t
150	5	5	t
160	6	1	t
170	7	2	t
180	8	3	t
190	9	4	t
200	10	5	t
110	1	1	t
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (login, password, name, sname, level, departament_id) FROM stdin;
qwe1	ewq1	Иван1	Иванов1	1	1
qwe2	ewq2	Иван2	Иванов2	2	2
qwe3	ewq3	Иван3	Иванов3	3	3
qwe4	ewq4	Иван4	Иванов4	4	4
qwe5	ewq5	Иван5	Иванов5	5	5
qwe6	ewq6	Иван6	Иванов6	1	6
qwe7	ewq7	Иван7	Иванов7	2	7
qwe8	ewq8	Иван8	Иванов8	3	8
qwe9	ewq9	Иван9	Иванов9	4	9
qwe10	ewq10	Иван10	Иванов10	5	10
\.


--
-- Name: adress_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.adress_id_seq', 17, true);


--
-- Name: audit_number_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.audit_number_seq', 23, true);


--
-- Name: departament_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.departament_id_seq', 22, true);


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: adress adress_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adress
    ADD CONSTRAINT adress_pkey PRIMARY KEY (id);


--
-- Name: audit audit_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.audit
    ADD CONSTRAINT audit_pkey PRIMARY KEY (number);


--
-- Name: departament departament_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departament
    ADD CONSTRAINT departament_pkey PRIMARY KEY (id);


--
-- Name: lock lock_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lock
    ADD CONSTRAINT lock_pkey PRIMARY KEY (id, id_street);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (login);


--
-- Name: audit audit_id_door_id_street_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.audit
    ADD CONSTRAINT audit_id_door_id_street_fkey FOREIGN KEY (id_door, id_street) REFERENCES public.lock(id, id_street);


--
-- Name: audit audit_login_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.audit
    ADD CONSTRAINT audit_login_fkey FOREIGN KEY (login) REFERENCES public.users(login);


--
-- Name: lock lock_id_street_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lock
    ADD CONSTRAINT lock_id_street_fkey FOREIGN KEY (id_street) REFERENCES public.adress(id);


--
-- Name: users users_departament_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_departament_id_fkey FOREIGN KEY (departament_id) REFERENCES public.departament(id);


--
-- PostgreSQL database dump complete
--

