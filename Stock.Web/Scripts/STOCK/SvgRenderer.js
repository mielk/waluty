﻿function AbstractSvgRenderer() {
    
    'use strict';

    var self = this;
    self.AbstractSvgRenderer = true;

    //Default values.
    self.default = {
        count: 300
    };

    //Params.
    self.params = {
        displayDateScale: false
    };

    //Size.
    self.size = {
        width: 0,
        height: 0
    };

    //Temporary variables.
    self.temp = {
        lastDate: null,
        previous: []
    };

    //Graphic data sets.
    self.objects = [];
    self.labels = {
        dates: [],
        values: [],
        extrema: []
    };
    //Temporary variables (for calculation purposes)

    self.paths = {};

    self.visibleItems = [];

    self.getPathObjects = function () {
        return self.objects;
    };

    self.getLabels = function () {
        return self.labels;
    };

    self.calculatePaths = function(items, params) {

        self.saveSize(params.width, params.height);
        self.saveParams(params.timeband);
        self.calculateDatesRange(items, params.startDate, params.endDate, params.startOffset, params.endOffset);
        self.calculateValuesRange(items, params.height);

        self.createPaths(items);
        self.objects = self.pathsObjects(self.paths);

    };

    self.saveSize = function(width, height) {
        self.size.width = width;
        self.size.height = height;
    };

    self.saveParams = function (timeband) {
        mielk.objects.addProperties(self.params, {
            timeband: timeband,
            displayDateScale: self.type.displayDateScale,
            minAllowed: self.type.minValue,
            maxAllowed: self.type.maxValue
        });
    };

    self.offsetBoundDates = function (startIndex, endIndex, startOffset, endOffset) {

        return {
            startIndex: startIndex + Math.floor(startOffset),
            endIndex: endIndex + Math.ceil(endOffset) - 1,
            startOffset: startOffset - Math.floor(startOffset),
            endOffset: 1 + (endOffset - Math.ceil(endOffset))
        };

    };

    self.calculateDatesRange = function (items, $startDate, $endDate, $startOffset, $endOffset) {
        var p = self.params;
        var endIndex;
        var startIndex;
        var endItem;
        var startItem;

        if ($startDate && $endDate) {
            startIndex = self.findIndexByDate(items, $startDate);
            endIndex = self.findIndexByDate(items, $endDate);
            var calculated = this.offsetBoundDates(startIndex, endIndex, $startOffset, $endOffset);
            startItem = items[calculated.startIndex];
            endItem = items[calculated.endIndex];
            

            //TODO - przekalkulować, jeżeli przesunięcie spowodowałoby wyjście poza wykres.
            mielk.objects.addProperties(p, {
                endItem: calculated.endIndex,
                endDate: endItem.date,
                endOffset: calculated.endOffset,
                startItem: calculated.startIndex,
                startDate: startItem.date,
                startOffset: calculated.startOffset
            });

        } else {
            endIndex = items.length - 1;
            startIndex = Math.max(endIndex - self.default.count + 1, 0);
            endItem = items[endIndex];
            startItem = items[startIndex];

            mielk.objects.addProperties(p, {
                endItem: endIndex,
                endDate: endItem.date,
                endOffset: 1,
                startItem: startIndex,
                startDate: startItem.date,
                startOffset: 0
            });
        }

        //Calculate the width of a single data unit.
        //First and last item can be cut - that's why p.startOffset and p.endOffset are being added.
        var itemsCounter = p.endItem - p.startItem + p.endOffset - p.startOffset;
        var unitWidth = self.size.width / itemsCounter;
        p.unitWidth = unitWidth;

    };

    self.findIndexByDate = function (items, date) {

        var index = mielk.arrays.firstGreater(items, date, function($i, $date) {
            if ($i.date > date) return 1;
            if ($i.date < date) return -1;
            return true;
        }, true);

        return index;

    },

    self.calculateValuesRange = function (items, height) {
        //Index of first and last visible item.
        var start = self.params.startItem;
        var end = self.params.endItem;
        
        //Calculate height.
        var min = mielk.arrays.getMin(items, self.fnMinEvaluation, start, end);
        var max = mielk.arrays.getMax(items, self.fnMaxEvaluation, start, end);
        var difference = max - min;
        
        //Calculate min and max value.
        var bottom = min - STOCK.CONFIG.chartMargin * difference;
        bottom = self.params.minAllowed !== null ? Math.max(bottom, self.params.minAllowed) : bottom;
        var top = max + STOCK.CONFIG.chartMargin * difference;
        top = self.params.maxAllowed !== null ? Math.min(top, self.params.maxAllowed) : top;
        var distance = top - bottom;
        var unitHeight = height / distance;

        //Add vertical bounds of the chart.
        mielk.objects.addProperties(self.params, {
            min: min,
            max: max,
            top: top,
            bottom: bottom,
            unitHeight: unitHeight
        });

    };

    self.isTurn = function (date) {
        if (self.temp.lastDate) {

            switch (self.params.timeband) {
                case STOCK.TIMEBANDS.D:
                    if (date.getYear() !== self.temp.lastDate.getYear()) {
                        return 3;
                    } else if (date.getMonth() !== self.temp.lastDate.getMonth()) {
                        return 2;
                    } else if (self.temp.lastDate.getDay() > date.getDay()) {
                        return 1;
                    } else {
                        return 0;
                    }
                case STOCK.TIMEBANDS.W:
                    if (date.getYear() !== self.temp.lastDate.getYear()) {
                        return 3;
                    } else if (date.getMonth() !== self.temp.lastDate.getMonth()) {
                        return 2;
                    } else {
                        return 0;
                    }
                case STOCK.TIMEBANDS.M:
                    if (date.getYear() !== self.temp.lastDate.getYear()) {
                        return 1;
                    } else {
                        return 0;
                    }
                default:
                    return 0;
            }

        } else {
            return 0;
        }
    };

    self.getY = function(value) {
        return self.size.height * (this.params.top - value) / (this.params.top - this.params.bottom);
    };

    self.createPaths = function (items) {

        var startIndex = self.params.startItem;
        var endIndex = self.params.endItem;

        self.items = items;

        //Clear visible items array.
        var j = 0;
        self.visibleItems.length = 0;

        for (var i = startIndex; i <= endIndex; i++) {
            var item = items[i];
            item.index = i;

            if (i === endIndex) {
                item.last = true;
            }

            item.paths = {};
            self.calculatePath(i, item, self.params.startOffset);

            //Add to the array of visible paths.
            self.visibleItems[j++] = items[i];

        }

        self.addValuePaths();
        self.totalPaths();

    };

    self.createDatePaths = function(i, item, offset) {
        var vertical = '';
        var dateScale = '';
        var index = i - self.params.startItem;
        
        //renderowanie linii oznaczających przedziały czasowe.
        var turn = self.isTurn(item.date);
        if (item.last) {
            turn = 3;
        }

        if (turn) {
            var x = ((index - offset) * self.params.unitWidth).toFixed(4);

            if (!item.last) {
                vertical = 'M' + x + ',' + 0 + 'L' + x + ',' + (self.size.height + 1) + 'Z';
                if (turn > 1) {
                    var length = (5 * Math.pow(STOCK.CONFIG.dateRangeIndicator, turn));
                    dateScale = 'M' + x + ',' + 0 + 'L' + x + ',' + length + 'Z';
                }

            }

            self.addDateLabels(index, turn);

            //Save the previous item to compare.
            self.temp.previous[turn] = item;
            if (turn === 3) {
                self.temp.previous[2] = item;
            }

            //Save the result path in item.
            var suffix;
            switch (turn) {
                case 1:
                    suffix = 'Weeks';
                    break;
                case 2:
                    suffix = 'Months';
                    break;
                case 3:
                    suffix = 'Years';
                    break;
                default:
                    suffix = 'Weeks';
            }
            item.paths['vertical' + suffix] = vertical;
            item.paths['dateScale' + suffix] = dateScale;

        }

        self.temp.lastDate = item.date;

    };

    self.addDateLabels = function(i, turn) {
        if (turn > 1) {
            //Dodaje podpisy wyświetlane w dateScale.
            var currentRight = (i + 1) * self.params.unitWidth;
            var previousLeft = self.params.unitWidth * 
                (self.temp.previous[turn] ? 
                (self.temp.previous[turn].index - self.params.startItem) :  //Dopasowuje indeks poprzedniego elementu do rozmiarów tablicy.
                                                                            //(Tablica może mieć więcej elementów, a pierwszy wyświetlany może mieć np. indeks 100
                                                                            //wtedy trzeba to uwzględnić w obliczaniu współrzędnych).
                0);

            var label = {
                dates: true,
                text: turn === 2 ? self.temp.lastDate.getMonth() + 1 : self.temp.lastDate.getFullYear(),
                x: previousLeft + (currentRight - previousLeft) / 2,
                y: (turn === 2 ? 10 : 30)
            };

            self.labels.dates.push(label);

            //For each year change, it is also month change.
            if (turn === 3) {
                self.addDateLabels(i, 2);
            }

        }
    };

    self.addValuePaths = function() {
        var step = self.calculateStep(self.params.bottom, self.params.top);
        var firstLevel = this.findFirstLineLevel(self.params.bottom, step);
        var linesPath = '';
        var scalePath = '';
        for (var i = firstLevel; i < self.params.top; i += step) {
            var y = (self.getY(i)).toFixed(4);
            linesPath += 'M0,' + y + 'L' + self.size.width + ',' + y + 'Z';
            scalePath += 'M0,' + y + 'L' + STOCK.CONFIG.valueScaleLineWidth + ',' + y + 'Z';

            var label = {
                values: true,
                text: i,
                x: STOCK.CONFIG.valueLabelLeft,
                y: y
            };

            self.labels.values.push(label);

        }
        self.paths.horizontalLines = linesPath;
        self.paths.valueScaleLines = scalePath;
    };

    self.getParams = function () {
        return {
            width: self.size.width,
            height: self.size.height,
            startDate: self.params.startDate,
            startItem: self.params.startItem,
            startOffset: self.params.startOffset,
            endDate: self.params.endDate,
            endItem: self.params.endItem,
            endOffset: self.params.endOffset,
            min: self.params.min,
            max: self.params.max,
            top: self.params.top,
            bottom: self.params.bottom
        };
    };

    self.getVisibleItems = function () {
        return self.visibleItems;
    };

    self.defaultStepCalculation = function (minValue, maxValue) {
        var lines = Math.round(self.size.height / 50);
        var difference = maxValue - minValue;
        var step = difference / lines;

        var logLevel = Math.floor(mielk.numbers.log10(step));
        var factor = Math.pow(10, logLevel);

        var compareValue = step / factor;
        if (compareValue <= 2) {
            return factor * 2;
        } else if (compareValue <= 5) {
            return factor * 5;
        } else {
            return factor * 10;
        }

    };

    self.getDateLineColor = function (timeband) {
        var tb = self.params.timeband;
        var cfg = STOCK.CONFIG;
        
        switch (timeband) {
            case STOCK.TIMEBANDS.W:
                return (tb === STOCK.TIMEBANDS.D ? cfg.verticalWeeksLinesColor : cfg.transparent);
            case STOCK.TIMEBANDS.M:
                if (tb === STOCK.TIMEBANDS.D) {
                    return cfg.verticalMonthsLinesColor;
                } else if (tb === STOCK.TIMEBANDS.W) {
                    return cfg.verticalWeeksLinesColor;
                } else {
                    return cfg.transparent;
                }
            case STOCK.TIMEBANDS.Y:
                return cfg.verticalYearsLinesColor;
            default:
                return cfg.transparent;
        }
    };

}




function PriceSvgRenderer() {
    
    'use strict';

    AbstractSvgRenderer.call(this);
    var self = this;
    self.PriceSvgRenderer = true;
    self.params.visibleLines = 10;
    self.type = STOCK.INDICATORS.PRICE;
}
mielk.objects.extend(AbstractSvgRenderer, PriceSvgRenderer);
PriceSvgRenderer.prototype = {
    
    //Function used to evaluate the minimum value of this chart.
    fnMinEvaluation: function(item) {
        return item.low;
    },
    
    //Function used to evaluate the minimum value of this chart.
    fnMaxEvaluation: function(item) {
        return item.high;
    },

    calculatePath: function (i, item, offset) {
        this.createCandlePath(i, item, offset);
        this.createDatePaths(i, item, offset);

        //TODO - dodać renderowanie dodatkowych wskaźników, takich jak np. średnie, Bollinger Bands albo Ichimoku Clouds

    },

    createCandlePath: function (i, item, offset) {
        var self = this;
        var index = i - self.params.startItem;
        var isAscending = (item.close > item.open);
        var space = STOCK.CONFIG.candleSpace * self.params.unitWidth;
        var bodyWidth = self.params.unitWidth - space;

        //Calculate coordinates.
        var bodyTop = this.getY(Math.max(item.open, item.close));
        var bodyBottom = this.getY(Math.min(item.open, item.close));
        var shadeTop = this.getY(item.high);
        var shadeBottom = this.getY(item.low);
        var left = (index - offset) * self.params.unitWidth + space / 2;
        var right = left + bodyWidth;
        var middle = left + (right - left) / 2;

        var path = 'M' + left + ',' + bodyBottom + 'L' + left + ',' + bodyTop + 'L' +
                    right + ',' + bodyTop + 'L' + right + ',' + bodyBottom + 'Z' +
                    'M' + middle + ',' + shadeBottom + 'L' + middle + ',' + bodyBottom + 'Z' +
                    'M' + middle + ',' + shadeTop + 'L' + middle + ',' + bodyTop + 'Z';

        //Save the coordinates of this item's candle (to display values on the chart).
        mielk.objects.addProperties(item, {
            left: left,
            right: right,
            middle: middle,
            shadeBottom: shadeBottom,
            shadeTop: shadeTop,
            bodyTop: bodyTop,
            bodyBottom: bodyBottom
        });

        //Save the result path in item.
        item.paths.ascending = isAscending ? path : null;
        item.paths.descending = isAscending ? null : path;

        //Add peak/through indicators.
        var peak = Math.max(item.peakByClose, item.peakByHigh);
        var scale = Math.min(1, peak / 10);
        var greyscale = 255 * (1 - scale);
        if (item.peakByClose) {
            self.labels.extrema.push({
                item: item,
                x: middle,
                y: shadeTop - 14,
                radius: Math.min(item.peakByClose, 10),
                stroke: 'rgb(' + greyscale + ',' + greyscale + ',' + greyscale + ')',
                fill: 'rgba(0, 255, 0, ' + scale + ')'
            });
        }

        var trough = Math.max(item.troughByClose, item.troughByLow);
        scale = Math.min(1, item.troughByClose / 10);
        greyscale = 255 * (1 - scale);
        if (item.troughByClose) {
            self.labels.extrema.push({
                item: item,
                x: middle,
                y: shadeBottom + 14,
                radius: Math.min(item.troughByClose, 10),
                stroke: 'rgb(' + greyscale + ',' + greyscale + ',' + greyscale + ')',
                fill: 'rgba(255, 0, 0, ' + scale + ')'
            });
        }

        
    },

    totalPaths: function () {
        var self = this;
        self.paths = mielk.objects.addProperties(self.paths, {
            ascending: '',
            descending: '',
            verticalWeeks: '',
            verticalMonths: '',
            verticalYears: '',
            dateScaleWeeks: '',
            dateScaleMonths: '',
            dateScaleYears: ''
        });

        for (var i = self.params.startItem; i < self.params.endItem; i++) {
            var item = self.items[i];
            self.paths.ascending += item.paths.ascending ? item.paths.ascending : '';
            self.paths.descending += item.paths.descending ? item.paths.descending : '';
            self.paths.verticalWeeks += item.paths.verticalWeeks ? item.paths.verticalWeeks : '';
            self.paths.verticalMonths += item.paths.verticalMonths ? item.paths.verticalMonths : '';
            self.paths.verticalYears += item.paths.verticalYears ? item.paths.verticalYears : '';
            self.paths.dateScaleWeeks += item.paths.dateScaleWeeks ? item.paths.dateScaleWeeks : '';
            self.paths.dateScaleMonths += item.paths.dateScaleMonths ? item.paths.dateScaleMonths : '';
            self.paths.dateScaleYears += item.paths.dateScaleYears ? item.paths.dateScaleYears : '';
        }

    },

    calculateStep: function(minValue, maxValue) {
        return this.defaultStepCalculation(minValue, maxValue);
    },

    findFirstLineLevel: function(minValue, step) {
        var lastLower = Math.floor(minValue / step) * step;
        return lastLower + step;
    },

    pathsObjects: function (paths) {
        var self = this;
        var array = [];

        //Array horizontal line.
        array.push({
            path: paths.horizontalLines,
            dates: false,
            attr: {
                'stroke': STOCK.CONFIG.horizontalLinesColor,
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.valueScaleLines,
            values: true,
            attr: {
                'stroke': STOCK.CONFIG.valueScaleIndicatorLength,
                'stroke-width': 1
            }
        });

        //Adding vertical lines.
        array.push({
            path: paths.verticalWeeks,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.W),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.verticalMonths,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.M),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.verticalYears,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.Y),
                'stroke-width': 1
            }
        });

        //Adding data scale lines.
        array.push({
            path: paths.dateScaleWeeks,
            dates: true,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.W),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.dateScaleMonths,
            dates: true,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.M),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.dateScaleYears,
            dates: true,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.Y),
                'stroke-width': 1
            }
        });

        //Adding ascending candles.
        array.push({
            path: paths.ascending,
            attr: {
                'stroke': 'black',
                'stroke-width': 1,
                'fill': STOCK.CONFIG.ascCandleColor
            }
        });

        //Adding descending candles.
        array.push({
            path: paths.descending,
            attr: {
                'stroke': 'black',
                'stroke-width': 1,
                'fill': STOCK.CONFIG.descCandleColor
            }
        });


        return array;

    }

};




function MacdSvgRenderer() {
    
    'use strict';

    AbstractSvgRenderer.call(this);
    var self = this;
    self.MacdSvgRenderer = true;
    self.params.visibleLines = 5;
    self.type = STOCK.INDICATORS.MACD;
}
mielk.objects.extend(AbstractSvgRenderer, MacdSvgRenderer);
MacdSvgRenderer.prototype = {
    
    //Function used to evaluate the minimum value of this chart.
    fnMinEvaluation: function (item) {
        return Math.min(item.macd, item.signal, item.histogram);
    },

    //Function used to evaluate the minimum value of this chart.
    fnMaxEvaluation: function (item) {
        return Math.max(item.macd, item.signal, item.histogram);
    },

    calculatePath: function (i, item, offset) {
        this.createBasePath(i, item, offset);
        this.createDatePaths(i, item, offset);
    },

    createBasePath: function (i, item, offset) {
        var self = this;
        var index = i - self.params.startItem;
        var space = STOCK.CONFIG.histogramSpace * self.params.unitWidth;
        var bodyWidth = self.params.unitWidth - space;

        //Calculate coordinates.
        var yMacd = this.getY(item.macd);
        var ySignal = this.getY(item.signal);
        var left = (index - offset) * self.params.unitWidth + space / 2;
        var right = left + bodyWidth;
        var middle = left + (right - left) / 2;
        var yHistogram = this.getY(item.histogram);
        var yZero = this.getY(0);

        var histogramPath = 'M' + left + ',' + yZero + 'L' + left + ',' + yHistogram + 'L' +
                    right + ',' + yHistogram + 'L' + right + ',' + yZero + 'Z';


        //Save the coordinates of this item's candle (to display values on the chart).
        mielk.objects.addProperties(item, {
            left: left,
            right: right,
            middle: middle,
            yMacd: yMacd,
            ySignal: ySignal,
            yHistogram: yHistogram,
            yZero: yZero
        });

        //Check if histogram is ascending.
        var isAscending = (self.temp.previousHistogram !== undefined && item.histogram - self.temp.previousHistogram > 0);

        //Save the result path in item.
        item.paths.macd = middle + ',' + yMacd;
        item.paths.signal = middle + ',' + ySignal;
        item.paths.histogramAsc = isAscending ? histogramPath : null;
        item.paths.histogramDesc = isAscending ? null : histogramPath;

        //Remember histogram for further calculations.
        self.temp.previousHistogram = item.histogram;

    },

    totalPaths: function () {
        var self = this;
        self.paths = mielk.objects.addProperties(self.paths, {
            macd: 'M',
            signal: 'M',
            histogramAsc: '',
            histogramDesc: '',
            verticalWeeks: '',
            verticalMonths: '',
            verticalYears: ''
        });

        for (var i = self.params.startItem; i < self.params.endItem; i++) {
            var item = self.items[i];
            self.paths.macd += item.paths.macd + 'L';
            self.paths.signal += item.paths.signal + 'L';
            self.paths.histogramDesc += item.paths.histogramDesc === null ? '' : item.paths.histogramDesc;
            self.paths.histogramAsc += item.paths.histogramAsc === null ? '' : item.paths.histogramAsc;
            self.paths.verticalWeeks += item.paths.verticalWeeks ? item.paths.verticalWeeks : '';
            self.paths.verticalMonths += item.paths.verticalMonths ? item.paths.verticalMonths : '';
            self.paths.verticalYears += item.paths.verticalYears ? item.paths.verticalYears : '';
        }

        //self.paths.macd = mielk.text.cut(self.paths.macd, 1) + 'Z';
        //self.paths.signal = mielk.text.cut(self.paths.signal, 1) + 'Z';
        
    },

    calculateStep: function (minValue, maxValue) {
        return this.defaultStepCalculation(minValue, maxValue);
    },

    findFirstLineLevel: function (minValue, step) {
        var lastLower = Math.floor(minValue / step) * step;
        return lastLower + step;
    },

    pathsObjects: function (paths) {
        var self = this;
        var array = [];
        var config = STOCK.CONFIG;

        //Array horizontal line.
        array.push({
            path: paths.horizontalLines,
            dates: false,
            attr: {
                'stroke': config.horizontalLinesColor,
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.valueScaleLines,
            values: true,
            attr: {
                'stroke': config.valueScaleIndicatorLength,
                'stroke-width': 1
            }
        });

        //Adding vertical lines.
        array.push({
            path: paths.verticalWeeks,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.W),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.verticalMonths,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.M),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.verticalYears,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.Y),
                'stroke-width': 1
            }
        });


        //Adding MACD line.
        array.push({
            path: paths.macd,
            attr: {
                'stroke': config.macdLineColor,
                'stroke-width': 1
            }
        });

        //Adding Signal line.
        array.push({
            path: paths.signal,
            attr: {
                'stroke': config.signalLineColor,
                'stroke-width': 1
            }
        });


        //Adding ascending histogram.
        array.push({
            path: paths.histogramAsc,
            attr: {
                'stroke': config.histogramLineColor,
                'stroke-width': 1,
                'fill': config.histogramAscFillColor
            }
        });

        //Adding descending histogram.
        array.push({
            path: paths.histogramDesc,
            attr: {
                'stroke': config.histogramLineColor,
                'stroke-width': 1,
                'fill': config.histogramDescFillColor
            }
        });


        return array;

    }

};


function AdxSvgRenderer() {
    
    'use strict';

    AbstractSvgRenderer.call(this);
    var self = this;
    self.AdxSvgRenderer = true;
    self.params.visibleLines = 4;
    self.type = STOCK.INDICATORS.ADX;
}
mielk.objects.extend(AbstractSvgRenderer, AdxSvgRenderer);
AdxSvgRenderer.prototype = {

    //Function used to evaluate the minimum value of this chart.
    fnMinEvaluation: function (item) {
        return 0;
    },

    //Function used to evaluate the minimum value of this chart.
    fnMaxEvaluation: function (item) {
        return Math.max(item.adx, item.diPlus, item.diMinus);
    },

    calculatePath: function (i, item, offset) {
        this.createBasePath(i, item, offset);
        this.createDatePaths(i, item, offset);
    },

    createBasePath: function (i, item, offset) {
        var self = this;
        var index = i - self.params.startItem;
        var space = STOCK.CONFIG.histogramSpace * self.params.unitWidth;
        var bodyWidth = self.params.unitWidth - space;

        //Calculate coordinates.
        var left = (index - offset) * self.params.unitWidth + space / 2;
        var right = left + bodyWidth;
        var middle = left + (right - left) / 2;
        var yAdx = this.getY(item.adx);
        var yDiPlus = this.getY(item.diPlus);
        var yDiMinus = this.getY(item.diMinus);


        //Save the coordinates of this item's candle (to display values on the chart).
        mielk.objects.addProperties(item, {
            left: left,
            right: right,
            middle: middle,
            yAdx: yAdx,
            yDiPlus: yDiPlus,
            yDiMinus: yDiMinus
        });


        //Save the result path in item.
        item.paths.adx = middle + ',' + (yAdx).toFixed(4);
        item.paths.diPlus = middle + ',' + (yDiPlus).toFixed(4);
        item.paths.diMinus = middle + ',' + (yDiMinus).toFixed(4);

    },

    totalPaths: function () {
        var self = this;
        self.paths = mielk.objects.addProperties(self.paths, {
            adx: 'M',
            diPlus: 'M',
            diMinus: 'M'
        });

        for (var i = self.params.startItem; i < self.params.endItem; i++) {
            var item = self.items[i];
            self.paths.adx += item.paths.adx + 'L';
            self.paths.diPlus += item.paths.diPlus + 'L';
            self.paths.diMinus += item.paths.diMinus + 'L';
            self.paths.verticalWeeks += item.paths.verticalWeeks ? item.paths.verticalWeeks : '';
            self.paths.verticalMonths += item.paths.verticalMonths ? item.paths.verticalMonths : '';
            self.paths.verticalYears += item.paths.verticalYears ? item.paths.verticalYears : '';
        }

    },

    calculateStep: function () {
        return 10;
    },

    findFirstLineLevel: function () {
        return 10;
    },

    pathsObjects: function (paths) {
        var self = this;
        var array = [];
        var config = STOCK.CONFIG;

        //Array horizontal line.
        array.push({
            path: paths.horizontalLines,
            dates: false,
            attr: {
                'stroke': config.horizontalLinesColor,
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.valueScaleLines,
            values: true,
            attr: {
                'stroke': config.valueScaleIndicatorLength,
                'stroke-width': 1
            }
        });

        //Adding vertical lines.
        array.push({
            path: paths.verticalWeeks,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.W),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.verticalMonths,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.M),
                'stroke-width': 1
            }
        });

        array.push({
            path: paths.verticalYears,
            attr: {
                'stroke': self.getDateLineColor(STOCK.TIMEBANDS.Y),
                'stroke-width': 1
            }
        });


        //Adding DI+ line.
        array.push({
            path: paths.diPlus,
            attr: {
                'stroke': config.diPlusLineColor,
                'stroke-width': 1
            }
        });

        //Adding DI- line.
        array.push({
            path: paths.diMinus,
            attr: {
                'stroke': config.diMinusLineColor,
                'stroke-width': 1
            }
        });

        //Adding ADX line.
        array.push({
            path: paths.adx,
            attr: {
                'stroke': config.adxLineColor,
                'stroke-width': 1
            }
        });

        return array;

    }

};