function ChartsContainer(params) {

    'use strict';

    //[Meta]
    var self = this;
    self.ChartsContainer = true;
    var controller = params.controller;

    //[UI]
    var controls = {};

    //[Parameters].
    self.company = params.company;
    self.timeframe = params.timeframe;
    self.simulationId = params.simulationId;

    //[Settings].
    self.settings = { };
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




    //jakie wskaźniki mają być widoczne.
    //zoom


    //[Timeframe charts].
    var activeTimeframeChartsContainer;
    var timeframeChartsContainersCache = {};


    //[Initialize functions].
    function initialize() {
        loadControls();
        assignEvents();
        reset();
    }

    function loadControls() {
        controls.container = document.getElementById(params.chartContainerId);
    }

    function assignEvents() {
        controller.bind({
            changeCompany: function (e) {
                changeCompany(e.company);
            },
            changeTimeframe: function (e) {
                changeTimeframe(e.timeframe);
            },
            changeSimulation: function (e) {

            },
            showMACD: function (e) {
                self.settings[STOCK.INDICATORS.MACD.name].visible = e.value;
            },
            showADX: function (e) {
                self.settings[STOCK.INDICATORS.ADX.name].visible = e.value;
            }
        });
    }

    function changeCompany(_company) {
        if (self.company !== _company) {
            self.company = _company;
            reset();
            load();
        }
    }

    function changeTimeframe(_timeframe) {
        if (self.timeframe !== _timeframe) {
            self.timeframe = _timeframe;
            load();
        }
    }

    function changeSimulationId(id) {
        if (self.simulationId !== id) {
            self.simulationId = id;
        }
    }

    function reset() {
        //Remove all loaded views.
        $(controls.container).empty();

        //Clear data collections.
        activeTimeframeChartsContainer = undefined;
        timeframeChartsContainersCache = {};

    }

    //[Loading functions].
    function load() {

        //Get the chart assigned to the current timeframe.
        var tcc = timeframeChartsContainersCache[self.timeframe.symbol];

        //If there is no such chart yet, create it and add to the collection.
        if (!tcc) {
            tcc = new TimeframeChartsContainer({
                  parent: self
                , container: controls.container
                //some params.
            });
            timeframeChartsContainersCache[self.timeframe.symbol] = tcc;
        }


        //Hide the previous chart (if there was one established) ...
        if (activeTimeframeChartsContainer)
            activeTimeframeChartsContainer.deactivate();

        //... assign the new chart as the active one and display it.
        activeTimeframeChartsContainer = tcc;
        activeTimeframeChartsContainer.activate();

    }



    //Public API.
    self.bind = function (e) {
        $(self).bind(e);
    }
    self.trigger = function (e) {
        $(self).trigger(e);
    }
    self.initialize = initialize;
    self.load = load;





    initialize();

}
