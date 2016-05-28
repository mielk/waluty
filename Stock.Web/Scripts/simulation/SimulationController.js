/*
 * Controller for managing charts.
 */
function SimulationController(params) {
    var self = this;
    self.SimulationController = true;

    var company = params.initialCompany || STOCK.COMPANIES.getCompany(1);
    var timeframe = params.initialTimeframe || STOCK.TIMEFRAMES.defaultValue();
    var controls = {}
    var incrementation = params.incrementation || 1;

    var optionPanel = (function (params) {

        var controls = {}

        function initialize() {
            getControls();
            loadCompanyOptions();
            loadTimeframeOptions();
            assignEvents();
        }

        function getControls() {
            controls.container = document.getElementById(params.optionPanelId);
            controls.companyDropdown = document.getElementById(params.companyDropdownId);
            controls.timeframeDropdown = document.getElementById(params.timeframeDropdownId);
            controls.runSimulationButton = document.getElementById(params.runSimulationButtonId);
            controls.simulationPrevButton = document.getElementById(params.simulationPrevButtonId);
            controls.simulationNextButton = document.getElementById(params.simulationNextButtonId);
        }

        function loadCompanyOptions() {
            var companies = params.companies || STOCK.COMPANIES.getList();

            companies.sort(function (a, b) {
                return a.name.localeCompare(b.name);
            });

            for (var iterator in companies) {
                var item = companies[iterator];
                var option = 1;
                $(controls.companyDropdown).append($('<option>', {
                    value: item.id,
                    text: item.name,
                    selected: (company && item.id === company.id ? true : false)
                }));
            }

            //Convert it into Select2.
            //$(controls.timeframeDropdown).select2();

        }

        function loadTimeframeOptions() {
            var timeframes = params.timeframes || STOCK.TIMEFRAMES.getValues();

            for (var iterator in timeframes) {
                var item = timeframes[iterator];
                var option = 1;
                $(controls.timeframeDropdown).append($('<option>', {
                    value: item.id,
                    text: item.name,
                    selected: (timeframe && item.id === timeframe.symbol ? true : false)
                }));
            }

            //Convert it into Select2.
            //$(controls.timeframeDropdown).select2();

        }


        function assignEvents() {
            //[Run simulation].
            $(controls.runSimulationButton).bind({
                click: function (e) {
                    runSimulation();
                }
            });


            //[Simulation previous step].
            $(controls.simulationPrevButton).bind({
                click: function (e) {
                    simulationPrevStep();
                }
            });


            //[Simulation next step].
            $(controls.simulationNextButton).bind({
                click: function (e) {
                    simulationNextStep();
                }
            });


            //[Change company].
            $(controls.companyDropdown).bind({
                change: function (e) {
                    changeCompany(this.value);
                }
            });


            //[Change timeframe].
            $(controls.timeframeDropdown).bind({
                change: function (e) {
                    changeTimeframe(this.value);
                }
            });

        }


        initialize();


        return {

        };

    })(params);
    var chart = null;



    function changeCompany(id) {
        company = STOCK.COMPANIES.getCompany(id);
        self.trigger({
            type: 'changeCompany',
            timeframe: timeframe,
            company: company
        });
    }

    function changeTimeframe(id) {
        timeframe = STOCK.TIMEFRAMES.getItem(id);
        self.trigger({
            type: 'changeTimeframe',
            timeframe: timeframe,
            company: company
        });
    }



    function runSimulation() {
        self.trigger({
            type: 'runSimulation',
            company: company,
            timeframe: timeframe
        });
    }

    function simulationNextStep() {
        self.trigger({
            type: 'nextSimulationStep'
        });
    }

    function simulationPrevStep() {
        self.trigger({
            type: 'previousSimulationStep'
        });
    }

    

    function initialize() {
        //Initialize chart object.
        chart = new SimulationChartsContainer({
            controller: self,
            chartContainerId: params.chartsContainerId,
            company: company,
            timeframe: timeframe,
            showPeaks: true,
            showTrendlines: true,
            showADX: false,
            showMACD: true
        });
    }


    function run() {
    }


    //Public API.
    self.initialize = initialize;
    self.incrementation = incrementation;

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