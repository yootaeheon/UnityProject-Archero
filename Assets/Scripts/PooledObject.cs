using System.Collections;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] bool autoRelease;
    [SerializeField] float releaseTime;

    private ObjectPool arrowPool;
    public ObjectPool ArrowPool { get { return arrowPool; } set { arrowPool = value; } }

    private void OnEnable()
    {
        if (autoRelease)
        {
            StartCoroutine(ReleaseRoutine());
        }
    }

    IEnumerator ReleaseRoutine()
    {
        yield return new WaitForSeconds(releaseTime);
        Release();
    }

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
