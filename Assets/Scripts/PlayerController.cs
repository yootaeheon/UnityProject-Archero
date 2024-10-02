using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { Attack, Clear, Dead, Size }
    [SerializeField] State curState = State.Attack;
    private BaseState[] states = new BaseState[(int)State.Size];
    public Coroutine arrowGetRoutine;
    public Coroutine arrowFireRoutine;
    private float arrowSpeed;
    public GameObject attackTarget;

    [SerializeField] GameObject arrowPrefabGameObject;
    [SerializeField] MonsterManager monsterManager;
   
    [SerializeField] Animator animator;
    [SerializeField] Transform respawnPos;
    [SerializeField] PooledObject arrowPrefab;
    [SerializeField] PooledObject arrowPrefab2;
    [SerializeField] PooledObject arrowPrefab3;
    [SerializeField] ObjectPool arrowPool;
    [SerializeField] PlayerModel playerModel;
    [SerializeField] PlayerMover playerMover;
    [SerializeField] GameObject player;

    [SerializeField] Transform muzzlePoint;

    private GameObject target;

    private static Vector3 attackVec;
    private void Awake()
    {
        states[(int)State.Attack] = new AttackState(this);
        states[(int)State.Clear] = new ClearState(this);
        states[(int)State.Dead] = new DeadState(this);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        arrowPool.CreatePool(arrowPrefab, 10, 10);


        states[(int)State.Attack].Enter();
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

    private class AttackState : PlayerModel
    {
        public GameObject attackTarget;
        public AttackState(PlayerController player) : base(player)
        {
            this.attackTarget = player.attackTarget;
        }

        public override void Enter()
        {
            if (player.playerMover.rigid.velocity.magnitude == 0)
            {
               player.arrowFireRoutine = player.StartCoroutine(player.FireRoutine());
              // player.fireRoutine2 = player.StartCoroutine(player.FireRoutine2());
               // player.fireRoutine3 = player.StartCoroutine(player.FireRoutine3());
            }
           
               
        }
        public override void Update()
        {
            Debug.Log("attackState Update 진입");
            //레이캐스트를 몬스터리스트에 있는 가장 가까운 몬스터를 쳐다보며 몬스터들에게 쏜다
            player.SetTarget();
            Debug.Log("SetTarget");
            
           //  if (attackTarget == null)
           //  {
           //      player.ChangeState(State.Clear);
           //  }
        }
       
    }

    private class ClearState : PlayerModel
    {
        public GameObject attackTarget;
        public ClearState(PlayerController player) : base(player)
        {
            this.attackTarget = player.attackTarget;
        }

        public override void Enter()
        {
           
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
           // player.Respawn();
        }

        public override void Update()
        {
            player.ChangeState(State.Attack);
        }
    }

    private void SetTarget()
    {
       
        if (MonsterManager.Instance.monsters.Count == 0)
            return;

        GameObject target;

        if (attackTarget == null)
        {
            target = MonsterManager.Instance.monsters[0];
        }
        else
        {
            target = attackTarget;
        }

       
        float curDistance = (target.transform.position - player.transform.position).sqrMagnitude;
        for (int i = 0; i < MonsterManager.Instance.monsters.Count; i++)
        {
            float distance = (MonsterManager.Instance.monsters[i].transform.position - player.transform.position).sqrMagnitude;
            if (curDistance > distance)
            {
                target = MonsterManager.Instance.monsters[i];

                attackTarget = target;
                Debug.Log(attackTarget.name);

                curDistance = distance;
            }
        }
        if (attackTarget != null)
        {
            curDistance = (attackTarget.transform.position - player.transform.position).sqrMagnitude;
        }

        if (curDistance > 250f)
        {
            attackTarget = null;
        }
    }


    private void Fire()
    {
        if (attackTarget == null)
            return;

        player.transform.LookAt(attackTarget.transform.position);

       
        PooledObject instance = arrowPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        Arrow arrow = instance.GetComponent<Arrow>();
        arrow.SetSpeed(arrowSpeed);

        Debug.Log("fire");
    }


    WaitForSeconds fireDelay = new WaitForSeconds(1f);
    IEnumerator FireRoutine() //기본 화살 
    {
        while (true)
        {
            Fire();
            yield return fireDelay;
            Debug.Log("fireCoroutine start");
        }
    }
    void StopFireCoroutine()
    {
        arrowFireRoutine = null;
    }


    Coroutine fireRoutine2; //double shot
    IEnumerator FireRoutine2()
    {
        while (true)
        {
            Fire();
            yield return fireDelay;
            Debug.Log("fireCoroutine2 start");
        }
    }

    Coroutine fireRoutine3;  //multy shot
    IEnumerator FireRoutine3()
    {
        while (true)
        {
            Fire();
            yield return fireDelay;
            Debug.Log("fireCoroutine2 start");
        }
    }
}

