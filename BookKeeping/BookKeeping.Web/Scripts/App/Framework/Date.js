var Framework;
(function (Framework) {
    var DateUtil = (function () {
        function DateUtil() {
        }
        DateUtil.format = function (date, format) {
            return format.replace("YYYY", padStr(date.getFullYear())).replace("MM", padStr(1 + date.getMonth())).replace("DD", padStr(date.getDate())).replace("hh", padStr(date.getHours())).replace("mm", padStr(date.getMinutes())).replace("ss", padStr(date.getSeconds()));

            function padStr(i) {
                return (i < 10) ? "0" + i : "" + i;
            }
        };

        DateUtil.now = function () {
            return new Date();
        };

        DateUtil.dayBefore = function () {
            return DateUtil.addDays(DateUtil.now(), -1);
        };
        DateUtil.dayAfter = function () {
            return DateUtil.addDays(DateUtil.now(), 1);
        };

        DateUtil.addDays = function (date, count) {
            var oneDayInMs = 86400000;
            var ms = date.getTime() + oneDayInMs * count;
            return new Date(ms);
        };

        DateUtil.fromISO = function (isoDate) {
            return new Date(isoDate);
        };
        return DateUtil;
    })();
    Framework.DateUtil = DateUtil;
})(Framework || (Framework = {}));
//# sourceMappingURL=Date.js.map
