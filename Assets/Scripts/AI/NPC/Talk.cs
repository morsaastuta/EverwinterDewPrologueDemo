using UnityEngine;

public class Talk : MonoBehaviour
{
    DataHUB dataHUB;
    DialogueController dialogueController;

    [SerializeField] DialogueData dialogueData;

    [SerializeField] float range;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] Animator iconAnimator;

    bool inRange = false;

    void Start()
    {
        dialogueController = GetComponentInParent<DialogueController>();
        dataHUB = GetComponentInParent<DataHUB>();
    }

    void FixedUpdate()
    {
        if (!dataHUB.world.pausedGame && !dataHUB.player.isInteracting)
        {
            if (!inRange && Physics.CheckSphere(transform.position, range, playerLayer))
            {
                inRange = true;
                iconAnimator.SetTrigger("canTalk");
            }
            else if (inRange && !Physics.CheckSphere(transform.position, range, playerLayer))
            {
                inRange = false;
                iconAnimator.SetTrigger("none");
            }
        }
    }

    void Update()
    {
        if (!dataHUB.world.pausedGame && !dataHUB.player.isInteracting)
        {
            if (inRange && Physics.CheckSphere(transform.position, range, playerLayer))
            {
                if (dataHUB.player.CompareKeyOnce(dataHUB.player.interactKey, true))
                {
                    dialogueController.StartInteraction(dialogueData);
                }
            }
        }
    }
}
