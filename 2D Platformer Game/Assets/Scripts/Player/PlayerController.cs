using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //Player Health 
    public int currentHealth = 100;
    bool invul = false; 
    Animator anim;
    public Bomb bomb;

    //Current Using Movement Variable
    public Vector2 speed = new Vector2(30, 30);
    private bool m_FacingRight = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
        }

        //Player Movement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
        anim.SetFloat("Speed", Mathf.Abs(inputX));

        if(inputX > 0 && !m_FacingRight)
        {
            Flip();
        }else if (inputX < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    public void TakeDamage(int amount)
    {        
        if(!invul)
        {            
            invul = true;
            currentHealth -= amount;    
            // StartCoroutine(InvulWait());
        }
    }

    public IEnumerator InvulWait() 
    {
        anim.SetTrigger ("Hit"); //play hit animation
        yield return new WaitForSeconds (1); //invul time
        invul = false;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
