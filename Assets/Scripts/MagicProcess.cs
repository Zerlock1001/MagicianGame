using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MagicProcess", menuName = "MagicProcess/MagicProcess")]
public class MagicProcess : ScriptableObject
{
    public MagicConditionExpression expression;
    public MagicProcessCondition[] conditions;
    public MagicProcessEffect[] effects;
    public virtual bool ConditionMet(){
        switch(expression){
            case MagicConditionExpression.And:
                return conditions.All(condition => condition.ConditionMet());
            case MagicConditionExpression.Or:
                return conditions.Any(condition => condition.ConditionMet());
            case MagicConditionExpression.Not:
                return !conditions.All(condition => condition.ConditionMet());
            case MagicConditionExpression.Xor:
                return conditions.Where(condition => condition.ConditionMet()).Count() == 1;
        }
        return false;
    }
    public virtual void Apply(){
        foreach(var effect in effects){
            effect.Apply();
        }
    }
    public virtual void UnApply(){
        foreach(var effect in effects){
            effect.UnApply();
        }
    }
    
    public enum MagicConditionExpression{
        And,
        Or,
        Not,
        Xor,
    }
}
