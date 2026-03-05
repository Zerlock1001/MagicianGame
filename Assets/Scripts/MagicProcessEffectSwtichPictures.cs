using UnityEngine;

[CreateAssetMenu(fileName = "MagicProcessEffectSwitchPictures", menuName = "MagicProcessEffect/SwitchPictures")]
public class MagicProcessEffectSwitchPictures : MagicProcessEffect
{
    public Sprite[] pictures;
    public string gameObjectName;
    public int currentPictureIndex = 0;
    public float switchTime = 1f;
    float switchTimeCounter = 0f;

    // 运行时专用变量，不序列化到资产，修改仅对本次运行生效
    [System.NonSerialized] int _runtimePictureIndex;
    [System.NonSerialized] bool _runtimeInitialized;

    public override void Apply(){
        if (!_runtimeInitialized)
        {
            _runtimePictureIndex = currentPictureIndex;
            _runtimeInitialized = true;
        }

        GameObject gameObject = GameObject.Find(gameObjectName);
        if(gameObject == null){
            return;
        }
        if(pictures.Length > 0 && _runtimePictureIndex >= 0 && _runtimePictureIndex < pictures.Length && switchTimeCounter >= switchTime){
            gameObject.GetComponent<SpriteRenderer>().sprite = pictures[_runtimePictureIndex];
            _runtimePictureIndex++;
            switchTimeCounter = 0f;
        }
        switchTimeCounter += Time.deltaTime;
    }
}
