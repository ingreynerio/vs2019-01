USE [Chinook]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetArtists]    Script Date: 23/02/2019 08:53:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetArtists]
(
	@FiltroByName NVARCHAR(100)
)
AS
BEGIN
	SELECT 
		[Name], 
		ArtistId
	FROM Artist WITH(NOLOCK)
	WHERE [Name] LIKE @FiltroByName
END
GO


