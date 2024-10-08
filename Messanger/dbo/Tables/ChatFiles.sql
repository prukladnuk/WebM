CREATE TABLE [dbo].[ChatFiles]
(
	[FileId]     INT            IDENTITY (1, 1) NOT NULL,
    [ChatId_FK]       INT NOT NULL,
    [UserID_FK]       NVARCHAR(450) NOT NULL,
    [File_Url]        NVARCHAR(450)           NOT NULL,
    [File_Type]    NVARCHAR(50)          NOT NULL,
    [Uploated_At]  ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Medias] PRIMARY KEY CLUSTERED ([FileId] ASC),
    CONSTRAINT [FK_ChatFiles_ChatID] FOREIGN KEY ([ChatId_FK]) REFERENCES [dbo].[Chats] ([ChatId]),
    CONSTRAINT [FK_ChatFiles_UserID] FOREIGN KEY ([UserID_FK]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
