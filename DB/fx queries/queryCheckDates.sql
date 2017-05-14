DELIMITER //
DROP FUNCTION IF EXISTS getLastDateInDatesTable //
CREATE FUNCTION getLastDateInDatesTable(timeframeId INT) RETURNS DATETIME
	BEGIN
    
		DECLARE result DATETIME;
        SELECT MAX(Date) INTO result
        FROM dates d
        WHERE d.TimeframeId = timeframeId;
        
        RETURN result;
    END //


DROP FUNCTION IF EXISTS getLastIndexInDatesTable //
CREATE FUNCTION getLastIndexInDatesTable(timeframeId INT) RETURNS INT
	BEGIN
    
		DECLARE result INT;
        SELECT MAX(indexNumber) INTO result
        FROM dates d
        WHERE d.TimeframeId = timeframeId;
        
        RETURN result;
    END //    


DROP FUNCTION IF EXISTS getPeriodUnitForTimeframe //
CREATE FUNCTION getPeriodUnitForTimeframe(timeframeId INT) RETURNS NVARCHAR(45)
	BEGIN
		DECLARE result NVARCHAR(45);
        SELECT PeriodUnit INTO result
        FROM timeframes tf
        WHERE tf.Id = timeframeId;
        
        RETURN result;
    END //


DROP FUNCTION IF EXISTS getPeriodMultiplierForTimeframe //
CREATE FUNCTION getPeriodMultiplierForTimeframe(timeframeId INT) RETURNS INT
	BEGIN
		DECLARE result INT;
        
        SELECT PeriodCounter INTO result
        FROM timeframes tf
        WHERE tf.Id = timeframeId;
        
        RETURN result;
    END //


DROP PROCEDURE IF EXISTS createIdsTable //
CREATE PROCEDURE createIdsTable(counter INT)
	BEGIN
		DROP TEMPORARY TABLE IF EXISTS tIds;
		CREATE TEMPORARY TABLE tIds (Id INT PRIMARY KEY AUTO_INCREMENT, Fake INT(1) NULL);
		INSERT INTO tIds(Fake) SELECT 1 FROM quotations LIMIT counter;
    END //


DROP PROCEDURE IF EXISTS createDatesTable //
CREATE PROCEDURE createDatesTable()
	BEGIN
        DROP TEMPORARY TABLE IF EXISTS tDates;
        CREATE TEMPORARY TABLE tDates (Id INT PRIMARY KEY AUTO_INCREMENT, Date DATETIME, TimeframeId INT, IndexNumber INT(11) NULL);
    END //


DROP PROCEDURE IF EXISTS populateDatesTable //
CREATE PROCEDURE populateDatesTable(timeframe INT, initialDate DATETIME, periodMultiplier INT, periodUnit NVARCHAR(45))
	BEGIN	
    
		IF periodUnit = 'MINUTE' THEN
			INSERT INTO tDates(Date, TimeframeId) SELECT DATE_ADD(initialDate, INTERVAL Id * periodMultiplier MINUTE), timeframe FROM tIds;
		ELSEIF periodUnit = 'HOUR' THEN
            INSERT INTO tDates(Date, TimeframeId) SELECT DATE_ADD(initialDate, INTERVAL Id * periodMultiplier HOUR), timeframe FROM tIds;
        ELSEIF periodUnit = 'DAY' THEN
			INSERT INTO tDates(Date, TimeframeId) SELECT DATE_ADD(initialDate, INTERVAL Id * periodMultiplier DAY), timeframe FROM tIds;
		ELSEIF periodUnit = 'WEEK' THEN
			INSERT INTO tDates(Date, TimeframeId) SELECT DATE_ADD(initialDate, INTERVAL Id * periodMultiplier * 7 DAY), timeframe FROM tIds;
        ELSEIF periodUnit = 'MONTH' THEN
			INSERT INTO tDates(Date, TimeframeId) SELECT DATE_ADD(initialDate, INTERVAL Id * periodMultiplier MONTH), timeframe FROM tIds;
        END IF;
        
    END //


DROP PROCEDURE IF EXISTS deleteWeekendDates //
CREATE PROCEDURE deleteWeekendDates()
	BEGIN
        DELETE FROM tDates
        WHERE 
			TimeframeId < 7
			AND (weekday(Date) >= 5
            OR (DAY(Date) = 1 AND MONTH(Date) = 1 AND TimeframeId < 7)
            OR (DAY(Date) = 25 AND MONTH(Date) = 12 AND TimeframeId < 7)
            OR (DAY(Date) = 24 AND MONTH(Date) = 12 AND HOUR(Date) >= 21)
            OR (DAY(Date) = 31 AND MONTH(Date) = 12 AND HOUR(Date) >= 21));
    END //     


DROP PROCEDURE IF EXISTS appendDatesIndex //
CREATE PROCEDURE appendDatesIndex()
	BEGIN
		SET @rank=0;
        
        DROP TEMPORARY TABLE IF EXISTS tempTable_AppendingDatesIndex;
        CREATE TEMPORARY TABLE tempTable_AppendingDatesIndex 
			SELECT 
				@rank := @rank + 1 AS Rank,
				d.Id AS Id
			FROM
				tDates d
			ORDER BY date;


		UPDATE tDates d
		JOIN tempTable_AppendingDatesIndex b
		ON d.Id = b.Id
		SET d.indexNumber = b.rank;

		DROP TEMPORARY TABLE IF EXISTS tempTable_AppendingDatesIndex;

    END //


DROP PROCEDURE IF EXISTS moveDatesToDestinationTable //
CREATE PROCEDURE moveDatesToDestinationTable(lastIndex INT)
	BEGIN
		
        INSERT INTO dates(Date, TimeframeId, IndexNumber)
		SELECT 
			Date, timeframeId AS TimeframeId, lastIndex + IndexNumber AS IndexNumber 
        FROM tDates;
        
    END //


DROP PROCEDURE IF EXISTS feedCalendarTableForSingleTimeframe //
CREATE PROCEDURE feedCalendarTableForSingleTimeframe(timeframeId INT)
	BEGIN
		DECLARE initialDate DATETIME;
        DECLARE lastIndex INT;
		DECLARE periodUnit NVARCHAR(45);
		DECLARE periodMultiplier INT;
		DECLARE difference INT;
		
		SET initialDate = getLastDateInDatesTable(timeframeId);
        SET lastIndex = getLastIndexInDatesTable(timeframeId);
		SET periodUnit = getPeriodUnitForTimeframe(timeframeId);
		SET periodMultiplier = getPeriodMultiplierForTimeframe(timeframeId);    
		SET difference = getDateDifferenceInTimeframes(initialDate, NOW(), periodUnit, periodMultiplier);
        
        #SELECT initialDate, lastIndex, periodUnit, periodMultiplier, difference;
        
        CALL createIdsTable(difference);
        CALL createDatesTable();
        CALL populateDatesTable(timeframeId, initialDate, periodMultiplier, periodUnit);
        CALL deleteWeekendDates();
        CALL appendDatesIndex();
		CALL moveDatesToDestinationTable(lastIndex);
        
		DROP TEMPORARY TABLE IF EXISTS tIds;
        DROP TEMPORARY TABLE IF EXISTS tDates;
		
	END //


DROP PROCEDURE IF EXISTS feedCalendarTable //
DROP PROCEDURE IF EXISTS _feedCalendarTable //
CREATE PROCEDURE _feedCalendarTable()
	BEGIN
		DECLARE timeframeId INT;
		DECLARE done INT DEFAULT FALSE;
		DECLARE cursor_i CURSOR FOR SELECT Id FROM timeframes ORDER BY Id ASC;
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
		OPEN cursor_i;    
		update_loop: LOOP
			FETCH cursor_i INTO timeframeId;
			IF done THEN
				LEAVE update_loop;
			END IF;
            CALL feedCalendarTableForSingleTimeframe(timeframeId);
		END LOOP;
	END //
    
call _feedCalendarTable();