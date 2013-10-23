module Expense {
	export class ExpenseService extends Framework.ServiceBase  {
		public addExpense(amount: number, category: string, onDone: () => void) {
			this.ajaxRequest("POST", "/api/expense/add", { amount: amount, category: category }, onDone, exceptionDetails => console.log(exceptionDetails));
		}

		public listExpenses(onDone: (data: ExpenseDTO[]) => any) {
			this.ajaxRequest("GET", "/api/expense/list", null, onDone, exceptionDetails => console.log(exceptionDetails));
		}
	}

	export class ExpenseDTO { 
		public id: number;
		public date: string;
		public category: string;
		public amount: number;
	}
}