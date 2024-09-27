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
            //몬스터 탐지하여 몬스터리스트에 추가
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
            //레이캐스트를 몬스터리스트에 있는 몬스터들에게 쏜다
            player.SetTarget();
            //가장 가까운 몬스터를 쳐다보며
            //
            //화살 옵젝풀로 생성하여 공격
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
            //모든 몬스터를 죽였을때 떨어진 골드 모두 캐릭터에게 흡수
        }
    }

    private class DeadState : PlayerModel
    {
        public DeadState(PlayerController player) : base(player)
        {
        }
        public override void Enter()
        {
            //부활 지점을 정하여 부활
            //부활 이펙트
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
        //코루틴으로 시도해보자
    }
}
