namespace PersonaModel
{
    public class People
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Int64 Identification { get; set; }
        public string? Email { get; set; }
        public int TypeId { get; set; }

        public People()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Identification = 0;
            Email = string.Empty;
            TypeId = 0;
        }
    }
}
