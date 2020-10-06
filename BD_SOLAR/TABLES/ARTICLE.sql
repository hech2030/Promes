IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'ARTICLE'))
BEGIN
	CREATE TABLE [dbo].[ARTICLE](
		[Id] bigint  NOT NULL,
	    [designation] nvarchar(max)  NULL,
	    [unit] nvarchar(max)  NULL,
	    [quantite] bigint  NULL,
	    [prix] bigint  NULL,
	    [newAttr] bigint  NULL,
	    [emplacement] nvarchar(max)  NULL,
	    [CATEGORIE_ARTId] bigint  NOT NULL,
	    [FOURNISSEURId] bigint  NOT NULL,
	    [MAGASINId] bigint  NOT NULL
	 CONSTRAINT [PK_ARTICLE] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] 
END
GO
