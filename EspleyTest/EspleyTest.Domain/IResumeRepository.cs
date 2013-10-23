using System.Collections.Generic;
using Util;

namespace EspleyTest.Domain
{
	public interface IResumeRepository
	{
		void Save(Resume resume);

		IReadOnlyCollection<Resume> Load(int skip, int take, out int totalCount);
		Resume Get(int id);
		Maybe<Resume> Find(int id);
	}
}