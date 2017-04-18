DELIMITER //

DROP PROCEDURE IF EXISTS createTemporaryTablesForAddingMissingQuotations //
CREATE PROCEDURE createTemporaryTablesForAddingMissingQuotations(_assetId INT)
	BEGIN
    
		DECLARE _lastUpdateDate DATETIME;
        
        #SET _lastUpdateDate = getLastUpdateDate(0, 0, 1);
        SET _lastUpdateDate = getLastUpdateDate(0, 0, 0);
    
    
		# Basic missing quotations table (with dates after last quotation).
		DROP TEMPORARY TABLE IF EXISTS tempTable_AddMissingQuotations;
        CREATE TEMPORARY TABLE tempTable_AddMissingQuotations 
				(INDEX tempIndex_Date (Date ASC), 
				INDEX tempIndex_Timeframe (TimeframeId ASC))
			SELECT
				d.timeframeId, d.Date
			FROM
				(SELECT * FROM quotations WHERE assetId = _assetId AND PriceDate >= _lastUpdateDate) q
				RIGHT JOIN 
				(SELECT * FROM dates WHERE Date >= _lastUpdateDate) d
				ON q.PriceDate = d.Date AND q.timeframeId = d.timeframeId
			WHERE
				q.PriceDate IS NULL;
		
        
        # Table containing last update dates for given asset and various timeframes.
		DROP TEMPORARY TABLE IF EXISTS tempTable_MaxQuotationDateForAssetByTimeframes;
        CREATE TEMPORARY TABLE tempTable_MaxQuotationDateForAssetByTimeframes 
			SELECT 
				timeframeId, MAX(PriceDate) AS maxDate, MIN(PriceDate) AS minDate
			FROM
				quotations
			WHERE
				assetId = 1
			GROUP BY timeframeId;
        
        # Remove out of date range records.
        CALL removeOutOfDateRangeRecords();
        
    END //

DROP PROCEDURE IF EXISTS removeOutOfDateRangeRecords //
CREATE PROCEDURE removeOutOfDateRangeRecords()
	BEGIN

		DELETE tempTable_AddMissingQuotations
		FROM 
			tempTable_AddMissingQuotations 
			INNER JOIN tempTable_MaxQuotationDateForAssetByTimeframes 
            USING (timeframeId)
		WHERE
			Date > MaxDate OR
			Date < MinDate;    
    
    END //


DROP PROCEDURE IF EXISTS insertRecordsForMissingDates //
CREATE PROCEDURE insertRecordsForMissingDates(_assetId INT)
	BEGIN
        DECLARE _timeframeId INT;
        DECLARE _date DATETIME;
		DECLARE done INT DEFAULT FALSE;
		DECLARE cursor_i CURSOR FOR SELECT DISTINCT TimeframeId, Date FROM tempTable_AddMissingQuotations ORDER BY Date ASC;
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
        
		OPEN cursor_i;
		add_loop: LOOP
			FETCH cursor_i INTO _timeframeId, _date;
			IF done THEN
				LEAVE add_loop;
			END IF;
            
            INSERT INTO quotations (PriceDate, AssetId, TimeframeId, OpenPrice, HighPrice, LowPrice, ClosePrice, RealClosePrice, Volume)
			SELECT
				_date AS PriceDate,
				q.AssetId,
				q.TimeFrameId,
				q.ClosePrice,
				q.ClosePrice,
				q.ClosePrice,
				q.ClosePrice,
				NULL,
				-1
			FROM 
				quotations q
			WHERE
				assetId = _assetId 
				AND timeframeId = _timeframeId 
				AND PriceDate = (SELECT 
									Date
								FROM 
									dates
								WHERE	
									timeframeId = _timeframeId AND 
									IndexNumber = (SELECT IndexNumber - 1 FROM dates WHERE timeframeId = _timeframeId AND Date = _date));

		END LOOP;

    END //


DROP PROCEDURE IF EXISTS removeTemporaryTablesForAddingMissingQuotations //
CREATE PROCEDURE removeTemporaryTablesForAddingMissingQuotations()
	BEGIN
		#DROP TEMPORARY TABLE IF EXISTS tempTable_AddMissingQuotations;
		DROP TEMPORARY TABLE IF EXISTS tempTable_MaxQuotationDateForAssetByTimeframes;
        DROP TEMPORARY TABLE IF EXISTS tempTable_RecordsToInsert;
    END //


DROP PROCEDURE IF EXISTS addMissingQuotationsForSingleAsset //
CREATE PROCEDURE addMissingQuotationsForSingleAsset(_assetId INT)
	BEGIN
		CALL createTemporaryTablesForAddingMissingQuotations(_assetId);
        CALL insertRecordsForMissingDates(_assetId);        
        CALL removeTemporaryTablesForAddingMissingQuotations();
    END //


DROP PROCEDURE IF EXISTS _addMissingQuotations //
CREATE PROCEDURE _addMissingQuotations()
	BEGIN
        DECLARE _assetId INT;
		DECLARE done INT DEFAULT FALSE;
		DECLARE cursor_i CURSOR FOR SELECT DISTINCT AssetId FROM v_all_symbols ORDER BY AssetId ASC;
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
        
		OPEN cursor_i;
		add_loop: LOOP
			FETCH cursor_i INTO _assetId;
			IF done THEN
				LEAVE add_loop;
			END IF;
            CALL addMissingQuotationsForSingleAsset(_assetId);
		END LOOP;
        
        SELECT 'Done' AS status, COUNT(*) AS QuotationsAfter FROM quotations;
        
	END //

CALL _addMissingQuotations();