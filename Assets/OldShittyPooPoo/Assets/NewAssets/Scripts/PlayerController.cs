using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject Mario;
    public GameObject Luigi;
    public GameObject AttackBlock;
    public GameObject OneUpBlock;
    public Animator MarioAnimator;
    public Animator LuigiAnimator;
    public Animator AttackBlockAnimator;
    public Animator OneUpBlockAnimator;
    private int turn = 0;
    public AudioSource jumpSound; //TODO: Why did I do all the sprites, sounds and animations this way, it is very fucking confusing. must fix
    public AudioSource landSound;
    public AudioSource BlockSound;
    public AudioSource MarioDies;
    public AudioSource LuigiReacts;
    public AudioSource LuigiDies;
    public AudioSource MarioReacts;
    public AudioSource SwitchBlock;
    public bool canMarioJump = true;
    public bool canLuigiJump = true;
    public bool canSwitchBlock = true;
    public int MarioHP = 20;
    public int LuigiHP = 20;
    private int MHasPlayed = 0;
    private int LHasPlayed = 0;
    private int activeBlock = 0;
    public SpriteRenderer HPDebugBlock;
    public Sprite OneUp;
    public Sprite DeathShroom;
    public AudioSource BGMusicLoop1;
    public float loop1time = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MarioAnimator.SetInteger("Health", MarioHP);
        LuigiAnimator.SetInteger("Health", LuigiHP);

        if (turn == 0)
        {
            MarioAnimator.SetBool("Attacking?", true);
            LuigiAnimator.SetBool("Attacking?", false);
        }
        if (turn == 1)
        {
            LuigiAnimator.SetBool("Attacking?", true);
            MarioAnimator.SetBool("Attacking?", false);
        }
        if (canMarioJump == true && Input.GetKeyDown("a") && MarioHP > 0)
        {
            canMarioJump = false;
            StartCoroutine(Jump());
            jumpSound.Play();
            MarioAnimator.SetTrigger("Jump?");
            if (turn == 0)
            {
                if (activeBlock == 0)
                {
                    AttackBlockAnimator.SetTrigger("MarioHitBlock?");
                }
                else
                {
                    OneUpBlockAnimator.SetTrigger("MarioHitBlock?");
                }
                BlockSound.Play();
            }
        }
        if (canLuigiJump == true && Input.GetKeyDown("b") && LuigiHP > 0)
        {
            canLuigiJump = false;
            StartCoroutine(Jump());
            jumpSound.Play();
            LuigiAnimator.SetTrigger("Jump?");
            if (turn == 1)
            {
                if (activeBlock == 0)
                {
                    AttackBlockAnimator.SetTrigger("LuigiHitBlock?");
                }
                else
                {
                    OneUpBlockAnimator.SetTrigger("LuigiHitBlock?");
                }
                BlockSound.Play();
            }
        }

        if (MarioHP == 0)
        {
            if (MHasPlayed == 0)
            {
                MarioDies.Play();
                MHasPlayed = 1;
                if (LuigiHP > 0)
                {
                    StartCoroutine(MarioDead());
                }
            }
            turn = 1;
            AttackBlockAnimator.SetBool("whoTurn?", true);
            OneUpBlockAnimator.SetBool("whoTurn?", true);
        }
        else
        {
            MHasPlayed = 0;
        }

        if (LuigiHP == 0)
        {
            if (LHasPlayed == 0)
            {
                LuigiDies.Play();
                LHasPlayed = 1;
                if (MarioHP > 0)
                {
                    StartCoroutine(LuigiDead());
                }
            }
            turn = 0;
            AttackBlockAnimator.SetBool("whoTurn?", false);
            OneUpBlockAnimator.SetBool("whoTurn?", false);
        }
        else
        {
            LHasPlayed = 0;
        }

        if (Input.GetKeyDown("right"))
        {
            if (canSwitchBlock == true)
            {
                if (activeBlock == 1)
                {
                    activeBlock = 0;
                    SwitchBlock.Play();
                    canSwitchBlock = false;
                    StartCoroutine(blockSwitchDelay());
                }
            } 
        }

        if (Input.GetKeyDown("left"))
        {
            if (canSwitchBlock == true)
            {
                if (activeBlock == 0)
                {
                    activeBlock = 1;
                    SwitchBlock.Play();
                    canSwitchBlock = false;
                    StartCoroutine(blockSwitchDelay());
                }
            }    
        }

        if (activeBlock == 0)
        {
            AttackBlockAnimator.SetBool("IsActive?", true);
            OneUpBlockAnimator.SetBool("IsActive?", false);
        }
        if (activeBlock == 1)
        {
            AttackBlockAnimator.SetBool("IsActive?", false);
            OneUpBlockAnimator.SetBool("IsActive?", true);
        }
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.20f);
        landSound.Play();
        if (canMarioJump == false)
        {
            canMarioJump = true;
            if (turn == 0 && activeBlock == 0)
            {
                turn = 1;
                canSwitchBlock = false;
                AttackBlockAnimator.SetBool("whoTurn?", true);
                OneUpBlockAnimator.SetBool("whoTurn?", true);
                StartCoroutine(blockSwitchDelay());
            }
            if (turn == 0 && activeBlock == 1)
            {

                if (LuigiHP > 0)
                {
                    LuigiHP = 0;
                    canMarioJump = false;
                    canSwitchBlock = false;
                    StartCoroutine(OneUpBlockDelay());
                    StartCoroutine(blockSwitchDelay());
                    HPDebugBlock.sprite = OneUp;
                }
                else
                {
                    LuigiHP = 20;
                    HPDebugBlock.sprite = DeathShroom;
                    canSwitchBlock = false;
                    StartCoroutine(blockSwitchDelay());
                }
            }
        }
       if (canLuigiJump == false)
        {
            canLuigiJump = true;
            if (turn == 1 && activeBlock == 0)
            {
                turn = 0;
                canSwitchBlock = false;
                AttackBlockAnimator.SetBool("whoTurn?", false);
                OneUpBlockAnimator.SetBool("whoTurn?", false);
                StartCoroutine(blockSwitchDelay());
            }
            if (turn == 1 && activeBlock == 1)
            {
                if(MarioHP > 0)
                {
                    MarioHP = 0;
                    canLuigiJump = false;
                    canSwitchBlock = false;
                    StartCoroutine(OneUpBlockDelay());
                    StartCoroutine(blockSwitchDelay());
                    HPDebugBlock.sprite = OneUp;
                }
                else
                {
                    MarioHP = 20;
                    HPDebugBlock.sprite = DeathShroom;
                    canSwitchBlock = false;
                    StartCoroutine(blockSwitchDelay());
                }
            }
        }
    }

    IEnumerator MarioDead()
    {
        yield return new WaitForSeconds(1.4f);
        LuigiAnimator.SetTrigger("MarioDead?");
        LuigiReacts.Play();
    }
    IEnumerator LuigiDead()
    {
        yield return new WaitForSeconds(1.4f);
        MarioAnimator.SetTrigger("LuigiDead?");
        MarioReacts.Play();
    }

    IEnumerator blockSwitchDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (canSwitchBlock == false)
        {
            canSwitchBlock = true;
        }
    }

    IEnumerator OneUpBlockDelay()
    {
        yield return new WaitForSeconds(2.2f);
        canMarioJump = true;
        canLuigiJump = true;
    }
}
