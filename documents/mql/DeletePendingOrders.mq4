//+------------------------------------------------------------------+
//|                                          DeletePendingOrders.mq4 |
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
   
   deletePendingOrders();
   
  }
//+------------------------------------------------------------------+



bool deletePendingOrders(){
   int total = OrdersTotal();
 
   for (int i = total - 1; i >= 0; i--){ 
   
      //Select order.
      if (!OrderSelect(i, SELECT_BY_POS)){
         return false;
      }
      
      bool result = false;
      string symbol = OrderSymbol();
      int ticket = OrderTicket();
      int type = OrderType();
      
      
      if (type == OP_BUY || type == OP_SELL){
               
      } else {
         
         result = OrderDelete(ticket, clrNONE);
         
      }
      
      
      if (result){
         Alert("Order " + (string) ticket + " successfully deleted");
      } else {
         Alert("Error when trying to delete order " + (string) ticket);
      }
      
      
   }
   
   return true;

}