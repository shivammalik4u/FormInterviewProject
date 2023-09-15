namespace FormInterviewProject.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
}
