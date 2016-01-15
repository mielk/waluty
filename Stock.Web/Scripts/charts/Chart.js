//Each object of [Chart] class represents a chart (all div's required for a single chart)
//for a single timeband.
function Chart(params) {

    'use strict';

    //[Meta].
    var self = this;
    self.Chart = true;

    //Properties.
    var parent = params.parent;
    var type = params.type;
    var index = params.index;
    var key = params.key + '_' + params.type.name;

    //UI.
    //var svg = new SvgPanel(self);
    var controls = { };



    function initialize() {
        //Generate GUI and assign events.
        loadControls();
        assignEvents();

        //Draw actual chart.

    }

    function loadControls() {
        controls.container = $('<div/>', {
            'class': 'chart-container'
        }).css({
            'background-color': params.type.color,
            'height': params.settings.height + 'px'
        }).appendTo(params.container);

        var html = parent.parent.company.displayed + ' | ' + parent.parent.timeband.symbol + ' | ' + params.type.name ;
        $(controls.container).html(html);

    }

    function assignEvents() {

    }

    function activate() {
        $(controls.container).css({
            'display': 'block',
            'visibility': 'visible'
        });
    }

    function deactivate() {
        $(controls.container).css({
            'display': 'none',
            'visibility': 'hidden'
        });
    }


    initialize();






    //Public API.
    self.activate = activate;
    self.deactivate = deactivate;
    self.bind = function (e) {
        $(self).bind(e);
    }
    self.trigger = function (e) {
        $(self).trigger(e);
    }

}






//function Chart(params) {
//    self.key = params.key;
//    self.controller = params.controller;
//    self.div = mielk.resizableDiv({
//        parent: params.container,
//        id: params.key || 'key',
//        minHeight: params.minHeight || 200,
//        maxHeight: params.maxHeight || 800,
//        height: params.height || 0,
//        'class': 'chart'
//    });
//    self.items = mielk.hashTable();
//    self.currentItemsSet = null;
//    self.displayDateScale = self.type.displayDateScale;

//    //Create SVG manager.
//    self.svg = new SvgPanel(self);    

//    //Cursor.
//    self.hoverService = new ChartHoverService(self);

//    //Drawing layer.
//    self.drawLayer = new ChartDrawLayer(self);
    

//    //Events listener.
//    (function eventsListener() {


//        //Changing company or timeband.
//        var companyChangeHandler = (function() {
//            self.controller.bind({
//                'changeCompany changeTimeband': function (e) {
//                    self.svg.reset();
//                    self.company = e.company || self.company;
//                    self.timeband = e.timeband || self.timeband;
//                }
//            });
//        })();


//        //Resizing chart panel.
//        var resizerHandler = (function() {
//            self.div.bind({
//                resize: function (e) {
//                    self.svg.resize();
//                }
//            });
//            $(window).resize(function () {
//                self.svg.resize();
//            });
//        })();


//        //Hovering.
//        var scrollHandler = (function () {

//            self.parent.bind({
//                moveChart: function(e) {
//                    self.svg.move(e.x, e.y);
//                }
//            });

//        })();


//        var scaleHandler = (function() {

//        })();

//        var hoverHandler = (function () {
//            //$(self.svg.ui.chartContainer).bind({
//            self.drawLayer.bind({
//                mousemove: function (e) {
//                    self.parent.trigger({
//                        type: 'hoverItem',
//                        pageX: e.pageX,
//                        pageY: e.pageY,
//                        x: e.offsetX,
//                        y: e.offsetY,
//                        source: self
//                    });
//                }
//            });


//            self.parent.bind({
//                hoverItem: function (e) {
//                    self.hoverService.hover(e);
//                }
//            });


//        })();


        
//    })();

//}
//Chart.prototype = {
//    injectQuotations: function (timeband, quotations, reload) {
//        var analyzer = this.type.analyzer();
//        var items = analyzer.run(quotations);
//        this.items.setItem(timeband.id, items);
//        this.currentItemsSet = items;
//        if (reload) this.reload(timeband);
//    },
//    reload: function (timeband) {
//        var items = this.items.getItem(timeband.id);
//        this.currentItemsSet = items;
//        this.svg.reload(timeband, items);
//    },
//    content: function () {
//        return this.div.content();
//    },
//    getItems: function (timeband) {
//        if (timeband) {
//            return this.items.getItem(timeband.id) || [];
//        } else {
//            return this.currentItemsSet || [];
//        }
//    }
//};
















//function ChartDrawLayer(chart) {

//    'use strict';

//    var self = this;
//    self.ChartDrawLayer = true;
//    self.chart = chart;
//    self.leftClicked = false;
//    self.rightClicked = false;
//    self.resetPosition();

//    self.container = $('<div/>', {        
//        'class': 'draw-layer'
//    }).appendTo(self.chart.content());

//    //Event listener.
//    $(self.container).bind({
//        mousedown: function (e) {
//            self.leftClicked = (e.which === 1);
//            self.rightClicked = (e.which === 3);
//            self.position = {
//                x: e.offsetX,
//                y: e.offsetY
//            };
//            mielk.notify.display('clicked | x: ' + self.position.x + '; y: ' + self.position.y);
//        },
//        mouseup: function(e) {
//            self.leftClicked = false;
//            self.rightClicked = false;
//            self.resetPosition();
//            mielk.notify.display('released');
//        },
//        mousemove: function (e) {
//            if (self.leftClicked) {
                
//                //Przesuwanie wykresem.
//                var position = { x: e.offsetX, y: e.offsetY };
//                var offset = {
//                    x: self.position.x - position.x,
//                    y: self.position.y - position.y
//                };
//                self.chart.parent.trigger({
//                    type: 'moveChart',
//                    x: offset.x,
//                    y: offset.y
//                });
                
//            } else if (self.rightClicked) {
//                //Skalowanie wykresu.
//            } else {
//                //Rysowanie linii trendu.
//            }
//        },
//        leave: function(e) {
//            mielk.notify.display('leaved');
//        }
//    });

//}

//ChartDrawLayer.prototype = {    
//    trigger: function(e) {
//        this.container.trigger(e);
//    },
//    bind: function(e) {
//        this.container.bind(e);
//    },
//    resetPosition: function() {
//        this.position = { x: 0, y: 0 };
//    }
//};


//function ChartHoverService(chart) {
    
//    'use strict';

//    var self = this;
//    self.ChartHoverService = true;
//    self.chart = chart;
//    self.currentItem = null;
//    self.detailsPanel = null;
//    self.horizontalLine = null;
//    self.verticalLine = null;
//    self.valueIndicator = null;
//    self.dateIndicator = null;
//    self.crosshairPosition = {
//        x: 0,
//        y: 0
//    };
//    //self.crosshair = null;


//    self.prepareUserInterface();

//}
//ChartHoverService.prototype = {

//    prepareUserInterface: function () {
//        var self = this;
        
//        self.horizontalLine = $('<div/>', {
//            'class': 'crosshair crosshair-horizontal'
//        }).appendTo(self.chart.svg.ui.chartContainer);

//        self.valueIndicator = $('<div/>', {
//            'class': 'scale-indicator value-indicator'
//        }).appendTo(self.chart.svg.ui.valuesContainer);

//        self.verticalLine = $('<div/>', {
//            'class': 'crosshair crosshair-vertical'
//        }).appendTo(self.chart.svg.ui.chartContainer);
        
//        self.dateIndicator = $('<div/>', {
//            'class': 'scale-indicator date-indicator'
//        }).appendTo(self.chart.svg.ui.datesContainer);

//        self.detailsPanel = $('<div/>', {
//            'class': 'chart-details-panel'
//        }).appendTo(self.chart.svg.ui.chartContainer);

//    },
    
//    hover: function (e) {
//        var self = this;

//        self.locateCrosshair(e.x, e.y, e.source === self.chart);
//        if (e.x > 0) {
//            self.displayItemSummary(e.x);
            
//        }
        

//        //if (item) {
//        //    self.currentItem = item;
//        //}

//    },
    
//    locateCrosshair: function (x, y, horizontal) {
//        var self = this;

//        if (horizontal) {
            
//            $(self.horizontalLine).css({
//                'display': 'block'
//            });

//            if (y > 0) {
//                $(self.horizontalLine).css({
//                    'top': y + 'px'
//                });
//                self.crosshairPosition.y = y;
//            }

//            self.displayValueIndicator(y);


//        } else {
//            $(self.horizontalLine).css({
//                'display': 'none'
//            });

//            $(self.valueIndicator).css({
//                'display': 'none'
//            });

//        }


//        if (x > 0) {
//            $(self.verticalLine).css({
//                'left': x + 'px'
//            });
//            self.crosshairPosition.x = x;
//        }

//    },

//    displayValueIndicator: function (y) {
//        var self = this;
//        var p = self.chart.svg.params;

//        //If params is null, chart is not loaded yet.
//        if (p && y > 0) {
//            var fromBottom = p.height - y;
//            var value = (p.top - p.bottom) * (fromBottom / p.height) + p.bottom;

//            $(self.valueIndicator).css({
//                'display': 'block',
//                'top': y + 'px'
//            });
//            $(self.valueIndicator).html(value.toFixed(2));

//        }
//    },
    
//    displayDateIndicator: function(x, date) {
//        var self = this;

//        //If params is null, chart is not loaded yet.
//        if (x > 0) {
//            var value = mielk.dates.toString(date);

//            $(self.dateIndicator).html(value);
//            $(self.dateIndicator).css({
//                'display': (value ? 'block' : 'none'),
//                'left': x + 'px'
//            });

//        }
//    },

//    displayItemSummary: function(x) {
//        var self = this;
//        var chart = this.chart;

//        //var item = mielk.arrays.firstGreater(chart.currentItemsSet, x, function ($i, $x) {
//        var item = mielk.arrays.firstGreater(chart.svg.visibleItems, x, function ($i, $x) {
//            if ($i.left > $x) return 1;
//            if ($i.right < $x) return -1;
//            return true;
//        });


//        if (item) {
//            var labelFactory = this.chart.type.labelFactory();
//            var labels = labelFactory.produceLabels(item);

//            $(self.detailsPanel).empty();
//            for (var i = 0; i < labels.length; i++) {
//                var set = labels[i];
//                set.appendTo(self.detailsPanel);
//            }

//            self.displayDateIndicator(x, item.date);

//        }


//    }

//};