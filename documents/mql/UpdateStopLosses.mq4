//+------------------------------------------------------------------+
//|                                             UpdateStopLosses.mq4 |
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
   
   double sl = 100;
   
   updateStopLosses(sl);
   
  }
//+------------------------------------------------------------------+





bool updateStopLosses(double slPoints){
   int total = OrdersTotal();
   
   
   Alert("Updating stop losses");
   
   
   for (int i = 0; i < total; i++){
      
      //Select order.
      if (!OrderSelect(i, SELECT_BY_POS)){
         return false;
      }
      
      //Check if it is open order (pending orders should be skipped).
      int orderType = OrderType();
      string symbol = OrderSymbol();
      bool isOpened = false;
      bool isCurrentStopLossCloser = false;
      double price = 0;
      double sl = 0;
      
      
      if (orderType == OP_BUY){
         isOpened = true;
         price = MarketInfo(symbol, MODE_BID);
         sl = price - slPoints * MarketInfo(symbol, MODE_POINT);
         isCurrentStopLossCloser = (sl < OrderStopLoss() && OrderStopLoss() > 0);
      } else if (orderType == OP_SELL) {
         isOpened = true;
         price = MarketInfo(symbol, MODE_ASK);
         sl = price + slPoints * MarketInfo(symbol, MODE_POINT);
         isCurrentStopLossCloser = (sl > OrderStopLoss() && OrderStopLoss() > 0);
      }
      
      
      if (isOpened){
         
         int ticket = OrderTicket();
         
         
         //Check if current stop loss is closer to the current price than the new one.
         if (isCurrentStopLossCloser){
         
            Alert("Stop Loss unchanged for order " + (string) ticket + " since the current one is closer to the price than the new one");
         
         } else {

            if (OrderModify(ticket, OrderOpenPrice(), sl, OrderTakeProfit(), OrderExpiration(), clrNONE))
            {
               Alert("Stop Loss successfully updated for order " + (string) ticket + " to the level " + (string) sl + " (Price: " + (string) price + ")");
            }
            else
            {
               Alert("[!] Error when trying to update stop loss for order " + (string) ticket + " | " + (string) GetLastError());
            }
         
         
         }
         

      }
      
   }
   
   return true;
   
}