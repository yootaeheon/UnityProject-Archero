using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TittleScene : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    [SerializeField] Animator animator;
 


    private void Start()
    {
        shotRoutine = StartCoroutine(ShotRoutine());

        animator.SetFloat("Speed", 2);
    }

    public void StartGame()
    {
       SceneManager.LoadScene("Stage1");

        shotRoutine = null;
       
    }

    Coroutine shotRoutine;
    WaitForSeconds ShotDelay = new WaitForSeconds(1f);
    IEnumerator ShotRoutine()
    {
        animator.SetTrigger("Shot");
        yield return ShotDelay;
    }
}
