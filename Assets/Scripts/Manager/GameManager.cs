using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] GameObject SlotMachine;
    [SerializeField] PlayerModel PlayerModel;


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


    private void Start()
    {
        PlayerModel.CurExp = 0;
        SlotMachine.SetActive(false);
    }
    private void Update()
    {
        if (PlayerModel.CurExp >= PlayerModel.MaxExp)
        {
            SlotMachine.SetActive(true);
            PlayerModel.CurExp = 0;
        }
    }
}
