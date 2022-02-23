using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    public float timer = 2;
    public bool hasExploded = false;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
       _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        float timeScale = 0.45f;
        Destroy( Instantiate(explosionEffect, transform.position, Quaternion.identity), timeScale);
        Destroy(gameObject);
    }
}
