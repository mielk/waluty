DELIMITER //

DROP PROCEDURE IF EXISTS _recreateDb //
DROP PROCEDURE IF EXISTS recreateTables //
DROP PROCEDURE IF EXISTS copyTable //
DROP PROCEDURE IF EXISTS recreateViews //
DROP PROCEDURE IF EXISTS recreateProcedures //

CREATE PROCEDURE _recreateDb()
	BEGIN
		CALL recreateTables();
	END //

CREATE PROCEDURE recreateTables()
	BEGIN
		DECLARE tableName NVARCHAR(255);
        DECLARE done INT DEFAULT FALSE;
		DECLARE tables_cursor CURSOR FOR SELECT table_name FROM information_schema.tables WHERE table_schema = 'fx_clone' AND table_type <> 'VIEW';
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
		OPEN tables_cursor;    
		update_loop: LOOP
			FETCH tables_cursor INTO tableName;
			IF done THEN
				LEAVE update_loop;
			END IF;
            #CALL feedCalendarTableForSingleTimeframe(timeframeId);
            SELECT tableName;
		END LOOP;
	END //

CREATE PROCEDURE copyTable(tableName NVARCHAR(255))
	BEGIN
    
    END //
    
CALL _recreateDb();