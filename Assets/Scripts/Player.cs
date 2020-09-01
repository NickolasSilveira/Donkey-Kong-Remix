using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public float JumpForce;
    public bool isJumping;
    private Rigidbody2D rig;
    private Animator anim;

    //Escada
    public float velocidadeSubir = 3;
    public LayerMask escadaMask;
    public float vertical;
    public bool subindo;
    public float checkRaio = 0.3f;
    private Collider2D col;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnAnimatorMove();
        Jump();

        vertical = Input.GetAxis("Vertical");
        SubirEscada();
    }

    private void OnAnimatorMove()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f,0f);
        transform.position += movement * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f)
         {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
         }

        if (Input.GetAxis("Horizontal") < 0f)
         {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0f)
         {
            anim.SetBool("walk", false);
         }
    }
    void Jump()
    {
     if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f,JumpForce),ForceMode2D.Impulse);
            anim.SetBool("jump", true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
     if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);

        }
     if(collision.gameObject.tag == "Barrel")
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
    bool TocandoEscada()
    {
        return col.IsTouchingLayers(escadaMask);
    }

    void SubirEscada()
    {
        bool up = Physics2D.OverlapCircle(transform.position, checkRaio, escadaMask);
        bool down = Physics2D.OverlapCircle(transform.position + new Vector3(0, -1), checkRaio, escadaMask);

        if (vertical != 0 && TocandoEscada())
        {
            subindo = true;
            rig.isKinematic = true;

        }
        if (subindo)
        {
            float y = vertical * velocidadeSubir;
            rig.velocity = new Vector2(0, y);

            if (!up && vertical >= 0)
            {
                TerminarSubida();
                return;
            }

            if (!down && vertical <= 0)
            {
                TerminarSubida();
                return;
            }
        }
    }
    void TerminarSubida()
    {
        subindo = false;
        rig.isKinematic = false;
    }
}
