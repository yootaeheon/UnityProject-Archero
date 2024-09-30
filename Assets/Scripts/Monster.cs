using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();

    private void Start()
    {
        monsters.Add(this);
    }
}

