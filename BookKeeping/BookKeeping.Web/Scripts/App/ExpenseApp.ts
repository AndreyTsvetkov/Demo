/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="Framework/Functional.ts" />

import date = Framework.DateUtil;
import func = Framework.Functional;

module Expense {
	export class ExpenseApp {
		constructor() {
			this.newExpense = new NewExpense(() => this.refresh());

			this.activeGrouping = ko.computed(() => this.groupings.filter(g => g.active())[0]);
			this.activeGrouping.subscribe((newVal) => { if (newVal) this.regroup(); });
		}
		 
		public run() {
			ko.applyBindings(this, $(".app-zone").get(0));
			this.refresh();
		}

		public newExpense: NewExpense;
		public groupings = [
			new Grouping("Group by month", expense => date.format(date.fromISO(expense.date), "YYYY / MM"), true), 
			new Grouping("Group by category", expense => expense.category)
		];
		public activeGrouping: KnockoutObservable<Grouping>;
		public groups = ko.observableArray(<Group[]>[]);

		private refresh() {
			this.service.listExpenses(items => this.showGrouped(items));
		}

		private regroup() {
			var items = func.selectMany(this.groups(), g => g.items);
			this.showGrouped(items);
		}

		private showGrouped(items: ExpenseDTO[]) {
			this.groups.removeAll();
			func
				.groupBy(items, dto => this.activeGrouping().keyExtractor(dto))
				.map(g => new Group(g.key, g.values))
				.forEach(g => this.groups.push(g));
		}

		private service = new ExpenseService();
	}

	export class Group {
		constructor(public name: string, public items: ExpenseDTO[]) {
			items.forEach(i => (<any>i).dateText = date.format(date.fromISO(i.date), "YYYY / MM / DD"));
		}
	}

	export class Grouping {
		private static all: Grouping[] = [];

		constructor(public text: string, public keyExtractor: (expense: ExpenseDTO) => string, active = false) {
			this.active = ko.observable(active);
			Grouping.all.push(this);
		}

		public active: KnockoutObservable<boolean>;
		public activate() {
			this.active(true);
			Grouping.all
				.filter(g => g !== this)
				.forEach(g => g.active(false));
		}
	}

	export class NewExpense {
		constructor(private notifyAdded: () => void) {	}

		public amount = ko.observable("");
		public category = ko.observable("");
		public add() {
			if (this.amount() && this.category()) {
				this.service.addExpense(parseInt(this.amount()), this.category(), () => this.notifyAdded());

				this.amount("");
				this.category("");
			}
			else {
				var $add = $(".btn.add");
				var oldColor = $add.css("background-color");
				$add.css("background-color", "red");
				window.setTimeout(() => $add.css("background-color", oldColor), 200);
			}
		}

		private service = new ExpenseService();
	}
}