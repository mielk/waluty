﻿var mock = mock || {};

// Before running tests every \\ int VAT number should be changed to \\\\, otherwise ajax refuses to parse such json

mock.ProductJsonString = '[{"Id":1,"Code":"9999","Name":"Product 1","Unit":"hours"},{"Id":2,"Code":"1232","Name":"Product 2","Unit":"hours"},{"Id":3,"Code":"1233","Name":"Product 3","Unit":"hours"},{"Id":4,"Code":"1234","Name":"Product 4","Unit":"hours"},{"Id":5,"Code":"1235","Name":"Product 5","Unit":"hours"}]';
mock.InvoiceJsonString = '[{"TypedInvoiceItems":[],"Id":1,"Number":"VAT\\\\12\\\\34\\\\232","VatId":null,"CreationDate":"2013-02-13T00:00:00","IsAChainInvoice":false,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":null,"CustomerAddressMailing":null},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":4215.46,"InvoiceItems":[],"IsACorrectiveInvoice":false},{"TypedInvoiceItems":[],"Id":2,"Number":"VAT\\\\12\\\\34\\\\233","VatId":null,"CreationDate":"2013-02-14T00:00:00","IsAChainInvoice":false,"Customer":{"Id":2,"MPSMid":"12345672","HIMMSMid":"87654322","Name":"Merchant 2","VatId":"1234-56-782","CustomerAddressLegal":null,"CustomerAddressMailing":null},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":5430.60,"InvoiceItems":[],"IsACorrectiveInvoice":false},{"TypedInvoiceItems":[],"Id":3,"Number":"VAT\\\\12\\\\34\\\\234","VatId":null,"CreationDate":"2013-02-15T00:00:00","IsAChainInvoice":false,"Customer":{"Id":3,"MPSMid":"12345673","HIMMSMid":"87654323","Name":"Merchant 3","VatId":"1234-56-783","CustomerAddressLegal":null,"CustomerAddressMailing":null},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":1971.95,"InvoiceItems":[],"IsACorrectiveInvoice":false}]';
mock.VatRatesJsonString = '{"Rates":[0,5,8,23]}';

// We are simulating reading from invoice item 1
mock.InvoiceItemsJsonString = '[{"TypedInvoice":{"Id":1,"Number":"VAT\\\\12\\\\34\\\\232","VatId":null,"CreationDate":"2013-02-13T00:00:00","IsAChainInvoice":false,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":4215.46,"InvoiceItems":[{"Id":4,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":2,"Code":"1232","Name":"Product 2","Unit":"hours"},"PricePerUnitNet":147.00,"Quantity":1,"VatRate":18,"TotalPriceNet":147.00,"TotalVAT":0.00,"TotalPriceGross":173.46},{"Id":7,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":3,"Code":"1233","Name":"Product 3","Unit":"hours"},"PricePerUnitNet":1250.00,"Quantity":3,"VatRate":7,"TotalPriceNet":3750.00,"TotalVAT":0.00,"TotalPriceGross":4012.50}],"IsACorrectiveInvoice":false},"Invoice":{"Id":1,"Number":"VAT\\\\12\\\\34\\\\232","VatId":null,"CreationDate":"2013-02-13T00:00:00","IsAChainInvoice":false,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":4215.46,"InvoiceItems":[{"Id":4,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":2,"Code":"1232","Name":"Product 2","Unit":"hours"},"PricePerUnitNet":147.00,"Quantity":1,"VatRate":18,"TotalPriceNet":147.00,"TotalVAT":0.00,"TotalPriceGross":173.46},{"Id":7,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":3,"Code":"1233","Name":"Product 3","Unit":"hours"},"PricePerUnitNet":1250.00,"Quantity":3,"VatRate":7,"TotalPriceNet":3750.00,"TotalVAT":0.00,"TotalPriceGross":4012.50}],"IsACorrectiveInvoice":false},"Id":1,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":1,"Code":"1231","Name":"Product 1","Unit":"hours"},"PricePerUnitNet":12.50,"Quantity":2,"VatRate":18,"TotalPriceNet":25.00,"TotalVAT":0.00,"TotalPriceGross":29.50},{"TypedInvoice":{"Id":1,"Number":"VAT\\\\12\\\\34\\\\232","VatId":null,"CreationDate":"2013-02-13T00:00:00","IsAChainInvoice":false,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":4215.46,"InvoiceItems":[{"Id":1,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":1,"Code":"1231","Name":"Product 1","Unit":"hours"},"PricePerUnitNet":12.50,"Quantity":2,"VatRate":18,"TotalPriceNet":25.00,"TotalVAT":0.00,"TotalPriceGross":29.50},{"Id":7,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":3,"Code":"1233","Name":"Product 3","Unit":"hours"},"PricePerUnitNet":1250.00,"Quantity":3,"VatRate":7,"TotalPriceNet":3750.00,"TotalVAT":0.00,"TotalPriceGross":4012.50}],"IsACorrectiveInvoice":false},"Invoice":{"Id":1,"Number":"VAT\\\\12\\\\34\\\\232","VatId":null,"CreationDate":"2013-02-13T00:00:00","IsAChainInvoice":false,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":4215.46,"InvoiceItems":[{"Id":1,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":1,"Code":"1231","Name":"Product 1","Unit":"hours"},"PricePerUnitNet":12.50,"Quantity":2,"VatRate":18,"TotalPriceNet":25.00,"TotalVAT":0.00,"TotalPriceGross":29.50},{"Id":7,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":3,"Code":"1233","Name":"Product 3","Unit":"hours"},"PricePerUnitNet":1250.00,"Quantity":3,"VatRate":7,"TotalPriceNet":3750.00,"TotalVAT":0.00,"TotalPriceGross":4012.50}],"IsACorrectiveInvoice":false},"Id":4,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":2,"Code":"1232","Name":"Product 2","Unit":"hours"},"PricePerUnitNet":147.00,"Quantity":1,"VatRate":18,"TotalPriceNet":147.00,"TotalVAT":0.00,"TotalPriceGross":173.46},{"TypedInvoice":{"Id":1,"Number":"VAT\\\\12\\\\34\\\\232","VatId":null,"CreationDate":"2013-02-13T00:00:00","IsAChainInvoice":false,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":4215.46,"InvoiceItems":[{"Id":1,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":1,"Code":"1231","Name":"Product 1","Unit":"hours"},"PricePerUnitNet":12.50,"Quantity":2,"VatRate":18,"TotalPriceNet":25.00,"TotalVAT":0.00,"TotalPriceGross":29.50},{"Id":4,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":2,"Code":"1232","Name":"Product 2","Unit":"hours"},"PricePerUnitNet":147.00,"Quantity":1,"VatRate":18,"TotalPriceNet":147.00,"TotalVAT":0.00,"TotalPriceGross":173.46}],"IsACorrectiveInvoice":false},"Invoice":{"Id":1,"Number":"VAT\\\\12\\\\34\\\\232","VatId":null,"CreationDate":"2013-02-13T00:00:00","IsAChainInvoice":false,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"ExemptFees":0.00,"TaxedFees":0.00,"VatTaxAmount":0.00,"GrossTaxAmount":0.00,"TotalValue":4215.46,"InvoiceItems":[{"Id":1,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":1,"Code":"1231","Name":"Product 1","Unit":"hours"},"PricePerUnitNet":12.50,"Quantity":2,"VatRate":18,"TotalPriceNet":25.00,"TotalVAT":0.00,"TotalPriceGross":29.50},{"Id":4,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":2,"Code":"1232","Name":"Product 2","Unit":"hours"},"PricePerUnitNet":147.00,"Quantity":1,"VatRate":18,"TotalPriceNet":147.00,"TotalVAT":0.00,"TotalPriceGross":173.46}],"IsACorrectiveInvoice":false},"Id":7,"Customer":{"Id":1,"MPSMid":"12345671","HIMMSMid":"87654321","Name":"Merchant 1","VatId":"1234-56-781","CustomerAddressLegal":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"},"CustomerAddressMailing":{"Id":1,"CustomerName":"Merchant 1","Street":"Puławska 17","City":"Warszawa","ZipCode":"02-515","AddressDate":"2013-07-09T09:56:27.083"}},"Product":{"Id":3,"Code":"1233","Name":"Product 3","Unit":"hours"},"PricePerUnitNet":1250.00,"Quantity":3,"VatRate":7,"TotalPriceNet":3750.00,"TotalVAT":0.00,"TotalPriceGross":4012.50}]';

mock.mockIcpAjaxService = function (appPatch) {
    
    $.mockjax({
        url: appPatch + 'api/Invoices/Get',
        response: function() {
            this.responseText = mock.InvoiceJsonString;
        }
    });
    
    $.mockjax({
        url: appPatch + 'api/VatRates',
        response: function () {
            this.responseText = mock.VatRatesJsonString;
        }
    });
        
    $.mockjax({
        url: appPatch + 'api/Product',
        response: function () {
            this.responseText = mock.ProductJsonString;
        }
    });

    $.mockjax({
        url: appPatch + 'api/InvoiceItems/*',
        response: function() {
            this.responseText = mock.InvoiceItemsJsonString;
        }
    });        

};

