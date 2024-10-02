using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab; 
    [SerializeField] float curHp;
    [SerializeField] float maxHp;

    private void Start()
    {
        curHp = maxHp;
    }

    public void TakeHit(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            curHp = 0;
            Destroy(gameObject);
        }
    }
}
    

