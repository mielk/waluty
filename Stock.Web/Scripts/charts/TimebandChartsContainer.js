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

    //Data sets.
    var dataSet;
    var properties;

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
        dataSet.loadQuotations(loadQuotations);

        //Draw actual chart.
        loadCharts();
        loadTimeScale();

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
        properties.width = properties.realQuotationsCounter * STOCK.CONFIG.candle.initialWidth;
    }

    function loadQuotations(quotations) {

        ////Create SVG Container if it has not been created before...
        //if (!svgContainer) createSvgContainer({
        //    parent: self
        //});
        ////... and invoke its method [loadQuotations] with the quotations
        ////passed as an input parameter.
        //svgContainer.loadQuotations(quotations);

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
        addChart(STOCK.INDICATORS.PRICE);
        addChart(STOCK.INDICATORS.MACD);
        addChart(STOCK.INDICATORS.ADX);
    }

    function addChart(type) {
        charts[type] = new Chart({
            parent: self,
            key: key,
            type: type,
            container: controls.container,
            visible: parent.settings[type.name].visible,
            height: type.initialHeight,
            width: properties.width
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