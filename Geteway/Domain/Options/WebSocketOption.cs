using System.ComponentModel.DataAnnotations;

namespace Domain.Options;

public class WebSocketOption
{
    public static string SectionKey => nameof(WebSocketOption);

    [Required] public int WebSocketDowntimeClose { get; set; }
}