using Abp.Domain.Entities;
using System.Collections.Generic;

namespace KTGKNHOM8.Toeic
{
    public class ExamPart : Entity<int>
    {
        public int ExamId { get; set; }
        public int PartType { get; set; } 
        public string Directions { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual ICollection<Passage> Passages { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
