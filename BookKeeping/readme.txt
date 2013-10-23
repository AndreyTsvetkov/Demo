This is a demo Expense Book Keeping project to show some bits of my code.

Some things can seem overcomplicated for this amount of functionality; this is due to artificial kind of the task. 
If we imagine a real app with extended functionality, all this 'over-ness' (such as IoC, mocks &c.) would melt away.

To create a database, check the connection string in Web.config and create the database, login and user as mentioned in the connection string. 
To create db tables for app data, run 'Update-Database -TargetMigration 'Initial' -Project BookKeeping.Domain.Store -StartupProject BookKeeping.Web'

Here is the demo task which is implemented here:

«
		Create very simple book-keeping web-application (ASP.NET MVC, JavaScript, AJAX, SQL Server 
		Express) with following functionality –
		1. User can enter new expense and save it.
		2. Expense includes sum of money spent and category (for example food, bills, taxi, 
		entertainment).
		3. User can browse expenses aggregated by month and category. 
		4. No other additional requirements. You have a freedom to decide on particular details of 
		the use cases mentioned above if anything is not clear.
		Task is supposed to take no longer than two hours.
		Try to use best practices you know (SOLID, TDD, MVVM, etc). Ideally, code should not have any 
		technical debts and you should be able increase functionality without huge efforts.
		Please, provide the code, which you could be proud of.
»