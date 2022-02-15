using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository;

public class User
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickNAme { get; set; }

    //[InverseProperty("PostingUser")]
    public ICollection<Comment> Comments { get; set; }
    [InverseProperty("User")]
    public ICollection<RoomEvent> RoomEvents { get; set; }
    [InverseProperty("TargetUser")]
    public ICollection<RoomEvent> RoomEventsTargetUser { get; set; }
}