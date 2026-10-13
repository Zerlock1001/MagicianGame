using UnityEngine;
[CreateAssetMenu(fileName = "MagicProcessCloseDialogueBox", menuName = "MagicProcess/CloseDialogueBox")]
public class MagicProcessCloseDialogueBox: MagicProcess
{
    public bool nextStepAfterApply = false;

    public override bool ConditionMet(){
        return true;
    }
    public override void Apply(){
        DialogueManager.Instance.EndDialogue();
        if(nextStepAfterApply){
            MagicProcessManager.instance.NextStep();    
        }
    }
}
