using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class DialogueManager : MonoBehaviour
{
    public static event Action<GameObject, int> OnInvokeDialogue;
    public static DialogueManager Instance;
    private static List<Tuple<GameObject, int> > _dialogList;
    void Awake()
    {
        Instance = this;
        _dialogList = new List<Tuple<GameObject, int>> ();
    }

    public void InvokeDialogue(GameObject character, int sentenceFromList) {
        _dialogList.Add (new Tuple<GameObject, int> (character, sentenceFromList));
    }

    private void Update() {
        GameObject lastChar = null;
        Dialogue charDia = null;
        List<Tuple<GameObject, int>> toRemove = new List<Tuple<GameObject, int>> ();
        foreach (var tmp in _dialogList)
        {
            var character = tmp.Item1;
            if (character != lastChar)
            {
                charDia = character.GetComponent<Dialogue> ();
                lastChar = character;
            }
            var sentence = tmp.Item2;

            Debug.Assert (charDia != null, nameof(charDia) + " != null");
            if (!charDia.busy)
            {
                charDia.busy = true;
                toRemove.Add (tmp);
                OnInvokeDialogue?.Invoke (character, sentence);
            }
        }

        foreach (var garbage in toRemove)
        {
            _dialogList.Remove (garbage); 
        }
    }
}
