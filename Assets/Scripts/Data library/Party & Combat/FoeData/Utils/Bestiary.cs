public static class Bestiary
{
    public static FoeData Generate(string codename, int initLV)
    {
        switch(codename)
        {
            case "rimebear_a": return new Foe_Rimebear_Assailant(initLV);
            case "rimebear_m": return new Foe_Rimebear_Mage(initLV);
            case "rimebear_h": return new Foe_Rimebear_Healer(initLV);
        }
        return null;
    }
}