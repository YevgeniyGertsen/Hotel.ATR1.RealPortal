namespace Hotel.ATR1.RealPortal.Models
{
    public partial class Language
    {
        public Language()
        {
            StringResources = new HashSet<StringResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Culture { get; set; }

        public virtual ICollection<StringResource> StringResources { get; set; }
    }
}
