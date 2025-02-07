using UnityEngine.SceneManagement;

public class ThrasciasForest_Exit : DialogueEvent
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
                dec.SwitchPOV(1);
                dec.MoveActor(1, 1, 2f);
                break;
            case 2:
                dec.SwitchPOV(2);
                dec.MoveActor(0, 2, 0.3f);
                break;
            case 3:
                dec.SwitchPOV(3);
                break;
            case 8:
                SceneManager.LoadSceneAsync("TitleScreen");
                CommonOutro();
                dec.ExitEvent();
                break;
        }
    }
}
