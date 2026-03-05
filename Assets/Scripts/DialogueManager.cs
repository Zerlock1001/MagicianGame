using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public GameObject[] choiceButtons;
    public GameObject nameTextObject;
    public GameObject portraitObject;
    public GameObject dialogueTextObject;
    public static DialogueManager Instance;
    public DialogueContent currentDialogueContent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }
    void Test(){
        if(Input.GetKeyDown(KeyCode.Space)){
            AwakeDialogue(currentDialogueContent);
        }
    }
    public void AwakeDialogue(DialogueContent dialogueContent){
        currentDialogueContent = dialogueContent;
        nameTextObject.GetComponent<TMP_Text>().text = currentDialogueContent.name;
        portraitObject.GetComponent<Image>().sprite = currentDialogueContent.portrait;
        dialogueTextObject.GetComponent<TMP_Text>().text = currentDialogueContent.content;
        for(int i = 0; i < choiceButtons.Length; i++){
            choiceButtons[i].SetActive(false);
        }
        for(int i = 0; i < currentDialogueContent.choicesCount; i++){
            choiceButtons[i].SetActive(true);
            choiceButtons[i].GetComponentInChildren<TMP_Text>().text = currentDialogueContent.choices[i];
        }
    }
    public void ChoiceButtonClick(int choiceIndex){
        Debug.Log("ChoiceButtonClick: " + choiceIndex);
        if(choiceIndex>=currentDialogueContent.effects.Length){
        }
        else if(currentDialogueContent.effects[choiceIndex].effect!=null){
            foreach(ChoiceEffect effect in currentDialogueContent.effects[choiceIndex].effect){
                effect.DoEffect();
            }
        }
        if(choiceIndex<currentDialogueContent.followingDialogues.Length){
            if(currentDialogueContent.followingDialogues[choiceIndex] != null){
             DialogueManager.Instance.AwakeDialogue(currentDialogueContent.followingDialogues[choiceIndex]);
            }
            else{
                EndDialogue();
            }
        }
        else{
            EndDialogue();
        }
    }
    public void ClickAnyWhere(){
        if(currentDialogueContent.choicesCount>0){
            return;
        }
        else{
            EndDialogue();
        }
    }
    public void EndDialogue(){
        gameObject.SetActive(false);
    }
}