using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { BattleIn, Attack, BattleOut, Dead, Size }
    [SerializeField] State curState = State.BattleIn;
    private BaseState[] states = new BaseState[(int)State.Size];

    private float arrowSpeed;
    private Monster attackTarget;
    [SerializeField] Monster monsterList;

    [SerializeField] ObjectPool arrowPool;
    [SerializeField] PlayerModel playerModel;

    [SerializeField] GameObject player;

    [SerializeField] Transform muzzlePoint;

    private Monster target;
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
    }

    private void Update()
    {
        states[(int)curState].Update();
        SetTarget();
        Fire();
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
           // player.SetTarget();

            //ȭ�� ����Ǯ�� �����Ͽ� ����
            // player.Fire();
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

        if (monsterList.monsters.Count == 0)
            return;

        Monster target = monsterList.monsters[0];
        float curDistance = (target.transform.position - player.transform.position).sqrMagnitude;
        for (int i = 0; i < monsterList.monsters.Count; i++)
        {
            float distance = (monsterList.monsters[i].transform.position - target.transform.position).sqrMagnitude;
            if (curDistance < distance)
                continue;

            Ray ray = new Ray(player.transform.transform.position, monsterList.monsters[i].transform.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null)
                    continue;
            }

            target = monsterList.monsters[i];

            attackTarget = target;
        }

        player.transform.LookAt(target.transform.position);
    }


    private void Fire()
    {
        if (attackTarget == null)
            return;

        PooledObject instance = arrowPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        Arrow arrow = instance.GetComponent<Arrow>();
        arrow.SetSpeed(arrowSpeed);

        Debug.Log("fire");
    }

    private void Respawn()
    {
        //�ڷ�ƾ���� �õ��غ���
    }
}
