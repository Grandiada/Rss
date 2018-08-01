CREATE UNIQUE NONCLUSTERED INDEX IX ON [Rss].[dbo].[RssRecords]
(
   [Title],
   [PublishDate]
)
INCLUDE 
(
   [Description],
   [NewsUrl],
   [SourceUrl],
   [NameSource]
)