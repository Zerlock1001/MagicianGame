using UnityEngine;

[CreateAssetMenu(fileName = "MagicProcessConditionComboPress", menuName = "MagicProcessCondition/ComboPress")]
public class MagicProcessConditionComboPress : MagicProcessCondition
{
    [Tooltip("需要同时按下的按键组合，例如 D + Space")]
    public KeyCode[] keys = { KeyCode.D, KeyCode.Space };

    [Tooltip("每次成功按下组合后，条件保持为真的时长")]
    public float pulseDuration = 0.2f;

    bool wasAllDown;
    float activeUntil = -1f;

    public override bool ConditionMet()
    {
        if (keys == null || keys.Length == 0)
        {
            return false;
        }

        bool allDown = true;
        for (int i = 0; i < keys.Length; i++)
        {
            if (!Input.GetKey(keys[i]))
            {
                allDown = false;
                break;
            }
        }

        if (allDown && !wasAllDown)
        {
            activeUntil = Time.unscaledTime + pulseDuration;
        }
        wasAllDown = allDown;

        return Time.unscaledTime < activeUntil;
    }

    public override void OnProcessEnter()
    {
        wasAllDown = false;
        activeUntil = -1f;
    }
}
