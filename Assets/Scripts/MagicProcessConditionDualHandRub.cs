using UnityEngine;

[CreateAssetMenu(fileName = "MagicProcessConditionDualHandRub", menuName = "MagicProcessCondition/DualHandRub")]
public class MagicProcessConditionDualHandRub : MagicProcessCondition
{
    [Header("左手")]
    public KeyCode[] leftKeys = { KeyCode.D, KeyCode.Space };
    public int requiredLeftPresses = 5;

    [Header("右手")]
    [Tooltip("需按住的鼠标键（默认右键）")]
    public int holdMouseButton = 1;
    [Tooltip("在按住 hold 键时点击的鼠标键（默认左键）")]
    public int tapMouseButton = 0;
    public int requiredRightPresses = 5;

    [Header("推进")]
    public float pulseDuration = 0.18f;

    int leftCount;
    int rightCount;
    bool wasLeftDown;
    float activeUntil = -1f;
    bool completed;

    public bool IsComplete => completed;

    public override bool ConditionMet()
    {
        if (completed)
        {
            return true;
        }

        bool leftDown = AllKeysDown(leftKeys);
        if (leftDown && !wasLeftDown)
        {
            leftCount++;
            activeUntil = Time.unscaledTime + pulseDuration;
        }
        wasLeftDown = leftDown;

        if (Input.GetMouseButton(holdMouseButton) && Input.GetMouseButtonDown(tapMouseButton))
        {
            rightCount++;
            activeUntil = Time.unscaledTime + pulseDuration;
        }

        if (leftCount >= requiredLeftPresses && rightCount >= requiredRightPresses)
        {
            completed = true;
            return true;
        }

        return Time.unscaledTime < activeUntil;
    }

    public override void OnProcessEnter()
    {
        leftCount = 0;
        rightCount = 0;
        wasLeftDown = false;
        activeUntil = -1f;
        completed = false;
    }

    static bool AllKeysDown(KeyCode[] keys)
    {
        if (keys == null || keys.Length == 0)
        {
            return false;
        }
        for (int i = 0; i < keys.Length; i++)
        {
            if (!Input.GetKey(keys[i]))
            {
                return false;
            }
        }
        return true;
    }
}
