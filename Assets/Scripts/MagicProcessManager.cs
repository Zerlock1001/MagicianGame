using UnityEngine;

public class MagicProcessManager : MonoBehaviour
{
    public static MagicProcessManager instance;
    public MagicProcess[] processesOrigin;
    MagicProcess currentProcess;
    MagicProcess[] processes;
    int currentProcessIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        DuplicateProcesses();
        currentProcessIndex = 0;
        currentProcess = processes[currentProcessIndex];
    }
    void DuplicateProcesses(){
        processes = new MagicProcess[processesOrigin.Length];
        for(int i = 0; i < processesOrigin.Length; i++){
            processes[i] = ScriptableObject.Instantiate(processesOrigin[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessApply();
    }
    public void NextStep(){
        Debug.Log("NextStep: " + currentProcessIndex);
        currentProcessIndex++;
        if(currentProcessIndex >= processes.Length){
            return;
        }
        currentProcess = processes[currentProcessIndex];
    }
    void ProcessApply(){
        if(currentProcess.ConditionMet()){
            currentProcess.Apply();
        }
        else{
            currentProcess.UnApply();
        }
    }
}
