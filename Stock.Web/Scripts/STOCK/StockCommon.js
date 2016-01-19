﻿/*
 * Common objects used in whole STOCK project.
 */

(function(window){

    'use strict';

    var timebands = {
        M5: {
            id: 1, name: '5 minutes', symbol: 'M5', period: 5, selectable: true, next: function (date) {
                return mielk.dates.addMinutes(date, date.getDay() === 5 && date.getHours() === 23 && date.getMinutes() === 55 ? 2885 : 5);
            }
        },
        M15: {
            id: 2, name: '15 minutes', symbol: 'M15', period: 15, selectable: true, next: function (date) {
                return mielk.dates.addMinutes(date, date.getDay() === 5 && date.getHours() === 23 && date.getMinutes() === 45 ? 2895 : 15);
            }
        },
        M30: {
            id: 3, name: '30 minutes', symbol: 'M30', period: 30, selectable: true, next: function (date) {
                return mielk.dates.addMinutes(date, date.getDay() === 5 && date.getHours() === 23 && date.getMinutes() === 30 ? 2910 : 30);
            }
        },
        H1: {
            id: 4, name: '1 hour', symbol: 'H1', period: 60, selectable: true, next: function (date) {
                return mielk.dates.addHours(date, date.getDay() === 5 && date.getHours() === 23 ? 49 : 1);
            }
        },
        H4: {
            id: 5, name: '4 hours', symbol: 'H4', period: 240, selectable: true, next: function (date) {
                return mielk.dates.addHours(date, date.getDay() === 5 && date.getHours() === 20 ? 52 : 4);
            }
        },
        D1: {
            id: 6, name: 'daily', symbol: 'D1', period: 1440, selectable: true, next: function (date) {
                return mielk.dates.addDays(date, date.getDay() === 5 ? 3 : 1);
            }
        },
        W1: {
            id: 7, name: 'weekly', symbol: 'W1', period: 7000, selectable: true, next: function (date) {
                return mielk.dates.addWeeks(date, 1);
            }
        },
        MN1: {
            id: 8, name: 'monthly', symbol: 'MN1', period: 30000, selectable: true, next: function (date) {
                return mielk.dates.addMonths(date, 1);
            }
        },
        //D: { id: 1, name: 'daily', symbol: '1D', period: 1, selectable: true },
        //W: { id: 2, name: 'weekly', symbol: '1W', period: 7, selectable: true },
        //M: { id: 3, name: 'monthly', symbol: '1M', period: 30, selectable: true },
        //Y: { id: 4, name: 'yearly', symbol: '1Y', period: 365, selectable: false },
        getItem: function (value) {
            for (var key in this) {
                if (this.hasOwnProperty(key)) {
                    var object = this[key];
                    if (object.symbol === value) {
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
            return this.M5;
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
            initialHeight: 500,
            minValue: 0,
            maxValue: null,
            color: 'blue'
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
            initialHeight: 150,
            negativeAllowed: true,
            minValue: null,
            maxValue: null,
            color: 'yellow'
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
            initialHeight: 150,
            negativeAllowed: false,
            minValue: 0,
            maxValue: 100,
            color: 'brown'
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
        loading: {
            packageSize: 1000
        },
        chart: {
            margin: 0.1
        },
        candle: {
            width: 8,
            space: 0.35,
            color: {
                ascending: 'white',
                descending: 'black'
            }
            
        }
        , timeScale: {
            height: 50
        }
        , valueScale: {
            width: 100
        }
        , peaks: {
            distance: 24
        }


        //chartMargin: 0.1,
        //candleSpace: 0.35,
        //ascCandleColor: 'white',
        //descCandleColor: 'black',
        //verticalWeeksLinesColor: '#EEE',
        //verticalMonthsLinesColor: '#CCC',
        //verticalYearsLinesColor: '#18285C',
        //transparent: 'rgba(255, 255, 255, 0)',
        
        //valuesScaleWidth: 50,
        //dateRangeIndicator: 2,
        //dataLabelColor: '#777',
        //valueLabelColor: '#777',
        //horizontalLinesColor: '#EEE',
        //valueScaleLineWidth: 5,
        //valueScaleIndicatorLength: '#777',
        //valueLabelLeft: 17,
        
        //MACD
        //histogramSpace: 0.35,
        //histogramLineColor: '#666',
        //macdLineColor: '#222',
        //signalLineColor: '#999',
        //histogramAscFillColor: 'green',
        //histogramDescFillColor: 'red',
        
        //ADX
        //diPlusLineColor: 'green',
        //diMinusLineColor: 'red',
        //adxLineColor: 'blue',
        
        //Peaks & Troughs
        //extremaCircleStrokeColor: '#CCC'

    };

    var stock = {
        TIMEBANDS: timebands,
        INDICATORS: indicators,
        CONFIG: config
    };


    // Expose STOCK to the global object
    window.STOCK = stock;


})(window);