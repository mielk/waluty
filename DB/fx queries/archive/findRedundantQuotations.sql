DELIMITER //

DROP FUNCTION IF EXISTS getLastUpdateDate //
CREATE FUNCTION getLastUpdateDate(_assetId INT, _timeframeId INT, _analysisTypeId INT) RETURNS DATETIME
	BEGIN
    
		DECLARE result DATETIME;
        SELECT LastDate INTO result
        FROM last_updates lu
        WHERE lu.AssetId = _assetId AND lu.TimeframeId = _timeframeId AND lu.AnalysisTypeId = _analysisTypeId;
        
        IF result IS NULL THEN
			SET result = '2000-01-01';
        END IF;
        
        RETURN result;
    END //


DROP FUNCTION IF EXISTS getRedundantQuotationIdsAsString //
CREATE FUNCTION getRedundantQuotationIdsAsString () RETURNS NVARCHAR(1000)
	BEGIN
		
        DECLARE quotationId INT;
		DECLARE done INT DEFAULT FALSE;
        DECLARE ids NVARCHAR(1000) DEFAULT '';
		DECLARE cursor_i CURSOR FOR SELECT * FROM tempRedundantQuotations;
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
        
		OPEN cursor_i;
		getQuotations_loop: LOOP
			FETCH cursor_i INTO quotationId;
			IF done THEN
				LEAVE getQuotations_loop;
			END IF;
            
            IF char_length(ids) > 0 THEN
				SET ids = CONCAT(ids, ', ', quotationId);
            ELSEIF char_length(ids) = 0 THEN
				SET ids = quotationId;
            END IF;
            
		END LOOP;
		
        RETURN ids;
        
	END //


DROP PROCEDURE IF EXISTS createRedundantQuotationTemporaryTable //
CREATE PROCEDURE createRedundantQuotationTemporaryTable(_assetId INT, _timeframeId INT, _lastUpdateDate DATETIME)
	BEGIN
		DROP TEMPORARY TABLE IF EXISTS tempRedundantQuotations;
        CREATE TEMPORARY TABLE tempRedundantQuotations SELECT
									QuotationId
								FROM
									(SELECT * FROM quotations WHERE PriceDate > _lastUpdateDate AND AssetId = _assetId AND TimeframeId = _timeframeId) q
									LEFT JOIN 
									(SELECT * FROM dates WHERE Date > _lastUpdateDate AND TimeframeId = _timeframeId) d
									ON q.PriceDate = d.Date
								WHERE
									d.Date IS NULL;
    END //

    
DROP PROCEDURE IF EXISTS deleteRedundantQuotationsForSingleSymbol //
CREATE PROCEDURE deleteRedundantQuotationsForSingleSymbol(_assetId INT, _timeframeId INT)
	BEGIN
		DECLARE _analysisTypeId INT DEFAULT 1;
		DECLARE _lastUpdateDate DATETIME;
        DECLARE _ids NVARCHAR(1000);
        
        SET _lastUpdateDate = getLastUpdateDate(_assetId, _timeframeId, _analysisTypeId);
        CALL createRedundantQuotationTemporaryTable(_assetId, _timeframeId, _lastUpdateDate);
        SET _ids = getRedundantQuotationIdsAsString ();
        
        IF char_length(_ids) > 0 THEN
			SET @sql =  CONCAT("DELETE FROM quotations WHERE QuotationId IN (", _ids, ");");
            PREPARE stmt FROM @sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
            SELECT CONCAT('IDs: ', _ids, ' removed for assetId: ', _assetId, ' and timeframeId: ', _timeframeId);
        END IF;
        
    END //


DROP PROCEDURE IF EXISTS _deleteRedundantQuotations //
CREATE PROCEDURE _deleteRedundantQuotations()
	BEGIN
		DECLARE _timeframeId INT;
        DECLARE _assetId INT;
		DECLARE done INT DEFAULT FALSE;
		DECLARE cursor_i CURSOR FOR SELECT AssetId, TimeframeId FROM v_all_symbols ORDER BY AssetId ASC, TimeframeId ASC;
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
		SELECT 'Quotations before: ', COUNT(*) FROM quotations;
        
		OPEN cursor_i;
		delete_loop: LOOP
			FETCH cursor_i INTO _assetId, _timeframeId;
			IF done THEN
				LEAVE delete_loop;
			END IF;
            CALL deleteRedundantQuotationsForSingleSymbol(_assetId, _timeframeId);
		END LOOP;
        
        #CALL deleteRedundantQuotationsForSingleSymbol(6, 3);
        #CALL deleteRedundantQuotationsForSingleSymbol(1, 2);
        
        SELECT 'Quotations after: ', COUNT(*) FROM quotations;
        
	END //