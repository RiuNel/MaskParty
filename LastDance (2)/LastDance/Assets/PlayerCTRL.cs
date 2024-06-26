using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Unity.VisualScripting;

public class PlayerCTRL : MonoBehaviourPun
{
    public Mapmanager mapmanager;
    public dice dice1;
    public dice dice2;
    public int PlayerPos = 0;
    public Networkmanager networkmanager;
    public GameObject Arrow;
    public Transform TargetPos;
    public void Move()
    {
        switch (PlayerPos)
        {
            case 14:
                Choose(132);
                break;
            case 22:
                Choose(118);
                break;
        }
        PlayerPos += (dice1.num + dice2.num);

    }
    public bool isChange()
    {
        GameObject Arrow1 = Instantiate(Arrow, gameObject.transform.position + new Vector3(10, 0, 0), Quaternion.identity);
        GameObject Arrow2 = Instantiate(Arrow, gameObject.transform.position + new Vector3(0, 0, 10), Quaternion.Euler(0,0,90));
        while(Arrow1.GetComponent<Arrow>().ClickOn() || Arrow2.GetComponent<Arrow>().ClickOn())
        {
            if (Arrow1.GetComponent<Arrow>().ClickOn())
            {
                return false;
            }
            else if (Arrow2.GetComponent<Arrow>().ClickOn())
            {
                return true;
            }
        }
        return false;
        
    }
    public void Choose(int Fastpath)
    {
        if(isChange())
        {
            TargetPos = mapmanager.Fastpath[Fastpath];
            PlayerPos = Fastpath;
        }
        else
        {
            TargetPos = mapmanager.mapPos[++PlayerPos];
        }
        
    }
}
