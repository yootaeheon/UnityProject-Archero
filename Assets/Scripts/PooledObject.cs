using System.Collections;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool arrowPool;
    public ObjectPool ArrowPool { get { return arrowPool; } set { arrowPool = value; } }

    public void Release()
    {
        if (arrowPool != null)
        {
            arrowPool.ReturnPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
