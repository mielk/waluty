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
				SELECT
					d.Date, d.timeframeId, d.IndexNumber
				FROM
					(SELECT * FROM quotations WHERE PriceDate >= _lastUpdateDate AND assetId = _assetId) q
					RIGHT JOIN 
					(SELECT * FROM dates WHERE Date >= _lastUpdateDate) d
					ON q.PriceDate = d.Date AND q.timeframeId = d.timeframeId
				WHERE
					q.PriceDate IS NULL;
		
        # Table containing last update dates for given assed and various timeframes.
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
		
    END //


DROP PROCEDURE IF EXISTS addMissingQuotationsForSingleAsset //
CREATE PROCEDURE addMissingQuotationsForSingleAsset(_assetId INT)
	BEGIN
		CALL createTemporaryTablesForAddingMissingQuotations(_assetId);
        CALL insertRecordsForMissingDates(_assetId);        
    END //


DROP PROCEDURE IF EXISTS _addMissingQuotations //
CREATE PROCEDURE _addMissingQuotations()
	BEGIN
        DECLARE _assetId INT;
		DECLARE done INT DEFAULT FALSE;
		DECLARE cursor_i CURSOR FOR SELECT DISTINCT AssetId FROM v_all_symbols ORDER BY AssetId ASC;
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
		SELECT 'Quotations before: ', COUNT(*) FROM quotations;
        
		OPEN cursor_i;
		add_loop: LOOP
			FETCH cursor_i INTO _assetId;
			IF done THEN
				LEAVE add_loop;
			END IF;
            #CALL addMissingQuotationsForSingleAsset(_assetId);
		END LOOP;
        
        CALL addMissingQuotationsForSingleAsset(1);
        #CALL deleteRedundantQuotationsForSingleSymbol(1, 2);
        
        SELECT 'Quotations after: ', COUNT(*) FROM quotations;
        
	END //

CALL _addMissingQuotations();