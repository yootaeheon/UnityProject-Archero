using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rigid.velocity = new Vector3(x * moveSpeed, 0 , z * moveSpeed);
        
    }
}
