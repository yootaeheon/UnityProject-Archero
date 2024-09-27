using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] float hp;
    public float HP { get { return hp; } set { hp = value; } }

    [SerializeField] float moveSpeed;
    public float MoveSpeed {  get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] float rate;
    public float Rate { get { return rate; } set { rate = value; } }

    [SerializeField] float shootSpeed;
    public float ShootSpeed { get {return shootSpeed; } set { shootSpeed = value; } }

    [SerializeField] float arrowSpeed;
    public float ArrowSpeed { get { return arrowSpeed; } set { arrowSpeed = value; } }
}
