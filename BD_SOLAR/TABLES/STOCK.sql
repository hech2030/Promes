IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'STOCK'))
BEGIN
	CREATE TABLE [dbo].[STOCK](
		[Id] [nvarchar](450) NOT NULL,
		[designation] [nvarchar](max) NULL,
		[marque] [nvarchar](max) NULL,
		[type] [nvarchar](max) NULL,
		[quantite] [nvarchar](max) NULL,
		[codeFournisseur] [nvarchar](max) NULL,
		[fournisseur] [nvarchar](max) NULL,
		[prixAchat] [nvarchar](max) NULL
	 CONSTRAINT [PK_STOCK] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
