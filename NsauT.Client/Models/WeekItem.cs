namespace NsauT.Client.Models
{
    public enum WeekType
    {
        Even = 0,
        Odd  = 1
    }

    public class WeekItem
    {
        public WeekType WeekType { get; set; }
        public string Title { get; set; }
    }
}
