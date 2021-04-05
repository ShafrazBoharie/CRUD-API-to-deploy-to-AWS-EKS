namespace TPICAP.TechChallenge.Model.Models
{
    public record LinkDto
    {
        public string Href { get; init; }
        public string Rel { get; init; }
        public string Method { get; init; }
    }
}