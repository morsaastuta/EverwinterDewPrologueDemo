

public class Hunter : Job
{
    public Hunter()
    {
        name = "Hunter";
        description = "Hunters prey on their foe from afar. They master the bow and have high speed, being able to take more turns than usual.";
        wield1 = typeof(BowItem);
    }
}
