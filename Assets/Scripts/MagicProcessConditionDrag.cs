using UnityEngine;

[CreateAssetMenu(fileName = "MagicProcessConditionDrag", menuName = "MagicProcessCondition/Drag")]
public class MagicProcessConditionDrag : MagicProcessCondition
{
    public bool fixedDirection = false;
    public Vector3 direction = Vector3.forward;
    Vector3 mousePosition;
    Vector3 mousePositionLastFrame;
    bool mouseDown = false;
    public override bool ConditionMet(){
        MouseDown();
        MouseUp();
        if(mouseDown){
            Vector3 mouseDelta = GetMouseDelta();
            if(fixedDirection){
                //Debug.Log(Vector3.Project(mouseDelta, direction).magnitude);
                return Vector3.Project(mouseDelta, direction).magnitude > 0.1f;
            }else{
                //Debug.Log(mouseDelta.magnitude);
                return mouseDelta.magnitude > 0.1f;
            }
        }
        return false;
    }
    public Vector3 GetMouseDelta(){
        mousePositionLastFrame = mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition - mousePositionLastFrame;
    }
    public void MouseDown(){
        if(Input.GetMouseButtonDown(0)){
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDown = true;
        }
    }
    public void MouseUp(){
        if(Input.GetMouseButtonUp(0)){
            mouseDown = false;
        }
    }
}
