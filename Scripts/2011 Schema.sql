
USE [HRLeague]
GO
BEGIN TRANSACTION

DELETE FROM ReportLeagueRaceBet
DELETE FROM UserStandings
DELETE FROM UserRaceDetail
DELETE FROM UserDue
DELETE FROM RaceDetailPayout
DELETE FROM RaceDetails
DELETE FROM LeagueRace
DELETE FROM Race

IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
	END
ELSE
	BEGIN
		COMMIT TRANSACTION
	END

DECLARE @RaceId INT

INSERT RACE SELECT 'Hutcheson', LTRIM(RTRIM('GP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '2/26/2011', 1)
INSERT RACE SELECT 'Fountain of Youth', LTRIM(RTRIM('GP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '2/26/2011', 1)
INSERT RACE SELECT 'Gotham', LTRIM(RTRIM('AQU')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '3/5/2011', 1)
INSERT RACE SELECT 'San Felipe', LTRIM(RTRIM('SA')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '3/12/2011', 1)
INSERT RACE SELECT 'Tampa Bay Derby', LTRIM(RTRIM('TAM')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '3/12/2011', 1)
INSERT RACE SELECT 'Rebel', LTRIM(RTRIM('OP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '3/19/2011', 1)
INSERT RACE SELECT 'Louisiana Derby', LTRIM(RTRIM('FG')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '3/26/2011', 1)
INSERT RACE SELECT 'Vinery Racing Spiral', LTRIM(RTRIM('TP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '3/26/2011', 1)
INSERT RACE SELECT 'Sunland Derby', LTRIM(RTRIM('SUN')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '3/27/2011', 1)
INSERT RACE SELECT 'Swale Stakes', LTRIM(RTRIM('GP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/3/2011', 1)
INSERT RACE SELECT 'Florida Derby', LTRIM(RTRIM('GP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/3/2011', 2)
INSERT RACE SELECT 'Santa Anita Derby', LTRIM(RTRIM('SA')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/9/2011', 2)
INSERT RACE SELECT 'Wood Memorial', LTRIM(RTRIM('AQU')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/9/2011', 2)
INSERT RACE SELECT 'Illinois Derby', LTRIM(RTRIM('HAW')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/9/2011', 1)
INSERT RACE SELECT 'Bay Shore', LTRIM(RTRIM('AQU')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/9/2011', 1)
INSERT RACE SELECT 'Toyota Blue Grass', LTRIM(RTRIM('KEE')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/16/2011', 2)
INSERT RACE SELECT 'Arkansas Derby', LTRIM(RTRIM('OP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/16/2011', 2)
INSERT RACE SELECT 'Coolmore Lexington', LTRIM(RTRIM('KEE')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/23/2011', 1)
INSERT RACE SELECT 'Jerome', LTRIM(RTRIM('BEL')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/23/2011', 1)
INSERT RACE SELECT 'The Cliff''s Edge Derby Trial', LTRIM(RTRIM('CD')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '4/30/2011', 1)
INSERT RACE SELECT 'Kentucky Derby', LTRIM(RTRIM('CD')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '5/7/2011', 4)
INSERT RACE SELECT 'Preakness', LTRIM(RTRIM('PIM')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '5/21/2011', 3)
INSERT RACE SELECT 'Belmont Stakes', LTRIM(RTRIM('BEL')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight) VALUES(@RaceId, '6/11/2011', 3)

/****** Object:  Table [dbo].[RaceDetailPayout]    Script Date: 01/20/2011 20:46:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RaceDetailPayout2](
	[RaceDetailPayoutId] [int] IDENTITY(1,1) NOT NULL,
	[RaceDetailId] [int] NOT NULL,
	[LeagueRaceId] [int] NOT NULL,
	[BetType] [int] NOT NULL,
	[WinAmount] [float] NULL,
	[PlaceAmount] [float] NULL,
	[ShowAmount] [float] NULL,
 CONSTRAINT [PK_RaceDetailPayout] PRIMARY KEY CLUSTERED 
(
	[RaceDetailPayoutId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--INSERT RaceDetailPayout2
--	SELECT RaceDetailId, LeagueRaceId, BetType, WinAmount, PlaceAmount, ShowAmount FROM RaceDetailPayout

GO

DROP TABLE RaceDetailPayout

GO

sp_RENAME 'RaceDetailPayout2' , 'RaceDetailPayout'

ALTER TABLE [dbo].[RaceDetailPayout]  WITH CHECK ADD  CONSTRAINT [FK_RaceDetailPayout_RaceDetails] FOREIGN KEY([RaceDetailId])
REFERENCES [dbo].[RaceDetails] ([RaceDetailId])
GO

ALTER TABLE [dbo].[RaceDetailPayout]  WITH CHECK ADD  CONSTRAINT [FK_RaceDetailPayout_LeagueRace] FOREIGN KEY([LeagueRaceId])
REFERENCES [dbo].[LeagueRace] ([Id])
GO

ALTER TABLE [dbo].[RaceDetailPayout] CHECK CONSTRAINT [FK_RaceDetailPayout_RaceDetails]
GO
ALTER TABLE [dbo].[RaceDetailPayout] CHECK CONSTRAINT [FK_RaceDetailPayout_LeagueRace]
GO


/****** Object:  Table [dbo].[ReportLeagueRaceBet]    Script Date: 01/20/2011 22:21:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE ReportLeagueRaceBet


GO
CREATE TABLE [dbo].[ReportLeagueRaceBet](
	[ReportLeagueRaceBetId] [int] IDENTITY(1,1) NOT NULL,	
	[LeagueRaceId] [int] NOT NULL,
	[RaceDetailId] [int] NOT NULL,
	[BetType] [int] NOT NULL,
	[UserBetCount] [int] NOT NULL,
CONSTRAINT [PK_ReportLeagueRaceBet] PRIMARY KEY CLUSTERED 
(
	[ReportLeagueRaceBetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



ALTER TABLE [dbo].[ReportLeagueRaceBet]  WITH CHECK ADD  CONSTRAINT [FK_ReportLeagueRaceBet_RaceDetails] FOREIGN KEY([RaceDetailId])
REFERENCES [dbo].[RaceDetails] ([RaceDetailId])
GO

ALTER TABLE [dbo].[ReportLeagueRaceBet] CHECK CONSTRAINT [FK_ReportLeagueRaceBet_RaceDetails]
GO

GO

ALTER TABLE [dbo].[ReportLeagueRaceBet]  WITH CHECK ADD  CONSTRAINT [FK_ReportLeagueRaceBet_LeagueRace] FOREIGN KEY([LeagueRaceId])
REFERENCES [dbo].[LeagueRace] ([Id])
GO

ALTER TABLE [dbo].[ReportLeagueRaceBet] CHECK CONSTRAINT [FK_ReportLeagueRaceBet_LeagueRace]
GO

GO

/****** Object:  Index [IX_ReportLeagueRaceBet]    Script Date: 01/20/2011 22:35:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ReportLeagueRaceBet] ON [dbo].[ReportLeagueRaceBet] 
(
	[RaceDetailId] ASC,
	[BetType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO




USE [HRLeague]
GO

/****** Object:  Table [dbo].[League]    Script Date: 01/23/2011 13:38:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


CREATE TABLE [dbo].[League](
	[LeagueId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
 CONSTRAINT [PK_League] PRIMARY KEY CLUSTERED 
(
	[LeagueId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT INTO League
	SELECT 'Main'
	
GO
SET ANSI_PADDING OFF
GO


USE [HRLeague]
GO

/****** Object:  Table [dbo].[UserLeague]    Script Date: 01/23/2011 13:38:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[UserLeague](
	[UserLeagueId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[LeagueId] [int] NOT NULL,
 CONSTRAINT [PK_UserLeague] PRIMARY KEY CLUSTERED 
(
	[UserLeagueId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserLeague]  WITH CHECK ADD  CONSTRAINT [FK_UserLeague_League] FOREIGN KEY([LeagueId])
REFERENCES [dbo].[League] ([LeagueId])
GO

ALTER TABLE [dbo].[UserLeague] CHECK CONSTRAINT [FK_UserLeague_League]
GO

ALTER TABLE [dbo].[UserLeague]  WITH CHECK ADD  CONSTRAINT [FK_UserLeague_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[UserLeague] CHECK CONSTRAINT [FK_UserLeague_UserId]
GO

ALTER TABLE [dbo].[LeagueRace]
ADD [LeagueId] [int] NULL

GO
INSERT UserLeague
	SELECT UserId, 1 FROM aspnet_Users
GO
UPDATE LeagueRace SET LeagueId = 1

GO

ALTER TABLE [dbo].[LeagueRace] ALTER COLUMN [LeagueId] [int] NOT NULL

GO
UPDATE LeagueRace SET LeagueId = 1

GO
ALTER TABLE [dbo].[LeagueRace]  WITH CHECK ADD  CONSTRAINT [FK_LeagueRace_League] FOREIGN KEY([LeagueId])
REFERENCES [dbo].[League] ([LeagueId])
GO

ALTER TABLE [dbo].[LeagueRace] CHECK CONSTRAINT [FK_LeagueRace_League]
GO

drop TABLE [dbo].[UserStandings]

GO

CREATE TABLE [dbo].[UserStandings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeagueId] [int] NOT NULL,
	[UserLeagueId] [int] NOT NULL,
	[Yr] [datetime] NOT NULL,
	[Total] [float] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[WinWinPct] [float] NOT NULL,
	[WinPlacePct] [float] NOT NULL,
	[WinShowPct] [float] NOT NULL,
	[PlacePlacePct] [float] NOT NULL,
	[PlaceShowPct] [float] NOT NULL,
	[ShowShowPct] [float] NOT NULL,
	[WinWinAvg] [float] NOT NULL,
	[WinPlaceAvg] [float] NOT NULL,
	[WinShowAvg] [float] NOT NULL,
	[PlacePlaceAvg] [float] NOT NULL,
	[PlaceShowAvg] [float] NOT NULL,
	[ShowShowAvg] [float] NOT NULL,
	[WinFavPct] [float] NOT NULL,
	[ROI] [float] NOT NULL,
 CONSTRAINT [PK_UserStandings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserStandings]  WITH CHECK ADD  CONSTRAINT [FK_UserStandings_UserLeague] FOREIGN KEY([UserLeagueId])
REFERENCES [dbo].[UserLeague] ([UserLeagueId])
GO

ALTER TABLE [dbo].[UserStandings] CHECK CONSTRAINT [FK_UserStandings_UserLeague]
GO

GO

ALTER TABLE [dbo].[UserStandings]  WITH CHECK ADD  CONSTRAINT [FK_UserStandings_League] FOREIGN KEY([LeagueId])
REFERENCES [dbo].[League] ([LeagueId])
GO

ALTER TABLE [dbo].[UserStandings] CHECK CONSTRAINT [FK_UserStandings_League]
GO

DROP TABLE UserRaceDetail

GO

CREATE TABLE [dbo].[UserRaceDetail](
	[UserRaceDetailId] [int] IDENTITY(1,1) NOT NULL,
	[RaceDetailId] [int] NOT NULL,
	[UserLeagueId] [int] NOT NULL,
	[BetType] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRaceDetail2] PRIMARY KEY CLUSTERED 
(
	[UserRaceDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

	
GO

GO

ALTER TABLE [dbo].[UserRaceDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserRaceDetail_UserLeague] FOREIGN KEY([UserLeagueId])
REFERENCES [dbo].[UserLeague] ([UserLeagueId])
GO

ALTER TABLE [dbo].[UserRaceDetail] CHECK CONSTRAINT [FK_UserRaceDetail_UserLeague]
GO


ALTER TABLE [dbo].[UserRaceDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserRaceDetail_RaceDetails] FOREIGN KEY([RaceDetailId])
REFERENCES [dbo].[RaceDetails] ([RaceDetailId])
GO

ALTER TABLE [dbo].[UserRaceDetail] CHECK CONSTRAINT [FK_UserRaceDetail_RaceDetails]
GO

ALTER TABLE [dbo].[UserRaceDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserRaceDetail_UserRaceDetail] FOREIGN KEY([UserRaceDetailId])
REFERENCES [dbo].[UserRaceDetail] ([UserRaceDetailId])
GO

ALTER TABLE [dbo].[UserRaceDetail] CHECK CONSTRAINT [FK_UserRaceDetail_UserRaceDetail]
GO

/****** Object:  Index [IX_UserRaceDetail]    Script Date: 01/22/2011 09:47:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserRaceDetail] ON [dbo].[UserRaceDetail] 
(
	[RaceDetailId] ASC,
	[UserLeagueId] ASC,
	[BetType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

USE [HRLeague]
GO

CREATE TABLE [dbo].[RaceExoticPayout](
	[RaceExoticPayoutId] [int] IDENTITY(1,1) NOT NULL,
	[LeagueRaceId] [int] NOT NULL,
	[BetType] [int] NOT NULL,
	[Amount] [float] NOT NULL
 CONSTRAINT [PK_RaceExoticPayout] PRIMARY KEY CLUSTERED 
(
	[RaceExoticPayoutId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RaceExoticPayout]  WITH CHECK ADD  CONSTRAINT [FK_RaceExoticPayout_LeagueRace] FOREIGN KEY([LeagueRaceId])
REFERENCES [dbo].[LeagueRace] ([Id])
GO

ALTER TABLE [dbo].[RaceExoticPayout] CHECK CONSTRAINT [FK_RaceExoticPayout_LeagueRace]
GO

GO


CREATE TABLE [dbo].[UserLeagueRaceExoticPayout](
	[UserLeagueRaceExoticPayoutId] [int] IDENTITY(1,1) NOT NULL,
	[RaceExoticPayoutId] [int] NOT NULL,
	[LeagueId] [int] NOT NULL,
	[UserLeagueId] [int] NOT NULL
	
 CONSTRAINT [PK_UserLeagueRaceExoticPayout] PRIMARY KEY CLUSTERED 
(
	[UserLeagueRaceExoticPayoutId] ASC
	
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserLeagueRaceExoticPayout]  WITH CHECK ADD  CONSTRAINT [FK_UserLeagueRaceExoticPayout_RaceExoticPayout] FOREIGN KEY([RaceExoticPayoutId])
REFERENCES [dbo].[RaceExoticPayout] ([RaceExoticPayoutId])
GO

ALTER TABLE [dbo].[UserLeagueRaceExoticPayout] CHECK CONSTRAINT [FK_UserLeagueRaceExoticPayout_RaceExoticPayout]
GO

ALTER TABLE [dbo].[UserLeagueRaceExoticPayout]  WITH CHECK ADD  CONSTRAINT [FK_UserLeagueRaceExoticPayout_UserLeague] FOREIGN KEY([UserLeagueId])
REFERENCES [dbo].[UserLeague] ([UserLeagueId])
GO

ALTER TABLE [dbo].[UserLeagueRaceExoticPayout] CHECK CONSTRAINT [FK_UserLeagueRaceExoticPayout_UserLeague]
GO

GO

ALTER TABLE [dbo].[UserLeagueRaceExoticPayout]  WITH CHECK ADD  CONSTRAINT [FK_UserLeagueRaceExoticPayout_League] FOREIGN KEY([LeagueId])
REFERENCES [dbo].[League] ([LeagueId])
GO

ALTER TABLE [dbo].[UserLeagueRaceExoticPayout] CHECK CONSTRAINT [FK_UserLeagueRaceExoticPayout_League]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UserLeagueRaceExoticPayout] ON [dbo].[UserLeagueRaceExoticPayout] 
(
	[RaceExoticPayoutId] ASC,
	[LeagueId] ASC,
	[UserLeagueId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  StoredProcedure [hrleague_user].[CalculateUserTotals]    Script Date: 02/07/2011 23:06:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[hrleague_user].[CalculateUserTotals]') AND type in (N'P', N'PC'))
DROP PROCEDURE [hrleague_user].[CalculateUserTotals]
GO

USE [HRLeague]
GO

/****** Object:  StoredProcedure [hrleague_user].[CalculateUserTotals]    Script Date: 02/07/2011 23:06:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CalculateUserTotals] 
(
	@LeagueId int
)
AS
	DECLARE @curUser as int
	DECLARE @Total as float
	DECLARE @raceDetailId as int
	DECLARE @betType as int
	DECLARE @leagueRaceId as int
	DECLARE @WinAmount as float
	DECLARE @PlaceAmount as float
	DECLARE @ShowAmount as float
	DECLARE @Weight as int

	/* Additional variables for detailed reports */
	DECLARE @TotalRaces as int
	DECLARE @TotalWinWin as float
	DECLARE @TotalWinPlace as float
	DECLARE @TotalWinShow as float
	DECLARE @TotalPlacePlace as float
	DECLARE @TotalPlaceShow as float
	DECLARE @TotalShowShow as float
	DECLARE @NumWinWin as float
	DECLARE @NumWinPlace as float
	DECLARE @NumWinShow as float
	DECLARE @NumPlacePlace as float
	DECLARE @NumPlaceShow as float
	DECLARE @NumShowShow as float
	DECLARE @NumWinFavorites as float
	DECLARE @TotalUnWeighted as float
	DECLARE @FavoriteId as int
	DECLARE @ROI as float
	/* ----------------------------------------- */

	DELETE FROM dbo.UserStandings WHERE Year(Yr) = Year(GetDate())

	SET @TotalRaces = (SELECT COUNT(distinct Id) FROM LeagueRace lr INNER JOIN RaceDetailPayout rdp on lr.Id = rdp.LeagueRaceId WHERE Year(Dt) = Year(GetDate()))
	PRINT 'TOTAL RACES: ' + CAST(@TotalRaces as varchar(30))

	DECLARE curUsers Cursor
	FOR
	SELECT DISTINCT urd.UserLeagueId
	FROM   dbo.UserRaceDetail urd 
	INNER JOIN dbo.RaceDetailPayout rdp ON urd.RaceDetailId = rdp.RaceDetailId
	 
	OPEN curUsers  
	FETCH NEXT FROM curUsers INTO @curUser

	WHILE @@FETCH_STATUS = 0  
	BEGIN 
		SET @Total = 0
		SET @TotalUnWeighted = 0
		SET @TotalWinWin = 0
		SET @TotalWinPlace  = 0
		SET @TotalWinShow  = 0
		SET @TotalPlacePlace  = 0
		SET @TotalPlaceShow  = 0
		SET @TotalShowShow  = 0
		SET @NumWinWin  = 0
		SET @NumWinPlace  = 0
		SET @NumWinShow  = 0
		SET @NumPlacePlace  = 0
		SET @NumPlaceShow  = 0
		SET @NumShowShow  = 0 
		SET @NumWinFavorites = 0
		SET @FavoriteId = -1

		DECLARE curRaces Cursor
		FOR
		SELECT urd.RaceDetailId, urd.BetType, rdp.LeagueRaceId, rdp.WinAmount, rdp.PlaceAmount, rdp.SHowAmount
		FROM   dbo.UserRaceDetail urd 
		INNER JOIN dbo.RaceDetailPayout rdp ON urd.RaceDetailId = rdp.RaceDetailId
		WHERE urd.UserLeagueId = @curUser AND urd.BetType < 4
		ORDER BY rdp.LeagueRaceId, urd.BetType
	
		OPEN curRaces  
		FETCH NEXT FROM curRaces INTO @raceDetailId, @betType, @leagueRaceId, @WinAmount, @PlaceAmount, @ShowAmount

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @Weight = (SELECT Weight FROM LeagueRace WHERE Id = @leagueRaceId)
			
			SET @WinAmount = ISNULL(@WinAmount, 0)
			SET @PlaceAmount = ISNULL(@PlaceAmount, 0)
			SET @ShowAmount = ISNULL(@ShowAmount, 0)

			IF @BetType = 1 --WIN
			BEGIN
				Set @Total = @Total + ((@WinAMount + @PlaceAmount + @ShowAmount) * @Weight)
				Set @TotalUnWeighted = @TotalUnWeighted + ((@WinAMount + @PlaceAmount + @ShowAmount))
			
				SET @TotalWinWin = @TotalWinWin + @WinAmount
				SET @TotalWinPlace = @TotalWinPlace + @PlaceAmount
				SET @TotalWinShow = @TotalWinShow + @ShowAmount

				IF @WinAmount > 0
				BEGIN 
					SET @NumWinWin = @NumWinWin + 1
				END
				
				IF @PlaceAmount > 0
				BEGIN 
					SET @NumWinPlace = @NumWinPlace + 1
				END

				IF @ShowAmount > 0
				BEGIN 
					SET @NumWinShow = @NumWinShow + 1
				END
				
				SET @FavoriteId = (SELECT RaceDetailId from RaceDetails WHERE LeagueRaceId = @leagueRaceId and OddsOrder = 1)
				
				If @FavoriteId = @RaceDetailId
				BEGIN
					SET @NumWinFavorites = @NumWinFavorites + 1
				END
			END
			ELSE IF @BetType = 2 --Place
			BEGIN
				Set @Total = @Total + ((@PlaceAmount + @ShowAmount) * @Weight)
				Set @TotalUnWeighted = @TotalUnWeighted + ((@PlaceAmount + @ShowAmount))
			
				SET @TotalPlacePlace = @TotalPlacePlace + @PlaceAmount
				SET @TotalPlaceShow = @TotalPlaceShow + @ShowAmount

				IF @PlaceAmount > 0
				BEGIN 
					SET @NumPlacePlace = @NumPlacePlace + 1
				END

				IF @ShowAmount > 0
				BEGIN 
					SET @NumPlaceShow = @NumPlaceShow + 1
				END
				
			END
			ELSE IF @BetType = 3 --Show 
			BEGIN
				Set @Total = @Total + (@ShowAmount * @Weight)
				Set @TotalUnWeighted = @TotalUnWeighted + (@ShowAmount)
			
				SET @TotalShowShow = @TotalShowShow + @ShowAmount

				IF @ShowAmount > 0
				BEGIN 
					SET @NumShowShow = @NumShowShow + 1
				END
			END

			FETCH NEXT FROM curRaces INTO @raceDetailId, @betType, @leagueRaceId, @WinAmount, @PlaceAmount, @ShowAmount
		END 

		CLOSE curRaces  
		DEALLOCATE curRaces

PRINT 'NumWinWIn: ' + CAST(@NumWinWin as varchar(100))
PRINT 'Total Unweighted: ' + CAST(@TotalUnWeighted as varchar(100))

		SET @ROI = (@TotalUnWeighted - (12 * @TotalRaces)) / (12 * @TotalRaces)

		INSERT dbo.UserStandings
		(
			LeagueId,
			UserLeagueId,
			Yr,
			Total,
			UpdateDate,
			WinWinPct,
			WinPlacePct,
			WinShowPct,
			PlacePlacePct,
			PlaceShowPct,
			ShowShowPct,
			WinWinAvg,
			WinPlaceAvg,
			WinShowAvg,
			PlacePlaceAvg,
			PlaceShowAvg,
			ShowShowAvg,
			WinFavPct,
			ROI
		)
		VALUES
		(
			@LeagueId,
			@curUser,
			GetDate(),
			@Total,
			GetDate(),
			@NumWinWin / @TotalRaces,
			@NumWinPlace / @TotalRaces,
			@NumWinShow / @TotalRaces,
			@NumPlacePlace / @TotalRaces,
			@NumPlaceShow / @TotalRaces,
			@NumShowShow / @TotalRaces,
			dbo.CalcAverage(@TotalWinWin, @NumWinWin),
			dbo.CalcAverage(@TotalWinPlace, @NumWinPlace),
			dbo.CalcAverage(@TotalWinShow, @NumWinShow),
			dbo.CalcAverage(@TotalPlacePlace, @NumPlacePlace),
			dbo.CalcAverage(@TotalPlaceShow, @NumPlaceShow),
			dbo.CalcAverage(@TotalShowShow, @NumShowShow),
			@NumWinFavorites / @TotalRaces,
			@ROI
		)

       		FETCH NEXT FROM curUsers INTO @curUser  
	END  

	CLOSE curUsers  
	DEALLOCATE curUsers
	--NOW LETS CALCULATE ALL THE REPORT TOTALS PER RACE
	EXEC ResetUserPickCountReport
	
GO

GRANT EXECUTE ON [dbo].[CalculateUserTotals] TO hrleague_user

USE [HRLeague]
GO


/****** Object:  StoredProcedure [hrleague_user].[GetCountOfUserPicksByBetType]    Script Date: 02/19/2011 09:29:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[hrleague_user].[GetCountOfUserPicksByBetType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [hrleague_user].[GetCountOfUserPicksByBetType]
GO

USE [HRLeague]
GO

/****** Object:  StoredProcedure [hrleague_user].[GetCountOfUserPicksByBetType]    Script Date: 02/19/2011 09:29:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetCountOfUserPicksByBetType] 
	@BetType int
AS
	SELECT rd.LeagueRaceId, urd.RaceDetailId, @BetType, Count(urd.RaceDetailId)
	FROM dbo.UserRaceDetail urd
		INNER JOIN RaceDetails rd ON urd.RaceDetailId = rd.RaceDetailId
	WHERE BetType = @BetType
	GROUP BY rd.LeagueRaceId, urd.RaceDetailId

	UNION

	SELECT rd.LeagueRaceId, RaceDetailId, @BetType, 0
	FROM RaceDetails rd
	WHERE RaceDetailId NOT  IN 
		(SELECT DISTINCT urd.RaceDetailId from UserRaceDetail urd WHERE bettype = @BetType)


GO

GRANT EXECUTE ON [dbo].[GetCountOfUserPicksByBetType] TO [hrleague_user] 

USE [HRLeague]
GO

/****** Object:  StoredProcedure [hrleague_user].[ResetUserPickCountReport]    Script Date: 02/19/2011 09:30:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[hrleague_user].[ResetUserPickCountReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [hrleague_user].[ResetUserPickCountReport]
GO

USE [HRLeague]
GO

/****** Object:  StoredProcedure [hrleague_user].[ResetUserPickCountReport]    Script Date: 02/19/2011 09:30:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ResetUserPickCountReport] 
AS
	DELETE FROM dbo.ReportLeagueRaceBet

	INSERT ReportLeagueRaceBet
		EXEC GetCountOfUserPicksByBetType 1

	INSERT ReportLeagueRaceBet
		EXEC GetCountOfUserPicksByBetType 2

	INSERT ReportLeagueRaceBet
		EXEC GetCountOfUserPicksByBetType 3


GO

GRANT EXECUTE ON [dbo].[ResetUserPickCountReport] to hrleague_user

