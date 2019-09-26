ALTER PROCEDURE [dbo].[AddMessage]
	-- Add the parameters for the stored procedure here
	@message nvarchar(MAX),
	@parentID int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Message (Message,ParentMessageID)
	values (@message,@parentID)
END
Go

ALTER PROCEDURE [dbo].[GetAllMessages]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Message
END
Go