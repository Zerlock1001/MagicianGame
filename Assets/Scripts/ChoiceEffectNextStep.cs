using UnityEngine;
[CreateAssetMenu(fileName = "ChoiceEffectNextStep", menuName = "ChoiceEffect/NextStep")]

public class ChoiceEffectNextStep: ChoiceEffect
{
    public override void DoEffect(){
        MagicProcessManager.instance.NextStep();
    }
}
