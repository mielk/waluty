//+------------------------------------------------------------------+
//|                                               CloseAllOrders.mq4 |
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
   
   closeAllOrders();
   
  }
//+------------------------------------------------------------------+



bool closeAllOrders(){
   int total = OrdersTotal();

   //Create array of trades IDs.
   int tickets[];
   ArrayResize(tickets, total);
   
   
   //Populate array with the trades' IDs.
   for (int i = 0; i < total; i++){ 
   
      //Select order.
      if (!OrderSelect(i, SELECT_BY_POS)){
         Alert(i);
         return false;
      }
      
      tickets[i] = OrderTicket();
      
   }
   
 
   for (int i = 0; i < total; i++){ 
   
      //Select order.
      int ticket = tickets[i];
      if (!OrderSelect(ticket, SELECT_BY_TICKET)){
         return false;
      }
      
      bool result = false;
      string symbol = OrderSymbol();
      int type = OrderType();
      double lots = OrderLots();
      
      
      if (type == OP_BUY){
      
         double price = MarketInfo(symbol, MODE_BID);
         result = OrderClose(ticket, lots, price, 10, clrNONE);
         
      } else if (type == OP_SELL){
      
         double price = MarketInfo(symbol, MODE_ASK);
         result = OrderClose(ticket, lots, price, 10, clrNONE);
         
      } else {
         
         result = OrderDelete(ticket, clrNONE);
         
      }
      
      
      if (result){
         Alert("Order " + (string) ticket + " successfully closed/deleted");
      } else {
         Alert("Error when trying to close/delete order " + (string) ticket);
      }
      
      
      double price = 1;
   
      
   }
   
   return true;

}