

public class Nikolaos : Profile
{
    public Nikolaos()
    {
        name = "Nikolaos";
        fullname = "Nikolaos Athanas";
        description = "The crown prince of Pantheos. Of 23 years of age already, he just started his Snowforcing pilgrimage. He was blessed by Khione, the Pagos goddess, in his early years, and as such he can change the flow of the Pagos winds at will. His swordsmanship is still on the works, but he's already an expert in sword dancing.";
        job = new Knight();

        level = 5;

        baseHP = 160;
        baseMP = 10;
        baseATK = 5;
        baseDFN = 5;
        baseMAG = 4;
        baseDFL = 4;
        baseSPI = 2;
        baseSPD = 5;
        baseMOV = 3;

        LoadStats();
    }
}
