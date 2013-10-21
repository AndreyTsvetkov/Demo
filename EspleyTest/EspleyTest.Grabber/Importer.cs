// ReSharper disable AccessToForEachVariableInClosure

using System.Threading;
using EspleyTest.Domain;
using Util;

namespace EspleyTest.Grabber
{
    public class Importer
    {
	    public Importer(IResumeRepository resumeRepository, IGrabber grabber)
	    {
		    _resumeRepository = resumeRepository;
		    _grabber = grabber;
	    }

	    public void Start(CancellationToken cancellationToken)
		{
		    foreach (var grabbedResume in _grabber.LoadAll())
			{
				if (cancellationToken.IsCancellationRequested)
					break;

			    var maybeExisting = _resumeRepository.Find(grabbedResume.Id);
				if (maybeExisting.Select(existing => existing.LastUpdated < grabbedResume.LastUpdated).OrElse(true))
 					_resumeRepository.Save(grabbedResume);
		    }
		}

		private readonly IResumeRepository _resumeRepository;
	    private readonly IGrabber _grabber;
    }
}
