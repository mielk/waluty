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