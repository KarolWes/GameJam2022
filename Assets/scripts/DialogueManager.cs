using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        if (_dialogList.Count > 0)
        {
            var tmp = _dialogList[0];
            var character = tmp.Item1;
            var sentence = tmp.Item2;
            _dialogList.Remove (tmp);
            OnInvokeDialogue?.Invoke(character, sentence);
        }
    }
}
