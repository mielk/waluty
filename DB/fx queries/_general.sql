DELIMITER //
DROP PROCEDURE IF EXISTS _checkQuotationsConsistency //
CREATE PROCEDURE _checkQuotationsConsistency()
	BEGIN

        DECLARE EXIT HANDLER FOR SQLEXCEPTION
        BEGIN
			ROLLBACK;
        END;

        DECLARE EXIT HANDLER FOR SQLWARNING
        BEGIN
			ROLLBACK;
        END;        

		START TRANSACTION;
			CALL _feedCalendarTable();
			CALL _deleteRedundantQuotations();
            CALL _stampQuotationsLastUpdate();
        COMMIT;
        
        SELECT 'Checking quotations consistency: Done' AS ProcessStatus;

    END //


DROP FUNCTION IF EXISTS _getLatestQuotationDate //
DROP FUNCTION IF EXISTS getLatestQuotationDate //
CREATE FUNCTION getLatestQuotationDate() RETURNS DATETIME
	BEGIN
		DECLARE latestQuotationDate DATETIME;
        SELECT MAX(PriceDate) INTO latestQuotationDate FROM quotations;
        RETURN latestQuotationDate;
    END //


DROP PROCEDURE IF EXISTS _stampQuotationsLastUpdate //
CREATE PROCEDURE _stampQuotationsLastUpdate()
	BEGIN
		DECLARE recordExists INT;
        DECLARE latestQuotationDate DATETIME;
        
        SELECT EXISTS(SELECT * FROM last_updates WHERE assetId = 0 AND timeframeId = 0 AND analysisTypeId = 1) INTO recordExists;
        SET latestQuotationDate = getLatestQuotationDate();
        
        IF recordExists = 1 THEN
			UPDATE last_updates SET LastDate = latestQuotationDate WHERE assetId = 0 AND timeframeId = 0 AND analysisTypeId = 1;
        ELSEIF recordExists = 0 THEN
			INSERT INTO last_updates(AssetId, TimeframeId, AnalysisTypeId, LastUpdate) VALUES(0, 0, 1, latestQuotationDate);
        END IF;
        
    END //


CALL _checkQuotationsConsistency();