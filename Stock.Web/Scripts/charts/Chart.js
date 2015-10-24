function Chart(params) {

    'use strict';

    var self = this;
    self.Chart = true;
    self.key = params.key;
    self.parent = params.parent;
    self.company = null;
    self.timeband = null;
    self.eventHandler = mielk.eventHandler();
    self.controller = params.controller;
    self.type = params.type;
    self.div = mielk.resizableDiv({
        parent: params.container,
        id: params.key || 'key',
        minHeight: params.minHeight || 200,
        maxHeight: params.maxHeight || 800,
        height: params.height || 0,
        'class': 'chart'
    });
    self.items = mielk.hashTable();
    self.currentItemsSet = null;
    self.displayDateScale = self.type.displayDateScale;

    //Create SVG manager.
    self.svg = new SvgPanel(self);    

    //Cursor.
    self.hoverService = new ChartHoverService(self);

    //Drawing layer.
    self.drawLayer = new ChartDrawLayer(self);
    

    //Events listener.
    (function eventsListener() {


        //Changing company or timeband.
        var companyChangeHandler = (function() {
            self.controller.bind({
                'changeCompany changeTimeband': function (e) {
                    self.svg.reset();
                    self.company = e.company || self.company;
                    self.timeband = e.timeband || self.timeband;
                }
            });
        })();


        //Resizing chart panel.
        var resizerHandler = (function() {
            self.div.bind({
                resize: function (e) {
                    self.svg.resize();
                }
            });
            $(window).resize(function () {
                self.svg.resize();
            });
        })();


        //Hovering.
        var scrollHandler = (function () {

            self.parent.bind({
                moveChart: function(e) {
                    self.svg.move(e.x, e.y);
                }
            });

        })();


        var scaleHandler = (function() {

        })();

        var hoverHandler = (function () {
            //$(self.svg.ui.chartContainer).bind({
            self.drawLayer.bind({
                mousemove: function (e) {
                    self.parent.trigger({
                        type: 'hoverItem',
                        pageX: e.pageX,
                        pageY: e.pageY,
                        x: e.offsetX,
                        y: e.offsetY,
                        source: self
                    });
                }
            });


            self.parent.bind({
                hoverItem: function (e) {
                    self.hoverService.hover(e);
                }
            });


        })();


        
    })();

}
Chart.prototype = {
    bind: function(e){
        this.eventHandler.bind(e);
    },
    trigger: function (e) {
        this.eventHandler.trigger(e);
    },
    injectQuotations: function (timeband, quotations, reload) {
        var analyzer = this.type.analyzer();
        var items = analyzer.run(quotations);
        this.items.setItem(timeband.id, items);
        this.currentItemsSet = items;
        if (reload) this.reload(timeband);
    },
    reload: function (timeband) {
        var items = this.items.getItem(timeband.id);
        this.currentItemsSet = items;
        this.svg.reload(timeband, items);
    },
    content: function () {
        return this.div.content();
    },
    getItems: function (timeband) {
        if (timeband) {
            return this.items.getItem(timeband.id) || [];
        } else {
            return this.currentItemsSet || [];
        }
    }
};


function ChartDrawLayer(chart) {

    'use strict';

    var self = this;
    self.ChartDrawLayer = true;
    self.chart = chart;
    self.leftClicked = false;
    self.rightClicked = false;
    self.resetPosition();

    self.container = $('<div/>', {        
        'class': 'draw-layer'
    }).appendTo(self.chart.content());

    //Event listener.
    $(self.container).bind({
        mousedown: function (e) {
            self.leftClicked = (e.which === 1);
            self.rightClicked = (e.which === 3);
            self.position = {
                x: e.offsetX,
                y: e.offsetY
            };
            mielk.notify.display('clicked | x: ' + self.position.x + '; y: ' + self.position.y);
        },
        mouseup: function(e) {
            self.leftClicked = false;
            self.rightClicked = false;
            self.resetPosition();
            mielk.notify.display('released');
        },
        mousemove: function (e) {
            if (self.leftClicked) {
                
                //Przesuwanie wykresem.
                var position = { x: e.offsetX, y: e.offsetY };
                var offset = {
                    x: self.position.x - position.x,
                    y: self.position.y - position.y
                };
                self.chart.parent.trigger({
                    type: 'moveChart',
                    x: offset.x,
                    y: offset.y
                });
                
            } else if (self.rightClicked) {
                //Skalowanie wykresu.
            } else {
                //Rysowanie linii trendu.
            }
        },
        leave: function(e) {
            mielk.notify.display('leaved');
        }
    });

}

ChartDrawLayer.prototype = {    
    trigger: function(e) {
        this.container.trigger(e);
    },
    bind: function(e) {
        this.container.bind(e);
    },
    resetPosition: function() {
        this.position = { x: 0, y: 0 };
    }
};

function SvgPanel(chart) {
    
    'use strict';

    var self = this;
    self.SvgPanel = true;
    //Parental chart.
    self.chart = chart;
    self.hasDateScale = self.chart.displayDateScale;

    self.chartPart = {
        CHART: 0,
        DATE: 1,
        VALUE: 2
    };

    //UI.
    self.ui = (function() {

        //Generate unique IDs for panels.
        var ids = {
            chart: mielk.numbers.generateUUID(),
            dates: mielk.numbers.generateUUID(),
            values: mielk.numbers.generateUUID()
        };

        //Parental container.
        var parent = chart.content();

        //Panel to store actual chart.
        var container = jQuery('<div/>', {
            'id': ids.chart,
            'class': 'svg-container'
        }).css({
            'bottom': self.chart.displayDateScale ? (STOCK.CONFIG.timeScaleHeight) + 'px' : 0,
            'right': (STOCK.CONFIG.valuesScaleWidth) + 'px'
        }).appendTo(parent);

        //DateScale ...
        var dateScale;
        if (self.chart.displayDateScale) {
            dateScale = jQuery('<div/>', {
                'id': ids.dates,
                'class': 'svg-date-scale-container'
            }).css({
                'right': STOCK.CONFIG.valuesScaleWidth + 'px',    //TODO - powinno się brać z configu - values.width
                'height': STOCK.CONFIG.timeScaleHeight + 'px'
            }).appendTo(parent);
        }

        //... and ValuesScale.
        var valueScale = jQuery('<div/>', {
            'id': ids.values,
            'class': 'svg-values-container'
        }).css({
            'bottom': self.hasDateScale ? (STOCK.CONFIG.timeScaleHeight - 1) + 'px' : 0,   //TODO - powinno się brać z configu - dates.height
            'width': STOCK.CONFIG.valuesScaleWidth + 'px'
        }).appendTo(parent);


        //SVG containers.
        var svgChart = Raphael(ids.chart);
        var svgDates = (self.hasDateScale ? Raphael(ids.dates) : null);
        var svgValues = Raphael(ids.values);




        function drawLabels(labels) {
            var dates = labels.dates;
            var values = labels.values;
            var extrema = labels.extrema;
            var label;
            var textfield;

            //Dates.
            if (self.hasDateScale && svgDates) {
                for (var i = 0; i < dates.length; i++) {
                    label = dates[i];
                    textfield = svgDates.text(label.x, label.y, label.text);
                    textfield.attr({
                        'fill': STOCK.CONFIG.dataLabelColor
                    });
                }
            }

            //Values.
            for (var j = 0; j < values.length; j++) {
                label = values[j];
                textfield = svgValues.text(label.x, label.y, label.text);
                textfield.attr({
                    'fill': STOCK.CONFIG.valueLabelColor
                });
            }
            

            //Peak & troughs.
            for (var k = 0; k < extrema.length; k++) {
                label = extrema[k];
                var circle = svgChart.circle(label.x, label.y, label.radius);
                circle.attr({
                    'stroke': label.stroke,
                    'stroke-width': 1,
                    'fill': label.fill
                });
            }

        }

        function drawPaths(paths) {
            for (var i = 0; i < paths.length; i++) {
                var object = paths[i];
                //Add to dates svg
                if (object.dates) {
                    if (dateScale) {
                        svgDates.path(object.path).attr(object.attr);
                    }
                } else if (object.values) {
                    //Add to values svg
                    svgValues.path(object.path).attr(object.attr);
                } else {
                    svgChart.path(object.path).attr(object.attr);
                }

            }
        }

        function setSize(size) {
            svgChart.clear();
            svgChart.setSize(size.width, size.height);
            svgValues.clear();
            svgValues.setSize(STOCK.CONFIG.valuesScaleWidth, size.height);
            if (self.dateScale) {
                svgDates.clear();
                svgDates.setSize(size.width, STOCK.CONFIG.timeScaleHeight);
            }

            self.size = {
                width: size.width,
                height: size.height
            };

        }

        function clear() {
            svgChart.clear();
            svgValues.clear();
            if (svgDates) {
                svgDates.clear();
            }
        }
        
        function getVisibleSize() {
            var width = $(container).width();
            var height = $(container).height();
        
            return {
                width: width,
                height: height
            };
            
        }

        function addPath(chartPart, path, attr) {
            var svg;
            
            switch (chartPart) {
                case self.chartPart.CHART:
                    svg = svgChart; break;
                case self.chartPart.DATE:
                    svg = svgDates; break;
                case self.chartPart.VALUE:
                    svg = svgValues; break;
                default:
                    svg = svgChart; break;
            }

            var result = svg.path(path).attr(attr ? attr : {});
            
            return result;

        }


        return {            
            chart: svgChart,
            chartContainer: container,
            dates: svgDates,
            datesContainer: (dateScale ? dateScale : null),
            values: svgValues,
            valuesContainer: valueScale,
            drawLabels: drawLabels,
            drawPaths: drawPaths,
            setSize: setSize,
            clear: clear,
            getVisibleSize: getVisibleSize,
            addPath: addPath
        };


    })();

    self.position = {
        startDate: null,
        startOffset: 0,
        endDate: null,
        endOffset: 0
    };

    //Size of chart.
    self.size = {
        width: 0,
        height: 0
    };

}
SvgPanel.prototype = {
    
    bind: function(e) {
        this.eventHandler.bind(e);
    },
    
    trigger: function(e) {
        this.eventHandler.trigger(e);
    },
    
    reload: function (timeband, items) {
        var self = this;

        if (!items || !timeband) return;

        //Get proper SVG renderer object.
        var renderer = self.chart.type.svgRenderer();
        if (!renderer) return;

        var visibleSize = this.ui.getVisibleSize();

        renderer.calculatePaths(items, {
            width: visibleSize.width,
            height: visibleSize.height,
            startDate: self.position.startDate,
            startOffset: self.position.startOffset,
            endDate: self.position.endDate,
            endOffset: self.position.endOffset,
            timeband: timeband
        });

        self.render(renderer, visibleSize);

    },
    
    render: function (renderer, visibleSize) {
        this.cleanSvgs();
        this.setSize(visibleSize);
        this.saveParams(renderer.getParams());
        this.saveVisibleItems(renderer.getVisibleItems());
        this.drawPaths(renderer);
        this.drawLabels(renderer);
    },

    cleanSvgs: function () {
        this.ui.clear();
    },

    saveVisibleItems: function(items){
        this.visibleItems = items;
    },

    saveParams: function (params) {
        this.params = params;
    },

    setSize: function (size) {
        this.ui.setSize(size);
    },

    resize: function () {
        var items = this.chart.currentItemsSet;
        var timeband = this.chart.controller.timeband;

        //Items cannot be reloaded, if they have not been loaded yet.
        if (items) {
            this.reload(timeband, items);
        }

    },

    calculateOffsetByItem: function (x) {
        var self = this;
        var itemsCounter = self.params.endItem - self.params.startItem + 1;
        var singleItemWidth = self.size.width / itemsCounter;
        return x / singleItemWidth;
    },

    move: function(x, y) {
        var self = this;
        var timeband = self.chart.timeband;
        var items = self.chart.currentItemsSet;

        if (!items || !timeband) return;

        //Get proper SVG renderer object.
        var renderer = self.chart.type.svgRenderer();
        if (!renderer) return;

        var visibleSize = this.ui.getVisibleSize();
        var offsetByItem = this.calculateOffsetByItem(x);


        renderer.calculatePaths(items, {
            width: visibleSize.width,
            height: visibleSize.height,
            startDate: self.params.startDate,
            startOffset: self.params.startOffset + offsetByItem,
            endDate: self.params.endDate,
            endOffset: self.params.endOffset + offsetByItem,
            timeband: timeband
        });

        self.render(renderer, visibleSize);

    },

    drawPaths: function (renderer) {
        var paths = renderer.getPathObjects();
        this.ui.drawPaths(paths);
    },

    drawLabels: function (renderer) {
        var labels = renderer.getLabels();
        this.ui.drawLabels(labels);
    },

    reset: function () {

        this.position = {
            startDate: null,
            startOffset: 0,
            endDate: null,
            endOffset: 0
        };

    }

};


function ChartHoverService(chart) {
    
    'use strict';

    var self = this;
    self.ChartHoverService = true;
    self.chart = chart;
    self.currentItem = null;
    self.detailsPanel = null;
    self.horizontalLine = null;
    self.verticalLine = null;
    self.valueIndicator = null;
    self.dateIndicator = null;
    self.crosshairPosition = {
        x: 0,
        y: 0
    };
    //self.crosshair = null;


    self.prepareUserInterface();

}
ChartHoverService.prototype = {

    prepareUserInterface: function () {
        var self = this;
        
        self.horizontalLine = $('<div/>', {
            'class': 'crosshair crosshair-horizontal'
        }).appendTo(self.chart.svg.ui.chartContainer);

        self.valueIndicator = $('<div/>', {
            'class': 'scale-indicator value-indicator'
        }).appendTo(self.chart.svg.ui.valuesContainer);

        self.verticalLine = $('<div/>', {
            'class': 'crosshair crosshair-vertical'
        }).appendTo(self.chart.svg.ui.chartContainer);
        
        self.dateIndicator = $('<div/>', {
            'class': 'scale-indicator date-indicator'
        }).appendTo(self.chart.svg.ui.datesContainer);

        self.detailsPanel = $('<div/>', {
            'class': 'chart-details-panel'
        }).appendTo(self.chart.svg.ui.chartContainer);

    },
    
    hover: function (e) {
        var self = this;

        self.locateCrosshair(e.x, e.y, e.source === self.chart);
        if (e.x > 0) {
            self.displayItemSummary(e.x);
            
        }
        

        //if (item) {
        //    self.currentItem = item;
        //}

    },
    
    locateCrosshair: function (x, y, horizontal) {
        var self = this;

        if (horizontal) {
            
            $(self.horizontalLine).css({
                'display': 'block'
            });

            if (y > 0) {
                $(self.horizontalLine).css({
                    'top': y + 'px'
                });
                self.crosshairPosition.y = y;
            }

            self.displayValueIndicator(y);


        } else {
            $(self.horizontalLine).css({
                'display': 'none'
            });

            $(self.valueIndicator).css({
                'display': 'none'
            });

        }


        if (x > 0) {
            $(self.verticalLine).css({
                'left': x + 'px'
            });
            self.crosshairPosition.x = x;
        }

    },

    displayValueIndicator: function (y) {
        var self = this;
        var p = self.chart.svg.params;

        //If params is null, chart is not loaded yet.
        if (p && y > 0) {
            var fromBottom = p.height - y;
            var value = (p.top - p.bottom) * (fromBottom / p.height) + p.bottom;

            $(self.valueIndicator).css({
                'display': 'block',
                'top': y + 'px'
            });
            $(self.valueIndicator).html(value.toFixed(2));

        }
    },
    
    displayDateIndicator: function(x, date) {
        var self = this;

        //If params is null, chart is not loaded yet.
        if (x > 0) {
            var value = mielk.dates.toString(date);

            $(self.dateIndicator).html(value);
            $(self.dateIndicator).css({
                'display': (value ? 'block' : 'none'),
                'left': x + 'px'
            });

        }
    },

    displayItemSummary: function(x) {
        var self = this;
        var chart = this.chart;

        //var item = mielk.arrays.firstGreater(chart.currentItemsSet, x, function ($i, $x) {
        var item = mielk.arrays.firstGreater(chart.svg.visibleItems, x, function ($i, $x) {
            if ($i.left > $x) return 1;
            if ($i.right < $x) return -1;
            return true;
        });


        if (item) {
            var labelFactory = this.chart.type.labelFactory();
            var labels = labelFactory.produceLabels(item);

            $(self.detailsPanel).empty();
            for (var i = 0; i < labels.length; i++) {
                var set = labels[i];
                set.appendTo(self.detailsPanel);
            }

            self.displayDateIndicator(x, item.date);

        }


    }

};