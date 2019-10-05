using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public EnemyType type;
    public Spawner spawner;
    public ParticleSystem particleSystem_prefab;
    private ParticleSystem _particleSystem;
    

    public event Action<EnemyType> OnEnemyDead = delegate { };
    public event Action OnDisposeEnemy = delegate { };
    public event Action<EnemyType> OnPinchoHit = delegate { };

    private void Start()
    {
        currentHp = maxHp;
        _particleSystem = Instantiate(particleSystem_prefab);
        
    }
    private void DestroySelf()
    {
        
        Handheld.Vibrate();
        OnEnemyDead(type);
        Destroy(gameObject);
        
    }

    public void DamageEnemy(int damage)
    {
 

        if (_particleSystem != null)
        {
            
            Debug.Log("Entre dentro de particle sistem");
            _particleSystem.transform.position = transform.position;
            _particleSystem.GetComponent<ParticleSelfDestroy>().PlayAndDestroy();

        }
        
        if (type == EnemyType.pincho)
        {
            // Hacer algo cuando se le pega a un pincho
            Debug.Log("Le pegue a un pincho");
            OnPinchoHit(type);
            return;
        }
        
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Debug.Log("Me muero");
            if (this.gameObject != null)
                DestroySelf();
            
        }

    }
    

    public void DisposeEnemy()
    {
        Destroy(gameObject);
        Destroy(_particleSystem.gameObject);
        OnDisposeEnemy();
    }

    public enum EnemyType
    {
        enemyBase,
        pincho,
        bigAkita
    }
}
