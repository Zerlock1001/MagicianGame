using UnityEngine;

[CreateAssetMenu(fileName = "MagicProcessConditionAlternatePress", menuName = "MagicProcessCondition/AlternatePress")]
public class MagicProcessConditionAlternatePress : MagicProcessCondition
{
    [Tooltip("按顺序循环要求的按键，例如 A → D → A → D …")]
    public KeyCode[] keys = { KeyCode.A, KeyCode.D };

    [Tooltip("每次正确按下后，条件保持为真的时长（用于推进动画）")]
    public float pulseDuration = 0.18f;

    int nextIndex;
    float activeUntil = -1f;

    public override bool ConditionMet()
    {
        if (keys == null || keys.Length == 0)
        {
            return false;
        }

        KeyCode expected = keys[nextIndex % keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            KeyCode key = keys[i];
            if (!Input.GetKeyDown(key))
            {
                continue;
            }

            if (key == expected)
            {
                nextIndex++;
                activeUntil = Time.unscaledTime + pulseDuration;
            }
            break;
        }

        return Time.unscaledTime < activeUntil;
    }

    public override void OnProcessEnter()
    {
        nextIndex = 0;
        activeUntil = -1f;
    }
}
