using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MagicProcessEffectPlayAnimation", menuName = "MagicProcessEffect/PlayAnimation")]
public class MagicProcessEffectPlayAnimation : MagicProcessEffect
{
    public string animationName;
    public string gameObjectName;
    public float speed = 1f;

    public override void Apply(){
        GameObject gameObject = GameObject.Find(gameObjectName);
        if(gameObject == null || gameObject.GetComponent<Animator>() == null){
            return;
        }
        if(!gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(animationName)){
            Debug.Log("PlayAnimation: " + animationName);
            Animator animator = gameObject.GetComponent<Animator>();
            animator.speed = speed;
            animator.Play(animationName);
            MagicProcessManager.instance.StartCoroutine(WaitForAnimationComplete(animator, animationName));
        }
        else{
            gameObject.GetComponent<Animator>().speed = speed;
        }
    }
    public override void UnApply(){
        GameObject gameObject = GameObject.Find(gameObjectName);
        if(gameObject == null || gameObject.GetComponent<Animator>() == null){
            return;
        }
        gameObject.GetComponent<Animator>().speed = 0f;
    }

    static IEnumerator WaitForAnimationComplete(Animator animator, string animationName){
        yield return null;
        while(true){
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f){
                break;
            }
            yield return null;
        }
        MagicProcessManager.instance.NextStep();
    }
}
