CREATE PROCEDURE dbo.getChildLastName @childLstName varchar(75)
AS
BEGIN
	SELECT * FROM dbo.[Case Database] WHERE childLastName = @childLstName
END
GO