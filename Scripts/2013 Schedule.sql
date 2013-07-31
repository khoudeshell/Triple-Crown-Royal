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
DELETE FROM UserLeagueRaceExoticPayout

UPDATE UserLeague SET HasPaid = 0


IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
	END
ELSE
	BEGIN
		COMMIT TRANSACTION
	END

DECLARE @RaceId INT

INSERT RACE SELECT 'Fasig-Tipton Fountain of Youth', LTRIM(RTRIM('GP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '02/23/2013', 1, 1)
INSERT RACE SELECT 'Risen Star', LTRIM(RTRIM('FG')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '02/23/2013', 1, 1)
INSERT RACE SELECT 'Gotham', LTRIM(RTRIM('AQU')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/02/2013', 1, 1)
INSERT RACE SELECT 'Tampa Bay Derby', LTRIM(RTRIM('TAM')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/09/2013', 1, 1)
INSERT RACE SELECT 'San Felipe', LTRIM(RTRIM('SA')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/09/2013', 1, 1)
INSERT RACE SELECT 'Rebel', LTRIM(RTRIM('OP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/16/2013', 1, 1)
INSERT RACE SELECT 'Spiral', LTRIM(RTRIM('TP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/23/2013', 1, 1)
INSERT RACE SELECT 'Sunland Derby', LTRIM(RTRIM('SUN')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/24/2013', 1, 1)
INSERT RACE SELECT 'Florida Derby', LTRIM(RTRIM('GP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/30/2013', 2, 1)
INSERT RACE SELECT 'Louisiana Derby', LTRIM(RTRIM('FG')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/30/2013', 1, 1)
INSERT RACE SELECT 'UAE Derby', LTRIM(RTRIM('MEY')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '03/30/2013', 1, 1)
INSERT RACE SELECT 'Wood Memorial', LTRIM(RTRIM('AQU')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '04/06/2013', 2, 1)
INSERT RACE SELECT 'Santa Anita Derby', LTRIM(RTRIM('SA')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '04/06/2013', 2, 1)
INSERT RACE SELECT 'Arkansas Derby', LTRIM(RTRIM('OP')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '04/13/2013', 2, 1)
INSERT RACE SELECT 'Blue Grass', LTRIM(RTRIM('KEE')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '04/13/2013', 2, 1)
INSERT RACE SELECT 'Lexington', LTRIM(RTRIM('KEE')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '04/20/2013', 1, 1)
INSERT RACE SELECT 'Derby Trial', LTRIM(RTRIM('CD')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '04/27/2013', 1, 1)
INSERT RACE SELECT 'Kentucky Derby', LTRIM(RTRIM('CD')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '05/04/2013 ', 4, 1)
INSERT RACE SELECT 'Preakness', LTRIM(RTRIM('PIM')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '05/18/2013 ', 3, 1)
INSERT RACE SELECT 'Belmont Stakes', LTRIM(RTRIM('BEL')) SET @RaceId = @@Identity INSERT LeagueRace (RaceId, Dt, Weight, LeagueId) VALUES(@RaceId, '06/09/2013', 3, 1)
