USE MusicDb
GO

CREATE TABLE [dbo].[Artist](
	ArtistId [int] NOT NULL PRIMARY KEY,
	Name varchar(150) NOT NULL,
	UrlSafeName varchar(150) NOT NULL
GO


CREATE TABLE [dbo].[Video](	
	VideoId [int] NOT NULL PRIMARY KEY,	
	Isrc varchar(150) NOT NULL,
	Title varchar(150) NOT NULL,	
	UrlSafeTitle varchar(200) NOT NULL,
	ArtistId [int] NOT NULL,
	CONSTRAINT [FK_Video_ToArtist] FOREIGN KEY ([artistId]) REFERENCES [dbo].[Artist]([Artistid])
)
CREATE NONCLUSTERED INDEX [IX_Video_ArtistId] ON [dbo].[Video] 
(
 [ArtistId]
)
GO


BEGIN
insert into artist (ArtistId, Name, UrlSafeName) values (1, 'Big Time Rush', 'big-time-rush')
insert into artist (ArtistId, Name, UrlSafeName) values (2, 'Shakira', 'shakira')
insert into artist (ArtistId, Name, UrlSafeName) values (3, 'Pitbull', 'pitbull')
insert into artist (ArtistId, Name, UrlSafeName) values (4, 'Enrique Iglesias', 'enrique-iglesias')
insert into artist (ArtistId, Name, UrlSafeName) values (5, 'Katy Perry', 'katy-perry')
insert into artist (ArtistId, Name, UrlSafeName) values (6, 'Calvin Harris', 'calvin-harris')
insert into artist (ArtistId, Name, UrlSafeName) values (7, 'One Republic', 'one-republic')
insert into artist (ArtistId, Name, UrlSafeName) values (8, 'John Legend', 'john-legend')
END
GO


BEGIN
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (1, 'USSM21200785', 'Windows Down', 'windows-down', 1 )
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (2, 'USRV81400102', 'La La La (Brazil 2014)', 'la-la-la-(brazil-2014)', 2 )
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (3, 'USRV81400057', 'We Are One (Ole Ola) [The Official 2014 FIFA World Cup Song] (Olodum Mix)', 'we-are-one-(ole-ola)-[the-official-2014-fifa-world-cup-song]-(olodum-mix)', 3 )
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (4, 'GBUV71400428', 'Bailando (Español)', 'bailando-(español)', 4 )
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (5, 'USUV71400083', 'Dark Horse (Official)', 'dark-horse-(official)', 5 )
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (6, 'GB1101400141', 'Summer', 'summer', 6 )
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (7, 'USUV71301101', 'Counting Stars', 'counting-stars', 7 )
insert into video (VideoId, Isrc, Title, UrlSafeTitle, ArtistId) values (8, 'USSM21302088', 'All of Me', 'all-of-me', 8 )
END
GO


CREATE PROCEDURE [dbo].[GetArtists]
	(@ArtistId int = NULL)	
AS
SET NOCOUNT ON
BEGIN
	SELECT a.ArtistId as ArtistId,
	a.name as Name,
	a.urlSafeName As UrlSafeName,
	COALESCE(v.Videoid, 0) As VideoId,
	COALESCE(v.ISrc, '') As ISrc,
	COALESCE(v.Title,'') As Title,
	COALESCE(v.UrlSafeTitle, '') As UrlSafeTitle

	FROM  Artist a LEFT JOIN Video v
	ON v.artistId = a.ArtistId
	WHERE a.ArtistId  = ISNULL(@artistId, a.ArtistId)
END
GO


CREATE PROCEDURE [dbo].[CreateNewArtist]
	@ArtistId int,
	@Name nvarchar(150),	
	@UrlSafeName nvarchar(150)	
	
AS
BEGIN
	SET NOCOUNT ON
	INSERT Artist(ArtistId, Name, UrlSafeName )		
	VALUES (@ArtistId, @Name, @UrlSafeName )	
END
GO


CREATE PROCEDURE [dbo].[DeleteArtist]
	@ArtistId int	
AS
BEGIN
	set nocount on
	delete Artist where ArtistId = @ArtistId 
END
GO


CREATE PROCEDURE [dbo].[GetVideos]
	(@videoId int = NULL)
AS
SET NOCOUNT ON
BEGIN
	SELECT 
	v.VideoId,
	v.iSrc,
	v.Title,
	v.urlSafeTitle,
	v.artistId As artistId
	FROM  Video v
	WHERE v.Artistid  = ISNULL(@videoId, v.Artistid)
END
GO

CREATE PROCEDURE [dbo].[UpdateArtist]
	@ArtistId int,
	@Name nvarchar(150),
	@UrlSafeName nvarchar(150)	
AS
BEGIN
	update Artist
	set Name = @Name, 
	UrlSafeName = @UrlSafeName		
	where ArtistId = @ArtistId	
END
GO


