using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UIElements;

public class Mapmanager : MonoBehaviour
{
    public Transform[] mapPos;
    public Transform[] Fastpath;

    public void GetMapPosition()
    {
        for(int i = 0; i <= 122; i++)
        {
            mapPos[i] = GameObject.Find("Board").transform.Find((i + 1).ToString()).transform;
        }
        for(int i = 123; i<135; i++)
        {
            Fastpath[i] = GameObject.Find("Board").transform.Find((i + 1).ToString()).transform;
        }
    }
    public Transform GetPosition(int Player_Pos)
    {
        GameObject Pos = GameObject.Find("Board").transform.Find(Player_Pos.ToString()).gameObject;
        return Pos.transform;
    }
}
