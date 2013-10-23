var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Expense;
(function (Expense) {
    var ExpenseService = (function (_super) {
        __extends(ExpenseService, _super);
        function ExpenseService() {
            _super.apply(this, arguments);
        }
        ExpenseService.prototype.addExpense = function (amount, category, onDone) {
            this.ajaxRequest("POST", "/api/expense/add", { amount: amount, category: category }, onDone, function (exceptionDetails) {
                return console.log(exceptionDetails);
            });
        };

        ExpenseService.prototype.listExpenses = function (onDone) {
            this.ajaxRequest("GET", "/api/expense/list", null, onDone, function (exceptionDetails) {
                return console.log(exceptionDetails);
            });
        };
        return ExpenseService;
    })(Framework.ServiceBase);
    Expense.ExpenseService = ExpenseService;

    var ExpenseDTO = (function () {
        function ExpenseDTO() {
        }
        return ExpenseDTO;
    })();
    Expense.ExpenseDTO = ExpenseDTO;
})(Expense || (Expense = {}));
//# sourceMappingURL=ExpenseService.js.map
