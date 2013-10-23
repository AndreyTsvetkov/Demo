using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EspleyTest.Domain;
using HtmlAgilityPack;

namespace EspleyTest.Grabber
{
	public interface IGrabber
	{
		IEnumerable<Resume> LoadAll();
	}

	public class Grabber : IGrabber
	{
		public IEnumerable<Resume> LoadAll()
		{
			for (int page = 1; page < 10; page++)
				foreach (var resume in LoadFromPage(page))
					yield return resume;
		}

		private IEnumerable<Resume> LoadFromPage(int page)
		{
			const string root = "http://myjob.uz";
			var listUrl = root + "/resume?industry=13&page=" + page;
			
			var listDoc = new HtmlDocument();
			using (var wc = new WebClient())
			{
				listDoc.LoadHtml(wc.DownloadString(listUrl));

				foreach (var row in listDoc.DocumentNode.SelectNodes("//a[starts-with(@href, '/resume/entry/')]/../.."))
				{
					var lastUpdated = DateTime.Parse(row.SelectSingleNode("./td[5]").InnerText);
					var name = row.SelectSingleNode("./td[3]/a").InnerText;
					var itemLink = root + row.SelectSingleNode("./td[3]/a/@href").InnerText;
		
					var id = int.Parse(itemLink.Split('/').Last());

					var itemDoc = new HtmlDocument();
					itemDoc.LoadHtml(wc.DownloadString(itemLink));

					var htmlBody = itemDoc.DocumentNode.SelectSingleNode("//img[starts-with(@id, 'ResumePhotoMain')]/../../../..").InnerHtml;
					yield return new Resume {ApplicantName = name, HtmlBody = htmlBody, Id = id, LastUpdated = lastUpdated};
				}
			}
		}
	}
}