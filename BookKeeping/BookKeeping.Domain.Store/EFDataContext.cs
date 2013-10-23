using System.Data.Entity;

namespace BookKeeping.Domain.Store
{
	public class EFDataContext : DbContext, IDbContextFor<Expense>
	{
		public EFDataContext() : this("DbConnection") { }

		public EFDataContext(string connectionString)
			: base(connectionString) { }

		public DbSet<Expense> Expenses { get; set; }

		IDbSet<Expense> IDbContextFor<Expense>.Set() { return Expenses; }
		
		public new void SaveChanges()
		{
			base.SaveChanges();
		}
	}
}