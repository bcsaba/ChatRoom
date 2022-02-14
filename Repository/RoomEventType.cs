using System.ComponentModel.DataAnnotations;

namespace Repository;

public class RoomEventType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}