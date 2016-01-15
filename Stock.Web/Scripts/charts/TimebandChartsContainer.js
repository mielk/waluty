//Each object of [Chart] class represents a chart (all div's required for a single chart)
//for a single timeband.
function TimebandChartsContainer(params) {

    'use strict';

    //[Meta].
    var self = this;
    self.TimebandChartsContainer = true;

    //Properties.
    var parent = params.parent;
    var key = params.key || mielk.numbers.generateUUID();
    //self.singleItemWidth = STOCK.CONFIG.candle.width;

    //Data sets.
    var dataSet;
    var properties;
    var quotations;

    //UI.
    var controls = { };
    var timeScale;
    var charts = { };



    function initialize() {
        //Generate GUI and assign events.
        loadControls();
        assignEvents();

        //Load data set and its properties.
        dataSet = parent.company.getDataSet(parent.timeband);
        dataSet.loadProperties(loadProperties);

        //Draw actual chart.
        loadCharts();
        loadTimeScale();

        dataSet.loadQuotations(loadQuotations);

    }

    function loadControls() {
        controls.container = $('<div/>', {
            'class': 'timeband-charts-container',
            id: 'chart-container-' + key
        }).css({
            'background-color': 'red'
        }).appendTo(params.container);
    }

    function assignEvents() {

    }

    function loadProperties($properties) {
        properties = $properties;
        properties.width = properties.realQuotationsCounter * STOCK.CONFIG.candle.width;
    }

    function loadQuotations($quotations) {
        quotations = $quotations;

        //Propagate to charts.
        var arrCharts = mielk.arrays.fromObject(charts);
        arrCharts.forEach(function (chart) {
            chart.loadQuotations(quotations);
        });

    }

    function loadTimeScale() {
        //Load date scale. This is the first moment, when this is possible, because 
        //earlier it was not known how many quotations this chart will contain.
        timeScale = new TimeScale({
            parent: self
            , container: controls.container
            , counter: properties.realQuotationsCounter
            , firstDate: properties.firstDate
            , lastDate: properties.lastDate
            , width: properties.width
        });
    }

    function loadCharts() {
        addChart(STOCK.INDICATORS.PRICE, 0);
        addChart(STOCK.INDICATORS.MACD, 1);
        addChart(STOCK.INDICATORS.ADX, 2);
    }

    function addChart(type, index) {
        charts[type.name] = new Chart({
            parent: self,
            key: key,
            index: index,
            type: type,
            container: controls.container,
            visible: parent.settings[type.name].visible,
            height: type.initialHeight,
            width: properties.width,
            properties: parent.settings[type.name].properties
        });
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




    //Public API.
    self.bind = function (e) {
        $(self).bind(e);
    }
    self.trigger = function (e) {
        $(self).trigger(e);
    }
    self.activate = activate;
    self.deactivate = deactivate;
    self.parent = parent;




    initialize();

}



function TimeScale(params) {

    'use strict';

    var self = this;
    self.TimeScale = true;
    self.parent = params.parent;

    //Properties.
    self.counter = params.counter;
    self.firstDate = params.firstDate;
    self.lastDate = params.lastDate;


    var controls = {};


    function initialize() {
        loadControls();
        assignEvents();
    }

    //określić minimalną szerokość - nie może być niższa niż szerokość całego panelu.

    function loadControls() {
        controls.container = $('<div/>', {
            'class': 'date-scale-container'
        }).css({
            'height': STOCK.CONFIG.timeScale.height + 'px',
            'padding-right': STOCK.CONFIG.valueScale.width + 'px'
        }).appendTo(params.container);

        controls.visible = $('<div/>', {
            'class': 'date-scale-visible'
        }).css({
            'right': STOCK.CONFIG.valueScale.width + 'px'
        }).appendTo(controls.container);

        controls.labels = $('<div/>', {
            'class': 'date-scale-labels'
        }).css({
            'width': params.width + 'px'
        }).appendTo(controls.visible);

    }
    
    function assignEvents() {

    }

    initialize();

}