using Abp.Domain.Entities;

namespace KTGKNHOM8.Toeic
{
    public class Answer : Entity<int>
    {
        public int QuestionId { get; set; }
        public string Label { get; set; } 
        public string Content { get; set; } 
        public bool IsCorrect { get; set; } 
        public virtual Question Question { get; set; }
    }
}
