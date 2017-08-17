namespace omicron.domain.entities
{
    public class GitRepo
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual int Stars { get; set; }
        public virtual int TodayStars { get; set; }
        public virtual string Language { get; set; }
    }   
}