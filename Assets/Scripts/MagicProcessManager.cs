using UnityEngine;

public class MagicProcessManager : MonoBehaviour
{
    public MagicProcess[] processesOrigin;
    MagicProcess currentProcess;
    MagicProcess[] processes;
    int currentProcessIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    void ProcessApply(){
        if(currentProcess.ConditionMet()){
            currentProcess.Apply();
        }
    }
}
