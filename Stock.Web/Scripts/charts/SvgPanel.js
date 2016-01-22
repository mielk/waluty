function SvgPanel(params) {

    'use strict';

    //[Meta]
    var self = this;
    self.SvgPanel = true;
    self.parent = params.parent;
    self.key = params.key + '_svg';
    self.properties = params.properties;

    //[UI]
    self.svg = self.svg;
    self.renderer = undefined;
    var controls = { };
    var size = {};
    var position = { };


    //[Initialize functions].
    function initialize() {
        loadControls();
        assignEvents();
        createRaphaelCanvas();
    }

    function loadControls() {

        controls.parentContainer = params.container;
        size = {
            width: $(controls.parentContainer).width(),
            height: $(controls.parentContainer).height()
        };

        controls.container = $('<div/>', {
            'class': 'chart-svg-container',
            id: self.key
        }).css({
            'height': size.height + 'px',
            'width': size.width + 'px',
            'right': 0
        }).appendTo(params.container);
    }

    function assignEvents() {

    }

    function createRaphaelCanvas() {
        self.svg = Raphael(self.key);
    }

    function loadQuotations(quotations) {

        self.quotations = quotations;

        //Ensure that renderer is loaded.
        if (!self.renderer) {
            self.renderer = new PriceSvgRenderer({
                parent: self,
                size: size,
                quotations: quotations.arr,
                complete: quotations.complete
            });
        } else {
            self.renderer.updateQuotations(quotations.arr, quotations.complete);
        }

        render();

    }

    function render() {
        var drawObjects = self.renderer.getDrawObjects(self.quotations.arr);
        self.svg.clear();
        drawPaths(drawObjects.paths);
        drawCircles(drawObjects.circles);
    }

    function drawPaths(paths) {
        paths.forEach(function (path) {
            self.svg.path(path.path).attr(path.attr);
        });
    }

    function drawCircles(objects) {
        objects.forEach(function (obj) {
            var circle = self.svg.circle(obj.x, obj.y, obj.radius);
            circle.attr({
                'stroke': obj.stroke,
                'stroke-width': 1,
                'fill': obj.fill
            });
        });
    }

    function findQuotation(x) {
        if (self.renderer) {
            return self.renderer.findQuotation(x);
        } else {
            return null;
        }
    }

    function getInfo(quotation) {
        if (self.renderer) {
            return self.renderer.getInfo(quotation);
        }
    }


    initialize();


    //Public API.
    self.bind = function (e) {
        $(self).bind(e);
    }
    self.trigger = function (e) {
        $(self).trigger(e);
    }
    self.initialize = initialize;
    self.loadQuotations = loadQuotations;
    self.render = render;
    self.findQuotation = findQuotation;
    self.getInfo = getInfo;

}







function AbstractSvgRenderer(params) {

    'use strict';

    var self = this;
    self.AbstractSvgRenderer = true;
    self.parent = params.parent;
    self.params = {
        created: true,
        complete: params.complete
    };   //Parameters specific for this type of chart.
    self.size = params.size;
    self.quotations = params.quotations;
    self.paths = {};
    self.offset = 0;


    self.interfaceInitialize = function () {

    }

    function calculateHorizontalBound(items) {
        var singleWidth = STOCK.CONFIG.candle.width;
        var totalWidth = items.length * singleWidth;

        self.offset = self.parent.parent.offset();
        self.params.firstItem = items.length - 1 - Math.floor((self.offset + self.size.width) / singleWidth);
        self.params.lastItem = items.length - 1 - Math.floor(self.offset / singleWidth);
        self.params.singleWidth = singleWidth;
        self.params.totalWidth = totalWidth;

    }

    function findFirstNonEmptyIndex(index, direction) {
        var step = (direction ? (direction / Math.abs(direction)) : 1);
        var quotation = null;

        for (var i = index; i >= 0 && i <= self.quotations.length; i += step) {
            quotation = self.quotations[index];
            if (quotation) {
                return i;
            }
        }

        return index;

    }

    //Function to calculate vertical limits for the current chart and measures for a single unit.
    function calculateVerticalBounds(items, height) {

        //Find [min] and [max] value.

        //[Handling gaps in prices]
        //Function [findFirstNonEmptyIndex] implement for handling gaps in prices.
        //If user moves to the screen where there are only gaps, range.min and range.max are null
        //and this function cannot proceed.
        var range = self.findVerticalRange(items, 
                            findFirstNonEmptyIndex(self.params.firstItem, 1), 
                            findFirstNonEmptyIndex(self.params.lastItem, -1));

        //[Handling gaps in prices]
        //If user moves to the screen where there are only gaps, range.min and range.max are null
        //and this function cannot proceed.
        if (range.min === null && range.max === null) {
            range = self.findVerticalRange(items);
        }

        var min = range.min;
        var max = range.max;
        var difference = max - min;

        var bottom = min - STOCK.CONFIG.chart.margin * difference;
        bottom = self.params.minAllowed !== null ? Math.max(bottom, self.params.minAllowed) : bottom;
        var top = max + STOCK.CONFIG.chart.margin * difference;
        top = self.params.maxAllowed !== null ? Math.min(top, self.params.maxAllowed) : top;
        var distance = top - bottom;
        var unitHeight = height / distance;

        //Add vertical bounds of the chart.
        mielk.objects.addProperties(
            self.params,
            {   min: min,
                max: max,
                top: top,
                bottom: bottom,
                unitHeight: unitHeight  }
        );

    }

    //Funkcja zwraca najniższą i najwyższą wartość dla zakresu ograniczonego 
    //przez indeksy [first] i [last] w zestawie danych [items].
    self.findVerticalRange = function (items, first, last) {
        var $first = first || 0;
        var $last = last || items.length - 1;

        var min = mielk.arrays.getMin(items, self.fnMinEvaluation, $first, $last);
        var max = mielk.arrays.getMax(items, self.fnMaxEvaluation, $first, $last);

        return {
            min: min,
            max: max
        };

    }

    //Funkcja zwraca wszystkie obiekty, które mają zostać narysowane na tym wykresie.
    self.getDrawObjects = function (quotations) {

        //Calculate offsets and ranges.
        calculateHorizontalBound(quotations);
        calculateVerticalBounds(quotations, self.size.height);

        //Params calculated here for performance reasons.
        var params = {
            width: STOCK.CONFIG.candle.width,
            space: STOCK.CONFIG.candle.width * STOCK.CONFIG.candle.space,
            other: self.parent.properties
        };

        //Create SVG path for each single quotation.
        var items = new Array(quotations.length);
        for (var i = self.params.firstItem; i <= self.params.lastItem; i++) {
            var invertedIndex = quotations.length - i;
            if (quotations[i])
                items[i] = self.createCandlePath(invertedIndex, quotations[i], params);
        }

        //Join all SVG paths together and return them.
        return {
            paths: self.joinPaths(items),
            circles: self.fetchCircleObjects(items)
        }

    }

    //Funkcja obliczająca pozycję pionową dla danej wartości (w zależności od wysokości kontenera).
    self.getY = function (value) {
        return self.size.height * (this.params.top - value) / (this.params.top - this.params.bottom);
    };

    //Funkcja zwraca proporcje pomiędzy podanym zakresem a całym zakresem dla aktualnego zestawu danych.
    self.getRatio = function (range) {
        var ratio = (range.max - range.min) / (self.params.top - self.params.bottom);
        return ratio;
    }

    //Funkcja zwraca notowanie, którego świeca jest aktualnie wyświetlona w odległości [x] od lewej
    //krawędzi ramki z notowaniami.
    self.findQuotation = function (x) {
        var firstItem = null;
        var index = self.params.firstItem - 1;

        while (!firstItem) {
            index++;
            firstItem = self.quotations[index];
            if (index >= self.params.lastItem) break;
        }

        if (!firstItem) return null;

        var firstItemOffset = firstItem.coordinates.left;
        var itemsOffset = Math.floor(x / self.params.singleWidth);
        var modItemsOffset = x % self.params.singleWidth;
        var foundItemIndex = index + itemsOffset + (-firstItemOffset + modItemsOffset > self.params.singleWidth ? 1 : 0) - (index - self.params.firstItem);

        return self.quotations[foundItemIndex];
    }

    //Funkcja odświeża notowania przypisane do tego wykresu. Wywoływana kilka razy podczas pobierania
    //danych z bazy, ponieważ notowania są pobierane paczkami.
    self.updateQuotations = function (quotations, complete) {
        self.quotations = quotations;
        self.params.complete = complete;

        if (self.params.complete) {
            alert('completed: ' + self.quotations.length);
        }

    }

}



function PriceSvgRenderer(params) {

    'use strict';

    AbstractSvgRenderer.call(this, params);
    var self = this;
    self.PriceSvgRenderer = true;
    self.type = STOCK.INDICATORS.PRICE;

    //Add parameters specific for this type of chart (i.e. for ADX minimum allowed is 0).
    self.params.minAllowed = 0;
    self.params.maxAllowed = null;



    self.interfaceInitialize();

}
mielk.objects.extend(AbstractSvgRenderer, PriceSvgRenderer);
PriceSvgRenderer.prototype = {

    //Function used to evaluate the minimum value of this chart.
    fnMinEvaluation: function (item) {
        return item ? item.low : undefined;
    },

    //Function used to evaluate the minimum value of this chart.
    fnMaxEvaluation: function (item) {
        return item ? item.high : undefined;
    },

    createCandlePath: function (i, item, params) {
        var self = this;
        var isAscending = (item.close > item.open);
        var bodyWidth = params.width - params.space;

        //Calculate coordinates.
        var bodyTop = this.getY(Math.max(item.open, item.close));
        var bodyBottom = this.getY(Math.min(item.open, item.close));
        var shadeTop = this.getY(item.high);
        var shadeBottom = this.getY(item.low);
        var left = (self.offset + self.size.width - (i * params.width) + (params.space / 2));
        var right = left + bodyWidth;
        var middle = left + (right - left) / 2;

        var path = 'M' + left + ',' + bodyBottom + 'L' + left + ',' + bodyTop + 'L' +
                    right + ',' + bodyTop + 'L' + right + ',' + bodyBottom + 'Z' +
                    'M' + middle + ',' + shadeBottom + 'L' + middle + ',' + bodyBottom + 'Z' +
                    'M' + middle + ',' + shadeTop + 'L' + middle + ',' + bodyTop + 'Z';


        //Save the coordinates of this item's candle 
        //(used later to display values on the chart and to scale charts).
        item.coordinates = {
            left: left,
            right: right,
            middle: middle,
            shadeBottom: shadeBottom,
            shadeTop: shadeTop,
            bodyTop: bodyTop,
            bodyBottom: bodyBottom
        };


        //Add peak/through indicators.
        function addExtremumLabel(extrema, isMin) {
            var dist = STOCK.CONFIG.peaks.distance;
            var extremum = isMin ?
                Math.max(item.troughByClose, item.troughByLow) :
                Math.max(item.peakByClose, item.peakByHigh);
            if (!extremum) return;

            var scale = Math.min(1, extremum / 10);
            var greyscale = 255 * (1 - scale);

            extrema.push({
                item: item,
                x: middle,
                y: isMin ? shadeBottom + dist : shadeTop - dist,
                radius: Math.min(extremum, 10),
                stroke: 'rgb(' + greyscale + ',' + greyscale + ',' + greyscale + ')',
                fill: 'rgba(' + (isMin ? '255, 0' : '0, 255') + ', 0, ' + scale + ')'
            });

            return true;

        }

        var extrema = [];
        if (params.other.peaks) {
            addExtremumLabel(extrema, true);
            addExtremumLabel(extrema, false);
        }

        return {
            ascending: isAscending ? path : null,
            descending: isAscending ? null : path,
            extrema: extrema
        };


    },

    joinPaths: function (items) {
        
        var paths = {
            ascending: '',
            descending: ''
            //verticalWeeks: '',
            //verticalMonths: '',
            //verticalYears: '',
            //dateScaleWeeks: '',
            //dateScaleMonths: '',
            //dateScaleYears: ''
        }

        items.forEach(function (item) {
            if (item.ascending) paths.ascending += item.ascending
            if (item.descending) paths.descending += item.descending
        });

        //Add properties for each path.
        var array = [];
        array.push({
            path: paths.ascending,
            attr: {
                'stroke': 'black',
                'stroke-width': 1,
                'fill': STOCK.CONFIG.candle.color.ascending
            }
        });

        //Adding descending candles.
        array.push({
            path: paths.descending,
            attr: {
                'stroke': 'black',
                'stroke-width': 1,
                'fill': STOCK.CONFIG.candle.color.descending
            }
        });

        //Inne ścieżki podejrzeć potem w pliku [SvgPanelOld.js].

        return array;

    },

    fetchCircleObjects: function (items) {
        var array = [];
        items.forEach(function (item) {
            if (item.extrema.length) {
                item.extrema.forEach(function (extremum) {
                    array.push(extremum);
                });
            }
        });

        return array;

    },

    getInfo: function (quotation) {
        var info = (quotation ?
                        'Open: ' + quotation.open + ' | ' +
                        'Low: ' + quotation.low + ' | ' +
                        'High: ' + quotation.high + ' | ' +
                        'Close: ' + quotation.close
                        : '');
        
        return info;

    }

};



//function AbstractSvgRenderer2() {

//    'use strict';

//    var self = this;
//    self.AbstractSvgRenderer2 = true;

//    //Default values.
//    self.default = {
//        count: 150
//    };

//    //Params.
//    self.params = {
//        displayDateScale: false
//    };

//    //Size.
//    self.size = {
//        width: 0,
//        height: 0
//    };

//    //Temporary variables.
//    self.temp = {
//        lastDate: null,
//        previous: []
//    };

//    //Graphic data sets.
//    self.objects = [];
//    self.labels = {
//        dates: [],
//        values: [],
//        extrema: []
//    };
//    //Temporary variables (for calculation purposes)

//    self.paths = {};

//    self.visibleItems = [];

//    self.getPathObjects = function () {
//        return self.objects;
//    };

//    self.getLabels = function () {
//        return self.labels;
//    };

//    self.calculatePaths = function (items, params) {

//        self.saveSize(params.width, params.height);
//        self.saveParams(params.timeband);
//        self.calculateDatesRange(items, params.startDate, params.endDate, params.startOffset, params.endOffset);
//        self.calculateValuesRange(items, params.height);

//        self.createPaths(items);
//        self.objects = self.pathsObjects(self.paths);

//    };

//    self.saveSize = function (width, height) {
//        self.size.width = width;
//        self.size.height = height;
//    };

//    self.saveParams = function (timeband) {
//        mielk.objects.addProperties(self.params, {
//            timeband: timeband,
//            displayDateScale: self.type.displayDateScale,
//            minAllowed: self.type.minValue,
//            maxAllowed: self.type.maxValue
//        });
//    };

//    self.offsetBoundDates = function (startIndex, endIndex, startOffset, endOffset) {

//        return {
//            startIndex: startIndex + Math.floor(startOffset),
//            endIndex: endIndex + Math.ceil(endOffset) - 1,
//            startOffset: startOffset - Math.floor(startOffset),
//            endOffset: 1 + (endOffset - Math.ceil(endOffset))
//        };

//    };

//    self.calculateDatesRange = function (items, $startDate, $endDate, $startOffset, $endOffset) {
//        var p = self.params;
//        var endIndex;
//        var startIndex;
//        var endItem;
//        var startItem;

//        if ($startDate && $endDate) {
//            startIndex = self.findIndexByDate(items, $startDate);
//            endIndex = self.findIndexByDate(items, $endDate);
//            var calculated = this.offsetBoundDates(startIndex, endIndex, $startOffset, $endOffset);
//            startItem = items[calculated.startIndex];
//            endItem = items[calculated.endIndex];


//            //TODO - przekalkulować, jeżeli przesunięcie spowodowałoby wyjście poza wykres.
//            mielk.objects.addProperties(p, {
//                endItem: calculated.endIndex,
//                endDate: endItem.date,
//                endOffset: calculated.endOffset,
//                startItem: calculated.startIndex,
//                startDate: startItem.date,
//                startOffset: calculated.startOffset
//            });

//        } else {
//            endIndex = items.length - 1;
//            startIndex = Math.max(endIndex - self.default.count + 1, 0);
//            endItem = items[endIndex];
//            startItem = items[startIndex];

//            mielk.objects.addProperties(p, {
//                endItem: endIndex,
//                endDate: endItem.date,
//                endOffset: 1,
//                startItem: startIndex,
//                startDate: startItem.date,
//                startOffset: 0
//            });
//        }

//        //Calculate the width of a single data unit.
//        //First and last item can be cut - that's why p.startOffset and p.endOffset are being added.
//        var itemsCounter = p.endItem - p.startItem + p.endOffset - p.startOffset;
//        var unitWidth = self.size.width / itemsCounter;
//        p.unitWidth = unitWidth;

//    };

//    self.findIndexByDate = function (items, date) {

//        var index = mielk.arrays.firstGreater(items, date, function ($i, $date) {
//            if ($i.date > date) return 1;
//            if ($i.date < date) return -1;
//            return true;
//        }, true);

//        return index;

//    },


//    self.isTurn = function (date) {
//        if (self.temp.lastDate) {

//            switch (self.params.timeband) {
//                case STOCK.TIMEBANDS.D:
//                    if (date.getYear() !== self.temp.lastDate.getYear()) {
//                        return 3;
//                    } else if (date.getMonth() !== self.temp.lastDate.getMonth()) {
//                        return 2;
//                    } else if (self.temp.lastDate.getDay() > date.getDay()) {
//                        return 1;
//                    } else {
//                        return 0;
//                    }
//                case STOCK.TIMEBANDS.W:
//                    if (date.getYear() !== self.temp.lastDate.getYear()) {
//                        return 3;
//                    } else if (date.getMonth() !== self.temp.lastDate.getMonth()) {
//                        return 2;
//                    } else {
//                        return 0;
//                    }
//                case STOCK.TIMEBANDS.M:
//                    if (date.getYear() !== self.temp.lastDate.getYear()) {
//                        return 1;
//                    } else {
//                        return 0;
//                    }
//                default:
//                    return 0;
//            }

//        } else {
//            return 0;
//        }
//    };


//    self.createPaths = function (items) {

//        var startIndex = self.params.startItem;
//        var endIndex = self.params.endItem;

//        self.items = items;

//        //Clear visible items array.
//        var j = 0;
//        self.visibleItems.length = 0;

//        for (var i = startIndex; i <= endIndex; i++) {
//            var item = items[i];
//            item.index = i;

//            if (i === endIndex) {
//                item.last = true;
//            }

//            item.paths = {};
//            self.calculatePath(i, item, self.params.startOffset);

//            //Add to the array of visible paths.
//            self.visibleItems[j++] = items[i];

//        }

//        self.addValuePaths();
//        self.totalPaths();

//    };

//    self.createDatePaths = function (i, item, offset) {
//        var vertical = '';
//        var dateScale = '';
//        var index = i - self.params.startItem;

//        //renderowanie linii oznaczających przedziały czasowe.
//        var turn = self.isTurn(item.date);
//        if (item.last) {
//            turn = 3;
//        }

//        if (turn) {
//            var x = ((index - offset) * self.params.unitWidth).toFixed(4);

//            if (!item.last) {
//                vertical = 'M' + x + ',' + 0 + 'L' + x + ',' + (self.size.height + 1) + 'Z';
//                if (turn > 1) {
//                    var length = (5 * Math.pow(STOCK.CONFIG.dateRangeIndicator, turn));
//                    dateScale = 'M' + x + ',' + 0 + 'L' + x + ',' + length + 'Z';
//                }

//            }

//            self.addDateLabels(index, turn);

//            //Save the previous item to compare.
//            self.temp.previous[turn] = item;
//            if (turn === 3) {
//                self.temp.previous[2] = item;
//            }

//            //Save the result path in item.
//            var suffix;
//            switch (turn) {
//                case 1:
//                    suffix = 'Weeks';
//                    break;
//                case 2:
//                    suffix = 'Months';
//                    break;
//                case 3:
//                    suffix = 'Years';
//                    break;
//                default:
//                    suffix = 'Weeks';
//            }
//            item.paths['vertical' + suffix] = vertical;
//            item.paths['dateScale' + suffix] = dateScale;

//        }

//        self.temp.lastDate = item.date;

//    };

//    self.addDateLabels = function (i, turn) {
//        if (turn > 1) {
//            //Dodaje podpisy wyświetlane w dateScale.
//            var currentRight = (i + 1) * self.params.unitWidth;
//            var previousLeft = self.params.unitWidth *
//                (self.temp.previous[turn] ?
//                (self.temp.previous[turn].index - self.params.startItem) :  //Dopasowuje indeks poprzedniego elementu do rozmiarów tablicy.
//                                                                            //(Tablica może mieć więcej elementów, a pierwszy wyświetlany może mieć np. indeks 100
//                                                                            //wtedy trzeba to uwzględnić w obliczaniu współrzędnych).
//                0);

//            var label = {
//                dates: true,
//                text: turn === 2 ? self.temp.lastDate.getMonth() + 1 : self.temp.lastDate.getFullYear(),
//                x: previousLeft + (currentRight - previousLeft) / 2,
//                y: (turn === 2 ? 10 : 30)
//            };

//            self.labels.dates.push(label);

//            //For each year change, it is also month change.
//            if (turn === 3) {
//                self.addDateLabels(i, 2);
//            }

//        }
//    };

//    self.addValuePaths = function () {
//        var step = self.calculateStep(self.params.bottom, self.params.top);
//        var firstLevel = this.findFirstLineLevel(self.params.bottom, step);
//        var linesPath = '';
//        var scalePath = '';
//        for (var i = firstLevel; i < self.params.top; i += step) {
//            var y = (self.getY(i)).toFixed(4);
//            linesPath += 'M0,' + y + 'L' + self.size.width + ',' + y + 'Z';
//            scalePath += 'M0,' + y + 'L' + STOCK.CONFIG.valueScaleLineWidth + ',' + y + 'Z';

//            var label = {
//                values: true,
//                text: i,
//                x: STOCK.CONFIG.valueLabelLeft,
//                y: y
//            };

//            self.labels.values.push(label);

//        }
//        self.paths.horizontalLines = linesPath;
//        self.paths.valueScaleLines = scalePath;
//    };

//    self.getParams = function () {
//        return {
//            width: self.size.width,
//            height: self.size.height,
//            startDate: self.params.startDate,
//            startItem: self.params.startItem,
//            startOffset: self.params.startOffset,
//            endDate: self.params.endDate,
//            endItem: self.params.endItem,
//            endOffset: self.params.endOffset,
//            min: self.params.min,
//            max: self.params.max,
//            top: self.params.top,
//            bottom: self.params.bottom
//        };
//    };

//    self.getVisibleItems = function () {
//        return self.visibleItems;
//    };

//    self.defaultStepCalculation = function (minValue, maxValue) {
//        var lines = Math.round(self.size.height / 50);
//        var difference = maxValue - minValue;
//        var step = difference / lines;

//        var logLevel = Math.floor(mielk.numbers.log10(step));
//        var factor = Math.pow(10, logLevel);

//        var compareValue = step / factor;
//        if (compareValue <= 2) {
//            return factor * 2;
//        } else if (compareValue <= 5) {
//            return factor * 5;
//        } else {
//            return factor * 10;
//        }

//    };

//    self.getDateLineColor = function (timeband) {
//        var tb = self.params.timeband;
//        var cfg = STOCK.CONFIG;

//        switch (timeband) {
//            case STOCK.TIMEBANDS.W:
//                return (tb === STOCK.TIMEBANDS.D ? cfg.verticalWeeksLinesColor : cfg.transparent);
//            case STOCK.TIMEBANDS.M:
//                if (tb === STOCK.TIMEBANDS.D) {
//                    return cfg.verticalMonthsLinesColor;
//                } else if (tb === STOCK.TIMEBANDS.W) {
//                    return cfg.verticalWeeksLinesColor;
//                } else {
//                    return cfg.transparent;
//                }
//            case STOCK.TIMEBANDS.Y:
//                return cfg.verticalYearsLinesColor;
//            default:
//                return cfg.transparent;
//        }
//    };

//}


//function PriceSvgRenderer2() {

//    'use strict';

//    AbstractSvgRenderer.call(this);
//    var self = this;
//    self.PriceSvgRenderer = true;
//    //self.params.visibleLines = 10;
//    self.type = STOCK.INDICATORS.PRICE;
//}
////mielk.objects.extend(AbstractSvgRenderer, PriceSvgRenderer);
//PriceSvgRenderer2.prototype = {



//    calculateStep: function (minValue, maxValue) {
//        return this.defaultStepCalculation(minValue, maxValue);
//    },

//    findFirstLineLevel: function (minValue, step) {
//        var lastLower = Math.floor(minValue / step) * step;
//        return lastLower + step;
//    },


//};