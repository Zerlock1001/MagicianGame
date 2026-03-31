using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
[CreateAssetMenu(fileName = "MagicProcessBlackScreen", menuName = "MagicProcess/BlackScreen")]
public class MagicProcessBlackScreen: MagicProcess
{
    public string blackScreenName;
    public bool fadeIn = true;
    public float fadeTime = 2f;
    [System.NonSerialized] public bool isFading = false;
    [System.NonSerialized] private GameObject cachedBlackScreen;
    public override bool ConditionMet(){
        return true;
    }
    public override void Apply(){
        if(isFading){
            return;
        }
        isFading = true;

        // GameObject.Find 只能找到激活状态的物体；当目标之前被 SetActive(false) 后会返回 null。
        // 这里先复用缓存，再兜底用 Resources.FindObjectsOfTypeAll 查找非激活物体。
        if(cachedBlackScreen == null){
            cachedBlackScreen = GameObject.Find(blackScreenName);
            if(cachedBlackScreen == null && !string.IsNullOrEmpty(blackScreenName)){
                cachedBlackScreen = Resources
                    .FindObjectsOfTypeAll<GameObject>()
                    .FirstOrDefault(go => go.name == blackScreenName);
            }
        }

        if(cachedBlackScreen == null){
            Debug.LogError($"[MagicProcessBlackScreen] 找不到黑屏物体: {blackScreenName}");
            isFading = false;
            MagicProcessManager.instance.NextStep();
            return;
        }

        Image img = cachedBlackScreen.GetComponent<Image>();
        if(img == null){
            Debug.LogError($"[MagicProcessBlackScreen] 黑屏物体缺少 Image 组件: {blackScreenName}");
            isFading = false;
            MagicProcessManager.instance.NextStep();
            return;
        }

        cachedBlackScreen.SetActive(true);
        img.DOFade(fadeIn ? 1f : 0f, fadeTime).OnComplete(() => {
            if(!fadeIn){
                cachedBlackScreen.SetActive(false);
            }
            isFading = false;
            MagicProcessManager.instance.NextStep();
        });
    }
}
