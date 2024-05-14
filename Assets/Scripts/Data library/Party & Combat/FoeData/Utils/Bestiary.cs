public static class Bestiary
{
    public static FoeData Generate(string codename, int initLV)
    {
        switch(codename)
        {
            case "nikolaos": return new Foe_Nikolaos(initLV);
            case "rimebear": return new Foe_Rimebear(initLV);
        }
        return null;
    }
}