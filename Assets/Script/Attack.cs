using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour 
{
    public GameObject spell;
    public float spellForce;

    private Animator anim;
    private Stat stat; 

	private void Start()
	{
        anim = GetComponentInChildren<Animator>();
	}

	void Update () 
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject newSpell = Instantiate(spell, transform.position, transform.rotation);
            newSpell.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(spellForce, 0f));

            anim.SetBool("isAttacking", true);
        }
        else if(!Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isAttacking", false);
        }
	}

}
