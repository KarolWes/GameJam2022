using System;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private String _type = "ghost";
    [SerializeField] private int _hp = 1;
    private int _possessionCount = 0;
    private Vector2 _startPos;
    private int _killCount = 0;
    private List<GameObject> _inventory;

    private void Start()
    {
        _inventory = new List<GameObject>();
    }

    public List<GameObject> Inventory
    {
        get => _inventory;
    }

    public string Type
    {
        get => _type;
        set => _type = value;
    }

    public int Hp
    {
        get => _hp;
        set => _hp = value;
    }

    public int PossessionCount
    {
        get => _possessionCount;
        set => _possessionCount = value;
    }

    public Vector2 StartPos
    {
        get => _startPos;
        set => _startPos = value;
    }

    public int KillCount
    {
        get => _killCount;
        set => _killCount = value;
    }
}
