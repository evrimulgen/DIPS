-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- =============================================
-- Author:		<Chuo Yeh Poo>
-- Create date: <01/11/2013>
-- Description:	<Insert Image File>
-- =============================================
CREATE PROCEDURE spr_InsertImages_v001
	-- Add the parameters for the stored procedure here
	@imgID int = NULL,
	@imgUID varchar(70) = NULL,
	@imgNum varchar(5) = NULL,
	@imgBlob varbinary(Max) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @imgNum = ''
		SET @imgNum = NULL

	IF @imgUID = ''
		SET @imgUID = NULL

    -- Insert statements for procedure here
	INSERT INTO images (seriesID,imageNumber,imageBlob,imageUID) VALUES (@imgID,@imgNum,@imgBlob,@imgUID)
END
GO
