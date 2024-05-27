using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public MyPlayerController MyPlayer { get; set; }
    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    public void Add(PlayerInfo info, bool myPlayer = false)
    {
        if (myPlayer)
        {
            GameObject go = Managers.Resource.Instantiate("Creature/MyPlayer");
            go.name = info.Name;
            _objects.Add(info.PlayerId, go);

            MyPlayer = go.GetComponent<MyPlayerController>();
            MyPlayer.id = info.PlayerId;
            // 에러나서 일단 주석처리 2024.05.27
            //MyPlayer.CellPos = new Vector3Int(info.PosX, info.PosY, 0);

        }
        else
        {
            GameObject go = Managers.Resource.Instantiate("Creature/Player");
            go.name = info.Name;
            _objects.Add(info.PlayerId, go);

            PlayerController pc = go.GetComponent<PlayerController>();
            pc.id = info.PlayerId;
            // 에러나서 일단 주석처리 2024.05.27
            //pc.CellPos = new Vector3Int(info.PosX, info.PosY, 0);
        }
    }
    public void Add(int id, GameObject go)
    {
        _objects.Add(id, go);
    }

    public void Remove(int id)
    {
        _objects.Remove(id);
    }

    public void RemoveMyPlayer()
    {
        if (MyPlayer == null)
            return;

        Remove(MyPlayer.id);
        MyPlayer = null;
    }

    public GameObject Find(Vector3Int cellPos)
    {
        foreach (GameObject obj in _objects.Values)
        {
            CreatureController cc = obj.GetComponent<CreatureController>();
            if (cc == null)            
                continue;
            
            if (cc.CellPos == cellPos)
                return obj;
        }

        return null;
    }

    public GameObject Find(Func<GameObject, bool> condition)
    {
        foreach (GameObject obj in _objects.Values)
        {
            if (condition.Invoke(obj))
                return obj;
        }

        return null;
    }

    public void Clear()
    {
        _objects.Clear();
    }
}
