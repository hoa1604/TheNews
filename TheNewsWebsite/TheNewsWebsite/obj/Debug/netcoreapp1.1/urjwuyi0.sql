IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

ALTER TABLE [Posts] DROP CONSTRAINT [FK_Posts_Admins_AdminId];

GO

DROP INDEX [IX_Posts_AdminId] ON [Posts];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Posts') AND [c].[name] = N'AdminId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Posts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Posts] DROP COLUMN [AdminId];

GO

CREATE INDEX [IX_Posts_PublishBy] ON [Posts] ([PublishBy]);

GO

ALTER TABLE [Posts] ADD CONSTRAINT [ForeignKey_Post_Publish] FOREIGN KEY ([PublishBy]) REFERENCES [Admins] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170628095941_migra', N'1.1.2');

GO

