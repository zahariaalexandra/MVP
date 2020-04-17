CREATE TABLE [dbo].[Players]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [name] VARCHAR(15) NOT NULL, 
    [games_won] INT NULL, 
    [games_lost] INT NULL, 
    [games_played] INT NULL
)
