USE [TransactionDb]
GO
/****** Object:  Table [dbo].[TransactionStatuses]    Script Date: 7/20/2020 9:26:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionStatuses](
	[Id] [bigint] NOT NULL,
	[DisplayName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TransactionStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
