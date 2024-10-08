CREATE TABLE [dbo].[Reactions]
(
    [Reaction_Id]      INT            IDENTITY (1, 1) NOT NULL,
    [MessageId_FK]       INT NOT NULL,
    [UserID_FK]       NVARCHAR(450) NOT NULL,
    [Reaction_Type]    NVARCHAR(50)          NOT NULL,
    [Reacted_At]         ROWVERSION     NOT NULL
    CONSTRAINT [PK_Reactions] PRIMARY KEY CLUSTERED ([Reaction_Id] ASC),
    CONSTRAINT [FK_Reactions_MessageID] FOREIGN KEY ([MessageId_FK]) REFERENCES [dbo].[Messages] ([MessageId]),
    CONSTRAINT [FK_Reactions_UserID] FOREIGN KEY ([UserID_FK]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
