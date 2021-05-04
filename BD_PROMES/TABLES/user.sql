IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'user'))
BEGIN
	CREATE TABLE [dbo].[user](
		[Id] [nvarchar](450) NOT NULL,
		[UserName] [nvarchar](max) NULL,
		[NormalizedUserName] [nvarchar](max) NULL,
		[Email] [nvarchar](max) NULL,
		[NormalizedEmail] [nvarchar](max) NULL,
		[EmailConfirmed] [bit] NOT NULL,
		[PasswordHash] [nvarchar](max) NULL,
		[SecurityStamp] [nvarchar](max) NULL,
		[ConcurrencyStamp] [nvarchar](max) NULL,
		[PhoneNumber] [nvarchar](max) NULL,
		[PhoneNumberConfirmed] [bit] NOT NULL,
		[TwoFactorEnabled] [bit] NOT NULL,
		[LockoutEnd] [datetimeoffset](7) NULL,
		[LockoutEnabled] [bit] NOT NULL,
		[AccessFailedCount] [int] NOT NULL,
		[role] [int] NOT NULL,
		[roleLabel] [varchar](50) NULL,
	 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
