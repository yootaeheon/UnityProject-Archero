using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PooledObject arrowPrefab;
    [SerializeField] int size;
    [SerializeField] int capacity;

    private Stack<PooledObject> arrowPool;

    public void CreatePool(PooledObject prefab, int size, int capacity)
    {
        this.arrowPrefab = prefab;
        this.size = size;
        this.capacity = capacity;

        arrowPool = new Stack<PooledObject>(capacity);
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.ArrowPool = this;
            arrowPool.Push(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (arrowPool.Count > 0)
        {
            PooledObject instance = arrowPool.Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.gameObject.SetActive(true);
            instance.ArrowPool = this;
            return instance;
        }
        else
        {
            PooledObject instance = Instantiate(arrowPrefab);
           
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            return instance;
        }
    }

    public void ReturnPool(PooledObject instance)
    {
       // if (arrowPool.Count < capacity)
        {
            Debug.Log("return");
            instance.gameObject.SetActive(false);
            arrowPool.Push(instance);
        }
       
    }
}
