using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    private bool playedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void PlayAndDestroy()
    {
        ps.Play();
        Destroy(gameObject, 5.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
