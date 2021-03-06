//+------------------------------------------------------------------+
//|                                          PlacePreEventOrders.mq4 |
//|                        Copyright 2015, MetaQuotes Software Corp. |
//|                                             https://www.mql5.com |
//+------------------------------------------------------------------+
#property copyright "Copyright 2015, MetaQuotes Software Corp."
#property link      "https://www.mql5.com"
#property version   "1.00"
#property strict
//+------------------------------------------------------------------+
//| Script program start function                                    |
//+------------------------------------------------------------------+
void OnStart()
  {
//---
   
   string symbol = Symbol();
   double volume = 0.1;
   double startMarginPips = 150;
   double difference = 50;
   double counter = 10;
   double sl = 100;
   bool clearPrevious = true;
   
   placeOrders(symbol, false, volume, startMarginPips, difference, counter, sl, clearPrevious);
   
  }
//+------------------------------------------------------------------+




bool placeOrders(string symbol, bool isBuy, double volume, double startMarginPips, double difference, double counter, double sl, 
                  bool clearPrevious){

   double ask = MarketInfo(symbol, MODE_ASK);   //kupno
   double bid = MarketInfo(symbol, MODE_BID);   //sprzedaż
   double point = MarketInfo(symbol, MODE_POINT);
   double price;
   
   
   
   //Clear previous orders if [clearPrevious] parameter is set to True.
   if (clearPrevious){
      Alert("Clearing previous orders");
   }
   
   
   //Place sell orders.
   price = bid - startMarginPips * point;
   for (int i = 0; i < counter; i++){
      bool result;
      result = OrderSend(symbol, OP_SELLSTOP, volume, price, 10, price + sl * point, 0);
      price -= difference * point;   
   }
   
   
   //Place buy orders.
   price = ask + startMarginPips * point;
   for (int i = 0; i < counter; i++){
      bool result;
      result = OrderSend(symbol, OP_BUYSTOP, volume, price, 10, price - sl * point, 0);
      price += difference * point;
   }


   Alert("Action completed");
   return true;

}