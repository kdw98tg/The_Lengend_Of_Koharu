using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;
    public GameObject[] points;
    
    
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }

    public void RoomStart()
    {
        points[0].SetActive(true);
        points[1].SetActive(false);
        points[2].SetActive(false);
    }

    public void RoomEnd()
    {
        points[0].SetActive(false);
        points[1].SetActive(true);
        points[2].SetActive(false);
    }

    public void RoomKey()
    {
        points[0].SetActive(false);
        points[1].SetActive(false);
        points[2].SetActive(true);
    }
}

