using Abp.Domain.Entities;
using System.Collections.Generic;

namespace KTGKNHOM8.Toeic
{
    public class Passage : Entity<int>
    {
        public int ExamPartId { get; set; }
        public string Content { get; set; }
        public virtual ExamPart ExamPart { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}