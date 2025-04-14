namespace ResunetDAL.Models;

public class ProfileSkillModel
{
    public string SkillName { get; set; } = null!; // not null
    public int Level { get; set; }
    public int ProfileId { get; set; }
    public int SkillId { get; set; }
}
