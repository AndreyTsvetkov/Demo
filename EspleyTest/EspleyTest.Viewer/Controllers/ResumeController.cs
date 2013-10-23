using System.Collections.Generic;
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

	    internal const int PageSize = 15;
        public ActionResult Index(int page = 0)
        {
	        int totalCount;
	        var items = _resumeRepository
		        .Load(skip: page*PageSize, take: PageSize, totalCount: out totalCount)
		        .Select(ResumeListItemDTO.FromDomain);
	        return View(new ResumeListDTO(items, totalCount, page));
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

	public class ResumeListDTO
	{
		public readonly IEnumerable<ResumeListItemDTO> Items;
		public readonly int TotalCount;
		private readonly int _currentPage;

		public ResumeListDTO(IEnumerable<ResumeListItemDTO> items, int totalCount, int currentPage)
		{
			Items = items;
			TotalCount = totalCount;
			_currentPage = currentPage;
		}


		public IEnumerable<int> Pages
		{
			get
			{
				int pagesCount = TotalCount/ResumeController.PageSize + (TotalCount%ResumeController.PageSize == 0 ? 0 : 1);
				return Enumerable.Range(0, pagesCount);
			}
		}

		public bool IsCurrent(int page)
		{
			return _currentPage == page;
		}
	}
	public class ResumeListItemDTO
	{
		public string ApplicantName;
		public string LastUpdatedString;
		public int ResumeId;

		public static ResumeListItemDTO FromDomain(Resume arg)
		{
			return new ResumeListItemDTO {ApplicantName = arg.ApplicantName, ResumeId = arg.Id, LastUpdatedString = arg.LastUpdated.ToLongDateString()};
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
			return html.Replace("src=\"/", "src=\"http://myjob.uz/");
		}
	}
}
