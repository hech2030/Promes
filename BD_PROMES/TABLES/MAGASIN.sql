IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'MAGASIN'))
BEGIN
	CREATE TABLE [dbo].[MAGASIN](
		[Id] bigint IDENTITY(1,1) NOT NULL,
    	[nomMagasin] nvarchar(max)  NULL
	 CONSTRAINT [PK_MAGASIN] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
