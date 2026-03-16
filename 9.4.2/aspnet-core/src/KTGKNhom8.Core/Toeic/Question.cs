    using Abp.Domain.Entities;
using System.Collections.Generic;

namespace KTGKNHOM8.Toeic
{
    public class Question : Entity<int>
    {
        public int ExamPartId { get; set; }
        public int? PassageId { get; set; } 
        public int QuestionNumber { get; set; } 
        public string Content { get; set; } 
        public bool IsShuffle { get; set; }
        public virtual ExamPart ExamPart { get; set; }
        public virtual Passage Passage { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
