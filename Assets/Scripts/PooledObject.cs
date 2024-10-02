using System.Collections;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] bool autoRelease;

    [SerializeField] float releaseTime;
    [SerializeField] PooledObject prefab;
    private ObjectPool arrowPool;
  //  [SerializeField] public ObjectPool returnPool;
    public ObjectPool ArrowPool { get { return arrowPool; } set { arrowPool = value; } }

    private void OnEnable()
    {
        if (arrowPool == null)
        {
            arrowPool = FindObjectOfType<ObjectPool>();
        }

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
         arrowPool.ReturnPool(this);
    }
}
