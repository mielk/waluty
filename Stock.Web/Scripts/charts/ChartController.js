/*
 * Controller for managing charts.
 */
function ChartController(params) {
    var self = this;
    self.ChartController = true;

    //State
    var company = params.initialCompany || STOCK.COMPANIES.getCompany(1);
    var timeband = params.initialTimeband || STOCK.TIMEBANDS.D;
    var showPeaks = params.showPeaks || false;
    var showTrendlines = params.showTrendlines || true;
    var parts = {
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
        }

        function loadCompanyOptions() {
            var companies = STOCK.COMPANIES.getList();

            for (var iterator in companies) {
                var item = companies[iterator];
                var option = 1;
                $(controls.companyDropdown).append($('<option>', {
                    value: item.id,
                    text: item.name,
                    selected: (company && item.id === company.id ? true : false)
                }));
            }

        }

        function loadTimebandOptions() {
            var timebands = STOCK.TIMEBANDS.getValues();

            for (var iterator in timebands) {
                var item = timebands[iterator];
                var option = 1;
                $(controls.timebandDropdown).append($('<option>', {
                    value: item.id,
                    text: item.name,
                    selected: (timeband && item.id === timeband.id ? true : false)
                }));
            }
        }


        function updateView() {
            $(controls.showPeaksCheckbox).prop('checked', showPeaks);
            $(controls.showTrendlinesCheckbox).prop('checked', showTrendlines);
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

        }


        initialize();


        return {

        };

    })(params);



    function run() {

    }

    function changeCompany(_company) {
        company = _company;

        ////Bind events to company.
        //self.company.bind({
        //    reloaded: function () {
        //        self.trigger({
        //            type: 'dataReloaded',
        //            company: self.company,
        //            timeband: self.timeband
        //        });
        //    }
        //});

        //self.trigger({
        //    type: 'changeCompany',
        //    company: $company,
        //    timeband: self.timeband
        //});

    }

    function changeTimeband(_timeband) {
        timeband = _timeband;
        self.trigger({
            type: 'changeTimeband',
            timeband: timeband,
            company: company
        });
    }

    function changeShowPeaksSetting(_value) {
        if (showPeaks != _value) {
            showPeaks = _value;
            alert('Show peaks setting changed to ' + _value);
        }
    }

    function changeShowTrendlinesSetting(_value) {
        if (showTrendlines != _value) {
            showTrendlines = _value;
            alert('Show trendlines setting changed to ' + _value);
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

}


function OptionPanel(controller) {

    'use strict';

    var self = this;
    self.OptionPanel = true;
    self.controller = controller;


    //GUI components.
    self.container = $('#options-panel')[0];

    //Company dropdown.
    var companyDropdown = (function () {

        function assetFormatResult(option) {
            //var asset = option.asset;
            //var flag = STOCK.MARKETS.getCssClass(asset.IdMarket);
            //var markup = '<table class="company-result"><tr>';
            ////markup += "<td class='company-image'><img class='company-flag' src='/images/flags/" + flag + "'/></td>";
            //markup += '<td class="company-image"><div class="company-flag ' + flag + '"></td>';
            //markup += '<td class="company-info"><div class="company-name">' + option.name + '</div></td>';
            //markup += '<td class="company-info"><div class="company-symbol">' + company.Short + '</div></td>';
            //markup += '</tr></table>';
            //return markup;

            return option.name;

        }

        function assetFormatSelection(asset) {
            return asset.name;
        }



        var dropdown = $('#companies-dropdown')[0];
        var data;
        mielk.db.fetch('Company', 'FilterCompanies', {
            q: dropdown.value,
            limit: 10
        }, {
            async: false,
            callback: function (r) {
                data = r.items;
            }
        });


        $(dropdown).select2({
            placeholder: 'Search',
            minimumInputLength: 1,
            data: data,
            //ajax: {
            //    url: '/Company/FilterCompanies',
            //    type: 'GET',
            //    dataType: 'json',
            //    data: function (term, page) {
            //        return {
            //            q: term,
            //            limit: 10
            //        };
            //    },
            //    results: function (data, page) {
            //        return { results: data.items };
            //    }
            //},
            formatResult: assetFormatResult, // omitted for brevity, see the source of this page
            formatSelection: assetFormatSelection,  // omitted for brevity, see the source of this page
            dropdownCssClass: 'bigdrop', // apply css that makes the dropdown taller
            escapeMarkup: function (m) { return m; } // we do not want to escape markup since we are displaying html in results
        });

        $(dropdown).bind({
            change: function (e) {
                var asset = STOCK.COMPANIES.getCompany(e.val);
                self.controller.changeCompany(asset);
            }
        });

    })();


    //Timeband dropdown.
    var timebandDropdown = (function () {
        var dropdown = $('#timebands-dropdown')[0];
        var def = STOCK.TIMEBANDS.defaultValue();

        function format(item) {
            return item.name + ' (' + item.symbol + ')';
        }

        var timebands = STOCK.TIMEBANDS.getValues();
        $(dropdown).select2({
            data: timebands,
            formatSelection: format,
            formatResult: format
            //initSelection: function (element, callback) {
            //    var data = { id: element.val(), text: element.val() };
            //    callback(data);
            //}
        });
        $(dropdown).select2('val', $('.select2 option:eq(1)').val());

        $(dropdown).bind({
            change: function (e) {
                self.controller.changeTimeband(e.added.object);
            }
        });

    })();





    //self.companyDropdown = new DropDown({
    //    container: $('#companies-dropdown')[0],
    //    data: STOCK.COMPANIES.getAll(),
    //    slots: 10,
    //    caseSensitive: false,
    //    confirmWithFirstClick: true
    //});
    //self.companyDropdown.bind({
    //    change: function (e) {
    //        self.controller.changeCompany(e.item);
    //    }
    //});





}