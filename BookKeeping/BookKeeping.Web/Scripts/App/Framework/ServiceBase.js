var Framework;
(function (Framework) {
    var ServiceBase = (function () {
        function ServiceBase() {
        }
        ServiceBase.prototype.ajaxRequest = function (type, url, data, callback, errorCallback) {
            if (typeof callback === "undefined") { callback = function (items) {
            }; }
            if (typeof errorCallback === "undefined") { errorCallback = function (exData) {
            }; }
            var _this = this;
            var options = {
                dataType: "json",
                contentType: "application/json",
                cache: false,
                type: type,
                data: isString(data) ? data : JSON.stringify(data)
            };
            function toUrlParams(data) {
                var result = "1=1&";
                for (var k in data) {
                    result += (encodeURIComponent(k) + "=" + encodeURIComponent(data[k] + "") + "&");
                }
                return result;
            }

            console.log("Requesting " + url + " with " + options.data + "...");

            return $.ajax(url, options).done(function (data, statusMessage, response) {
                console.log("Success: Status code " + response.status + "(" + response.statusText + ") when requesting " + url);
                if (isArray(data))
                    callback((data).map(function (item) {
                        return _this.propertiesToCamel(item);
                    }));
else if (isString(data))
                    callback([data]);
else
                    callback([_this.propertiesToCamel(data)]);
            }).fail(function (response) {
                console.log("Fail: Status code " + response.status + "(" + response.statusText + ") when requesting " + url);
                errorCallback(response.responseBody);
            });

            function isArray(obj) {
                return Object.prototype.toString.call(obj) === "[object Array]";
            }
            function isString(obj) {
                return Object.prototype.toString.call(obj) == '[object String]';
            }
        };

        ServiceBase.prototype.propertiesToCamel = function (obj) {
            var _this = this;
            if (!isObject(obj))
                return obj;

            var result = {};
            for (var k in obj) {
                var property = obj[k];
                result[toCamel(k)] = isObject(property) ? this.propertiesToCamel(property) : isArray(property) ? (property).map(function (o) {
                    return _this.propertiesToCamel(o);
                }) : property;
            }
            return result;

            function toCamel(key) {
                if (!key)
                    return "";

                if (key.length == 1)
                    return key.toLowerCase();

                var start = key.substr(0, 1);
                var end = key.substr(1);
                return start.toLowerCase() + end;
            }
            function isObject(o) {
                return o && o.constructor === Object;
            }
            function isArray(o) {
                return o && o.constructor === Array;
            }
        };
        return ServiceBase;
    })();
    Framework.ServiceBase = ServiceBase;
})(Framework || (Framework = {}));
//# sourceMappingURL=ServiceBase.js.map
