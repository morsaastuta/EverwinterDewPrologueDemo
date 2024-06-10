public class ThrasciasForest_Arrival : DialogueEvent
{
    public override void CheckIndex(int messageIndex)
    {
        switch (messageIndex)
        {
            case 0:
                CommonIntro();
                dec.SwitchPOV(0);
                dec.MoveActor(0, 0, 0.3f);
                break;
            case 1:
                dec.MoveActor(0, 1, 0.3f);
                break;
            case 2:
                dec.MoveActor(0, 2, 0.3f);
                break;
            case 3:
                dec.MoveActor(0, 3, 0.3f);
                break;
            case 4:
                dec.SwitchPOV(1);
                break;
            case 7:
                dec.MoveActor(0, 4, 0.4f);
                break;
            case 8:
                dec.MoveActor(0, 5, 0.5f);
                break;
            case 9:
                dec.MoveActor(0, 3, 0.4f);
                break;
            case 12:
                dec.MoveActor(0, 6, 0.6f);
                break;
            case 13:
                dec.SwitchPOV(2);
                dec.MoveActor(1, 7, 0.4f);
                break;
            case 18:
                CommonOutro();
                dec.ExitEvent();
                break;
        }
    }
}
