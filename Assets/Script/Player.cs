using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UIManager uiManager;

    [SerializeField]
    private Stat health;

    public int nbOfAmmo = 10;

    private int maxAmmo = 10;

    public float speed;

    private Vector3 movement;
    private Rigidbody rb;
    int floorMAsk;
    float camRayLength = 100f;

    public GameObject spell;
    public float spellForce;

    private Animator anim;
    private Stat stat;

    private bool isImmune = false;

    private void Awake()
    {
        floorMAsk = LayerMask.GetMask("Floor");

        health.Initialize();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        InputAction();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Move(horizontal, vertical);
        Turn();
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void Turn()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMAsk))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }

    private void InputAction()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int layer = uiManager.GetCurrentLayer();

            switch (layer)
            {
                case UIManager.ENNEMIES:
                    Shoot();
                    break;
                case UIManager.WALLS:
                    Shoot();
                    break;
                case UIManager.HEALTH:
                    Heal();
                    break;
                case UIManager.AMMO:
                    Reload();
                    break;
                case UIManager.PAUSE:
                    Pause();
                    break;
                default:
                    Debug.Log("currentLayer Undefined");
                    break;
            }
        }
    }

    void Shoot()
    {

        if (nbOfAmmo > 0)
        {
            Vector3 offset = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);

            GameObject newSpell = Instantiate(spell, transform.position + 0.1f * transform.forward, transform.rotation);
            newSpell.GetComponent<Rigidbody>().AddForce(transform.forward * spellForce);

            nbOfAmmo--;

            //anim.SetBool("isAttacking", true);
        }
        /*else if(!Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isAttacking", false);
        }*/
    }

    void Heal()
    {
        if(health.CurrentVal < health.MaxVal)
        {
            health.CurrentVal += 10;
            GetComponentInChildren<ParticleSystem>().Play();
        }

    }

    void Reload()
    {
        if (nbOfAmmo < maxAmmo)
        {
            nbOfAmmo++;
        }
    }

    void Pause()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10, other);
        }
    }

    public void TakeDamage(int damage, Collider collider)
    {
        if (!isImmune)
        {
            health.CurrentVal -= damage;

            isImmune = true;

            //effet de recul
            bouncingback(collider);

            StartCoroutine(ImmunityAfterDamage(0.1f, 0.1f, collider));

        }

        void bouncingback(Collider coll)
        {
            // force is how forcefully we will push the player away from the enemy.
            float force = 25;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = collider.transform.position - transform.position;
            Debug.Log("DIR : " + dir.ToString());
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            rb.AddForce(dir * force, ForceMode.Impulse);
        }

        if (health.CurrentVal <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ImmunityAfterDamage(float duration, float blinkTime, Collider collider)
    {

        while (duration >= 0f)
        {
            duration -= Time.deltaTime;

            this.GetComponent<Renderer>().enabled = !this.GetComponent<Renderer>().enabled;

            yield return new WaitForSeconds(blinkTime);
        }

        isImmune = false;
    }

    public bool IsImmune() { return isImmune; }
}
