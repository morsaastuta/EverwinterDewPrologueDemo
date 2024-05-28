using System.Runtime.InteropServices.WindowsRuntime;

public static class ItemIndex
{
    public static Item Generate(string codename)
    {
        switch(codename)
        {
            // Consumables
            case "c_ether_1": return new EtherI();
            case "c_ration_1": return new RationI();
            case "c_ration_2": return new RationII();

            // Materials
            case "m_claw_rimebearClaw": return new ClawRimebear();
            case "m_flower_snowdrop": return new FlowerSnowdrop();
            case "m_herbs_thrasciasHerbs": return new HerbsThrascias();
            case "m_pelt_rimebearPelt": return new PeltRimebear();

            // Keys
            case "k_galanthus": return new Galanthus();

            // Body gear
            case "g_body_winterCoat": return new BodyWinter();

            // Arm gear
            case "g_arms_winterGloves": return new ArmsWinter();

            // Leg gear
            case "g_legs_winterBoots": return new LegsWinter();

            // Accessories
            case "g_accessory_bellSouvenir": return new AccessoryBellSouvenir();
            case "g_accessory_chrysanthemumCorola": return new AccessoryChrysanthemumCorola();

            // Swords
            case "w_sword_athanasSword": return new SwordAthanas();
            case "w_sword_bellOfStephanos": return new SwordBell();

            // Shields
            case "w_shield_floeShield": return new ShieldFloe();

            // Bows
            case "w_bow_anemoneBow": return new BowAnemone();
        }
        return null;
    }
}