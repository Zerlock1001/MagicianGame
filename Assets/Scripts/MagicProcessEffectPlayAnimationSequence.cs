using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MagicProcessEffectPlayAnimationSequence", menuName = "MagicProcessEffect/PlayAnimationSequence")]
public class MagicProcessEffectPlayAnimationSequence : MagicProcessEffect
{
    public string gameObjectName = "VideoPlayer";
    public string[] animationNames;
    public float speed = 1f;

    [System.NonSerialized] bool playing;

    public override void Apply()
    {
        if (playing || animationNames == null || animationNames.Length == 0)
        {
            return;
        }
        GameObject target = GameObject.Find(gameObjectName);
        if (target == null)
        {
            return;
        }
        Animator animator = target.GetComponent<Animator>();
        if (animator == null || MagicProcessManager.instance == null)
        {
            return;
        }
        playing = true;
        MagicProcessManager.instance.StartCoroutine(PlayAll(animator));
    }

    IEnumerator PlayAll(Animator animator)
    {
        for (int i = 0; i < animationNames.Length; i++)
        {
            string animationName = animationNames[i];
            if (string.IsNullOrEmpty(animationName))
            {
                continue;
            }
            animator.speed = speed;
            animator.Play(animationName, 0, 0f);
            animator.Update(0f);

            bool sawStart = false;
            while (true)
            {
                if (animator == null || MagicProcessManager.instance == null)
                {
                    playing = false;
                    yield break;
                }
                animator.speed = speed;
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName(animationName))
                {
                    if (stateInfo.normalizedTime < 0.99f)
                    {
                        sawStart = true;
                    }
                    else if (sawStart)
                    {
                        break;
                    }
                    else
                    {
                        animator.Play(animationName, 0, 0f);
                        animator.Update(0f);
                    }
                }
                yield return null;
            }
        }

        playing = false;
        if (MagicProcessManager.instance != null)
        {
            MagicProcessManager.instance.NextStep();
        }
    }
}
