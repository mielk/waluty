function ChartsContainer(params) {

    'use strict';

    //[Meta]
    var self = this;
    self.ChartsContainer = true;
    var controller = params.controller;

    //[UI]
    var controls = {};

    //[Parameters].
    var company = params.company;
    var timeband = params.timeband;

    //[Quotations].
    //Collection of quotation sets for various timebands.
    //If [company] is reloaded, this object is cleared.
    var charts = {};

    //[Chart view parameters].
    var viewReady = false;
    var displayedQuotations = params.displayedQuotations || 200;
    var firstVisibleDate = params.firstVisibleDate;
    var lastVisibleDate = params.lastVisibleDate || mielk.dates.addDays(new Date(), 5);
    var minLevel = params.minLevel;
    var maxLevel = params.maxLevel;



    //[Initialize functions].
    function initialize() {
        loadControls();
        assignEvents();
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
        if (company !== _company) {
            company = _company;
            //Reset quotations set.
            reset();
            load();
        }
    }

    function changeTimeband(_timeband) {
        if (timeband !== _timeband) {
            timeband = _timeband;
            load();
        }
    }

    function reset() {
        //Remove all loaded views.
        $(controls.container).empty();

        //Remove all objects.
        charts = null;
        charts = {};
    }

    //[Loading functions].
    function load() {

        //Get the chart assigned to the current timeband.
        //If there is no such chart yet, create it and add to the collection.
        var chart = charts[timeband.symbol];
        if (!chart) {
            chart = new Chart({
                parent: self,
                type: STOCK.INDICATORS.PRICE,
                timeband: timeband,
                company: company,
                container: controls.container,
                displayDateScale: true
            });
            charts[timeband.symbol] = chart;
        }

        chart.activate();

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
