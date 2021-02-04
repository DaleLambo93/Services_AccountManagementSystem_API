/****** Object:  Table [dbo].[Accounts]    Script Date: 08/12/2020 10:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[CustomerReference] [varchar](250) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountDetails]    Script Date: 08/12/2020 10:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountStatements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[FirstName] [int] NOT NULL,
	[Surname] [datetime] NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NULL,
        [PostCode] [varchar](8) NOT NULL,
        [Country] [varchar](100) NOT NULL,
        [PostCode] [varchar](8) NOT NULL,
        [EmailAddress] [varchar](320) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
 CONSTRAINT [PK_AccountStatements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_CreatedAt]  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  CONSTRAINT [DF_AccountDetails_CreatedAt]  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
/****** Object:  Trigger [dbo].[TR_Accounts_ModifiedAt]    Script Date: 08/12/2020 10:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   CREATE TRIGGER [dbo].[TR_Accounts_ModifiedAt]
			ON [dbo].[Accounts] after update
			AS
				UPDATE [dbo].[Accounts]
				SET [ModifiedAt] = GETUTCDATE()
				WHERE Id IN (SELECT DISTINCT Id FROM Inserted)
GO
ALTER TABLE [dbo].[AccountDetails] ENABLE TRIGGER [TR_AccountDetails_ModifiedAt]
GO
/****** Object:  Trigger [dbo].[TR_AccountDetails_ModifiedAt]    Script Date: 08/12/2020 10:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   CREATE TRIGGER [dbo].[TR_AccountDetails_ModifiedAt]
			ON [dbo].[AccountDetails] after update
			AS
				UPDATE [dbo].[AccountDetails]
				SET [ModifiedAt] = GETUTCDATE()
				WHERE Id IN (SELECT DISTINCT Id FROM Inserted)
GO
ALTER TABLE [dbo].[AccountDetails] ENABLE TRIGGER [TR_AccountDetails_ModifiedAt]
GO