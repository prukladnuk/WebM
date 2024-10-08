CREATE TABLE [dbo].[Chats]
(
	[ChatId]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NULL,
    [Icon]          IMAGE NULL,
    [Description]   VARCHAR (100) NULL,
    [Is_Group]      BIT           NOT NULL,
    [Is_Channel]    BIT           NOT NULL,
    CONSTRAINT [PK_Chats] PRIMARY KEY CLUSTERED ([ChatId] ASC)
)

