using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimHelper : MonoBehaviour
{
    public Enemy enemy;
    public int timeToGoDown;
    private float _count;
    private bool _countingTime;
    
    public void DisposeEnemy()
    {
        enemy.DisposeEnemy();
    }

    /// <summary>
    /// Llamado por un evento de animacion al subir que hace arrancar una cuenta regresiva para ver cuanto tiempo el enemigo va a mantenerse en el lugar
    /// </summary>
    public void StartCountUp()
    {
        _countingTime = !_countingTime;
        _count = 0;
    }
    
    private void Update()
    {
        if (!_countingTime)
            return;

        _count += Time.deltaTime;
        if (_count >= timeToGoDown)
        {
            _countingTime = false;
            GetComponent<Animator>().SetTrigger("Down");
        }
    }
}
