using UnityEngine;

public class MagicProcessCondition : ScriptableObject
{
    public virtual bool ConditionMet(){
        return false;
    }
}
