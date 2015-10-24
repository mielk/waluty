/*
* OptionPanel
* Class representing option panel on the top of charts page.
*/
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




