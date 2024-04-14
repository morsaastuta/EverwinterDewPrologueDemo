using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nikolaos : Profile
{
    public Nikolaos()
    {
        name = "Nikolaos";
        fullname = "Nikolaos Athanas";
        description = "The crown prince of Pantheos. Of 23 years of age already, he just started his Snowforcing pilgrimage. He was blessed by Khione, the Pagos goddess, in his early years, and as such he can change the flow of the Pagos winds at will. His swordsmanship is still on the works, but he's already an expert in sword dancing.";
        job = new Knight();
        level = 5;

        statHP = 100;
        statMP = 0;
        statATK = 0;
        statDFN = 0;
        statMAG = 0;
        statDFL = 0;
        statSPI = 0;
    }
}
