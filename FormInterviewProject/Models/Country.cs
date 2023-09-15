namespace FormInterviewProject.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public ICollection<State> States { get; set; }
    }
}
