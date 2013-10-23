/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="Framework/Functional.ts" />
var date = Framework.DateUtil;
var func = Framework.Functional;

var Expense;
(function (Expense) {
    var ExpenseApp = (function () {
        function ExpenseApp() {
            var _this = this;
            this.groupings = [
                new Grouping("Group by month", function (expense) {
                    return date.format(date.fromISO(expense.date), "YYYY / MM");
                }, true),
                new Grouping("Group by category", function (expense) {
                    return expense.category;
                })
            ];
            this.groups = ko.observableArray([]);
            this.service = new Expense.ExpenseService();
            this.newExpense = new NewExpense(function () {
                return _this.refresh();
            });

            this.activeGrouping = ko.computed(function () {
                return _this.groupings.filter(function (g) {
                    return g.active();
                })[0];
            });
            this.activeGrouping.subscribe(function (newVal) {
                if (newVal)
                    _this.regroup();
            });
        }
        ExpenseApp.prototype.run = function () {
            ko.applyBindings(this, $(".app-zone").get(0));
            this.refresh();
        };

        ExpenseApp.prototype.refresh = function () {
            var _this = this;
            this.service.listExpenses(function (items) {
                return _this.showGrouped(items);
            });
        };

        ExpenseApp.prototype.regroup = function () {
            var items = func.selectMany(this.groups(), function (g) {
                return g.items;
            });
            this.showGrouped(items);
        };

        ExpenseApp.prototype.showGrouped = function (items) {
            var _this = this;
            this.groups.removeAll();
            func.groupBy(items, function (dto) {
                return _this.activeGrouping().keyExtractor(dto);
            }).map(function (g) {
                return new Group(g.key, g.values);
            }).forEach(function (g) {
                return _this.groups.push(g);
            });
        };
        return ExpenseApp;
    })();
    Expense.ExpenseApp = ExpenseApp;

    var Group = (function () {
        function Group(name, items) {
            this.name = name;
            this.items = items;
            items.forEach(function (i) {
                return (i).dateText = date.format(date.fromISO(i.date), "YYYY / MM / DD");
            });
        }
        return Group;
    })();
    Expense.Group = Group;

    var Grouping = (function () {
        function Grouping(text, keyExtractor, active) {
            if (typeof active === "undefined") { active = false; }
            this.text = text;
            this.keyExtractor = keyExtractor;
            this.active = ko.observable(active);
            Grouping.all.push(this);
        }
        Grouping.prototype.activate = function () {
            var _this = this;
            this.active(true);
            Grouping.all.filter(function (g) {
                return g !== _this;
            }).forEach(function (g) {
                return g.active(false);
            });
        };
        Grouping.all = [];
        return Grouping;
    })();
    Expense.Grouping = Grouping;

    var NewExpense = (function () {
        function NewExpense(notifyAdded) {
            this.notifyAdded = notifyAdded;
            this.amount = ko.observable("");
            this.category = ko.observable("");
            this.service = new Expense.ExpenseService();
        }
        NewExpense.prototype.add = function () {
            var _this = this;
            if (this.amount() && this.category()) {
                this.service.addExpense(parseInt(this.amount()), this.category(), function () {
                    return _this.notifyAdded();
                });

                this.amount("");
                this.category("");
            } else {
                var $add = $(".btn.add");
                var oldColor = $add.css("background-color");
                $add.css("background-color", "red");
                window.setTimeout(function () {
                    return $add.css("background-color", oldColor);
                }, 200);
            }
        };
        return NewExpense;
    })();
    Expense.NewExpense = NewExpense;
})(Expense || (Expense = {}));
//# sourceMappingURL=ExpenseApp.js.map
