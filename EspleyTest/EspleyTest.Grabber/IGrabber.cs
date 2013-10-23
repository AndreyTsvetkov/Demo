using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
				wc.Encoding = Encoding.UTF8;
				wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.76 Safari/537.36");
				wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
				wc.Headers.Add("Accept-Encoding", "deflate,sdch");
				wc.Headers.Add("Cookie", ".ASPXAUTH=952F90D1ED96EC40A8F7628F8B89181A704C9B0E3334AF4B084EAE6382D3C4D6B25B60554DA8CA814226EE2612240329DA7C945B5B8C52B7CB6CE27DCE3139F19FAD4286481345344711FE0D75A625469EE62E77281C9D2529F2D74E3F0DF444DE3D90EF6185299E0046AB2CB73AB3B2551C26B73FF2ACD34BBC619C32DF14B5ABE5D6ECA0E7B00DB64F52EC867AA33F0E435B77; outusername=ts.andrey@gmail.com");

				listDoc.LoadHtml(wc.DownloadString(listUrl));

				foreach (var row in listDoc.DocumentNode.SelectNodes("//a[starts-with(@href, '/resume/entry/')]/../.."))
				{
					var lastUpdated = DateTime.Parse(row.SelectSingleNode("./td[5]").InnerText);
					var name = row.SelectSingleNode("./td[3]/a").InnerText.Trim();
					var itemLink = root + row.SelectSingleNode("./td[3]/a").GetAttributeValue("href", "never-needed");
		
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