using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {
    private Image _image;
    void Start()
    {
        
    }

    private void Awake() {
        GameManager.OnGameStateChange += GameManagerOnGameStateChanged;
        _image = GetComponent<Image> ();
    }

    private void OnDestroy() {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.End)
        {
            _image.enabled = true;
        }
    }
}
