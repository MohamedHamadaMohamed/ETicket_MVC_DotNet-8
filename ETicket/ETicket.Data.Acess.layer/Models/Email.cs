namespace ETicket.Data.Acess.layer.Models
{
	public class Email
	{
		public int Id { get; set; }
		public string To { get; set; } = string.Empty;
		public string? Subject { get; set; }
		public string? Body { get; set; }
	}
}
