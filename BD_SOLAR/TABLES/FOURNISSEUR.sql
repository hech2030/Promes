IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'FOURNISSEUR'))
BEGIN
	CREATE TABLE [dbo].[FOURNISSEUR](
		[Id] bigint IDENTITY(1,1) NOT NULL,
	    [numF] bigint  NOT NULL,
	    [NomF] nvarchar(max)  NULL,
	    [adresse] nvarchar(max)  NULL,
	    [codP] int  NULL,
	    [ville] nvarchar(max)  NULL,
	    [payes] nvarchar(max)  NULL,
	    [tele] bigint  NULL,
	    [fax] bigint  NULL,
	    [email] nvarchar(max)  NULL
	 CONSTRAINT [PK_FOURNISSEUR] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
