using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpVar : MonoBehaviour
{
    [SerializeField] Slider playerHpVar;
    [SerializeField] Transform player;
    [SerializeField] PlayerModel playerModel;

    [SerializeField] float curHP;
    [SerializeField] float maxHP;

    private void Start()
    {
        curHP = playerModel.CurHp;
        maxHP = playerModel.MaxHp;
    }
    private void Update()
    {
        transform.position = player.position;
        playerHpVar.value = curHP / maxHP;

        transform.Rotate(Vector3.right * 0);
    }
}
