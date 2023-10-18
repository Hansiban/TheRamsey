using UnityEngine;

public class Cotton : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float destroy = 5;
    [SerializeField] private GameObject trail;
    private GameObject player;
    private PlayerControll playerControll;
    private Animator bulletanimation;
    private bool isMove = true;
    private float h;
    [SerializeField] AudioClip[] sounds;
    //[0]=pop/[1] = touch
    private AudioSource sound;

    private void Awake()
    {
        bulletanimation = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
        playerControll = player.GetComponent<PlayerControll>();
        sound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        h = playerControll.h;
        if (h == 1)
        {
            //h가 1이라면 rotation변경
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void Update()
    {
        //너무 멀리가면 삭제
        if (this.transform.position.x > destroy || this.transform.position.x < -destroy)
        {
            Destroy(this.gameObject);
        }

        //이동
        if (isMove)
        {
            CottonMove();
        }
    }
    private void CottonMove()
    {
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            isMove = false;
            bulletanimation.SetTrigger("Bloom");
            trail.SetActive(false);
            bulletanimation.SetBool("Keep", true);

            //sound
            sound.clip = sounds[0];
            sound.Play();
        }

        if (other.CompareTag("Player"))
        {
            bulletanimation.SetBool("Remove", true);
            playerControll.Jump(1.5f);

            //sound
            sound.clip = sounds[1];
            sound.Play();
        }
    }
}
