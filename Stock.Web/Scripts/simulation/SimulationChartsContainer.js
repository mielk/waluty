function SimulationChartsContainer(params) {

    'use strict';

    //[Meta]
    var self = this;
    self.SimulationChartsContainer = true;
    var controller = params.controller;

    //[Properties]
    var key = params.key || mielk.numbers.generateUUID();

    //[Parameters]
    self.company = function () {
        return params.company;
    }
    self.timeband = function () {
        return params.timeband;
    }

    //[Settings]
    self.settings = {};
    self.settings[STOCK.INDICATORS.PRICE.name] = {
        visible: true,
        properties: {
            trendlines: params.showTrendlines,
            peaks: params.showPeaks
        }
    };
    self.settings[STOCK.INDICATORS.MACD.name] = {
        visible: params.showMACD
    };
    self.settings[STOCK.INDICATORS.ADX.name] = {
        visible: params.showADX
    };

    //[Data sets]
    var dataSet;
    var properties;
    var quotations;

    //[UI]
    var controls = {};
    var timeScale;
    var charts = {};
    self.offset = {
        value: -50,
        min: -100,
        max: undefined
    }





    function runSimulation() {
        mielk.db.fetch(
            'Simulation',
            'InitializeSimulation',
            {
                pair: self.company().symbol,
                timeband: self.timeband().symbol
            },
            {
                async: true,
                callback: function (r) {
                    if (r.result) {
                        alert('Simulation object has been successfully loaded');
                    }
                }
            }
        );
    }



    //[Initialize functions].
    function initialize() {
        //Generate GUI.
        loadControls();
        assignEvents();
    }



    function loadControls() {
        controls.container = document.getElementById(params.chartContainerId);
    }

    function loadData() {

        //Load data set and its properties.
        dataSet = self.company().getDataSet(self.timeband());
        dataSet.loadProperties(loadProperties);

        //Draw actual chart.
        loadCharts();
        loadTimeScale();

        //Assign events. It must be placed here, after charts and time scale is already loaded.
        assignEvents();

        dataSet.loadQuotations(loadQuotations);

    }

    function assignEvents() {
        controller.bind({
            runSimulation: function (e) {
                runSimulation();
            },
            nextSimulationStep: function (e) {
                alert('next step');
            },
            previousSimulationStep: function (e) {
                alert('prev step');
            }
        });
    }

    function loadCharts() {
        addChart(STOCK.INDICATORS.PRICE, 0);
        //addChart(STOCK.INDICATORS.MACD, 1);
        //addChart(STOCK.INDICATORS.ADX, 2);
    }

    function addChart(type, index) {
        charts[type.name] = new Chart({
            parent: self,
            key: key,
            index: index,
            type: type,
            container: controls.container,
            visible: self.settings[type.name].visible,
            height: type.initialHeight,
            width: properties.width,
            properties: self.settings[type.name].properties
        });
    }

    function loadTimeScale() {
        //Load date scale. This is the first moment, when this is possible, because 
        //earlier it was not known how many quotations this chart will contain.
        timeScale = new ChartTimeScale({
            parent: self
            , container: controls.container
            , counter: properties.realQuotationsCounter
            , firstDate: properties.firstDate
            , lastDate: properties.lastDate
            , width: properties.width
        });
    }

    function loadProperties($properties) {
        properties = $properties;
        properties.width = properties.realQuotationsCounter * STOCK.CONFIG.candle.width;
    }

    function loadQuotations(result) {
        quotations = result;

        //Set max offset.
        self.offset.max = result.arr.length * STOCK.CONFIG.candle.width
                            - $(controls.container).width()
                            + STOCK.CONFIG.valueScale.width;

        //Propagate to charts.
        var arrCharts = mielk.arrays.fromObject(charts);
        arrCharts.forEach(function (chart) {
            chart.loadQuotations(result);
        });

    }




    function reset() {
        //    //Remove all loaded views.
        //    $(controls.container).empty();

        //    //Clear data collections.
        //    activeTimebandChartsContainer = undefined;
        //    timebandChartsContainersCache = {};

    }





    self.slide = function (offset) {
        var $offset = self.offset.value + offset;
        self.offset.value = Math.max(Math.min($offset, self.offset.max), self.offset.min);

        mielk.arrays.fromObject(charts).forEach(function (chart) {
            chart.slide(self.offset);
        });
    }

    self.scale = function () {
        mielk.arrays.fromObject(charts).forEach(function (chart) {
            chart.render();
        });
    }




    //Public API.
    self.bind = function (e) {
        $(self).bind(e);
    }
    self.trigger = function (e) {
        $(self).trigger(e);
    }
    self.initialize = initialize;
    self.parent = controller;


    initialize();
}