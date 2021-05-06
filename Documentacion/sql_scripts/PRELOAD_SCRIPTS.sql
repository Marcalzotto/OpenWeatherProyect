USE [OpenWeatherDB]
GO

DECLARE

@COUNTRYID INT

BEGIN

--SCRIPT DE PRECARGA DE PAISES Y CIUDADES, EJECUTAR LUEGO DE CREADA LA BASE DE DATOS
INSERT INTO [dbo].[Country] ([CODE],[NAME]) VALUES ('AR','Argentina');
INSERT INTO [dbo].[Country] ([CODE],[NAME]) VALUES ('GB','Great Britain');
INSERT INTO [dbo].[Country] ([CODE],[NAME]) VALUES ('ES','Spain');
INSERT INTO [dbo].[Country] ([CODE],[NAME]) VALUES ('BR','Brazil');
INSERT INTO [dbo].[Country] ([CODE],[NAME]) VALUES ('US','United States');


--CIUDADES DE ARGENTINA
SELECT @COUNTRYID = ID FROM dbo.Country where CODE = 'AR'; 

INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (6942842,'Bella Italia',null,-61.423962,-31.27268,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7116865,'Caril�',null,-56.891331,-37.165241,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (6693229,'San Nicolas',null,-58.376068,-34.605019,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7116866,'Villa Mercedes',null,-65.457832,-33.675709,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7535637,'Avellaneda',null,-58.367439,-34.660179,@COUNTRYID);

--CIUDADES DE ESPA�A

SELECT @COUNTRYID = ID FROM dbo.Country where CODE = 'ES'; 

INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (6697035,'Navas de San Juan',null,-3.31598,38.183819,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (6697039,'Basauri',null,-2.8858,43.2397,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7577022,'Tres Cantos',null,-3.70806,40.600922,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7577023,'Castellar del Riu',null,1.77378,42.12299,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7577026,'Capolat',null,1.75352,42.077202,@COUNTRYID);


--CIUDADES DE GRAN BRETA�A

SELECT @COUNTRYID = ID FROM dbo.Country where CODE = 'GB'; 

INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7284876,'Heathrow',null,-0.4529,51.4673,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (7287910,'Fetcham',null,-0.35582,51.288792,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (2633351,'City of York',null,-1.09142,53.963959,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (2633352,'York',null,-1.08271,53.95763,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (2633481,'Writtle',null,0.42938,51.729061,@COUNTRYID);

--CIUDADES DE BRASIL

SELECT @COUNTRYID = ID FROM dbo.Country where CODE = 'BR'; 

INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (3384983,'Vitorino Freire',null,-45.166672,-4.06667,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (3384986,'Vit�ria do Mearim',null,-44.87056,-3.46222,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (3385022,'Viseu',null,-46.139999,-1.19667,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (3385077,'Conde',null,-34.907501,-7.25972,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (3385106,'Vi�osa do Cear�',null,-41.09222,-3.56222,@COUNTRYID);


--CIUDADES DE ESTADOS UNIDOS

SELECT @COUNTRYID = ID FROM dbo.Country where CODE = 'US'; 

INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (4046255,'Bay Minette','AL',-87.773048,30.882959,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (4046274,'Edna','TX',-96.646088,28.97859,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (4046319,'Bayou La Batre','AL',-88.24852,30.403521,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (4046440,'Northrup','TX',-96.970261,30.1005,@COUNTRYID);
INSERT INTO [dbo].[City]([ID],[NAME],[STATE],[LONGITUDE],[LATITUDE],[COUNTRY_ID])
				 VALUES (4046499,'Bear Creek','AL',-87.700577,34.274818,@COUNTRYID);

END;