using System.Collections.Generic;
using System.IO;

namespace Asc2Pnt
{
	public interface ITextSerializer<T>
	{
		string Serialize(IEnumerable<T> objects);
		T[] Deserialize(string input);
	}

	public class FileStorage<T>
	{
		public FileStorage(ITextSerializer<T> serializer)
		{
			_serializer = serializer;
		}
		public T[] LoadFromStorage(string name)
		{
			return _serializer.Deserialize(File.ReadAllText(name));
		}

		public void SaveToStorage(string name, IEnumerable<T> points)
		{
			File.WriteAllText(name, _serializer.Serialize(points));
		}

		private readonly ITextSerializer<T> _serializer;
	}
}