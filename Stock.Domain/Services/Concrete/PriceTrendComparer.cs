using Stock.Domain.Entities;
using Stock.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class PriceTrendComparer : IPriceTrendComparer
    {

        /* Constans */
        private const double limit = 0.001;
        /* Results */
        private TrendHit trendHit = null;
        private TrendBreak trendBreak = null;
        private double priceOverBreak;
        /* Temporary variables */
        private DataItem item;
        private DataItem previousHit;
        private double level;
        private TrendlineType type;


        private void Reset()
        {
            this.trendBreak = null;
            this.trendHit = null;
        }


        public void AssignProperties(DataItem item, double level, TrendlineType type, DataItem previousItem)
        {
            this.item = item;
            this.level = level;
            this.type = type;
            this.previousHit = previousItem;
        }

        public void Compare(DataItem item, double level, TrendlineType type, DataItem previousItem)
        {

            /* Zresetuj obiekt przed rozpoczęciem nowych obliczeń. */
            Reset();
            AssignProperties(item, level, type, previousItem);

            this.priceOverBreak = CalculatePriceOverBreak();

            /* Oblicz różnicę w dystansie. Jeżeli świeca leży daleko od linii trendu
             * jej punktacja wynosi 0 i funkcja od razu kończy działanie, żeby nie 
             * marnować zasobów.
             * Świece leżące po złej stronie linii trendu otrzymują ocenę ujemną.
             * Świece leżące blisko linii trendu otrzymują ocenę dodatnią. */
            var priceScore = EvaluateScore();


            /* Najpierw sprawdza czy notowanie jest przełamaniem linii trendu
             * (czyli cena zamknięcia powyżej linii oporu lub poniżej linii wsparcia).
             * Jeżeli tak, obliczenia nie są kontynuowane.*/
            AnalyzeBreak(item, level, type);
            if (IsBreak()) return;




        }


        /* 
         * Funkcja oblicza punktację za przekroczenie linii trendu.
         */
        private double CalculatePriceOverBreak()
        {
            var price = (type == TrendlineType.Resistance ? item.Quotation.High : item.Quotation.Low);
            var factor = (type == TrendlineType.Resistance ? 1d : -1d);
            return (price - level) * factor;

        }

        public bool IsHit(){
            return (trendHit != null);
        }

        public TrendHit GetHit()
        {
            return trendHit;
        }

        public bool IsBreak()
        {
            return (trendBreak != null);
        }

        public TrendBreak GetBreak()
        {
            return trendBreak;
        }

        public double GetPriceOverBreak()
        {
            return priceOverBreak;
        }


        private void AnalyzeBreak(DataItem item, double level, TrendlineType type)
        {

            /* Sprawdź czy cena zamknięcia leży poniżej linii wsparcia lub powyżej linii oporu. */
            if (type == TrendlineType.Resistance && item.Quotation.Close > level ||
                type == TrendlineType.Support && item.Quotation.Close < level)
            {
                this.trendBreak = new TrendBreak
                {
                    Item = item,
                    TrendLevel = level
                };
            }

        }




        private double EvaluateScore()
        {

            var compared = item.GetHighOrLowPrice(type);
            var difference = level - compared;
            var percentageDifference = difference / level;



            return 0;
        }

        private double EvaluatePriceDistance()
        {
            return 0;
        }

        private double EvaluateDistanceToPreviousEvent()
        {
            return 0;
        }


        /* Funkcja przeliczająca na punkty wahanie ceny w danej świecy.
         * Wysoko ocenione muszą być dwie sytuacje:
         * - świeca z bardzo małym korpusem, ale tylko wtedy, kiedy występuje
         *   od razu po innym trafieniu (najlepiej ze świecą o dużym korpusie).
         * - w innych sytuacjach im większy korpus, tym lepiej.
         */
        private double EvaluatePriceChange()
        {
            return 0;
        }
 


    }
}
