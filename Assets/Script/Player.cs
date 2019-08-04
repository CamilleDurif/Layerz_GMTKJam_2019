using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public UIManager uiManager;

    [SerializeField]
    private Stat health;

   
    public int nbOfAmmo;
    public int maxAmmo;

    public float speed;

    private Vector3 movement;
    private Rigidbody rb;
    int floorMAsk;
    float camRayLength = 100f;

    public GameObject spell;
    public float spellForce;

    private Animator anim;
    private Animator cameraAnim;

    private Stat stat;

    private bool isImmune = false;
    private bool canShoot = true;

    public Renderer[] renderers;

    public CameraController camera;

    private AudioSource gameOverSound;
    private AudioSource shootSound;
    private AudioSource damageSound;
    private AudioSource reloadSound;
    private AudioSource healingSound;

    public TextMeshProUGUI pauseText; 

    private void Awake()
    {
        floorMAsk = LayerMask.GetMask("Floor");

        health.Initialize();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cameraAnim = Camera.main.GetComponent<Animator>();
        gameOverSound = GameObject.Find("game over").GetComponent<AudioSource>();
        shootSound = GameObject.Find("shoot").GetComponent<AudioSource>();
        damageSound = GameObject.Find("damage").GetComponent<AudioSource>();
        reloadSound = GameObject.Find("reload").GetComponent<AudioSource>();
        healingSound = GameObject.Find("healing").GetComponent<AudioSource>();
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

        if (nbOfAmmo > 0 && canShoot)
        {
            Vector3 offset = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);

            Vector3 bulletPosition = transform.position + 0.1f * transform.forward;
            GameObject newSpell = Instantiate(spell, bulletPosition, transform.rotation);
            newSpell.GetComponent<Rigidbody>().AddForce(transform.forward * spellForce);

            nbOfAmmo--;

            camera.ShakeAmplitude = 0.8f;
            camera.ShakeFrequency = 1.5f;
            camera.ShakeDuration = 0.3f;

            camera.ShakeElapsedTime = camera.ShakeDuration;

            anim.SetTrigger("isShooting");
            shootSound.Play();

            StartCoroutine(WaitingToShoot(1.0f));
        }

    }

    void Heal()
    {
        if(health.CurrentVal < health.MaxVal)
        {
            health.CurrentVal += 10;
            healingSound.Play();
            GetComponentInChildren<ParticleSystem>().Play();
        }

    }

    void Reload()
    {
        if (nbOfAmmo < maxAmmo)
        {
            nbOfAmmo++;
            reloadSound.Play();
        }
    }

    void Pause()
    {
        if(Time.timeScale <= 0f)
        {
            Time.timeScale = 0f;
            pauseText.text = "Click to unpause";
        }
        else
        {
            Time.timeScale = 1f;
            pauseText.text = "Click to pause";
        }


    }

    IEnumerator WaitingToShoot(float seconds)
    {
        canShoot = false;
        yield return new WaitForSeconds(seconds);
        canShoot = true;
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

            camera.ShakeElapsedTime = camera.ShakeDuration;
            damageSound.Play();

            health.CurrentVal -= damage;

            isImmune = true;

            StartCoroutine(ImmunityAfterDamage(0.1f, 0.1f, collider));

        }

        if (health.CurrentVal <= 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator ImmunityAfterDamage(float duration, float blinkTime, Collider collider)
    {

        while (duration >= 0f)
        {
            duration -= Time.deltaTime;

            renderers[0].enabled = !renderers[0].enabled;
            renderers[1].enabled = !renderers[1].enabled;
            renderers[2].enabled = !renderers[2].enabled;
            renderers[3].enabled = !renderers[3].enabled;
            yield return new WaitForSeconds(blinkTime);
        }

        isImmune = false;
    }

    public bool IsImmune() { return isImmune; }

    IEnumerator PlayerDeath()
    {
        gameOverSound.Play();

        while(gameOverSound.isPlaying)
        {
            yield return null; 
        }

        Destroy(gameObject);
    }
}
