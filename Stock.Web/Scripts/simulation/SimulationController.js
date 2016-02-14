/*
 * Controller for managing charts.
 */
function SimulationController(params) {
    var self = this;
    self.SimulationController = true;

    var company = params.initialCompany || STOCK.COMPANIES.getCompany(1);
    var timeband = params.initialTimeband || STOCK.TIMEBANDS.defaultValue();
    var controls = {}

    var optionPanel = (function (params) {

        var controls = {}

        function initialize() {
            getControls();
            loadCompanyOptions();
            loadTimebandOptions();
            assignEvents();
        }

        function getControls() {
            controls.container = document.getElementById(params.optionPanelId);
            controls.companyDropdown = document.getElementById(params.companyDropdownId);
            controls.timebandDropdown = document.getElementById(params.timebandDropdownId);
            controls.runSimulationButton = document.getElementById(params.runSimulationButtonId);
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
            //$(controls.timebandDropdown).select2();

        }

        function loadTimebandOptions() {
            var timebands = params.timebands || STOCK.TIMEBANDS.getValues();

            for (var iterator in timebands) {
                var item = timebands[iterator];
                var option = 1;
                $(controls.timebandDropdown).append($('<option>', {
                    value: item.id,
                    text: item.name,
                    selected: (timeband && item.id === timeband.symbol ? true : false)
                }));
            }

            //Convert it into Select2.
            //$(controls.timebandDropdown).select2();

        }


        function assignEvents() {
            //[Run simulation].
            $(controls.runSimulationButton).bind({
                click: function (e) {

                    mielk.db.fetch(
                        'Simulation',
                        'RunProcess',
                        {
                            pair: company.symbol,
                            timeband: timeband.symbol,
                            value: 1
                        },
                        {
                            async: true,
                            callback: function (r) {
                                alert(r.value);
                            }
                        }
                    );

                }
            });

            //[Change company].
            $(controls.companyDropdown).bind({
                change: function (e) {
                    changeCompany(this.value);
                }
            });


            //[Change timeband].
            $(controls.timebandDropdown).bind({
                change: function (e) {
                    changeTimeband(this.value);
                }
            });

        }


        initialize();


        return {

        };

    })(params);




    function changeCompany(id) {
        company = STOCK.COMPANIES.getCompany(id);
        self.trigger({
            type: 'changeCompany',
            timeband: timeband,
            company: company
        });
    }

    function changeTimeband(id) {
        timeband = STOCK.TIMEBANDS.getItem(id);
        self.trigger({
            type: 'changeTimeband',
            timeband: timeband,
            company: company
        });
    }


    function initialize() {
        ////Initialize chart object.
        //chart = new ChartsContainer({
        //    controller: self,
        //    chartContainerId: 'charts-container',
        //    company: company,
        //    timeband: timeband,
        //    showPeaks: showPeaks,
        //    showTrendlines: showTrendlines,
        //    showADX: indicators.ADX,
        //    showMACD: indicators.MACD
        //});
        //chart.load();
    }


    function run() {

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