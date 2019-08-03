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
            Vector3 offset = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

            GameObject newSpell = Instantiate(spell, offset, transform.rotation);
            newSpell.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(spellForce, 0, 0));

            //anim.SetBool("isAttacking", true);
        }
        /*else if(!Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isAttacking", false);
        }*/
	}

}
