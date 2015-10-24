/*
 * Controller for managing charts.
 */

$(
    function () {

        'use strict';

        function ChartController() {
            var self = this;
            self.ChartController = true;
            self.eventHandler = mielk.eventHandler();
            //State
            self.company = undefined;
            self.timeband = STOCK.TIMEBANDS.D;
            self.parts = {
                PRICE: true,
                MACD: true,
                ADX: true
            };

            //GUI components
            self.optionsPanel = new OptionPanel(this);
            var chartsContainer = new ChartsContainer(this);

        }
        ChartController.prototype = {
            bind: function (e) {
                this.eventHandler.bind(e);
            },
            trigger: function (e) {
                this.eventHandler.trigger(e);
            },
            changeCompany: function ($company) {
                var self = this;
                self.company = $company;
                
                //Bind events to company.
                self.company.bind({
                    reloaded: function () {
                        self.eventHandler.trigger({
                            type: 'dataReloaded',
                            company: self.company,
                            timeband: self.timeband
                        });
                    }
                });

                self.eventHandler.trigger({
                    type: 'changeCompany',
                    company: $company,
                    timeband: self.timeband
                });

            },
            changeTimeband: function ($timeband) {
                this.timeband = $timeband;
                this.eventHandler.trigger({
                    type: 'changeTimeband',
                    timeband: $timeband,
                    company: self.company
            });
            }
        };

        var controller = new ChartController();

    }

);