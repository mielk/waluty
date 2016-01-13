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
    self.timeband = params.timeband;

    //[Settings].
    //jakie wskaźniki mają być widoczne.
    //zoom


    //[Timeband charts].
    var activeTimebandChartsContainer;
    var timebandChartsContainersCache = {};


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
            changeTimeband: function (e) {
                changeTimeband(e.timeband);
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

    function changeTimeband(_timeband) {
        if (self.timeband !== _timeband) {
            self.timeband = _timeband;
            load();
        }
    }

    function reset() {
        //Remove all loaded views.
        $(controls.container).empty();

        //Clear data collections.
        activeTimebandChartsContainer = undefined;
        timebandChartsContainersCache = {};

    }

    //[Loading functions].
    function load() {

        //Get the chart assigned to the current timeband.
        var tcc = timebandChartsContainersCache[self.timeband.symbol];

        //If there is no such chart yet, create it and add to the collection.
        if (!tcc) {
            tcc = new TimebandChartsContainer({
                  parent: self
                //some params.
            });
        }


        //Hide the previous chart (if there was one established) ...
        if (activeTimebandChartsContainer)
            activeTimebandChartsContainer.deactivate();

        //... assign the new chart as the active one and display it.
        activeTimebandChartsContainer = tcc;
        activeTimebandChartsContainer.activate();

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
    self.load = load;

}
