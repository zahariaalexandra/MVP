CREATE PROCEDURE [dbo].[InsertName]
	@name varchar(15)
AS
	insert into [dbo].[Players] (name) values (@name)
RETURN 0
