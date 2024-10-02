using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] float curHp;
    public float CurHp { get { return curHp; } set { curHp = value; } }

    [SerializeField] float maxHp;
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] float rate;
    public float Rate { get { return rate; } set { rate = value; } }

    [SerializeField] float shootSpeed;
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }

    [SerializeField] float arrowSpeed;
    public float ArrowSpeed { get { return arrowSpeed; } set { arrowSpeed = value; } }

    [SerializeField] float curExp;
    public float CurExp { get { return curExp; } set { curExp = value; } }

    [SerializeField] float maxExp;
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }
}
