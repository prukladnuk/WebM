CREATE TABLE [dbo].[Messages]
(
	[MessageId]      INT            IDENTITY (1, 1) NOT NULL,
    [ChatId_FK]       INT NOT NULL,
    [UserID_FK]       NVARCHAR(450) NOT NULL,
    [FileID_FK]       INT            NOT NULL,
    [Sent_At]         ROWVERSION     NOT NULL,
    [Edited_At]       DATETIME            NOT NULL,
    [Content]         TEXT           NOT NULL,
    [Message_Type]    NVARCHAR(50)          NOT NULL,
    [Is_Read]         BIT            NOT NULL,
    [Is_Deleted]      BIT            NOT NULL,
    [Is_Forwarded]    BIT            NOT NULL
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([MessageId] ASC),
    CONSTRAINT [FK_Messages_ChatID] FOREIGN KEY ([ChatId_FK]) REFERENCES [dbo].[Chats] ([ChatId]),
    CONSTRAINT [FK_Messages_FileId] FOREIGN KEY ([FileID_FK]) REFERENCES [dbo].[ChatFiles] ([FileId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Messages_UserID] FOREIGN KEY ([UserID_FK]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
