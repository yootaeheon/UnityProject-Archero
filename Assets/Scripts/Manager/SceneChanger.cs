using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject SloatMachineCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" /*&& playerController.attackTarget == null*/)
        {
           SceneManager.LoadScene("Stage2");
           SloatMachineCanvas.SetActive(false);
          //  playerController.ChangeState(player.playerController.State.Attack);
        }
    }
}
