USE [TravelBlogCapstone]
GO

/****** Object:  StoredProcedure [dbo].[AddNewPost]    Script Date: 6/23/2016 1:41:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddNewPost]
(
	@PostID int OUTPUT,
	@Title varchar(100),
	@PostContent text,
	@LastModifyDate datetime2(7),
	@PublishedDate datetime2(7),
	@ExpiredDate datetime2(7),
	@UserID int,
	@CategoryID int
)AS
DECLARE @InitialPostDate datetime2(7)

SET @InitialPostDate = GetDate()

INSERT INTO Posts(Title, PostContent, LastModifiedDate, InitialPostDate, PublishDate, [ExpireDate], UserID, StatusID, CategoryID)
VALUES (@Title, @PostContent, @LastModifyDate, @InitialPostDate, @PublishedDate, @ExpiredDate, @UserID, 2, @CategoryID)
SET @PostID = Scope_Identity()
RETURN




GO


