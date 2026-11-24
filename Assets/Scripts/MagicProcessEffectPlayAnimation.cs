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
        bool started = false;
        while(true){
            if(animator == null || MagicProcessManager.instance == null){
                yield break;
            }
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName(animationName)){
                // 播到最后一帧就进入下一步。不再因为 DualHandRub 未完成而把速度压成 0。
                if(stateInfo.normalizedTime < 0.99f){
                    started = true;
                }
                else if(started){
                    break;
                }
            }
            yield return null;
        }
        if(MagicProcessManager.instance != null){
            MagicProcessManager.instance.NextStep();
        }
    }
}
