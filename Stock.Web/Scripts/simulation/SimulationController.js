/*
 * Controller for managing charts.
 */
function SimulationController(params) {
    var self = this;
    self.SimulationController = true;


    var controls = {}

    function loadControls() {
        controls.runSimulationButton = document.getElementById(params.runAnalysisButtonId);
    }

    function assignEvents() {
        $(controls.runSimulationButton).bind({
            click: function (e) {

                mielk.db.fetch(
                    'Simulation',
                    'RunProcess',
                    {
                        
                    },
                    {
                        async: true,
                        callback: function (r) {
                            alert(r);
                        }
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

SimulationController.prototype = {
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