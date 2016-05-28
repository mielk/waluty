/*
 * Controller for managing charts.
 */
function AnalysisController(params) {
    var self = this;
    self.AnalysisController = true;
    self.fromScratch = params.fromScratch || false; //Zmienić, żeby odczytywało z check boxa.


    var controls = {}

    function loadControls() {
        controls.runAnalysisButton = document.getElementById(params.runAnalysisButtonId);
    }

    function assignEvents() {
        $(controls.runAnalysisButton).bind({
            click: function (e) {
                runAnalysis({

                });
            }
        });
    }


    function runAnalysis(params) {
        var fromScratch = params.fromScratch || false;
        var assetsList = STOCK.COMPANIES.getList();
        var timeframes = STOCK.TIMEFRAMES.getValues();

        assetsList.forEach(function (asset) {

            timeframes.forEach(function (timeframe) {

                mielk.db.fetch(
                    'Process',
                    'RunProcess',
                    {
                        asset: asset.name,
                        timeframe: timeframe.symbol,
                        fromScratch: false
                    },
                    {
                        async: true,
                        callback: function (result) {

                            if (result) {
                                mielk.notify.display(asset.name + ' ' + timeframe.symbol + ' successfully loaded', 1);
                            } else {
                                mielk.notify.display('Error when processing ' + asset.name + ' ' + timeframe.symbol, 0);
                            }

                        }
                    }
                );

            });

        });



    }


    function initialize() {
        loadControls();
        assignEvents();
    }


    //Public API.
    self.initialize = initialize;

}

AnalysisController.prototype = {
    bind: function (e) {
        $(this).bind(e);
    },
    trigger: function (e) {
        $(this).trigger(e);
    },
    run: function () {
        this.initialize();
    }
};