using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] public Rigidbody rigid;
    [SerializeField] Animator playerAnimator;
    [SerializeField] PlayerModel playerModel;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3 (x, 0,z);

        if (dir == Vector3.zero)
        {
            return;
        }

        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize ();
        }

        rigid.velocity = dir * playerModel.MoveSpeed;
        
        
        playerAnimator.SetFloat("Speed", Mathf.Abs(playerModel.MoveSpeed) * dir.sqrMagnitude);

      
        //rigid.rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), playerModel.Rate * Time.deltaTime);
        
    }
}

