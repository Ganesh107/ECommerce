CREATE TABLE [ecom].[User] (
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (128)   NOT NULL,
    [Email]        NVARCHAR (MAX)   NOT NULL,
    [PhoneNumber]  NVARCHAR (14)    NOT NULL,
    [Hash]         VARBINARY (MAX)  NOT NULL,
    [Salt]         VARBINARY (MAX)  NOT NULL,
    [CreatedBy]    NVARCHAR (128)   NOT NULL,
    [CreatedDate]  DATETIME         NOT NULL,
    [ModifiedBy]   NVARCHAR (128)   NULL,
    [ModifiedDate] DATETIME         NULL,
    [IsActive]     BIT              DEFAULT (CONVERT([bit],(1))) NOT NULL,
    CONSTRAINT [PK_User_UserId] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

