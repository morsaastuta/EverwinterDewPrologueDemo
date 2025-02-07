public class ThrasciasForest_Elder : DialogueEvent
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
                dec.SwitchPOV(1);
                break;
            case 3:
                dec.SwitchPOV(2);
                break;
            case 6:
                dec.SwitchPOV(3);
                dec.MoveActor(0, 2, 0.7f);
                break;
            case 7:
                dec.MoveActor(1, 8, 4f);
                break;
            case 8:
                dec.MoveActor(0, 3, 0.5f);
                break;
            case 9:
                dec.SwitchPOV(4);
                dec.MoveActor(1, 4, 0.6f);
                break;
            case 11:
                dec.MoveActor(1, 6, 0.4f);
                break;
            case 12:
                dec.MoveActor(0, 5, 0.4f);
                break;
            case 13:
                dec.SwitchPOV(2);
                break;
            case 14:
                dec.SwitchPOV(4);
                dec.MoveActor(1, 7, 0.4f);
                break;
            case 16:
                dec.MoveActor(0, 9, 0.3f);
                break;
            case 17:
                CommonOutro();
                dec.ExitEvent();
                break;
        }
    }
}
