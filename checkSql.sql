
CREATE TABLE [dbo].[fieldDefCheck](
	[a] [bigint] NOT NULL,
	[b] [binary](50) NULL,
	[c] [bit] NOT NULL,
	[d] [char](10) NULL,
	[e] [date] NULL,
	[f] [datetime] NULL,
	[g] [datetime2](7) NULL,
	[h] [datetimeoffset](7) NULL,
	[i] [decimal](10, 0) NULL,
	[j] [float] NULL,
	[k] [geography] NULL,
	[ll] [geometry] NULL,
	[m] [hierarchyid] NULL,
	[n] [image] NULL,
	[o] [int] NULL,
	[p] [money] NULL,
	[q] [nchar](10) NULL,
	[r] [ntext] NULL,
	[s] [numeric](18, 0) NULL,
	[t] [nvarchar](50) NULL,
	[u] [nvarchar](max) NULL,
	[v] [real] NULL,
	[w] [smalldatetime] NULL,
	[x] [smallint] NULL,
	[y] [smallmoney] NULL,
	[z] [sql_variant] NULL,
	[aa] [text] NULL,
	[bb] [time](7) NULL,
	[cc] [timestamp] NULL,
	[dd] [tinyint] NULL,
	[ee] [uniqueidentifier] NULL,
	[ff] [varbinary](50) NULL,
	[gg] [varbinary](max) NULL,
	[hh] [varchar](50) NULL,
	[ii] [varchar](max) NULL,
	[jj] [xml] NULL,
	[kk] [nchar](10) NULL,
 CONSTRAINT [PK_fieldDefCheck] PRIMARY KEY CLUSTERED 
(
	[a] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


