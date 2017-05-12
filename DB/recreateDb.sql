DELIMITER //

DROP PROCEDURE IF EXISTS _recreateDb //
DROP PROCEDURE IF EXISTS recreateDb //
DROP PROCEDURE IF EXISTS recreateSchema //
DROP PROCEDURE IF EXISTS recreateTables //
DROP PROCEDURE IF EXISTS createTable //
DROP PROCEDURE IF EXISTS recreateViews //
DROP PROCEDURE IF EXISTS feedData //
DROP PROCEDURE IF EXISTS recreateProcedures //

CREATE PROCEDURE recreateDb()
	BEGIN
    
		BEGIN
			DECLARE EXIT HANDLER FOR SQLEXCEPTION
			ROLLBACK;
		END;    
    
		START TRANSACTION;
		CALL recreateSchema();
		CALL recreateTables();
        CALL recreateViews();
        CALL feedData();
        COMMIT;
        
	END //

CREATE PROCEDURE recreateSchema()
	BEGIN
		DROP DATABASE IF EXISTS  fx_unittests;
        CREATE DATABASE fx_unittests /*!40100 DEFAULT CHARACTER SET utf8 */;
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
            CALL createTable(tableName);
		END LOOP;
	END //

CREATE PROCEDURE recreateViews()
	BEGIN
		DECLARE sqlQuery NVARCHAR(5000);
        DECLARE done INT DEFAULT FALSE;
		DECLARE tables_cursor CURSOR FOR SELECT CONCAT('CREATE VIEW fx_unittests.', TABLE_NAME, ' AS ', VIEW_DEFINITION) AS sql_query FROM information_schema.views WHERE table_schema = 'fx_clone';
		DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
		OPEN tables_cursor;    
		update_loop: LOOP
			FETCH tables_cursor INTO sqlQuery;            
			IF done THEN
				LEAVE update_loop;
			END IF;
			SET @sql = sqlQuery;
			PREPARE stmt FROM @sql;
			EXECUTE stmt;
			DEALLOCATE PREPARE stmt;
		END LOOP;
	END //

CREATE PROCEDURE createTable(tableName NVARCHAR(255))
	BEGIN
		SET @sql =  CONCAT("CREATE TABLE fx_unittests.", tableName, " LIKE fx_clone.", tableName);
		PREPARE stmt FROM @sql;
		EXECUTE stmt;
		DEALLOCATE PREPARE stmt;
        select "createTable:done";
    END //
    
CREATE PROCEDURE feedData()
	BEGIN
		INSERT INTO fx_unittests.dates SELECT * FROM fx_clone.dates;
    END //
    
CALL recreateDb();