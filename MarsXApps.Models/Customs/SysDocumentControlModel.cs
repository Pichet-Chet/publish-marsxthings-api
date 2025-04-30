using System;
namespace MarsXApps.Models.Customs
{
	public class SysDocumentControlModel
	{
		public SysDocumentControlModel()
		{
		}

        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Year { get; set; } = null!;

        public string Month { get; set; } = null!;

        public int? Running { get; set; }

        public string Type { get; set; } = null!;
    }
}

