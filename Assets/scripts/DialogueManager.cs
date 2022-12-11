using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static event Action<GameObject, int> OnInvokeDialogue;
    public static DialogueManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public void InvokeDialogue(GameObject character, int sentenceFromList)
    {
        OnInvokeDialogue?.Invoke(character, sentenceFromList);
    }
}
