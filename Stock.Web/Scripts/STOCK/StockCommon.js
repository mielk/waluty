/*
 * Common objects used in whole STOCK project.
 */

(function(window){

    'use strict';

    var timebands = {
        M5: { id: 1, name: '5 minutes', symbol: 'M5', period: 5, selectable: true },
        M15: { id: 1, name: '15 minutes', symbol: 'M15', period: 15, selectable: true },
        M30: { id: 1, name: '30 minutes', symbol: 'M30', period: 30, selectable: true },
        H1: { id: 1, name: '1 hour', symbol: 'H1', period: 60, selectable: true },
        H4: { id: 1, name: '4 hours', symbol: 'H4', period: 240, selectable: true },
        D1: { id: 1, name: 'daily', symbol: 'D1', period: 1440, selectable: true },
        W1: { id: 1, name: 'weekly', symbol: 'W1', period: 7000, selectable: true },
        MN1: { id: 1, name: 'monthly', symbol: 'MN1', period: 30000, selectable: true },
        //D: { id: 1, name: 'daily', symbol: '1D', period: 1, selectable: true },
        //W: { id: 2, name: 'weekly', symbol: '1W', period: 7, selectable: true },
        //M: { id: 3, name: 'monthly', symbol: '1M', period: 30, selectable: true },
        //Y: { id: 4, name: 'yearly', symbol: '1Y', period: 365, selectable: false },
        getItem: function (value) {
            for (var key in this) {
                if (this.hasOwnProperty(key)) {
                    var object = this[key];
                    if (object.id === value) {
                        return object;
                    }
                }
            }
            return null;
        },
        getValues: function () {
            var array = [];
            for (var key in this) {
                if (this.hasOwnProperty(key)) {
                    var item = this[key];
                    if (item && typeof (item) !== 'function' && item.selectable) {
                        var object = {
                            id: key,
                            name: item.name,
                            symbol: item.symbol,
                            object: item
                        };
                        array.push(object);
                    }
                }
            }
            return array;
        },
        defaultValue: function () {
            return this.D;
        }
    };

    var indicators = {
        PRICE: {
            id: 1,
            name: 'prices',
            analyzer: function () {
                return new PriceAnalyzer();
            },
            svgRenderer: function () {
                return new PriceSvgRenderer();
            },
            labelFactory: function() {
                return new PriceLabelFactory();
            },
            displayDateScale: true,
            initialHeight: 600,
            minValue: 0,
            maxValue: null
        },
        MACD: {
            id: 2,
            name: 'MACD',
            analyzer: function () {
                return new MacdAnalyzer();
            },
            svgRenderer: function () {
                return new MacdSvgRenderer();
            },
            labelFactory: function () {
                return new MacdLabelFactory();
            },
            displayDateScale: false,
            initialHeight: 300,
            negativeAllowed: true,
            minValue: null,
            maxValue: null
        },
        ADX: {
            id: 3,
            name: 'ADX',
            analyzer: function () {
                return new AdxAnalyzer();
            },
            svgRenderer: function () {
                return new AdxSvgRenderer();
            },
            labelFactory: function () {
                return new AdxLabelFactory();
            },
            displayDateScale: false,
            initialHeight: 300,
            negativeAllowed: false,
            minValue: 0,
            maxValue: 100
        },
        getItem: function (value) {
            for (var key in this) {
                if (this.hasOwnProperty(key)) {
                    var object = this[key];
                    if (key === value || object.id === value) {
                        return object;
                    }
                }
            }
            return null;
        },
        getValues: function () {
            var array = [];
            for (var key in this) {
                if (this.hasOwnProperty(key)) {
                    var item = this[key];
                    if (item && typeof (item) !== 'function') {
                        var object = {
                            id: key,
                            name: item.name,
                            object: item
                        };
                        array.push(object);
                    }
                }
            }
            return array;
        },
        defaultValue: function () {
            return this.PRICE;
        }
    };

    var config = {
        chartMargin: 0.1,
        candleSpace: 0.35,
        ascCandleColor: 'white',
        descCandleColor: 'black',
        verticalWeeksLinesColor: '#EEE',
        verticalMonthsLinesColor: '#CCC',
        verticalYearsLinesColor: '#18285C',
        transparent: 'rgba(255, 255, 255, 0)',
        timeScaleHeight: 50,
        valuesScaleWidth: 50,
        dateRangeIndicator: 2,
        dataLabelColor: '#777',
        valueLabelColor: '#777',
        horizontalLinesColor: '#EEE',
        valueScaleLineWidth: 5,
        valueScaleIndicatorLength: '#777',
        valueLabelLeft: 17,
        
        //MACD
        histogramSpace: 0.35,
        histogramLineColor: '#666',
        macdLineColor: '#222',
        signalLineColor: '#999',
        histogramAscFillColor: 'green',
        histogramDescFillColor: 'red',
        
        //ADX
        diPlusLineColor: 'green',
        diMinusLineColor: 'red',
        adxLineColor: 'blue',
        
        //Peaks & Troughs
        extremaCircleStrokeColor: '#CCC'

    };

    var stock = {
        TIMEBANDS: timebands,
        INDICATORS: indicators,
        CONFIG: config
    };


    // Expose STOCK to the global object
    window.STOCK = stock;


})(window);