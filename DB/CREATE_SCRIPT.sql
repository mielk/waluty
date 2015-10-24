DROP DATABASE FX;
CREATE DATABASE FX;
USE FX;


CREATE TABLE Currencies(
	Id INT(10) UNSIGNED PRIMARY KEY,
    CurrencyName VARCHAR(3) NOT NULL UNIQUE,
    CurrencyFullName VARCHAR(255) NULL
);
INSERT INTO Currencies(Id, CurrencyName) 
VALUES
	(1, 'USD'), 
    (2, 'EUR'), 
    (3, 'JPY'), 
    (4, 'CHF'), 
    (5, 'AUD'), 
    (6, 'NZD'), 
    (7, 'GBP'), 
    (8, 'CAD');


CREATE TABLE Pairs(
	Id INT(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    PairName VARCHAR(6) NOT NULL UNIQUE,
    BaseCurrency INT(3) NOT NULL,
    QuoteCurrency INT(3) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
INSERT INTO Pairs(PairName, BaseCurrency, QuoteCurrency) 
VALUES
	('EURUSD', 2, 1), 
    ('AUDUSD', 5, 1), 
    ('NZDUSD', 6, 1), 
    ('GBPUSD', 7, 1), 
    ('USDJPY', 1, 3), 
    ('EURCHF', 2, 4), 
    ('EURJPY', 2, 3), 
    ('GBPEUR', 7, 2), 
    ('AUDJPY', 5, 3), 
    ('NZDJPY', 6, 3), 
    ('USDCAD', 1, 8);

CREATE TABLE Timeframes(
	Id INT(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    Symbol VARCHAR(6) NOT NULL UNIQUE,
    Period DOUBLE NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
INSERT INTO Timeframes(Symbol, Period) 
VALUES
	('M5', 5), 
    ('M15', 15), 
    ('M30', 30), 
    ('H1', 60), 
    ('H4', 240), 
    ('D1', 0),
    ('W1', 0),
    ('MN1', 0);

CREATE TABLE last_updates(
	Id INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Symbol VARCHAR(255) NOT NULL,
    Last_Quotation_Update DATETIME NULL,
    Last_Quotation_Verification DATETIME NULL,
    Last_Price_Update DATETIME NULL,    
    Last_Macd_Update DATETIME NULL,
    Last_Adx_Update DATETIME NULL
);
INSERT INTO last_updates (Symbol)
SELECT 
	CONCAT(pairs.PairName, '_', timeframes.Symbol) AS Symbol 
FROM 
	pairs, timeframes;


CREATE TABLE quotations_template (
  Id INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  PriceDate DATETIME NOT NULL,
  AssetId INT(10) UNSIGNED NOT NULL,
  OpenPrice DOUBLE NOT NULL,
  HighPrice DOUBLE NOT NULL,
  LowPrice DOUBLE NOT NULL,
  ClosePrice DOUBLE NOT NULL,
  RealClosePrice DOUBLE DEFAULT NULL,
  Volume DOUBLE DEFAULT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY UniqueQuot (PriceDate, AssetId) USING BTREE,
  KEY FK_quotD_company (AssetId),
  CONSTRAINT FK_quot_pairs FOREIGN KEY (AssetId) REFERENCES Pairs(id)
) ENGINE=InnoDB AUTO_INCREMENT=6246195 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;


CREATE TABLE tm_prices_template (
  Id INT(10) UNSIGNED NOT NULL,
  AssetId INT(10) UNSIGNED NOT NULL,
  PriceDate DATETIME NOT NULL,
  DeltaClosePrice DOUBLE DEFAULT NULL,
  PriceDirection3D INT(10) DEFAULT NULL,
  PriceDirection2D INT(10) DEFAULT NULL,
  IsPeakByClosePrice TINYINT(1) DEFAULT NULL,
  LastPeakByClosePrice DOUBLE DEFAULT NULL,
  PeakSlopeByClosePrice DOUBLE DEFAULT NULL,
  IsTroughByClosePrice TINYINT(1) DEFAULT NULL,
  LastTroughByClosePrice DOUBLE DEFAULT NULL,
  TroughSlopeByClosePrice DOUBLE DEFAULT NULL,
  TotalDeltas INT(10) DEFAULT NULL,
  MinusDeltas INT(10) DEFAULT NULL,
  PlusDeltas INT(10) DEFAULT NULL,
  IsPeakByHighPrice TINYINT(1) DEFAULT NULL,
  LastPeakByHighPrice DOUBLE DEFAULT NULL,
  PeakSlopeByHighPrice DOUBLE DEFAULT NULL,
  IsTroughByLowPrice TINYINT(1) DEFAULT NULL,
  LastTroughByLowPrice DOUBLE DEFAULT NULL,
  TroughSlopeByLowPrice DOUBLE DEFAULT NULL,
  UNIQUE KEY PairId (AssetId, PriceDate),
  KEY PK (AssetId, PriceDate),
  CONSTRAINT FK_price_pair FOREIGN KEY (AssetId) REFERENCES Pairs(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;


CREATE TABLE tm_macd_template (
  Id INT(11) UNSIGNED NOT NULL,
  AssetId INT(11) UNSIGNED NOT NULL,
  PriceDate DATETIME NOT NULL,
  MA13 FLOAT DEFAULT NULL,
  EMA13 FLOAT DEFAULT NULL,
  MA26 FLOAT DEFAULT NULL,
  EMA26 FLOAT DEFAULT NULL,
  MACDLine FLOAT DEFAULT NULL,
  SignalLine FLOAT DEFAULT NULL,
  Hist FLOAT DEFAULT NULL,
  histAvg FLOAT DEFAULT NULL,
  histExtremum FLOAT DEFAULT NULL,
  DeltaHist FLOAT DEFAULT NULL,
  DeltaHistPositive INT(11) DEFAULT NULL,
  DeltaHistNegative INT(11) DEFAULT NULL,
  DeltaHistZero INT(11) DEFAULT NULL,
  HistDirection2D INT(1) DEFAULT NULL,
  HistDirection3D INT(1) DEFAULT NULL,
  HistDirectionChanged INT(1) DEFAULT NULL,
  HistToOX INT(1) DEFAULT NULL,
  HistRow INT(11) DEFAULT NULL,
  OXCrossing FLOAT DEFAULT NULL,
  DivergenceByAvg INT(11) DEFAULT NULL,
  MACDPeak INT(11) DEFAULT NULL,
  LastMACDPeak FLOAT DEFAULT NULL,
  MACDPeakSlope FLOAT DEFAULT NULL,
  MACDTrough INT(11) DEFAULT NULL,
  LastMACDTrough FLOAT DEFAULT NULL,
  MACDTroughSlope FLOAT DEFAULT NULL,
  Divergence INT(11) UNSIGNED DEFAULT NULL,
  PRIMARY KEY (AssetId, PriceDate) USING BTREE,
  UNIQUE KEY uc_CompanyPrice (AssetId, PriceDate),
  CONSTRAINT FK_macd_pair FOREIGN KEY (AssetId) REFERENCES Pairs(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;


CREATE TABLE tm_adx_template (
  Id INT(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  AssetId INT(11) UNSIGNED NOT NULL,
  PriceDate DATETIME NOT NULL,
  TR FLOAT DEFAULT NULL,
  DM1Pos FLOAT DEFAULT NULL,
  DM1Neg FLOAT DEFAULT NULL,
  TR14 FLOAT DEFAULT NULL,
  DM14Pos FLOAT DEFAULT NULL,
  DM14Neg FLOAT DEFAULT NULL,
  DI14Pos FLOAT DEFAULT NULL,
  DI14Neg FLOAT DEFAULT NULL,
  DI14Diff FLOAT DEFAULT NULL,
  DI14Sum FLOAT DEFAULT NULL,
  DX FLOAT DEFAULT NULL,
  ADX FLOAT DEFAULT NULL,
  DaysUnder20 INT(11) DEFAULT NULL,
  DaysUnder15 INT(11) DEFAULT NULL,
  Cross20 FLOAT DEFAULT NULL,
  DeltaDIPos FLOAT DEFAULT NULL,
  DeltaDINeg FLOAT DEFAULT NULL,
  DeltaADX FLOAT DEFAULT NULL,
  DIPosDirection3D INT(11) DEFAULT NULL,
  DIPosDirection2D INT(11) DEFAULT NULL,
  DINegDirection3D INT(11) DEFAULT NULL,
  DINegDirection2D INT(11) DEFAULT NULL,
  ADXDirection3D INT(11) DEFAULT NULL,
  ADXDirection2D INT(11) DEFAULT NULL,
  DIPosDirectionChanged INT(11) DEFAULT NULL,
  DINegDirectionChanged INT(11) DEFAULT NULL,
  ADXDirectionChanged INT(11) DEFAULT NULL,
  DIDifference FLOAT DEFAULT NULL,
  DILinesCrossing INT(11) DEFAULT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY uc_ADX_CompanyPrice (AssetId, PriceDate)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

DROP procedure IF exists test;
DELIMITER $$
CREATE DEFINER=root@localhost PROCEDURE test(IN tableName VARCHAR(255))
BEGIN
  DECLARE done INT DEFAULT FALSE;
  DECLARE insideDone INT DEFAULT FALSE;
  DECLARE a CHAR(15);
  DECLARE cur1 CURSOR FOR SELECT CONCAT(pairs.PairName, '_', timeframes.Symbol) AS Symbol FROM pairs, timeframes;
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

  OPEN cur1;


  read_loop: LOOP
    FETCH cur1 INTO a;
	IF done THEN
	  LEAVE read_loop;
	END IF;
	
	SET @c = CONCAT('CREATE TABLE ', 'quotations_', a, ' LIKE quotations_template;');
	PREPARE stmt from @c;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;    
        
  END LOOP read_loop;

  CLOSE cur1;
  
END$$
DELIMITER ;

CALL test('abc');



CREATE TABLE markets (
  id INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) COLLATE utf8_polish_ci NOT NULL,
  short VARCHAR(3) COLLATE utf8_polish_ci NOT NULL,
  startTime VARCHAR(8) COLLATE utf8_polish_ci DEFAULT NULL,
  endTime VARCHAR(8) COLLATE utf8_polish_ci DEFAULT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;


CREATE TABLE companies (
  id INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) COLLATE utf8_polish_ci NOT NULL,
  idMarket INT(10) UNSIGNED DEFAULT NULL,
  idBranch INT(10) UNSIGNED DEFAULT NULL,
  short VARCHAR(20) COLLATE utf8_polish_ci NOT NULL,
  lastPriceUpdate DATETIME NOT NULL DEFAULT '1900-01-01 00:00:00',
  lastCalculation DATETIME NOT NULL DEFAULT '1900-01-01 00:00:00',
  pricesChecked TINYINT(1) NOT NULL DEFAULT '0',
  lastTrendlinesReview DATETIME NOT NULL DEFAULT '1900-01-01 00:00:00',
  PRIMARY KEY (id),
  KEY FK_company_market (idMarket),
  CONSTRAINT FK_company_market FOREIGN KEY (idMarket) REFERENCES markets (id)
) ENGINE=InnoDB AUTO_INCREMENT=1555 DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;
