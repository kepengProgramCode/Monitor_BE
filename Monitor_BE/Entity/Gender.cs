namespace Monitor_BE.Entity
{
    public class Gender
    {
        public string? genderLabel { get; set; }
        public int? genderValue { get; set; }
    }

    public class Status
    {
        public string? userLabel { get; set; }
        public int? userStatus { get; set; }
        public string? tagType { get; set; }
    }
}
