using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;
using Stock.Domain.Enums;
using Stock.DAL.Repositories;
using Stock.DAL.Infrastructure;
using Stock.DAL.TransferObjects;

namespace Stock.Domain.Services
{

    public class TrendlineAnalyzer : Analyzer, ITrendlineAnalyzer
    {

        public override AnalysisType Type
        {
            get { return AnalysisType.Trendline; }
        }

        /* Constants */

        /* Ile notowań przed wierzchołkiem początkowym ma być uwzględnione w analizie. */
        public int QuotationsBeforeInitialItem = 3;

        /* Maksymalny zakres bez żadnego odbicia od linii trendu. Po napotkaniu tylu notowań bez odbicia,
         * trend jest uznawany za zakończony. */
        public int MaxRangeWithoutHit = 300;

        /* Minimalny dystans pomiędzy wierzchołkami, aby mogły być zaliczone do tej samej linii trendu.
         * Ma to zapobiec naliczaniu do tej samej linii trendu dwóch leżących koło siebie ekstremów, 
         * z których jeden jest ekstremum wg ceny zamknięcia a drugi ekstremum wg ceny ekstremalnej. */
        public int MinDistance = 3;

        /* Maksymalne odchylenie dla uznania wierzchołka za odbicie od linii trendu. 
         * Po przekroczeniu takiego odchylenia wierzchołek nie będzie uznawany za odbicie. */
        public double MaxExtremumVariation = 0.002d;

        /* Maksymalne odchylenie dla uznania nie-wierzchołka za odbicie od linii trendu. 
         * Po przekroczeniu takiego odchylenia nie-wierzchołek nie będzie uznawany za odbicie. */
        public double MaxNonExtremumVariation = 0.001d;

        /* Minimalne przekroczenie linii trendu potrzebne, żeby uznać dane notowanie za przebicie (TrendBreak).
         * Jeżeli przekroczenie linii jest mniejsze, nie jest ono uznawane za przebicie. */
        public double MinForTrendlineBreak = 0.001d;

        /* Minimalna punktacja potrzebna, żeby linia trendu była przekazana w wyniku działania funkcji Analyze.
         * Wszystkie linie trendu, które uzyskają mniejszą punktację, są ignorowane. */
        public int MinTrendlineScore = 1;



        /* Data collections */
        public DataItem[] Items { get; set; }

        /* Calculation variables */
        private Trendline Trendline;



        public TrendlineAnalyzer(AssetTimeframe atf)
            : base(atf)
        {
        }


        protected override void initialize_specific()
        {
            ItemsForAnalysis = 300;
        }

        protected override IAnalyzerProcessor getProcessor()
        {
            //if (processor == null) processor = new adxpro(this);
            //return processor;
            return null;
        }





        public void LoadItems(DataItem[] items)
        {
            this.Items = items;
        }

        public void Initialize(DataItem initialItem, double initialLevel, DataItem boundItem, double boundLevel)
        {

            this.Trendline = new Trendline(AssetTimeframe.asset, AssetTimeframe.timeframe, initialItem, initialLevel, boundItem, boundLevel);

        }


        public override void Analyze(DataItem[] items)
        {
            
        }


        public Trendline Analyze(DataItem initialItem, double initialLevel, DataItem boundItem, double boundLevel)
        {

            /* Assign values to this object's properties. */
            Initialize(initialItem, initialLevel, boundItem, boundLevel);


            /*  Iteracja po wszystkich notowaniach.
             *  Rozpoczyna od pierwszego elementu - 5.
             *  Kończy w momencie, kiedy trend zostaje uznany za zakończony lub
             *  gdy dojdzie do końca tablicy z notowaniami. */
            for (var i = Math.Max(initialItem.Index - QuotationsBeforeInitialItem, 0); i < Items.Length; i++)
            {

                DataItem item = Items[i];
                DataItem nextItem = (i < Items.Length - 1 ? Items[i + 1] : null);

                
                /* Każde pojedyncze notowanie jest wysyłane do obiektu [Trendline], gdzie jest
                    * ono sprawdzane pod kątem położenia względem linii trendu. */
                Trendline.Check(item, nextItem);
                


                /* Po dodaniu jakiegokolwiek zdarzenia (odbicie od linii trendu, przebicie jej),
                 * obiekt klasy Trendline przelicza swoją punktację [Score] i sprawdza czy trend
                 * powinien zostać uznany za zakończony, wtedy ustawaia mu [IsFinished] = true. 
                 *
                 * W tym miejscu sprawdzamy, czy trend w trakcie dodawania elementu został uznany za 
                 * zakończony. W takiej sytuacji program opuszcza pętlę. */
                if (Trendline.IsFinished) break;


            }


            return Trendline.Score >= MinTrendlineScore ? Trendline : null;

        }




    }
}
