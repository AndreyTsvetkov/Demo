using System.Collections.Generic;
using Util;

namespace EspleyTest.Domain
{
	public interface IResumeRepository
	{
		void Save(Resume resume);

		IReadOnlyCollection<Resume> Load(int skip, int take);
		Resume Get(int id);
		Maybe<Resume> Find(int id);
	}
}