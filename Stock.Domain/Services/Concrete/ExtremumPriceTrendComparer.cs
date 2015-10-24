using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class ExtremumPriceTrendComparer : IPriceTrendComparer
    {

        private double priceOverBreak;

        public void Compare(DataItem item, double level, TrendlineType type, DataItem previousItem)
        {

        }

        public bool IsHit()
        {
            return false;
        }

        public TrendHit GetHit()
        {
            return null;
        }


        public bool IsBreak()
        {
            return false;
        }


        public TrendBreak GetBreak()
        {
            return null;
        }

        public double GetPriceOverBreak()
        {
            return priceOverBreak;
        }



        private double EvaluatePriceDistance(DataItem item)
        {
            return 0;
        }

        private double EvaluateDistanceToPreviousEvent(int index)
        {
            return 0;
        }


        /* Funkcja przeliczająca na punktu wahanice ceny w danej świecy.
         * Wysoko ocenione muszą być dwie sytuacje:
         * - świeca z bardzo małym korpusem, ale tylko wtedy, kiedy występuje
         *   od razu po innym trafieniu (najlepiej ze świecą o dużym korpusie).
         * - w innych sytuacjach im większy korpus, tym lepiej.
         */
        private double EvaluatePriceChange(DataItem item)
        {
            return 0;
        }
 


    }
}
