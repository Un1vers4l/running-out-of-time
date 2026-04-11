using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public RuntimeDialogueGraph RuntimeGraph;

    [Header("UI Components")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText; 
    public TextMeshProUGUI DialogueText;

    private RuntimeDialogueNode _currentNode;
    private  Dictionary<string, RuntimeDialogueNode> _nodeLookup = new Dictionary<string, RuntimeDialogueNode>();
    private void Start(){
        
    }


    public void StartDialogue(RuntimeDialogueGraph RuntimeGraph){
        
        foreach (var node in RuntimeGraph.AllNodes){
            _nodeLookup[node.NodeID] = node;
        }

        if(!string.IsNullOrEmpty(RuntimeGraph.EntryNodeID)){
            ShowNode(RuntimeGraph.EntryNodeID);
        }else{
            EndDialogue();
        }
    }

    private void Update(){
        if(Mouse.current.leftButton.wasPressedThisFrame && _currentNode != null){
            if(!string.IsNullOrEmpty(_currentNode.NextNodeID)){
                ShowNode(_currentNode.NextNodeID);
            }else{
                EndDialogue();
            }
        }
    }

    private void ShowNode(string nodeID){
        if(!_nodeLookup.ContainsKey(nodeID)){
            EndDialogue();
            return;
        }

        _currentNode = _nodeLookup[nodeID];

        DialoguePanel.SetActive(true);
        Debug.Log(_currentNode.DialogueText);
        SpeakerNameText.SetText(_currentNode.SpeakerName);
        DialogueText.SetText(_currentNode.DialogueText);
    }

    private void EndDialogue(){
        DialoguePanel.SetActive(false);
        _currentNode = null;
    }
}