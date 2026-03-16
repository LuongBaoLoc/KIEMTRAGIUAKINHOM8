using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace KTGKNHOM8.Toeic
{
    public class Exam : FullAuditedEntity<int> 
    {
        public string Title { get; set; }
        public int TimeLimit { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ExamPart> ExamParts { get; set; }
    }
}