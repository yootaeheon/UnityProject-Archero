using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }

    public List<GameObject> monsters = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Monster"))
        {
            monsters.Add(other.gameObject);
            Debug.Log(other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            monsters.Remove(other.gameObject);
            Debug.Log(other.gameObject.name);
        }
    }
}
