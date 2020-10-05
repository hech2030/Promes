IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'ARTICLE'))
BEGIN
	CREATE TABLE [dbo].[ARTICLE](
		[Id] [BIGINT] NOT NULL,
		[designation] [nvarchar](max) NULL,
		[unit] [nvarchar](max) NULL,
		[quantite] [BIGINT] NULL,
		[prix] [BIGINT] NULL,
		[newAttr] [BIGINT] NULL,
		[emplacement] [nvarchar](max) NULL,
		[IdFournisseur] [BIGINT] NOT NULL,
		[IdCtegorieArt] [BIGINT] NOT NULL,
		[IdMagasin] [BIGINT] NOT NULL
	 CONSTRAINT [PK_ARTICLE] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] 
END
GO
