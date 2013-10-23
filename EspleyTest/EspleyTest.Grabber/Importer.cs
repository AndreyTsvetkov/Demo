// ReSharper disable AccessToForEachVariableInClosure

using System.Diagnostics;
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

				Trace.TraceInformation("Got resume of " + grabbedResume.ApplicantName);
				if ((from existing in _resumeRepository.Find(grabbedResume.Id)
				     select existing.LastUpdated < grabbedResume.LastUpdated)
					.OrElse(true))
				{
					Trace.TraceInformation("Saving resume of " + grabbedResume.ApplicantName);
					_resumeRepository.Save(grabbedResume);
				}
			}
		}

		private readonly IResumeRepository _resumeRepository;
	    private readonly IGrabber _grabber;
    }
}
