/*
 * Controller for managing charts.
 */
function AnalysisController(params) {
    var self = this;
    self.AnalysisController = true;


    var controls = {}

    function loadControls() {
        controls.runAnalysisButton = document.getElementById(params.runAnalysisButtonId);
    }

    function assignEvents() {
        $(controls.runAnalysisButton).bind({
            click: function (e) {

                mielk.db.fetch(
                    'Process',
                    'RunProcess',
                    {
                        fromScratch: false
                    },
                    {
                        async: true
                    }
                );

            }
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