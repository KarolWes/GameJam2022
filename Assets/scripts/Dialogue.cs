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
    [SerializeField] private int timeSec = 2;
    

    private List<GameObject> _tmpBubble;

    public bool busy = false;
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
        while (_tmpBubble.Count > 0)
        {
            var tmp = _tmpBubble[0];
            _tmpBubble.Remove(tmp);
            DestroyImmediate(tmp);
        }
    }

    async Task ManageBubble(Vector3 pos, string t)
    {
        var b = Instantiate(bubble, new Vector3(pos.x + 1, pos.y + 1), Quaternion.identity);
        //b.GetComponent<SpriteRenderer>().sortingOrder = 100;
        b.GetComponentInChildren<TextMeshPro>().text = t;
        _tmpBubble.Add(b);
        await Task.Delay(timeSec*1000);
        if (_tmpBubble.Count > 0)
        {
            var toDestroy = _tmpBubble[0];
            _tmpBubble.Remove(_tmpBubble[0]);
            DestroyImmediate(toDestroy);
        }
        await Task.Yield();
    }
    private void DMInvokeDialogue(GameObject character, int sentence)
    {
        if (character == self)
        {
            if (sentence < texts.Count)
            {
                
                foreach (var bu in _tmpBubble)
                {
                    Destroy(bu);
                }

                _tmpBubble = new List<GameObject>();
                var textTmp = new List<String>();
                var tmp = "";
                var ar = texts[sentence].Split(' ');
                foreach (var s in ar)
                {
                    if (tmp.Length + s.Length < 30)
                    {
                        tmp += " " + s;
                    }
                    else
                    {
                        tmp += "...";
                        textTmp.Add(tmp);
                        Debug.Log(tmp);
                        tmp = s;
                    }
                }
                textTmp.Add(tmp);
                Show(textTmp);
            }
        }
    }

    private async void Show(List<String> textTmp)
    {
        foreach(var t in textTmp)
        {
            var pos = self.transform.position;
            await ManageBubble(pos,t);
        }

        busy = false;
    }
}
