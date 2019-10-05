using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Martillo : MonoBehaviour
{
    public float maxOffset;
    public float smoothSpeed = 0.125f;
    public Transform idleSpot;
    public MartilloAnim_helper model;
    private bool _moving;
    private Enemy _currentEnemyHitted;

    public void DoTheHit(Enemy enemy, Action<int> callback)
    {
        //Hacer que el martillo vaya hacia el target
        //Ejecute animacion de golpe
        //Vuelva a su lugar

        Vector3 startPos = transform.position;
        Vector3 endPos = enemy.spawner.hammerPlace.transform.position;
        _currentEnemyHitted = enemy;
        StartCoroutine(AproachEnemy(startPos, endPos));
        
    }

    private IEnumerator AproachEnemy(Vector3 startPos, Vector3 endPos)
    {
        _moving = true;
        while (Vector3.Distance(transform.position, endPos) >= maxOffset)
        {
            Vector3 smoothPos = Vector3.Lerp(transform.position, endPos, smoothSpeed);
            transform.position = smoothPos;
            yield return new WaitForEndOfFrame();
        }

        transform.position = endPos;
   
        GetComponentInChildren<Animator>().SetTrigger("Hit");

        

    }

    private IEnumerator AproachIdlePlace(Vector3 startPos, Vector3 endPos)
    {
        
        while (Vector3.Distance(transform.position, endPos) >= maxOffset)
        {
            Vector3 smoothPos = Vector3.Lerp(transform.position, endPos, smoothSpeed);
            transform.position = smoothPos;
            yield return new WaitForEndOfFrame();
        }

        transform.position = endPos;
        model.transform.localPosition = model.localPosOrigin;
        _moving = false;
    }

    public void GoBackToPos()
    {
        StartCoroutine(AproachIdlePlace(transform.position , idleSpot.position));
    }

    public void DamageTarget()
    {
        Debug.Log("Te pego");
        _currentEnemyHitted.DamageEnemy(1);
    }
    private void FixedUpdate()
    {
        if (_moving)
            return;

        transform.position = idleSpot.transform.position;
    }
    
    public void FreeHammerAnimationHit()
    {
        //isHitting = false;
        model.transform.localPosition = model.localPosOrigin;
    }
}
