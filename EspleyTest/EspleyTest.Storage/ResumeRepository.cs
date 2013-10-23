using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using EspleyTest.Domain;
using Util;

namespace EspleyTest.Storage
{
	public class ResumeRepository : DbContext, IResumeRepository
    {
		public ResumeRepository() : base("DefaultConnection") { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var resume = modelBuilder.Entity<Resume>();
			resume
				.Property(_ => _.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
			resume
				.Property(_ => _.ApplicantName)
				.IsRequired()
				.IsUnicode();
			resume
				.Property(_ => _.HtmlBody)
				.IsRequired()
				.IsUnicode();
		}

		public void Save(Resume resume)
		{
			var maybeExisting = Find(resume.Id);
			if (!maybeExisting.HasValue)
				Resumes.Add(resume);
			else // обновляем существующий объект
				Mapper.Map(resume, maybeExisting.Value);

			SaveChanges();
		}

		public IReadOnlyCollection<Resume> Load(int skip, int take, out int totalCount)
		{
			totalCount = Resumes.Count();
			return Resumes
				.OrderByDescending(_ => _.LastUpdated)
				.Skip(skip)
				.Take(take)
				.ToArray();
		}

		public Resume Get(int id)
		{
			return Find(id).OrElse(() => new ArgumentException("No resume #" + id));
		}

		public Maybe<Resume> Find(int id)
		{
			return Resumes.FirstMaybe(_ => _.Id == id);
		}

		public DbSet<Resume> Resumes { get; set; } 
    }
}
