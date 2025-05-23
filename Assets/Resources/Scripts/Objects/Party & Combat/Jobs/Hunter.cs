using System;

[Serializable]
public class Hunter : Job
{
    public Hunter(int initLevel)
    {
        name = "Hunter";
        description = "Hunters prey on their foe from afar. They master the bow and have high speed, being able to take more turns than usual.";
        wield1 = typeof(BowItem);

        level = initLevel;
    }
}
