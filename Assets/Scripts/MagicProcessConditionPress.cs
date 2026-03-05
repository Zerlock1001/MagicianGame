using UnityEngine;

[CreateAssetMenu(fileName = "MagicProcessConditionPress", menuName = "MagicProcessCondition/Press")]
public class MagicProcessConditionPress : MagicProcessCondition
{
    public KeyCode keyCode;
    public override bool ConditionMet(){
        return Input.GetKey(keyCode);
    }
}
