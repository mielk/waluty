function ChartController(params) {
    var self = this;
    self.ChartController = true;

    //State
    var company = params.initialCompany || STOCK.COMPANIES.getCompany(1);
    var timeband = params.initialTimeband || STOCK.TIMEBANDS.defaultValue();
    var showPeaks = params.showPeaks || true;
    var showTrendlines = params.showTrendlines || true;
    var indicators = {
        PRICE: params.showPriceChart || true,
        MACD: params.showMACDChart || true,
        ADX: params.showADXChart || true
    };
    var optionPanel = (function (params) {

        var controls = {}

        function initialize() {
            getControls();
            updateView();
            loadCompanyOptions();
            loadTimebandOptions();
            assignEvents();
        }

        function getControls() {
            controls.container = document.getElementById(params.optionPanelId);
            controls.companyDropdown = document.getElementById(params.companyDropdownId);
            controls.timebandDropdown = document.getElementById(params.timebandDropdownId);
            controls.showPeaksCheckbox = document.getElementById(params.showPeaksCheckboxId);
            controls.showTrendlinesCheckbox = document.getElementById(params.showTrendlinesCheckboxId);
            controls.showMACDCheckbox = document.getElementById(params.showMACDCheckboxId);
            controls.showADXCheckbox = document.getElementById(params.showADXCheckboxId);
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


        function updateView() {
            $(controls.showPeaksCheckbox).prop('checked', showPeaks);
            $(controls.showTrendlinesCheckbox).prop('checked', showTrendlines);
            $(controls.showMACDCheckbox).prop('checked', indicators.MACD);
            $(controls.showADXCheckbox).prop('checked', indicators.ADX);
        }

        function assignEvents() {
            //[Show peaks] checkbox.
            $(controls.showPeaksCheckbox).bind({
                click: function (e) {
                    var $this = $(this);
                    changeShowPeaksSetting($this.is(':checked'));
                }
            });

            //[Show trendlines] checkbox.
            $(controls.showTrendlinesCheckbox).bind({
                click: function (e) {
                    var $this = $(this);
                    changeShowTrendlinesSetting($this.is(':checked'));
                }
            });

            //[Show ADX] checkbox.
            $(controls.showADXCheckbox).bind({
                click: function (e) {
                    var $this = $(this);
                    changeShowADXSetting($this.is(':checked'));
                }
            });

            //[Show MACD] checkbox.
            $(controls.showMACDCheckbox).bind({
                click: function (e) {
                    var $this = $(this);
                    changeShowMACDSetting($this.is(':checked'));
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
    var chart = null;


    function initialize() {
        //Initialize chart object.
        chart = new ChartsContainer({
            controller: self,
            chartContainerId: params.chartsContainerId,
            company: company,
            timeband: timeband,
            showPeaks: showPeaks,
            showTrendlines: showTrendlines,
            showADX: indicators.ADX,
            showMACD: indicators.MACD
        });
        chart.load();
    }

    function run() {
        
    }

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

    function changeShowPeaksSetting(_value) {
        if (showPeaks != _value) {
            showPeaks = _value;
            self.trigger({
                type: 'showPeaks',
                value: showPeaks
            });
        }
    }

    function changeShowTrendlinesSetting(_value) {
        if (showTrendlines != _value) {
            showTrendlines = _value;
            self.trigger({
                type: 'showTrendlines',
                value: showTrendlines
            });
        }
    }

    function changeShowADXSetting(_value) {
        if (indicators.ADX != _value) {
            indicators.ADX = _value;
            self.trigger({
                type: 'showADX',
                value: indicators.ADX
            });
        }
    }

    function changeShowMACDSetting(_value) {
        if (indicators.MACD != _value) {
            indicators.MACD = _value;
            self.trigger({
                type: 'showMACD',
                value: indicators.MACD
            });
        }
    }


    //Public API.
    self.bind = function (e) {
        $(self).bind(e);
    };
    self.trigger = function (e) {
        $(self).trigger(e);
    };
    self.run = run;
    //self.changeCompany = changeCompany;
    //self.changeTimeband = changeTimeband;


    initialize();

}