using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    public bool hasExploded = false;
    public float timer = 2;
    public GameObject explosionEffect;
    public float blastRadius = 5f;
    public CameraShake CameraShake;

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
            CheckArea();    
            CameraShake.Shake(0.1f, .1f);   
            hasExploded = true;
        }
    }

    void Explode()
    {
       float timeScale = 0.45f;
       Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), timeScale);
       Destroy(gameObject);
    }

    void CheckArea()
    {      
        Vector3 explosionPos = transform.position;
        Collider2D[] objsInRange = Physics2D.OverlapCircleAll(explosionPos, blastRadius);
        foreach (Collider2D blah in objsInRange)
        {
            //Debug.Log("Player hit" + blah.tag);
            Vector2 impulse = blah.transform.position - this.transform.position;
            Rigidbody2D rb = blah.GetComponent<Rigidbody2D>();
            var kk = blah.GetComponent<PlayerController>();
            var ani = blah.GetComponent<Animator>();        
            if(rb != null) 
            {
                if(blah.tag != "Untagged")
                {
                    ani.SetTrigger("Hit");       
                    // kk.TakeDamage(20);
                    rb.AddForce(impulse * 6, ForceMode2D.Impulse);
                }else
                {
                    rb.AddForce(impulse * 6, ForceMode2D.Impulse);
                }       
            }            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, blastRadius);
    }
}