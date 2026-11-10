using UnityEngine;

[CreateAssetMenu(fileName = "MagicProcessConditionReleaseAfterMove", menuName = "MagicProcessCondition/ReleaseAfterMove")]
public class MagicProcessConditionReleaseAfterMove : MagicProcessCondition
{
    [Tooltip("需要先移动的方向（世界坐标）")]
    public Vector3 direction = Vector3.left;

    [Tooltip("累计移动达到该距离后，才允许通过松开鼠标完成")]
    public float requiredDistance = 1.5f;

    [Tooltip("需要松开的鼠标键（默认右键）")]
    public int mouseButton = 1;

    [Tooltip("移动时是否必须按住该鼠标键")]
    public bool requireHoldWhileMoving = true;

    float accumulated;
    bool armed;
    bool completed;
    Vector3 lastMouseWorld;
    bool hasLastMouse;

    public override bool ConditionMet()
    {
        if (completed)
        {
            return true;
        }

        UpdateMoveProgress();

        if (armed && Input.GetMouseButtonUp(mouseButton))
        {
            completed = true;
            return true;
        }

        return false;
    }

    public override void OnProcessEnter()
    {
        accumulated = 0f;
        armed = false;
        completed = false;
        hasLastMouse = false;
    }

    void UpdateMoveProgress()
    {
        if (requireHoldWhileMoving && !Input.GetMouseButton(mouseButton))
        {
            hasLastMouse = false;
            return;
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!hasLastMouse)
        {
            lastMouseWorld = mouseWorld;
            hasLastMouse = true;
            return;
        }

        Vector3 delta = mouseWorld - lastMouseWorld;
        lastMouseWorld = mouseWorld;

        float projected = Vector3.Dot(delta, direction.normalized);
        if (projected > 0f)
        {
            accumulated += projected;
            if (accumulated >= requiredDistance)
            {
                armed = true;
            }
        }
    }
}
