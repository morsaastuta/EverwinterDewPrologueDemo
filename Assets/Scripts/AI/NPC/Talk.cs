using UnityEngine;

public class Talk : MonoBehaviour
{
    DataHUB dataHUB;
    DialogueController dialogueController;

    [SerializeField] DialogueData dialogueData;

    [SerializeField] float range;
    [SerializeField] LayerMask playerLayer;

    void Start()
    {
        dialogueController = GetComponentInParent<DialogueController>();
        dataHUB = GetComponentInParent<DataHUB>();
    }

    void Update()
    {
        if (!dataHUB.world.pausedGame && !dataHUB.player.isInteracting)
        {
            if (Physics.CheckSphere(transform.position, range, playerLayer))
            {
                if (dataHUB.player.CompareKeyOnce(dataHUB.player.interactKey, true))
                {
                    dialogueController.StartInteraction(dialogueData);
                }
            }
        }
    }
}
