using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform muzzlePoint;

    [SerializeField] float arrowSpeed;


    private void Update()
    {
        fff = StartCoroutine(FireRoutine());
    }

    private void Shoot()
    {
        GameObject instance = Instantiate(prefab, muzzlePoint.position, muzzlePoint.rotation);
        Arrow arrow = instance.GetComponent<Arrow>();
        arrow.SetSpeed(arrowSpeed);
    }




    WaitForSeconds fireDelay = new WaitForSeconds(3f);
    Coroutine fff;
    IEnumerator FireRoutine()
    {
        while (true)
        {
            Shoot();
            yield return fireDelay;
        }
    }
    void StopFireCoroutine()
    {
        fff = null;
    }
}
