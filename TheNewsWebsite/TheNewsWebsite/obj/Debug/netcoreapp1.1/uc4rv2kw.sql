IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Admins] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max),
    [Password] nvarchar(50) NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Admins] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Authors] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max),
    [NumPost] int NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Categaries] (
    [Id] int NOT NULL IDENTITY,
      [Name] nvarchar(max),
    [Status] bit NOT NULL,
    CONSTRAINT [PK_Categaries] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Avatar] nvarchar(max),
    [Email] nvarchar(max) NOT NULL,
    [Gender] nvarchar(max),
    [Name] nvarchar(max),
    [Password] nvarchar(50) NOT NULL,
    [Status] nvarchar(max),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [CategaryChilds] (
    [Id] int NOT NULL IDENTITY,
    [CategariesId] int not null,
    [Name] nvarchar(max),
    [Status] bit NOT NULL,
    CONSTRAINT [PK_CategaryChilds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CategaryChilds_Categaries_CategariesId] FOREIGN KEY ([CategariesId]) REFERENCES [Categaries] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Posts] (
    [Id] int NOT NULL IDENTITY,
    [AdminId] int,
    [AuthorId] int NOT NULL,
    [CateId] int NOT NULL,
    [CategaryChildId] int,
    [Content] nvarchar(max) NOT NULL,
    [DateCreate] datetime2 NOT NULL,
    [DatePublish] datetime2 NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Image] nvarchar(max),
    [Keyword] nvarchar(max),
    [NumView] int NOT NULL,
    [PublishBy] int NOT NULL,
    [Status] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_Admins_AdminId] FOREIGN KEY ([AdminId]) REFERENCES [Admins] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Posts_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Posts_CategaryChilds_CategaryChildId] FOREIGN KEY ([CategaryChildId]) REFERENCES [CategaryChilds] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Cmt] nvarchar(max),
    [CmtDate] datetime2 NOT NULL,
    [PostId] int NOT NULL,
    [Status] nvarchar(max),
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Categaries_CategaryId] ON [Categaries] ([CategaryId]);

GO

CREATE INDEX [IX_CategaryChilds_CategariesId] ON [CategaryChilds] ([CategariesId]);

GO

CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);

GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);

GO

CREATE INDEX [IX_Posts_AdminId] ON [Posts] ([AdminId]);

GO

CREATE INDEX [IX_Posts_AuthorId] ON [Posts] ([AuthorId]);

GO

CREATE INDEX [IX_Posts_CategaryChildId] ON [Posts] ([CategaryChildId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170629041304_MyMigration', N'1.1.2');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170629042509_RunSqlScript', N'1.1.2');

GO

