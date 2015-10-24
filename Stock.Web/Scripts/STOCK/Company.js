/*
 * Company object used in whole application.
 */
function Company(params) {
    var self = this;
    this.Company = true;
    this.eventHandler = mielk.eventHandler();
    this.id = params.Id || 0;
    this.name = params.Name || '';
    this.idMarket = params.IdMarket || 0;
    this.symbol = params.Short;
    this.lastUpdate = params.LastPriceUpdate;
    this.lastCalculation = params.LastCalculation;
    this.lastTrendlinesReview = params.LastTrendlinesReview;
    this.pricesChecked = params.PricesChecked;
    this.isFx = params.IsFx || false;
    this.displayed = this.name + (this.isFx ? '' : ' (' + this.symbol + ')');

    //Quotations.
    this.quotations = {};

}
Company.prototype = {
    toString: function () {
        return this.name + ' (' + this.id + ')';
    },
    bind: function(e){
        this.eventHandler.bind(e);
    },
    trigger: function (e) {
        this.eventHandler.trigger(e);
    },
    load: function (timeband, params) {
        var self = this;
        var results;
        if (!params) params = {};

        //Check if data are already loaded. If yes, there is no
        //point to re-load them. In this case only callback 
        //function is called.
        if (this.checkData(timeband, params)) {
            results = this.getQuotationsSet(timeband).getQuotations();
            if (mielk.objects.isFunction(params.callback)) {
                (params.callback)(results);
            }
            return;
        } 


        //Otherwise, the data are loaded from the database.
        results = mielk.db.fetch('Company',
            self.isFx ? 'GetFxQuotations' : 'GetQuotations',
            {
                pairSymbol: self.isFx ? self.name : self.id,
                timeband: self.isFx ? timeband.symbol : timeband.id,
                count: params.count || 0
            },
            {
                async: true,
                callback: function (r) {
                    var quotations = STOCK.QUOTATIONS.convertToQuotations(r);
                    self.setQuotations(timeband, quotations);

                    //Run additional callback functions if they are given.
                    if (mielk.objects.isFunction(params.callback)){
                        (params.callback)(quotations);
                    }

                }
            }
        );
    },
    checkData: function(timeband, params){
        var set = this.getQuotationsSet(timeband);

        if (!set) return false;

        return set.containsData(params);

    },
    getQuotationsSet: function(timeband){
        if (this.quotations.hasOwnProperty(timeband.id)) {
            return this.quotations[timeband.id];
        } else {
            return null;
        }
    },
    setQuotations: function (timeband, quotations) {
        var self = this;
        var set = this.getQuotationsSet(timeband);

        if (set) {

            //If set already exists, it has to be checked if it
            //already contains the given quotations.
            set.appendQuotations(quotations);

        } else {

            //New quotations set is created ...
            set = function(tb, q){
                var $timeband = tb;
                var $quotations = q;

                function startDate(){
                    return $quotations[0].date;
                }

                function endDate(){
                    return mielk.arrays.getLastItem($quotations).date;
                }

                function containsData(params) {
                    if (!params) return false;

                    //Sprawdza pod względem liczebności.
                    var size = params.size || params.count || params.length || 0;
                    if (size) {
                        return $quotations.length >= size;
                    }

                    //Sprawdza zakres dat.
                    var start = params.start || params.startDate || 0;
                    var end = params.end || params.endDate || 0;

                    return (startDate <= start && endDate >= end);

                }

                function appendQuotations($q) {

                    //TODO zmienić implementację - zakresy dat mogą na siebie nachodzić w różny sposób - 
                    //niekoniecznie jeden będzie się całkowicie zawierał w drugim.

                    var start = $q[0].date;
                    var end = mielk.arrays.getLastItem($q).date;

                    if (start < startDate || end > endDate) {
                        quotations = $q;

                        //Trigger an event of data reloading.
                        self.trigger({ type: 'reloaded' });

                    }

                }

                function getQuotations(params) {

                    //TODO zmienić implementację, żeby uwzględniała różne zakresy dat
                    //oraz umożliwiała pobranie wybranej ilości ostatnich notowań.

                    return quotations;

                }

                return {
                    getQuotations: getQuotations,
                    containsData: containsData,
                    appendQuotations: appendQuotations
                };

            }(timeband, quotations);
            
            //... and added to the collection of quotations sets.
            this.quotations[timeband.id] = set;

            //Trigger an event of data reloading.
            self.trigger({ type: 'reloaded' });

        }

    },
    getQuotations: function (timeband) {
        var set = this.getQuotationsSet(timeband);
        return set ? set.getQuotations() : [];
    }
    
};


$(function () {

        'use strict';

        var companies = (function () {

            var items = mielk.hashTable(null);


            function loadCompany(id) {
                var objects = mielk.db.fetch('Company', 'GetCompany', { 'id': id }, {});
                var company = new Company(objects);
                items.setItem(company.id, company);
                return company;
            }


            function getAll() {
                return items.values();
            }


            function getCompany(id) {
                return items.getItem(id) || loadCompany(id);
            }


            return {
                getAll: getAll,
                getCompany: getCompany
            };

        })();

        //Add as an item of STOCK library.
        STOCK.COMPANIES = companies;

    }

);