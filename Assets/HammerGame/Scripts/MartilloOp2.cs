using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MartilloOp2 : MonoBehaviour
{
    //public ParticleSystem starsParticle_hit;
    private bool isHitting = false;
    public float maxHitDistance;
    //public float smoothSpeed = 0.125f;
    AudioSource _audioSourcesource;
    [SerializeField]
    private AudioClip pinchoSound;
    [SerializeField]
    private AudioClip enemySound;
    public MartilloAnim_helper model;
    //private bool _moving;
    private Enemy _currentEnemyHitted;

    private void Awake()
    {
        _audioSourcesource = GetComponent<AudioSource>();
    }

    public void DoTheHit(Enemy enemy, Action<int> callback)
    {
        _currentEnemyHitted = enemy;

        Debug.Log("Voy a pegar");
        GetComponentInChildren<Animator>().SetTrigger("Hit");
    }

    public void DamageTarget()
    {
        if (_currentEnemyHitted == null)
            return;
        
        switch (_currentEnemyHitted.type)
        {
            case Enemy.EnemyType.pincho:
            {
                _audioSourcesource.clip = pinchoSound;
                break;
            }
            case Enemy.EnemyType.enemyBase:
            {
                _audioSourcesource.clip = enemySound;
                break;
            }
        }
        
        
        
        _currentEnemyHitted.DamageEnemy(1);
        _audioSourcesource.Play();

        
    }

    public void FreeHammerAnimationHit()
    {

        Destroy(this.gameObject);
    }
}
