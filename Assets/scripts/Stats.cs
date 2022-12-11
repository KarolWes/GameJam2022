using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
 
        if(File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        BinaryFormatter bf = new BinaryFormatter();
        var l = new List<int>();
        l.Add(_possessionCount);
        l.Add(_killCount);
        bf.Serialize(file, l);
        file.Close();
    }
 
    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
 
        if(File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }
 
        BinaryFormatter bf = new BinaryFormatter();
        List<int> data = (List<int>) bf.Deserialize(file);
        file.Close();

        _possessionCount = data[0];
        _killCount = data[1];
 
        Debug.Log(_possessionCount);
        Debug.Log(_killCount);
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
