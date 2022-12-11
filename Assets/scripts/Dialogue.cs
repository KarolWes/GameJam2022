using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private List<String> texts;

    [SerializeField] private GameObject self;

    [SerializeField] private GameObject bubble;

    private List<GameObject> _tmpBubble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DialogueManager.OnInvokeDialogue += DMInvokeDialogue;
        _tmpBubble = new List<GameObject>();
    }

    private void OnDestroy()
    {
        DialogueManager.OnInvokeDialogue -= DMInvokeDialogue;
    }

    async void CloseBubble()
    {
        await Task.Delay(5000);
        var toDestroy = _tmpBubble[0];
        _tmpBubble.Remove(_tmpBubble[0]);
        Destroy(toDestroy);
    }
    private void DMInvokeDialogue(GameObject character, int sentence)
    {
        if (character == self)
        {
            if (sentence < texts.Count)
            {
                var pos = self.transform.position;
                Debug.Log(texts[sentence]);
                foreach (var bu in _tmpBubble)
                {
                    Destroy(bu);
                }

                _tmpBubble = new List<GameObject>();
                var b = Instantiate(bubble, new Vector3(pos.x + 1, pos.y + 1), Quaternion.identity);
                b.GetComponentInChildren<TextMeshPro>().text = texts[sentence];
                _tmpBubble.Add(b);
                Debug.Log("bubble open");
                CloseBubble();
            }
        }
    }
}
