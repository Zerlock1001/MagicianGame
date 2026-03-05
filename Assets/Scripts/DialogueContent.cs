using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "DialogueContent", menuName = "Scriptable Objects/DialogueContent")]
public class DialogueContent : ScriptableObject
{
    public string name;
    public Sprite portrait;
    public string content;
    public int choicesCount = 0;
    public string[] choices;

    public DialogueContent[] followingDialogues;
    [SerializeField] public Effects[] effects;
    [System.Serializable] public class Effects{
        public List<ChoiceEffect> effect;
    }
}
