CREATE TABLE [dbo].[ForwardMessages]
(
	[ForwardId]      INT            IDENTITY (1, 1) NOT NULL,
    [OriginalMessageId_FK]       INT NOT NULL,
    [NewChatId_FK]       INT NOT NULL,
    [Forward_At]         ROWVERSION     NOT NULL,
    [OriginalIs_Deleted]      BIT            NOT NULL,
    CONSTRAINT [PK_ForwardMessages] PRIMARY KEY CLUSTERED ([ForwardId] ASC),
    CONSTRAINT [FK_ForwardMessages_MessageID] FOREIGN KEY ([OriginalMessageId_FK]) REFERENCES [dbo].[Messages] ([MessageId]),
    CONSTRAINT [FK_ForwardMessages_ChatID] FOREIGN KEY ([NewChatId_FK]) REFERENCES [dbo].[Chats] ([ChatId]),
)
