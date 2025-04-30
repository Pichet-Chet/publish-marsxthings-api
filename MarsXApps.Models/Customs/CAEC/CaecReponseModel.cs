using System;
namespace MarsXApps.Models.Customs.CAEC
{
	public class CaecReponseModel
	{
		public CaecReponseModel()
		{
		}

        public bool STATUS { get; set; }
        public int STATUS_CODE { get; set; }
        public int HTTP_CODE { get; set; }
        public string CODE { get; set; }
        public string MESSAGE { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public string ERROR_STACK { get; set; }
        public string INNER_EXCEPTION { get; set; }
        public string CURRENT_METHOD { get; set; }
        public object OUTPUT_DATA { get; set; }
        public object DATA { get; set; }
        public string? RESPONSE_TIME { get; set; }
        public int? PAGE_NUMBER { get; set; }
        public int? PAGE_SIZE { get; set; }
        public int? EFFECT_ROW { get; set; }
    }
}

