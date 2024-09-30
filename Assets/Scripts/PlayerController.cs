using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public enum State { BattleIn, Attack, BattleOut, Dead, Size }
    [SerializeField] State curState = State.BattleIn;
    private BaseState[] states = new BaseState[(int)State.Size];

    private float arrowSpeed;
    private GameObject monsterList;

    [SerializeField] ObjectPool arrowPool;
    [SerializeField] PlayerModel playerModel;

    [SerializeField] GameObject player;
    
    [SerializeField] Transform muzzlePoint;

    private void Awake()
    {
        states[(int)State.BattleIn] = new BattleInState(this);
        states[(int)State.Attack] = new AttackState(this);
        states[(int)State.BattleOut] = new BattleOutState(this);
        states[(int)State.Dead] = new DeadState(this);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
      

        states[(int)State.BattleIn].Enter();

        monsterList.GetComponent<Monster>();
    }

    private void Update()
    {
        states[(int)curState].Update();
    }

    public void ChangeState(State nextState)
    {
        states[(int)curState].Exit();
        curState = nextState;
        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private class PlayerModel : BaseState
    {
        public PlayerController player;
        public PlayerModel(PlayerController player)
        {
            this.player = player;

        }
    }

    private class BattleInState : PlayerModel
    {
        public BattleInState(PlayerController player) : base(player)
        {
        }
        public override void Enter()
        {
            //���� Ž���Ͽ� ���͸���Ʈ�� �߰�
        }
        public override void Exit()
        {
            player.ChangeState(State.Attack);
        }
    }

    private class AttackState : PlayerModel
    {
        public AttackState(PlayerController player) : base(player)
        {

        }
        public override void Update()
        {
            //����ĳ��Ʈ�� ���͸���Ʈ�� �ִ� ���� ����� ���͸� �Ĵٺ��� ���͵鿡�� ���
            player.SetTarget();
           
            //ȭ�� ����Ǯ�� �����Ͽ� ����
            player.Fire();
        }
        public override void Exit()
        {
            player.ChangeState(State.BattleOut);
        }
    }

    private class BattleOutState : PlayerModel
    {
        public BattleOutState(PlayerController player) : base(player)
        {
        }

        public override void Enter()
        {
            //��� ���͸� �׿����� ������ ��� ��� ĳ���Ϳ��� ���
        }
    }

    private class DeadState : PlayerModel
    {
        public DeadState(PlayerController player) : base(player)
        {
        }
        public override void Enter()
        {
            //��Ȱ ������ ���Ͽ� ��Ȱ
            //��Ȱ ����Ʈ
            player.Respawn();
        }

        public override void Exit()
        {
            player.ChangeState(State.BattleIn);
        }
    }

    private void SetTarget()
    {

        if (Monster.monsters.Count == 0)
            return;

        Monster target = Monster.monsters[0];
        float curDistance = (target.transform.position - player.transform.position).sqrMagnitude;
        for (int i = 0; i < Monster.monsters.Count; i++)
        {
            float distance = (Monster.monsters[i].transform.position - target.transform.position).sqrMagnitude;
            if (curDistance < distance)
                continue;

            Ray ray = new Ray(player.transform.transform.position, Monster.monsters[i].transform.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null)
                    continue;
            }

            target = Monster.monsters[i];
        }



        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, z);

      //  if (dir == Vector3.zero && Monster.monsters != 0)
      //  {
      //      transform.LookAt(target.transform);
      //  }
    }

   
    private void Fire()
    {
        PooledObject instance = arrowPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        Arrow arrow = instance.GetComponent<Arrow>();
            arrow.SetSpeed(arrowSpeed);
    }

    private void Respawn()
    {
        //�ڷ�ƾ���� �õ��غ���
    }
}
