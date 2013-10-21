using System;

namespace EspleyTest.Domain
{
    public class Resume
    {
		public int Id { get; set; }

		public DateTime LastUpdated { get; set; }
		public string ApplicantName { get; set; }
		public string HtmlBody { get; set; }
    }
}
