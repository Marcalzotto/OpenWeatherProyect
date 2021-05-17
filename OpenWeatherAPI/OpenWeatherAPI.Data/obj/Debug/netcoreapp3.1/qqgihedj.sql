IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Country] (
    [ID] int NOT NULL IDENTITY,
    [CODE] varchar(3) NOT NULL,
    [NAME] varchar(100) NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [City] (
    [ID] int NOT NULL,
    [NAME] varchar(100) NOT NULL,
    [STATE] varchar(100) NULL,
    [LONGITUDE] float NOT NULL,
    [LATITUDE] float NOT NULL,
    [COUNTRY_ID] int NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_City_Country1] FOREIGN KEY ([COUNTRY_ID]) REFERENCES [Country] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Branch_office] (
    [ID] int NOT NULL IDENTITY,
    [DESCRIPTION] varchar(100) NOT NULL,
    [CITY_ID] int NOT NULL,
    CONSTRAINT [PK_Branch_office] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Branch_office_City1] FOREIGN KEY ([CITY_ID]) REFERENCES [City] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Weather_condition] (
    [ID] int NOT NULL IDENTITY,
    [BASE] varchar(50) NOT NULL,
    [TEMPERATURE] float NOT NULL,
    [TEMP_MIN] float NOT NULL,
    [TEMP_MAX] float NOT NULL,
    [FEELS_LIKE] float NOT NULL,
    [PRESSURE] int NOT NULL,
    [HUMIDITY] int NOT NULL,
    [SEA_LEVEL] int NULL,
    [GROUND_LEVEL] int NULL,
    [WIND_SPEED] float NOT NULL,
    [WIND_DEGREES] int NOT NULL,
    [WIND_GUST] float NULL,
    [CLOUDS] int NOT NULL,
    [RAIN_VOLUME_1H] float NULL,
    [RAIN_VOLUME_3H] float NULL,
    [SNOW_VOLUME_1H] float NULL,
    [SNOW_VOLUME_3H] float NULL,
    [SUNRISE] int NOT NULL,
    [SUNSET] int NOT NULL,
    [TIMEZONE] int NOT NULL,
    [DT] int NOT NULL,
    [CITY_ID] int NOT NULL,
    [STATUS_CODE] int NOT NULL,
    [REG_DATE] datetime NOT NULL,
    CONSTRAINT [PK_Weather_condition] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Weather_condition_City] FOREIGN KEY ([CITY_ID]) REFERENCES [City] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Weather_type] (
    [ID] int NOT NULL IDENTITY,
    [MAIN] varchar(50) NOT NULL,
    [DESCRIPTION] varchar(50) NOT NULL,
    [ICON] varchar(50) NOT NULL,
    [CONDITION_ID] int NOT NULL,
    [TYPE_ID] int NOT NULL,
    CONSTRAINT [PK_Weather_type] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Weather_type_Weather_condition] FOREIGN KEY ([CONDITION_ID]) REFERENCES [Weather_condition] ([ID]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_Branch_office_City_ID] ON [Branch_office] ([CITY_ID]);
GO

CREATE CLUSTERED INDEX [CITY_PK] ON [City] ([ID]);
GO

CREATE INDEX [IX_City_COUNTRY_ID] ON [City] ([COUNTRY_ID]);
GO

CREATE INDEX [IX_Weather_condition_CITY_ID] ON [Weather_condition] ([CITY_ID]);
GO

CREATE INDEX [IX_Weather_type] ON [Weather_type] ([ID]);
GO

CREATE INDEX [IX_Weather_type_CONDITION_ID] ON [Weather_type] ([CONDITION_ID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210507010050_Initial', N'5.0.5');
GO

COMMIT;
GO

