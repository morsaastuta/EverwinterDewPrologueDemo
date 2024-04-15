

public class Knight : Job
{
    public Knight()
    {
        name = "Knight";
        description = "Knights have sworn to protect their allies from any danger. They master the sword and shield, being able to block their foes while attacking at a close range.";
        wield1 = typeof(SwordItem);
        wield2 = typeof(ShieldItem);
    }
}
