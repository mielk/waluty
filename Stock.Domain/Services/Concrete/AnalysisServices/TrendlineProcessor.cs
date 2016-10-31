using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class TrendlineProcessor : ITrendlineProcessor
    {

        /* Calculation variables */
        private Trendline Trendline;
        private ITrendlineAnalyzer analyzer;


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



        public TrendlineProcessor(ITrendlineAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }




        public void runRightSide(IAnalyzer analyzer, int index, AssetTimeframe atf) { }

        public void runRightSide(IAnalyzer analyzer, DataItem item, AssetTimeframe atf) { }

        public void runFull(IAnalyzer analyzer, int index, AssetTimeframe atf) { }

        public void runFull(IAnalyzer analyzer, DataItem item, AssetTimeframe atf) { }

        public void Analyze(Trendline trendline, DataItem[] items, DataItem startItem)
        {

        }



    }
}
