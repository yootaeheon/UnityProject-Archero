using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] Monster monster;
    [SerializeField] Rigidbody rb;


    private void OnEnable()
    {
       // rb.velocity.magnitude = 0;
        rb.AddRelativeForce(Vector3.forward * 30, ForceMode.Impulse);
    }
    private void Start()
    {
    }
    
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void OnColliderEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            monster.TakeHit(damage);
            Debug.Log(monster.name);
        }
    }

   
}
