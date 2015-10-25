function ChartFX(params) {

    'use strict';

    //[Meta]
    var self = this;
    self.Chart = true;
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
            var dataSet = company.getDataSet(timeband);
            chart = new ChartContent({
                parent: self,
                timeband: timeband,
                company: company,
                container: controls.container
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



//Each object of [ChartContent] class represents a chart (all div's required for a single chart)
//for a single timeband.
function ChartContent(params) {

    'use strict';

    //[Meta].
    var self = this;
    self.ChartContent = true;

    //Properties.
    var parent = params.parent;
    var timeband = params.timeband;
    var company = params.company;
    var dataSet;
    var properties;

    //UI.
    var controls = {
        parentContainer: params.container
    };




    function initialize() {
        //Generate GUI and assign events.
        loadControls();
        assignEvents();

        //Load data set and its properties.
        loadDataSet();

        //Trigger data.


        //Draw actual chart.

    }

    function loadControls() {
        controls.container = $('<div/>', {
            'class': 'chart-container'
        }).css({
            'background-color' : 'red'
        }).appendTo(controls.parentContainer);
    }

    function assignEvents() {

    }

    function loadDataSet() {
        //Get the properties from the [dataSet] obtained above.
        dataSet = company.getDataSet(timeband);
        properties = dataSet.getProperties();

        var html = '   counter: ' + properties.counter +
            ' | firstDate: ' + mielk.dates.toString(properties.firstDate, true) + ' (' + properties.firstDate.getDay() + ')' +
            ' | lastDate: ' + mielk.dates.toString(properties.lastDate, true) + ' (' + properties.lastDate.getDay() + ')' +
            ' | minPrice: ' + properties.minLevel +
            ' | maxPrice: ' + properties.maxLevel;
        $(controls.container).html(html);

    }





    function activate() {
        $(controls.parentContainer).children().each(function () {
            $(this).css({
                'display': 'none'
            });
        });

        $(controls.container).css({
            'display': 'block',
            'visibility': 'visible'
        });

    }

    initialize();


    //Public API.
    self.activate = activate;

}



/*
* ChartsContainer
* Class to manager containers for differents parts of the technical analysis.
*/
function ChartsContainer(controller) {

    'use strict';

    var self = this;
    self.ChartsContainer = true;
    self.controller = controller;
    self.eventHandler = mielk.eventHandler();
    self.charts = mielk.hashTable(null);
    self.parts = controller.parts;
    self.loadedTimebands = {};

    //GUI components.
    self.ui = (function () {
        var container = $('#charts-container')[0];

        for (var key in self.parts) {
            if (self.parts.hasOwnProperty(key)) {
                var type = STOCK.INDICATORS.getItem(key);
                var chart = new Chart({
                    parent: self,
                    controller: controller,
                    container: container,
                    key: key,
                    height: type.initialHeight,
                    type: type
                });
                self.charts.setItem(chart.key, chart);
            }
        }

        return {
            container: container
        };

    })();
    

    //Events listener.
    (function eventsListener() {

        self.controller.bind({
            changeCompany: function (e) {
                self.trigger({
                    type: 'changeCompany',
                    company: e.company
                });
                reset();
                loadData(e.company, self.controller.timeband);
            },
            changeTimeband: function (e) {
                self.trigger({
                    type: 'changeTimeband',
                    timeband: e.timeband
                });
                loadData(self.controller.company, e.timeband);
            }
        });

    })();


    function reset() {
        self.loadedTimebands = {};
    }

    function isTimebandLoaded(timeband) {
        return self.loadedTimebands[timeband.id] ? true : false;
    }

    function loadData(company, timeband) {

        //Only if both parameters are given, data can be loaded.
        if (!company || !timeband) return;

        //Show spinners informing about process in progress.
        mielk.spinner.start();

        if (isTimebandLoaded(timeband)) {
            reloadCharts(timeband);
            mielk.spinner.stop();
        } else {
            //Loading first data from the database.
            company.load(timeband, {
                count: 300,
                callback: function (results) {

                    propagateQuotations(timeband, results, true);
                    mielk.spinner.stop();

                    //Loading all data.
                    company.load(timeband, {
                        callback: function ($results) {
                            self.loadedTimebands[timeband.id] = true;
                            propagateQuotations(timeband, $results, false);
                        }
                    });
                }
            });
        }

    }

    function propagateQuotations(timeband, results, reload) {
        //Propagate quotations to charts.
        self.charts.each(function (key, chart) {
            chart.injectQuotations(timeband, results, reload);
        });
    }


    function reloadCharts(timeband) {
        self.charts.each(function (key, chart) {
            chart.reload(timeband);
        });
    }


    ////Loading data.
    //function reloadData() {
    //    var timeband = self.controller.timeband;
    //    var company = self.controller.company;

    //    //Show spinners informing about process in progress.
    //    mielk.spinner.start();

    //    //Loading first data from the database.
    //    company.load(timeband, {
    //        count: 300,
    //        callback: function (results) {
    //            mielk.spinner.stop();
    //            //Loading all data.
    //            company.load(timeband);
    //        }
    //    });

    //}


    //function reloadQuotations(results) {
    //    var quotations = STOCK.QUOTATIONS.convertToQuotations(results);
    //    company.setQuotations(timeband, quotations);
    //    self.controller.trigger({
    //        type: 'dataReloaded'
    //    });
    //}


}
ChartsContainer.prototype = {
    bind: function (e) {
        this.eventHandler.bind(e);
    },
    trigger: function (e) {
        this.eventHandler.trigger(e);
    }
};