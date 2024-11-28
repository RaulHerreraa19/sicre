
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/27/2020 17:19:09
-- Generated from EDMX file: C:\Users\PCPro\source\repos\VL-Citas\VLCitas.DataLayer\VL_Citas.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VL_Citas];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Advertise__offic__5AEE82B9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Advertisement] DROP CONSTRAINT [FK__Advertise__offic__5AEE82B9];
GO
IF OBJECT_ID(N'[dbo].[FK__Advertise__statu__5BE2A6F2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Advertisement] DROP CONSTRAINT [FK__Advertise__statu__5BE2A6F2];
GO
IF OBJECT_ID(N'[dbo].[FK__Citas__status_id__06CD04F7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas] DROP CONSTRAINT [FK__Citas__status_id__06CD04F7];
GO
IF OBJECT_ID(N'[dbo].[FK__Customers__statu__5DCAEF64]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK__Customers__statu__5DCAEF64];
GO
IF OBJECT_ID(N'[dbo].[FK__Departame__offic__5EBF139D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Consultory] DROP CONSTRAINT [FK__Departame__offic__5EBF139D];
GO
IF OBJECT_ID(N'[dbo].[FK__Departame__ofici__60A75C0F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Schedule] DROP CONSTRAINT [FK__Departame__ofici__60A75C0F];
GO
IF OBJECT_ID(N'[dbo].[FK__Departame__statu__5FB337D6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Consultory] DROP CONSTRAINT [FK__Departame__statu__5FB337D6];
GO
IF OBJECT_ID(N'[dbo].[FK__Offices__custome__6383C8BA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices] DROP CONSTRAINT [FK__Offices__custome__6383C8BA];
GO
IF OBJECT_ID(N'[dbo].[FK__Offices__status___6477ECF3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices] DROP CONSTRAINT [FK__Offices__status___6477ECF3];
GO
IF OBJECT_ID(N'[dbo].[FK__Roles__status_id__656C112C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK__Roles__status_id__656C112C];
GO
IF OBJECT_ID(N'[dbo].[FK__Users__status_id__73BA3083]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK__Users__status_id__73BA3083];
GO
IF OBJECT_ID(N'[dbo].[FK_cita_attachments_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas_Attachments] DROP CONSTRAINT [FK_cita_attachments_id];
GO
IF OBJECT_ID(N'[dbo].[fk_cita_departament]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas] DROP CONSTRAINT [fk_cita_departament];
GO
IF OBJECT_ID(N'[dbo].[FK_Citas_Paciente_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas] DROP CONSTRAINT [FK_Citas_Paciente_id];
GO
IF OBJECT_ID(N'[dbo].[FK_citas_servicios_Citas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas_servicios] DROP CONSTRAINT [FK_citas_servicios_Citas];
GO
IF OBJECT_ID(N'[dbo].[FK_citas_servicios_servicios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas_servicios] DROP CONSTRAINT [FK_citas_servicios_servicios];
GO
IF OBJECT_ID(N'[dbo].[fk_citas_uiddoctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citas] DROP CONSTRAINT [fk_citas_uiddoctor];
GO
IF OBJECT_ID(N'[dbo].[fk_concultory_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Consultories] DROP CONSTRAINT [fk_concultory_user];
GO
IF OBJECT_ID(N'[dbo].[FK_config_time_zone_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices_Configuration] DROP CONSTRAINT [FK_config_time_zone_id];
GO
IF OBJECT_ID(N'[dbo].[fk_configs_img1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Configs] DROP CONSTRAINT [fk_configs_img1];
GO
IF OBJECT_ID(N'[dbo].[fk_configs_img2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Configs] DROP CONSTRAINT [fk_configs_img2];
GO
IF OBJECT_ID(N'[dbo].[fk_configs_img4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Configs] DROP CONSTRAINT [fk_configs_img4];
GO
IF OBJECT_ID(N'[dbo].[fk_configss_img3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Configs] DROP CONSTRAINT [fk_configss_img3];
GO
IF OBJECT_ID(N'[dbo].[FK_Council_Doctor_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Council_Certifications] DROP CONSTRAINT [FK_Council_Doctor_id];
GO
IF OBJECT_ID(N'[dbo].[FK_council_medical_specialty_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Specialties] DROP CONSTRAINT [FK_council_medical_specialty_id];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_Council_Medical_Specialty_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Council_Certifications] DROP CONSTRAINT [FK_Doctor_Council_Medical_Specialty_id];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_Specialities_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Specialties] DROP CONSTRAINT [FK_Doctor_Specialities_id];
GO
IF OBJECT_ID(N'[dbo].[Fk_office_configuration_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices] DROP CONSTRAINT [Fk_office_configuration_id];
GO
IF OBJECT_ID(N'[dbo].[Fk_office_user_schedule_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Office_Users_Schedules] DROP CONSTRAINT [Fk_office_user_schedule_id];
GO
IF OBJECT_ID(N'[dbo].[FK_office_user_uid]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices_Users] DROP CONSTRAINT [FK_office_user_uid];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_Prescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patient_Prescriptions] DROP CONSTRAINT [FK_Patient_Prescription];
GO
IF OBJECT_ID(N'[dbo].[FK_prescription_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices_Users] DROP CONSTRAINT [FK_prescription_id];
GO
IF OBJECT_ID(N'[dbo].[fk_repotrs_roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reports] DROP CONSTRAINT [fk_repotrs_roles];
GO
IF OBJECT_ID(N'[dbo].[fk_rm_roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles_Module] DROP CONSTRAINT [fk_rm_roles];
GO
IF OBJECT_ID(N'[dbo].[fk_rm_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles_Module] DROP CONSTRAINT [fk_rm_users];
GO
IF OBJECT_ID(N'[dbo].[fk_ruo_roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles_Office_Users] DROP CONSTRAINT [fk_ruo_roles];
GO
IF OBJECT_ID(N'[dbo].[fk_ruo_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles_Office_Users] DROP CONSTRAINT [fk_ruo_users];
GO
IF OBJECT_ID(N'[dbo].[Fk_schedule_office_user_uid]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Office_Users_Schedules] DROP CONSTRAINT [Fk_schedule_office_user_uid];
GO
IF OBJECT_ID(N'[dbo].[FK_servicios_usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Servicios] DROP CONSTRAINT [FK_servicios_usuario];
GO
IF OBJECT_ID(N'[dbo].[fk_sex_patient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paciente] DROP CONSTRAINT [fk_sex_patient];
GO
IF OBJECT_ID(N'[dbo].[FK_Spelcialities_Doctor_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctor_Specialties] DROP CONSTRAINT [FK_Spelcialities_Doctor_id];
GO
IF OBJECT_ID(N'[dbo].[fk_user_consultory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Consultories] DROP CONSTRAINT [fk_user_consultory];
GO
IF OBJECT_ID(N'[dbo].[FK_user_office_uid]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices_Users] DROP CONSTRAINT [FK_user_office_uid];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Prescription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patient_Prescriptions] DROP CONSTRAINT [FK_User_Prescription];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Customers_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Customers] DROP CONSTRAINT [FK_Users_Customers_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Customers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Customers] DROP CONSTRAINT [FK_Users_Customers_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Doctor_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Doctor_id];
GO
IF OBJECT_ID(N'[dbo].[fk_vacations_usr]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vacations] DROP CONSTRAINT [fk_vacations_usr];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Adverstisement_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adverstisement_Status];
GO
IF OBJECT_ID(N'[dbo].[Advertisement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Advertisement];
GO
IF OBJECT_ID(N'[dbo].[Citas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citas];
GO
IF OBJECT_ID(N'[dbo].[Citas_Attachments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citas_Attachments];
GO
IF OBJECT_ID(N'[dbo].[Citas_servicios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citas_servicios];
GO
IF OBJECT_ID(N'[dbo].[Configs_Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Configs_Images];
GO
IF OBJECT_ID(N'[dbo].[Consultory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consultory];
GO
IF OBJECT_ID(N'[dbo].[Consultory_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consultory_Status];
GO
IF OBJECT_ID(N'[dbo].[Councils
_Medical_Specialties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Councils
_Medical_Specialties];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Customers_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers_Status];
GO
IF OBJECT_ID(N'[dbo].[Doctor_Configs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctor_Configs];
GO
IF OBJECT_ID(N'[dbo].[Doctor_Council_Certifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctor_Council_Certifications];
GO
IF OBJECT_ID(N'[dbo].[Doctor_Specialties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctor_Specialties];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Module]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Module];
GO
IF OBJECT_ID(N'[dbo].[Office_Users_Schedules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Office_Users_Schedules];
GO
IF OBJECT_ID(N'[dbo].[Offices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offices];
GO
IF OBJECT_ID(N'[dbo].[Offices_Configuration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offices_Configuration];
GO
IF OBJECT_ID(N'[dbo].[Offices_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offices_Status];
GO
IF OBJECT_ID(N'[dbo].[Offices_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offices_Users];
GO
IF OBJECT_ID(N'[dbo].[Paciente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paciente];
GO
IF OBJECT_ID(N'[dbo].[Patient_Prescriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patient_Prescriptions];
GO
IF OBJECT_ID(N'[dbo].[Prescriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prescriptions];
GO
IF OBJECT_ID(N'[dbo].[Reports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reports];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Roles_Module]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles_Module];
GO
IF OBJECT_ID(N'[dbo].[Roles_Office_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles_Office_Users];
GO
IF OBJECT_ID(N'[dbo].[Roles_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles_Status];
GO
IF OBJECT_ID(N'[dbo].[Schedule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Schedule];
GO
IF OBJECT_ID(N'[dbo].[Servicios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Servicios];
GO
IF OBJECT_ID(N'[dbo].[Sex]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sex];
GO
IF OBJECT_ID(N'[dbo].[Specialties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specialties];
GO
IF OBJECT_ID(N'[dbo].[Status_Citas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status_Citas];
GO
IF OBJECT_ID(N'[dbo].[TimeZones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeZones];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Users_Consultories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Consultories];
GO
IF OBJECT_ID(N'[dbo].[Users_Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Customers];
GO
IF OBJECT_ID(N'[dbo].[Users_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Status];
GO
IF OBJECT_ID(N'[dbo].[Vacations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vacations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Adverstisement_Status'
CREATE TABLE [dbo].[Adverstisement_Status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NULL,
    [description] varchar(255)  NULL
);
GO

-- Creating table 'Advertisement'
CREATE TABLE [dbo].[Advertisement] (
    [uId] uniqueidentifier  NOT NULL,
    [name] varchar(1000)  NULL,
    [status_id] int  NULL,
    [office_uId] uniqueidentifier  NULL,
    [start_date] datetime  NULL,
    [update_date] datetime  NULL
);
GO

-- Creating table 'Citas'
CREATE TABLE [dbo].[Citas] (
    [uId] uniqueidentifier  NOT NULL,
    [consultory_uId] uniqueidentifier  NULL,
    [create_date] datetime  NULL,
    [cita_date] datetime  NULL,
    [status_id] int  NULL,
    [Code] varchar(20)  NULL,
    [title] varchar(255)  NULL,
    [telefono] varchar(255)  NULL,
    [razon] varchar(255)  NULL,
    [precio] decimal(19,4)  NULL,
    [observaciones] nvarchar(max)  NULL,
    [id_paciente] int  NULL,
    [observaciones_asistente] nvarchar(max)  NULL,
    [peso] float  NULL,
    [temperatura] float  NULL,
    [presion] varchar(50)  NULL,
    [receta] nvarchar(max)  NULL,
    [cancel_date] datetime  NULL,
    [complete_date] datetime  NULL,
    [confirm_date] datetime  NULL,
    [doctor_uid] uniqueidentifier  NULL,
    [altura] float  NULL,
    [observacion_doctor] nvarchar(max)  NULL,
    [glucosa] float  NULL,
    [motivo] nvarchar(255)  NULL,
    [diagnostico] nvarchar(255)  NULL,
    [ritmo_cardiaco] nvarchar(255)  NULL
);
GO

-- Creating table 'Citas_Attachments'
CREATE TABLE [dbo].[Citas_Attachments] (
    [uid] uniqueidentifier  NOT NULL,
    [cita_uId] uniqueidentifier  NULL,
    [name] varchar(255)  NULL,
    [path] varchar(255)  NULL,
    [date] datetime  NULL
);
GO

-- Creating table 'Configs_Images'
CREATE TABLE [dbo].[Configs_Images] (
    [config_id] int  NOT NULL,
    [image_id] int  NOT NULL
);
GO

-- Creating table 'Consultory'
CREATE TABLE [dbo].[Consultory] (
    [uId] uniqueidentifier  NOT NULL,
    [name] varchar(60)  NULL,
    [description] varchar(255)  NULL,
    [office_uId] uniqueidentifier  NULL,
    [alias] varchar(30)  NULL,
    [status_id] int  NULL,
    [image_url] varchar(255)  NULL,
    [create_date] datetime  NULL,
    [update_date] datetime  NULL,
    [serie] varchar(5)  NULL,
    [allow_citas] bit  NULL,
    [allow_turn] bit  NULL,
    [time_prom] int  NULL
);
GO

-- Creating table 'Consultory_Status'
CREATE TABLE [dbo].[Consultory_Status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NULL,
    [description] varchar(255)  NULL
);
GO

-- Creating table 'Councils__Medical_Specialties'
CREATE TABLE [dbo].[Councils__Medical_Specialties] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NULL,
    [abbreviation] varchar(255)  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [uId] uniqueidentifier  NOT NULL,
    [name] varchar(60)  NULL,
    [address] varchar(75)  NULL,
    [alias] varchar(30)  NULL,
    [image_url] varchar(255)  NULL,
    [website] varchar(255)  NULL,
    [phone] varchar(30)  NULL,
    [status_id] int  NULL,
    [create_date] datetime  NULL,
    [update_date] datetime  NULL
);
GO

-- Creating table 'Customers_Status'
CREATE TABLE [dbo].[Customers_Status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NULL,
    [description] varchar(255)  NULL
);
GO

-- Creating table 'Doctor_Configs'
CREATE TABLE [dbo].[Doctor_Configs] (
    [id] int IDENTITY(1,1) NOT NULL,
    [professional_license] varchar(255)  NULL,
    [resume] nvarchar(max)  NULL,
    [job_description] nvarchar(max)  NULL,
    [logo1] int  NULL,
    [logo2] int  NULL,
    [logo3] int  NULL,
    [logo4] int  NULL,
    [facebook] varchar(50)  NULL,
    [twitter] varchar(50)  NULL,
    [instagram] varchar(50)  NULL,
    [webpage] varchar(100)  NULL,
    [has_attachements] bit  NOT NULL
);
GO

-- Creating table 'Image'
CREATE TABLE [dbo].[Image] (
    [id] int IDENTITY(1,1) NOT NULL,
    [fileName] varchar(255)  NULL,
    [webPath] varchar(255)  NULL,
    [systemPath] varchar(255)  NULL
);
GO

-- Creating table 'Module'
CREATE TABLE [dbo].[Module] (
    [idmodules] int IDENTITY(1,1) NOT NULL,
    [codemodules] varchar(50)  NULL,
    [namemodules] varchar(250)  NOT NULL,
    [parentmenuid] int  NOT NULL,
    [ordernumber] int  NOT NULL,
    [menuurl] varchar(100)  NULL,
    [menuicon] varchar(50)  NULL,
    [menuaction] varchar(50)  NULL,
    [menucontrolador] varchar(50)  NULL
);
GO

-- Creating table 'Offices'
CREATE TABLE [dbo].[Offices] (
    [uId] uniqueidentifier  NOT NULL,
    [name] varchar(60)  NULL,
    [address] varchar(75)  NULL,
    [status_id] int  NULL,
    [customer_uId] uniqueidentifier  NULL,
    [image_url] varchar(255)  NULL,
    [alias] varchar(30)  NULL,
    [create_date] datetime  NULL,
    [update_date] datetime  NULL,
    [latitud] varchar(255)  NULL,
    [longitud] varchar(255)  NULL,
    [configuration_id] int  NOT NULL,
    [phone] varchar(255)  NULL,
    [phone_2] varchar(255)  NULL
);
GO

-- Creating table 'Offices_Configuration'
CREATE TABLE [dbo].[Offices_Configuration] (
    [id] int IDENTITY(1,1) NOT NULL,
    [time_zone_id] int  NOT NULL
);
GO

-- Creating table 'Offices_Status'
CREATE TABLE [dbo].[Offices_Status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NULL,
    [description] varchar(255)  NULL
);
GO

-- Creating table 'Offices_Users'
CREATE TABLE [dbo].[Offices_Users] (
    [office_uid] uniqueidentifier  NULL,
    [user_uid] uniqueidentifier  NULL,
    [prescription_id] int  NOT NULL,
    [medical_appointment_duration] int  NULL,
    [uid] uniqueidentifier  NOT NULL,
    [price_per_appoinment] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'Paciente'
CREATE TABLE [dbo].[Paciente] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(200)  NULL,
    [apellidos] varchar(500)  NULL,
    [telefono] varchar(150)  NULL,
    [curp] varchar(25)  NULL,
    [fecha_nacimiento] datetime  NULL,
    [alergias] varchar(600)  NULL,
    [cirugias] varchar(500)  NULL,
    [email] varchar(255)  NULL,
    [sex_id] int  NULL,
    [heredofamiliares] nvarchar(max)  NULL,
    [antecedentes_patologicos] nvarchar(max)  NULL,
    [antecedentes_no_patologicos] nvarchar(max)  NULL,
    [created_date] datetime  NULL
);
GO

-- Creating table 'Patient_Prescriptions'
CREATE TABLE [dbo].[Patient_Prescriptions] (
    [uid] uniqueidentifier  NOT NULL,
    [patient_id] int  NULL,
    [date] datetime  NULL,
    [description] nvarchar(max)  NULL,
    [user_uid] uniqueidentifier  NULL
);
GO

-- Creating table 'Prescriptions'
CREATE TABLE [dbo].[Prescriptions] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NULL,
    [path] varchar(255)  NULL,
    [is_template] bit  NOT NULL
);
GO

-- Creating table 'Reports'
CREATE TABLE [dbo].[Reports] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(255)  NULL,
    [description] nvarchar(255)  NULL,
    [rol_id] int  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NULL,
    [status_id] int  NULL
);
GO

-- Creating table 'Roles_Status'
CREATE TABLE [dbo].[Roles_Status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NULL,
    [description] varchar(255)  NULL
);
GO

-- Creating table 'Schedule'
CREATE TABLE [dbo].[Schedule] (
    [id] int IDENTITY(1,1) NOT NULL,
    [lunes] bit  NULL,
    [martes] bit  NULL,
    [miercoles] bit  NULL,
    [jueves] bit  NULL,
    [viernes] bit  NULL,
    [sabado] bit  NULL,
    [domingo] bit  NULL,
    [start_hour] time  NULL,
    [end_hour] time  NULL,
    [oficina_uId] uniqueidentifier  NULL,
    [status] int  NULL,
    [schedule_name] varchar(200)  NULL
);
GO

-- Creating table 'Servicios'
CREATE TABLE [dbo].[Servicios] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(255)  NULL,
    [precio] float  NULL,
    [descripcion] nvarchar(255)  NULL,
    [activo] bit  NULL,
    [user_uId] uniqueidentifier  NULL,
    [default] bit  NULL
);
GO

-- Creating table 'Sex'
CREATE TABLE [dbo].[Sex] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(255)  NULL
);
GO

-- Creating table 'Specialties'
CREATE TABLE [dbo].[Specialties] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(255)  NULL,
    [description] nvarchar(max)  NULL,
    [council_medical_specialty_id] int  NULL
);
GO

-- Creating table 'Status_Citas'
CREATE TABLE [dbo].[Status_Citas] (
    [id] int IDENTITY(1,1) NOT NULL,
    [status] varchar(100)  NULL,
    [color] varchar(255)  NULL,
    [translate] varchar(255)  NULL
);
GO

-- Creating table 'TimeZones'
CREATE TABLE [dbo].[TimeZones] (
    [id] int IDENTITY(1,1) NOT NULL,
    [TimeZoneId] nvarchar(150)  NULL,
    [DaylightName] nvarchar(150)  NULL,
    [DisplayName] nvarchar(150)  NULL,
    [StandardName] nvarchar(150)  NULL,
    [BaseUtcOffset] nvarchar(20)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [uId] uniqueidentifier  NOT NULL,
    [first_name] varchar(50)  NULL,
    [last_name] varchar(50)  NULL,
    [email] varchar(70)  NULL,
    [password] varchar(255)  NULL,
    [status_id] int  NULL,
    [address] varchar(255)  NULL,
    [phone] varchar(255)  NULL,
    [profile_picture] int  NULL,
    [doctor_id] int  NULL
);
GO

-- Creating table 'Users_Status'
CREATE TABLE [dbo].[Users_Status] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(100)  NULL,
    [description] varchar(255)  NULL
);
GO

-- Creating table 'Vacations'
CREATE TABLE [dbo].[Vacations] (
    [id] int IDENTITY(1,1) NOT NULL,
    [user_uid] uniqueidentifier  NULL,
    [start_date] datetime  NULL,
    [end_date] datetime  NULL,
    [reason] varchar(255)  NULL
);
GO

-- Creating table 'Citas_servicios'
CREATE TABLE [dbo].[Citas_servicios] (
    [Citas_uId] uniqueidentifier  NOT NULL,
    [Servicios_id] int  NOT NULL
);
GO

-- Creating table 'Doctor_Council_Certifications'
CREATE TABLE [dbo].[Doctor_Council_Certifications] (
    [Doctor_Configs_id] int  NOT NULL,
    [Councils__Medical_Specialties_id] int  NOT NULL
);
GO

-- Creating table 'Doctor_Specialties'
CREATE TABLE [dbo].[Doctor_Specialties] (
    [Specialties_id] int  NOT NULL,
    [Doctor_Configs_id] int  NOT NULL
);
GO

-- Creating table 'Office_Users_Schedules'
CREATE TABLE [dbo].[Office_Users_Schedules] (
    [Schedule_id] int  NOT NULL,
    [Offices_Users_uid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Roles_Module'
CREATE TABLE [dbo].[Roles_Module] (
    [Roles_id] int  NOT NULL,
    [Module_idmodules] int  NOT NULL
);
GO

-- Creating table 'Roles_Office_Users'
CREATE TABLE [dbo].[Roles_Office_Users] (
    [Roles_id] int  NOT NULL,
    [Offices_Users_uid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Users_Consultories'
CREATE TABLE [dbo].[Users_Consultories] (
    [Consultory_uId] uniqueidentifier  NOT NULL,
    [Users_uId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Users_Customers'
CREATE TABLE [dbo].[Users_Customers] (
    [Customers_uId] uniqueidentifier  NOT NULL,
    [Users_uId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Adverstisement_Status'
ALTER TABLE [dbo].[Adverstisement_Status]
ADD CONSTRAINT [PK_Adverstisement_Status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [uId] in table 'Advertisement'
ALTER TABLE [dbo].[Advertisement]
ADD CONSTRAINT [PK_Advertisement]
    PRIMARY KEY CLUSTERED ([uId] ASC);
GO

-- Creating primary key on [uId] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [PK_Citas]
    PRIMARY KEY CLUSTERED ([uId] ASC);
GO

-- Creating primary key on [uid] in table 'Citas_Attachments'
ALTER TABLE [dbo].[Citas_Attachments]
ADD CONSTRAINT [PK_Citas_Attachments]
    PRIMARY KEY CLUSTERED ([uid] ASC);
GO

-- Creating primary key on [config_id], [image_id] in table 'Configs_Images'
ALTER TABLE [dbo].[Configs_Images]
ADD CONSTRAINT [PK_Configs_Images]
    PRIMARY KEY CLUSTERED ([config_id], [image_id] ASC);
GO

-- Creating primary key on [uId] in table 'Consultory'
ALTER TABLE [dbo].[Consultory]
ADD CONSTRAINT [PK_Consultory]
    PRIMARY KEY CLUSTERED ([uId] ASC);
GO

-- Creating primary key on [id] in table 'Consultory_Status'
ALTER TABLE [dbo].[Consultory_Status]
ADD CONSTRAINT [PK_Consultory_Status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Councils__Medical_Specialties'
ALTER TABLE [dbo].[Councils__Medical_Specialties]
ADD CONSTRAINT [PK_Councils__Medical_Specialties]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [uId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([uId] ASC);
GO

-- Creating primary key on [id] in table 'Customers_Status'
ALTER TABLE [dbo].[Customers_Status]
ADD CONSTRAINT [PK_Customers_Status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Doctor_Configs'
ALTER TABLE [dbo].[Doctor_Configs]
ADD CONSTRAINT [PK_Doctor_Configs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [PK_Image]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [idmodules] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [PK_Module]
    PRIMARY KEY CLUSTERED ([idmodules] ASC);
GO

-- Creating primary key on [uId] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [PK_Offices]
    PRIMARY KEY CLUSTERED ([uId] ASC);
GO

-- Creating primary key on [id] in table 'Offices_Configuration'
ALTER TABLE [dbo].[Offices_Configuration]
ADD CONSTRAINT [PK_Offices_Configuration]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Offices_Status'
ALTER TABLE [dbo].[Offices_Status]
ADD CONSTRAINT [PK_Offices_Status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [uid] in table 'Offices_Users'
ALTER TABLE [dbo].[Offices_Users]
ADD CONSTRAINT [PK_Offices_Users]
    PRIMARY KEY CLUSTERED ([uid] ASC);
GO

-- Creating primary key on [id] in table 'Paciente'
ALTER TABLE [dbo].[Paciente]
ADD CONSTRAINT [PK_Paciente]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [uid] in table 'Patient_Prescriptions'
ALTER TABLE [dbo].[Patient_Prescriptions]
ADD CONSTRAINT [PK_Patient_Prescriptions]
    PRIMARY KEY CLUSTERED ([uid] ASC);
GO

-- Creating primary key on [id] in table 'Prescriptions'
ALTER TABLE [dbo].[Prescriptions]
ADD CONSTRAINT [PK_Prescriptions]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [PK_Reports]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Roles_Status'
ALTER TABLE [dbo].[Roles_Status]
ADD CONSTRAINT [PK_Roles_Status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Schedule'
ALTER TABLE [dbo].[Schedule]
ADD CONSTRAINT [PK_Schedule]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Servicios'
ALTER TABLE [dbo].[Servicios]
ADD CONSTRAINT [PK_Servicios]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Sex'
ALTER TABLE [dbo].[Sex]
ADD CONSTRAINT [PK_Sex]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Specialties'
ALTER TABLE [dbo].[Specialties]
ADD CONSTRAINT [PK_Specialties]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Status_Citas'
ALTER TABLE [dbo].[Status_Citas]
ADD CONSTRAINT [PK_Status_Citas]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'TimeZones'
ALTER TABLE [dbo].[TimeZones]
ADD CONSTRAINT [PK_TimeZones]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [uId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([uId] ASC);
GO

-- Creating primary key on [id] in table 'Users_Status'
ALTER TABLE [dbo].[Users_Status]
ADD CONSTRAINT [PK_Users_Status]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Vacations'
ALTER TABLE [dbo].[Vacations]
ADD CONSTRAINT [PK_Vacations]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Citas_uId], [Servicios_id] in table 'Citas_servicios'
ALTER TABLE [dbo].[Citas_servicios]
ADD CONSTRAINT [PK_Citas_servicios]
    PRIMARY KEY CLUSTERED ([Citas_uId], [Servicios_id] ASC);
GO

-- Creating primary key on [Doctor_Configs_id], [Councils__Medical_Specialties_id] in table 'Doctor_Council_Certifications'
ALTER TABLE [dbo].[Doctor_Council_Certifications]
ADD CONSTRAINT [PK_Doctor_Council_Certifications]
    PRIMARY KEY CLUSTERED ([Doctor_Configs_id], [Councils__Medical_Specialties_id] ASC);
GO

-- Creating primary key on [Specialties_id], [Doctor_Configs_id] in table 'Doctor_Specialties'
ALTER TABLE [dbo].[Doctor_Specialties]
ADD CONSTRAINT [PK_Doctor_Specialties]
    PRIMARY KEY CLUSTERED ([Specialties_id], [Doctor_Configs_id] ASC);
GO

-- Creating primary key on [Schedule_id], [Offices_Users_uid] in table 'Office_Users_Schedules'
ALTER TABLE [dbo].[Office_Users_Schedules]
ADD CONSTRAINT [PK_Office_Users_Schedules]
    PRIMARY KEY CLUSTERED ([Schedule_id], [Offices_Users_uid] ASC);
GO

-- Creating primary key on [Roles_id], [Module_idmodules] in table 'Roles_Module'
ALTER TABLE [dbo].[Roles_Module]
ADD CONSTRAINT [PK_Roles_Module]
    PRIMARY KEY CLUSTERED ([Roles_id], [Module_idmodules] ASC);
GO

-- Creating primary key on [Roles_id], [Offices_Users_uid] in table 'Roles_Office_Users'
ALTER TABLE [dbo].[Roles_Office_Users]
ADD CONSTRAINT [PK_Roles_Office_Users]
    PRIMARY KEY CLUSTERED ([Roles_id], [Offices_Users_uid] ASC);
GO

-- Creating primary key on [Consultory_uId], [Users_uId] in table 'Users_Consultories'
ALTER TABLE [dbo].[Users_Consultories]
ADD CONSTRAINT [PK_Users_Consultories]
    PRIMARY KEY CLUSTERED ([Consultory_uId], [Users_uId] ASC);
GO

-- Creating primary key on [Customers_uId], [Users_uId] in table 'Users_Customers'
ALTER TABLE [dbo].[Users_Customers]
ADD CONSTRAINT [PK_Users_Customers]
    PRIMARY KEY CLUSTERED ([Customers_uId], [Users_uId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [status_id] in table 'Advertisement'
ALTER TABLE [dbo].[Advertisement]
ADD CONSTRAINT [FK__Advertise__statu__5BE2A6F2]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[Adverstisement_Status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Advertise__statu__5BE2A6F2'
CREATE INDEX [IX_FK__Advertise__statu__5BE2A6F2]
ON [dbo].[Advertisement]
    ([status_id]);
GO

-- Creating foreign key on [office_uId] in table 'Advertisement'
ALTER TABLE [dbo].[Advertisement]
ADD CONSTRAINT [FK__Advertise__offic__5AEE82B9]
    FOREIGN KEY ([office_uId])
    REFERENCES [dbo].[Offices]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Advertise__offic__5AEE82B9'
CREATE INDEX [IX_FK__Advertise__offic__5AEE82B9]
ON [dbo].[Advertisement]
    ([office_uId]);
GO

-- Creating foreign key on [status_id] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [FK__Citas__status_id__06CD04F7]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[Status_Citas]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Citas__status_id__06CD04F7'
CREATE INDEX [IX_FK__Citas__status_id__06CD04F7]
ON [dbo].[Citas]
    ([status_id]);
GO

-- Creating foreign key on [cita_uId] in table 'Citas_Attachments'
ALTER TABLE [dbo].[Citas_Attachments]
ADD CONSTRAINT [FK_cita_attachments_id]
    FOREIGN KEY ([cita_uId])
    REFERENCES [dbo].[Citas]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_cita_attachments_id'
CREATE INDEX [IX_FK_cita_attachments_id]
ON [dbo].[Citas_Attachments]
    ([cita_uId]);
GO

-- Creating foreign key on [consultory_uId] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [fk_cita_departament]
    FOREIGN KEY ([consultory_uId])
    REFERENCES [dbo].[Consultory]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_cita_departament'
CREATE INDEX [IX_fk_cita_departament]
ON [dbo].[Citas]
    ([consultory_uId]);
GO

-- Creating foreign key on [id_paciente] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [FK_Citas_Paciente_id]
    FOREIGN KEY ([id_paciente])
    REFERENCES [dbo].[Paciente]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Citas_Paciente_id'
CREATE INDEX [IX_FK_Citas_Paciente_id]
ON [dbo].[Citas]
    ([id_paciente]);
GO

-- Creating foreign key on [doctor_uid] in table 'Citas'
ALTER TABLE [dbo].[Citas]
ADD CONSTRAINT [fk_citas_uiddoctor]
    FOREIGN KEY ([doctor_uid])
    REFERENCES [dbo].[Users]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_citas_uiddoctor'
CREATE INDEX [IX_fk_citas_uiddoctor]
ON [dbo].[Citas]
    ([doctor_uid]);
GO

-- Creating foreign key on [office_uId] in table 'Consultory'
ALTER TABLE [dbo].[Consultory]
ADD CONSTRAINT [FK__Departame__offic__5EBF139D]
    FOREIGN KEY ([office_uId])
    REFERENCES [dbo].[Offices]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Departame__offic__5EBF139D'
CREATE INDEX [IX_FK__Departame__offic__5EBF139D]
ON [dbo].[Consultory]
    ([office_uId]);
GO

-- Creating foreign key on [status_id] in table 'Consultory'
ALTER TABLE [dbo].[Consultory]
ADD CONSTRAINT [FK__Departame__statu__5FB337D6]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[Consultory_Status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Departame__statu__5FB337D6'
CREATE INDEX [IX_FK__Departame__statu__5FB337D6]
ON [dbo].[Consultory]
    ([status_id]);
GO

-- Creating foreign key on [council_medical_specialty_id] in table 'Specialties'
ALTER TABLE [dbo].[Specialties]
ADD CONSTRAINT [FK_council_medical_specialty_id]
    FOREIGN KEY ([council_medical_specialty_id])
    REFERENCES [dbo].[Councils__Medical_Specialties]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_council_medical_specialty_id'
CREATE INDEX [IX_FK_council_medical_specialty_id]
ON [dbo].[Specialties]
    ([council_medical_specialty_id]);
GO

-- Creating foreign key on [status_id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK__Customers__statu__5DCAEF64]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[Customers_Status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Customers__statu__5DCAEF64'
CREATE INDEX [IX_FK__Customers__statu__5DCAEF64]
ON [dbo].[Customers]
    ([status_id]);
GO

-- Creating foreign key on [customer_uId] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [FK__Offices__custome__6383C8BA]
    FOREIGN KEY ([customer_uId])
    REFERENCES [dbo].[Customers]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Offices__custome__6383C8BA'
CREATE INDEX [IX_FK__Offices__custome__6383C8BA]
ON [dbo].[Offices]
    ([customer_uId]);
GO

-- Creating foreign key on [logo1] in table 'Doctor_Configs'
ALTER TABLE [dbo].[Doctor_Configs]
ADD CONSTRAINT [fk_configs_img1]
    FOREIGN KEY ([logo1])
    REFERENCES [dbo].[Image]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_configs_img1'
CREATE INDEX [IX_fk_configs_img1]
ON [dbo].[Doctor_Configs]
    ([logo1]);
GO

-- Creating foreign key on [logo2] in table 'Doctor_Configs'
ALTER TABLE [dbo].[Doctor_Configs]
ADD CONSTRAINT [fk_configs_img2]
    FOREIGN KEY ([logo2])
    REFERENCES [dbo].[Image]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_configs_img2'
CREATE INDEX [IX_fk_configs_img2]
ON [dbo].[Doctor_Configs]
    ([logo2]);
GO

-- Creating foreign key on [logo4] in table 'Doctor_Configs'
ALTER TABLE [dbo].[Doctor_Configs]
ADD CONSTRAINT [fk_configs_img4]
    FOREIGN KEY ([logo4])
    REFERENCES [dbo].[Image]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_configs_img4'
CREATE INDEX [IX_fk_configs_img4]
ON [dbo].[Doctor_Configs]
    ([logo4]);
GO

-- Creating foreign key on [logo3] in table 'Doctor_Configs'
ALTER TABLE [dbo].[Doctor_Configs]
ADD CONSTRAINT [fk_configss_img3]
    FOREIGN KEY ([logo3])
    REFERENCES [dbo].[Image]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_configss_img3'
CREATE INDEX [IX_fk_configss_img3]
ON [dbo].[Doctor_Configs]
    ([logo3]);
GO

-- Creating foreign key on [doctor_id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Doctor_id]
    FOREIGN KEY ([doctor_id])
    REFERENCES [dbo].[Doctor_Configs]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Doctor_id'
CREATE INDEX [IX_FK_Users_Doctor_id]
ON [dbo].[Users]
    ([doctor_id]);
GO

-- Creating foreign key on [oficina_uId] in table 'Schedule'
ALTER TABLE [dbo].[Schedule]
ADD CONSTRAINT [FK__Departame__ofici__60A75C0F]
    FOREIGN KEY ([oficina_uId])
    REFERENCES [dbo].[Offices]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Departame__ofici__60A75C0F'
CREATE INDEX [IX_FK__Departame__ofici__60A75C0F]
ON [dbo].[Schedule]
    ([oficina_uId]);
GO

-- Creating foreign key on [status_id] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [FK__Offices__status___6477ECF3]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[Offices_Status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Offices__status___6477ECF3'
CREATE INDEX [IX_FK__Offices__status___6477ECF3]
ON [dbo].[Offices]
    ([status_id]);
GO

-- Creating foreign key on [configuration_id] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [Fk_office_configuration_id]
    FOREIGN KEY ([configuration_id])
    REFERENCES [dbo].[Offices_Configuration]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'Fk_office_configuration_id'
CREATE INDEX [IX_Fk_office_configuration_id]
ON [dbo].[Offices]
    ([configuration_id]);
GO

-- Creating foreign key on [office_uid] in table 'Offices_Users'
ALTER TABLE [dbo].[Offices_Users]
ADD CONSTRAINT [FK_office_user_uid]
    FOREIGN KEY ([office_uid])
    REFERENCES [dbo].[Offices]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_office_user_uid'
CREATE INDEX [IX_FK_office_user_uid]
ON [dbo].[Offices_Users]
    ([office_uid]);
GO

-- Creating foreign key on [time_zone_id] in table 'Offices_Configuration'
ALTER TABLE [dbo].[Offices_Configuration]
ADD CONSTRAINT [FK_config_time_zone_id]
    FOREIGN KEY ([time_zone_id])
    REFERENCES [dbo].[TimeZones]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_config_time_zone_id'
CREATE INDEX [IX_FK_config_time_zone_id]
ON [dbo].[Offices_Configuration]
    ([time_zone_id]);
GO

-- Creating foreign key on [prescription_id] in table 'Offices_Users'
ALTER TABLE [dbo].[Offices_Users]
ADD CONSTRAINT [FK_prescription_id]
    FOREIGN KEY ([prescription_id])
    REFERENCES [dbo].[Prescriptions]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_prescription_id'
CREATE INDEX [IX_FK_prescription_id]
ON [dbo].[Offices_Users]
    ([prescription_id]);
GO

-- Creating foreign key on [user_uid] in table 'Offices_Users'
ALTER TABLE [dbo].[Offices_Users]
ADD CONSTRAINT [FK_user_office_uid]
    FOREIGN KEY ([user_uid])
    REFERENCES [dbo].[Users]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_user_office_uid'
CREATE INDEX [IX_FK_user_office_uid]
ON [dbo].[Offices_Users]
    ([user_uid]);
GO

-- Creating foreign key on [patient_id] in table 'Patient_Prescriptions'
ALTER TABLE [dbo].[Patient_Prescriptions]
ADD CONSTRAINT [FK_Patient_Prescription]
    FOREIGN KEY ([patient_id])
    REFERENCES [dbo].[Paciente]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Patient_Prescription'
CREATE INDEX [IX_FK_Patient_Prescription]
ON [dbo].[Patient_Prescriptions]
    ([patient_id]);
GO

-- Creating foreign key on [sex_id] in table 'Paciente'
ALTER TABLE [dbo].[Paciente]
ADD CONSTRAINT [fk_sex_patient]
    FOREIGN KEY ([sex_id])
    REFERENCES [dbo].[Sex]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_sex_patient'
CREATE INDEX [IX_fk_sex_patient]
ON [dbo].[Paciente]
    ([sex_id]);
GO

-- Creating foreign key on [user_uid] in table 'Patient_Prescriptions'
ALTER TABLE [dbo].[Patient_Prescriptions]
ADD CONSTRAINT [FK_User_Prescription]
    FOREIGN KEY ([user_uid])
    REFERENCES [dbo].[Users]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Prescription'
CREATE INDEX [IX_FK_User_Prescription]
ON [dbo].[Patient_Prescriptions]
    ([user_uid]);
GO

-- Creating foreign key on [rol_id] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [fk_repotrs_roles]
    FOREIGN KEY ([rol_id])
    REFERENCES [dbo].[Roles]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_repotrs_roles'
CREATE INDEX [IX_fk_repotrs_roles]
ON [dbo].[Reports]
    ([rol_id]);
GO

-- Creating foreign key on [status_id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK__Roles__status_id__656C112C]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[Roles_Status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Roles__status_id__656C112C'
CREATE INDEX [IX_FK__Roles__status_id__656C112C]
ON [dbo].[Roles]
    ([status_id]);
GO

-- Creating foreign key on [user_uId] in table 'Servicios'
ALTER TABLE [dbo].[Servicios]
ADD CONSTRAINT [FK_servicios_usuario]
    FOREIGN KEY ([user_uId])
    REFERENCES [dbo].[Users]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_servicios_usuario'
CREATE INDEX [IX_FK_servicios_usuario]
ON [dbo].[Servicios]
    ([user_uId]);
GO

-- Creating foreign key on [status_id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK__Users__status_id__73BA3083]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[Users_Status]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Users__status_id__73BA3083'
CREATE INDEX [IX_FK__Users__status_id__73BA3083]
ON [dbo].[Users]
    ([status_id]);
GO

-- Creating foreign key on [user_uid] in table 'Vacations'
ALTER TABLE [dbo].[Vacations]
ADD CONSTRAINT [fk_vacations_usr]
    FOREIGN KEY ([user_uid])
    REFERENCES [dbo].[Users]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_vacations_usr'
CREATE INDEX [IX_fk_vacations_usr]
ON [dbo].[Vacations]
    ([user_uid]);
GO

-- Creating foreign key on [Citas_uId] in table 'Citas_servicios'
ALTER TABLE [dbo].[Citas_servicios]
ADD CONSTRAINT [FK_Citas_servicios_Citas]
    FOREIGN KEY ([Citas_uId])
    REFERENCES [dbo].[Citas]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Servicios_id] in table 'Citas_servicios'
ALTER TABLE [dbo].[Citas_servicios]
ADD CONSTRAINT [FK_Citas_servicios_Servicios]
    FOREIGN KEY ([Servicios_id])
    REFERENCES [dbo].[Servicios]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Citas_servicios_Servicios'
CREATE INDEX [IX_FK_Citas_servicios_Servicios]
ON [dbo].[Citas_servicios]
    ([Servicios_id]);
GO

-- Creating foreign key on [Doctor_Configs_id] in table 'Doctor_Council_Certifications'
ALTER TABLE [dbo].[Doctor_Council_Certifications]
ADD CONSTRAINT [FK_Doctor_Council_Certifications_Doctor_Configs]
    FOREIGN KEY ([Doctor_Configs_id])
    REFERENCES [dbo].[Doctor_Configs]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Councils__Medical_Specialties_id] in table 'Doctor_Council_Certifications'
ALTER TABLE [dbo].[Doctor_Council_Certifications]
ADD CONSTRAINT [FK_Doctor_Council_Certifications_Councils__Medical_Specialties]
    FOREIGN KEY ([Councils__Medical_Specialties_id])
    REFERENCES [dbo].[Councils__Medical_Specialties]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Doctor_Council_Certifications_Councils__Medical_Specialties'
CREATE INDEX [IX_FK_Doctor_Council_Certifications_Councils__Medical_Specialties]
ON [dbo].[Doctor_Council_Certifications]
    ([Councils__Medical_Specialties_id]);
GO

-- Creating foreign key on [Specialties_id] in table 'Doctor_Specialties'
ALTER TABLE [dbo].[Doctor_Specialties]
ADD CONSTRAINT [FK_Doctor_Specialties_Specialties]
    FOREIGN KEY ([Specialties_id])
    REFERENCES [dbo].[Specialties]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Doctor_Configs_id] in table 'Doctor_Specialties'
ALTER TABLE [dbo].[Doctor_Specialties]
ADD CONSTRAINT [FK_Doctor_Specialties_Doctor_Configs]
    FOREIGN KEY ([Doctor_Configs_id])
    REFERENCES [dbo].[Doctor_Configs]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Doctor_Specialties_Doctor_Configs'
CREATE INDEX [IX_FK_Doctor_Specialties_Doctor_Configs]
ON [dbo].[Doctor_Specialties]
    ([Doctor_Configs_id]);
GO

-- Creating foreign key on [Schedule_id] in table 'Office_Users_Schedules'
ALTER TABLE [dbo].[Office_Users_Schedules]
ADD CONSTRAINT [FK_Office_Users_Schedules_Schedule]
    FOREIGN KEY ([Schedule_id])
    REFERENCES [dbo].[Schedule]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Offices_Users_uid] in table 'Office_Users_Schedules'
ALTER TABLE [dbo].[Office_Users_Schedules]
ADD CONSTRAINT [FK_Office_Users_Schedules_Offices_Users]
    FOREIGN KEY ([Offices_Users_uid])
    REFERENCES [dbo].[Offices_Users]
        ([uid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Office_Users_Schedules_Offices_Users'
CREATE INDEX [IX_FK_Office_Users_Schedules_Offices_Users]
ON [dbo].[Office_Users_Schedules]
    ([Offices_Users_uid]);
GO

-- Creating foreign key on [Roles_id] in table 'Roles_Module'
ALTER TABLE [dbo].[Roles_Module]
ADD CONSTRAINT [FK_Roles_Module_Roles]
    FOREIGN KEY ([Roles_id])
    REFERENCES [dbo].[Roles]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Module_idmodules] in table 'Roles_Module'
ALTER TABLE [dbo].[Roles_Module]
ADD CONSTRAINT [FK_Roles_Module_Module]
    FOREIGN KEY ([Module_idmodules])
    REFERENCES [dbo].[Module]
        ([idmodules])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Roles_Module_Module'
CREATE INDEX [IX_FK_Roles_Module_Module]
ON [dbo].[Roles_Module]
    ([Module_idmodules]);
GO

-- Creating foreign key on [Roles_id] in table 'Roles_Office_Users'
ALTER TABLE [dbo].[Roles_Office_Users]
ADD CONSTRAINT [FK_Roles_Office_Users_Roles]
    FOREIGN KEY ([Roles_id])
    REFERENCES [dbo].[Roles]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Offices_Users_uid] in table 'Roles_Office_Users'
ALTER TABLE [dbo].[Roles_Office_Users]
ADD CONSTRAINT [FK_Roles_Office_Users_Offices_Users]
    FOREIGN KEY ([Offices_Users_uid])
    REFERENCES [dbo].[Offices_Users]
        ([uid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Roles_Office_Users_Offices_Users'
CREATE INDEX [IX_FK_Roles_Office_Users_Offices_Users]
ON [dbo].[Roles_Office_Users]
    ([Offices_Users_uid]);
GO

-- Creating foreign key on [Consultory_uId] in table 'Users_Consultories'
ALTER TABLE [dbo].[Users_Consultories]
ADD CONSTRAINT [FK_Users_Consultories_Consultory]
    FOREIGN KEY ([Consultory_uId])
    REFERENCES [dbo].[Consultory]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_uId] in table 'Users_Consultories'
ALTER TABLE [dbo].[Users_Consultories]
ADD CONSTRAINT [FK_Users_Consultories_Users]
    FOREIGN KEY ([Users_uId])
    REFERENCES [dbo].[Users]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Consultories_Users'
CREATE INDEX [IX_FK_Users_Consultories_Users]
ON [dbo].[Users_Consultories]
    ([Users_uId]);
GO

-- Creating foreign key on [Customers_uId] in table 'Users_Customers'
ALTER TABLE [dbo].[Users_Customers]
ADD CONSTRAINT [FK_Users_Customers_Customers]
    FOREIGN KEY ([Customers_uId])
    REFERENCES [dbo].[Customers]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_uId] in table 'Users_Customers'
ALTER TABLE [dbo].[Users_Customers]
ADD CONSTRAINT [FK_Users_Customers_Users]
    FOREIGN KEY ([Users_uId])
    REFERENCES [dbo].[Users]
        ([uId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Customers_Users'
CREATE INDEX [IX_FK_Users_Customers_Users]
ON [dbo].[Users_Customers]
    ([Users_uId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------