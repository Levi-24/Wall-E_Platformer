using UnityEngine;

public class Mace : MonoBehaviour
{
    public float speed = 6f;
    public float range = 3;
    public float knockbackForce = 10f;
    private float startingX;
    private float initialScaleX;
    private int direction = 1;

    void Start()
    {
        startingX = transform.position.x;
        initialScaleX = transform.localScale.x;
    }

    void Update()
    {
        // Mozgatjuk az objektumot az �j ir�nyba
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);

        // Ellen�rizz�k, hogy az objektum el�rte-e a hat�rt, �s ha igen, v�ltoztatjuk az ir�nyt �s forgatjuk a sprite-ot
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            direction *= -1;
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        // Forgatjuk a sprite-ot
        transform.localScale = new Vector3(initialScaleX * direction, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ellen�rizz�k, hogy az �tk�z�tt objektum rendelkezik-e a PlayerMovement komponenssel
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Rigidbody2D playerRb = playerMovement.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // A knockback sz�m�t�sa �s alkalmaz�sa a j�t�kos objektumra
                Vector2 knockbackDirection = new Vector2(direction, 1).normalized;
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0); // Resetelj�k az aktu�lis y sebess�get
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
