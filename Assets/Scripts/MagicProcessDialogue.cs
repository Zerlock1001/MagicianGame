using UnityEngine;
[CreateAssetMenu(fileName = "MagicProcessDialogue", menuName = "MagicProcess/Dialogue")]
public class MagicProcessDialogue: MagicProcess
{
    public DialogueContent dialogueContent;
    public bool nextStepAfterApply = false;
    [System.NonSerialized] public bool isDialogueActive = false;

    public override bool ConditionMet(){
        return true;
    }
    public override void Apply(){
        if(isDialogueActive){
            return;
        }
        DialogueManager.Instance.AwakeDialogue(dialogueContent);
        isDialogueActive = true;
        if(nextStepAfterApply){
            MagicProcessManager.instance.NextStep();
        }
    }
}
