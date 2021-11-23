"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.DropdownService = void 0;
var DropdownService = /** @class */ (function () {
    function DropdownService(httpClient) {
        this.httpClient = httpClient;
    }
    DropdownService.prototype.getCityDropdown = function () {
        return this.httpClient.get('/api/DropDown/cityDropdown');
    };
    DropdownService.prototype.getCloudsDropdown = function () {
        return this.httpClient.get('/api/DropDown/cloudsDropdown');
    };
    DropdownService.prototype.getCoordDropdown = function () {
        return this.httpClient.get('/api/DropDown/coordDropdown');
    };
    DropdownService.prototype.getMainDropdown = function () {
        return this.httpClient.get('/api/DropDown/mainDropdown');
    };
    DropdownService.prototype.getSysDropdown = function () {
        return this.httpClient.get('/api/DropDown/sysDropdown');
    };
    DropdownService.prototype.getWeatherDropdown = function () {
        return this.httpClient.get('/api/DropDown/weatherDropdown');
    };
    DropdownService.prototype.getWindDropdown = function () {
        return this.httpClient.get('/api/DropDown/windDropdown');
    };
    return DropdownService;
}());
exports.DropdownService = DropdownService;
//# sourceMappingURL=dropdown.service.js.map