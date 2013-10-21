using System.Linq;
using System.Web.Mvc;
using EspleyTest.Domain;
using Util;

namespace EspleyTest.Viewer.Controllers
{
    public class ResumeController : Controller
    {
	    public ResumeController(IResumeRepository resumeRepository)
		{
			_resumeRepository = resumeRepository;
		}

	    private const int PageSize = 5;
        public ActionResult Index(int page = 0)
        {
	        return View(_resumeRepository
		                    .Load(skip: page*PageSize, take: PageSize)
		                    .Select(ResumeListItemDTO.FromDomain));
        }

        public ActionResult Details(int id)
        {
	        return (from resume in _resumeRepository.Find(id)
	                let resumeDto = ResumeDTO.FromDomain(resume)
	                select (ActionResult) View(resumeDto))
		        .OrElse(() => HttpNotFound("No resume #" + id));
        }

		private readonly IResumeRepository _resumeRepository;
	}

	public class ResumeListItemDTO
	{
		public string ApplicantName;
		public int ResumeId;

		public static ResumeListItemDTO FromDomain(Resume arg)
		{
			return new ResumeListItemDTO {ApplicantName = arg.ApplicantName, ResumeId = arg.Id};
		}
	}

	public class ResumeDTO
	{
		public string HtmlBody;
		public string ApplicantName;

		public static ResumeDTO FromDomain(Resume arg)
		{
			return new ResumeDTO {HtmlBody = FixImages(arg.HtmlBody), ApplicantName = arg.ApplicantName};
		}

		private static string FixImages(string html)
		{
			return html.Replace("src=\"/company/getimage", "src=\"http://myjob.uz/company/getimage");
		}
	}
}
