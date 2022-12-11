using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private List<String> texts;

    [SerializeField] private GameObject self;

    [SerializeField] private GameObject bubble;
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
        // bubble.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnDestroy()
    {
        DialogueManager.OnInvokeDialogue -= DMInvokeDialogue;
    }

    async void CloseBubble()
    {
        await Task.Delay(5000);
        bubble.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void DMInvokeDialogue(GameObject character, int sentence)
    {
        if (character == self)
        {
            if (sentence < texts.Count)
            {
                Debug.Log(texts[sentence]);
                bubble.GetComponent<SpriteRenderer>().enabled = true;
                Debug.Log("bubble open");
                bubble.transform.position = self.transform.position + new Vector3(1, 1);
                CloseBubble();
            }
        }
    }
}
