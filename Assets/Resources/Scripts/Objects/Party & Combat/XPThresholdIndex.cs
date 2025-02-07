
public static class XPThresholdIndex
{
    static int characterBase = 10;
    static int jobBase = 5;

    public static void CheckCharacterLevel(Profile character)
    {
        int prevLv = character.level;

        int requirement = 0;

        for (int i = 0; i < character.level + 1; i++) requirement += characterBase + i * characterBase;

        if (character.experience >= requirement) character.LevelUp();

        if (prevLv != character.level) CheckCharacterLevel(character);
    }

    public static int GetCharacterRemainder(Profile character, bool max)
    {
        int neededXP = characterBase + character.level * characterBase;

        int overflowXP = 0;
        for (int i = 0; i < character.level; i++) overflowXP += characterBase + i * characterBase;

        int currentXP = character.experience - overflowXP;

        if (!max) return currentXP;
        else return neededXP;
    }

    public static void CheckJobLevel(Profile character, Job job)
    {
        int prevLv = job.level;

        int requirement = 0;

        for (int i = 0; i < job.level + 1; i++) requirement += jobBase + i * jobBase;

        if (job.experience >= requirement) job.LevelUp(character);

        if (prevLv != job.level) CheckJobLevel(character, job);
    }

    public static int GetJobRemainder(Job job, bool max)
    {
        int neededXP = jobBase + job.level * jobBase;

        int overflowXP = 0;
        for (int i = 0; i < job.level; i++) overflowXP += jobBase + i * jobBase;

        int currentXP = job.experience - overflowXP;

        if (!max) return currentXP;
        else return neededXP;
    }
}