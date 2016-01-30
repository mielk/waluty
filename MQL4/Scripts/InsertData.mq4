//+------------------------------------------------------------------+
//|                                                    MySQL-003.mq5 |
//|                                   Copyright 2014, Eugene Lugovoy |
//|                                              http://www.mql5.com |
//| Inserting data with multi-statement (DEMO)                       |
//+------------------------------------------------------------------+
#property copyright "Copyright 2014, Eugene Lugovoy."
#property link      "http://www.mql5.com"
#property version   "1.00"
#property strict

#include <hash.mqh>
#include "..\libraries\functions.mq4"
#include "..\libraries\mysql.mq4"

//+------------------------------------------------------------------+
//| Script program start function                                    |
//+------------------------------------------------------------------+
void OnStart()
{
   //Alert(10);
   quotationsFullUpdate();
}


void quotationsFullUpdate(){
   string sqlPairs = "SELECT * FROM v_symbols_last_updates;";
   string pairs[][2];
   
   fetchArray(sqlPairs, pairs);
   
   
   for (int i = 0; i < ArrayRange(pairs, 0); i++){
      string symbol = pairs[i, 0];
      string pair = StringSubstr(symbol, 0, 6);
      datetime lastUpdate = convertStringToDate(pairs[i, 1]);
      //datetime lastUpdate = convertStringToDate("2015-03-27 01:30:00");
      ENUM_TIMEFRAMES timeframe = stringToTimeframe(StringSubstr(symbol, 7, StringLen(symbol) - 7));

      Alert("[Begin] " + symbol + " | " + dateToTime(lastUpdate));
      int inserted = InsertSingleQuotationSet(pair, timeframe, lastUpdate);
      Alert("Done: " + symbol + " (" + inserted + " records)");
      
   }
   
   Alert("end");
   
}

int InsertSingleQuotationSet(string symbol, ENUM_TIMEFRAMES timeframe, datetime lastUpdate){
   
   MqlRates rates[];
   int copied = 0;
   datetime lastRate = 0;
   
   
   //Fetch quotations from broker servers.
   if (TimeYear(lastUpdate) < 2010){
      //LastUpdate == NULL
      copied = getAllAvailableRates(symbol, timeframe, 0, rates);         
   } else {
      copied = getRates(symbol, timeframe, lastUpdate, rates);
   }
   
   
   
	if(copied > 0)
     {

      int size = copied;

      for (int i = 0; i < size; i++)
      {
         datetime date = rates[i].time;
         
         if (lastRate == 0 || lastRate < date){
            lastRate = date;
         }

         string query = "INSERT INTO quotations_" + symbol + "_" + periodToString(timeframe) + 
                  "(PriceDate, AssetId, OpenPrice, HighPrice, LowPrice, ClosePrice, Volume) VALUES (" + 
                  "'" + dateToTime(rates[i].time) + "', " + 
                  (string) 1 + ", " + 
                  (string) rates[i].open + ", " + 
                  (string) rates[i].high + ", " + 
                  (string) rates[i].low + ", " + 
                  (string) rates[i].close + ", " + 
                  (string) rates[i].tick_volume + ");";
         //Alert(query);
         insert(query);
         
         //Try updating.
         string updateQuery = "UPDATE quotations_" + symbol + "_" + periodToString(timeframe) + " SET " + 
                  " OpenPrice = " + (string) rates[i].open + ", " + 
                  " HighPrice = " + (string) rates[i].high + ", " + 
                  " LowPrice = " + (string) rates[i].low + ", " + 
                  " ClosePrice = " + (string) rates[i].close + ", " + 
                  " Volume = " + (string) rates[i].tick_volume + " " + 
                  " WHERE PriceDate = " + "'" + dateToTime(rates[i].time) + "';";                 
         //Alert(updateQuery);
         insert(updateQuery);
         
      }
  
  
      string lastUpdateQuery = "UPDATE last_updates " +
                  "SET Last_Quotation_Update = " + "'" + dateToTime(lastRate) + "' " +
                  "WHERE Symbol = " + "'" + symbol + "_" + periodToString(timeframe) + "';";
      Alert(lastUpdateQuery);
      insert(lastUpdateQuery);
  
      
      return copied;
      
     }
   else Print("Failed to get history data for the symbol ", Symbol());
  
  return 0;
   
}




int getRates(string symbol, ENUM_TIMEFRAMES timeframe, datetime startDate, MqlRates& rates[]){
   int copied = CopyRates(symbol, timeframe, startDate, TimeCurrent(), rates); //);
   return ArrayRange(rates, 0);
}


int getRatesForTimeRange(string symbol, ENUM_TIMEFRAMES timeframe, datetime startDate, datetime endDate, MqlRates& rates[]){
   int copied = CopyRates(symbol, timeframe, startDate, endDate, rates);
   return ArrayRange(rates, 0);
}


int getAllAvailableRates(string symbol, ENUM_TIMEFRAMES timeframe, int startPos, MqlRates& rates[]){
   int count = 5000;
   int copied = CopyRates(symbol, timeframe, startPos, count, rates);
   return ArrayRange(rates, 0);
}

