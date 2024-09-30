using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static List<Monster> monsters = new List<Monster>();

    private void Update()
    {
        monsters.Add(this);
    }
}

