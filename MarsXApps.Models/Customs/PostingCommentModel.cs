using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsXApps.Models.Customs
{
    public class PostingCommentModel
    {
        public PostingCommentModel()
        {
        }

        public int? Id { get; set; }

        public int PostingTransactionId { get; set; }

        public Guid SysCustomersId { get; set; }

        public string CommentDescription { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }


        [NotMapped]
        public string? FirstName { get; set; }

        [NotMapped]
        public string? CommentAgoTh { get; set; }

        [NotMapped]
        public string? CommentAgoEn { get; set; }

        [NotMapped]
        public string? ProfileImage { get; set; }
    }
}

