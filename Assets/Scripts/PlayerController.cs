using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { BattleIn, Attack, BattleOut, Dead, Size }
    [SerializeField] State curState = State.BattleIn;
    private BaseState[] states = new BaseState[(int)State.Size];

    List<GameObject> monsterList = new List<GameObject>();
    [SerializeField] ObjectPool arrowPool;
    [SerializeField] PlayerModel playerModel;

    [SerializeField] GameObject player;
    [SerializeField] Transform monster;
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
        monster = GameObject.FindGameObjectWithTag("Monster").transform;

        states[(int)State.BattleIn].Enter();
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
            //����ĳ��Ʈ�� ���͸���Ʈ�� �ִ� ���͵鿡�� ���
            player.SetTarget();
            //���� ����� ���͸� �Ĵٺ���
            //
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
        Ray arrowRay = new Ray(muzzlePoint.position, monster.position);
        if (Physics.Raycast(arrowRay, out RaycastHit hit))
        {
            Debug.DrawRay(muzzlePoint.position, monster.position * hit.distance, Color.red, 1f);
        }
    }

    private void Fire()
    {
        PooledObject instance = arrowPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);       
        Arrow arrow = instance.GetComponent<Arrow>();
    }

    private void Respawn()
    {
        //�ڷ�ƾ���� �õ��غ���
    }
}
