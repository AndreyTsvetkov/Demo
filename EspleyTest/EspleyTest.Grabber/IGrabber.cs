using System.Collections.Generic;
using EspleyTest.Domain;

namespace EspleyTest.Grabber
{
	public interface IGrabber
	{
		IEnumerable<Resume> LoadAll();
	}
}