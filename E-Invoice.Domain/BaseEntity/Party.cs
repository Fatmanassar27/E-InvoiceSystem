namespace E_Invoice.Domain.BaseEntity
{
    public abstract class Party
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Type is required.")]
        [RegularExpression("^(B|P|F)$", ErrorMessage = "Type must be one of the following: B (Business), P (Person), or F (Foreigner).")]
        public string Type { get; set; } = "B";
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
