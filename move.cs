using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour
{
    public float speed = 10f;
    public float flySpeed = 20f;
    public float gravity = 9.81f;
    public float flyGravity = 1f;
    public float flyThreshold = 0.5f;

    private Rigidbody2D rb;
    private bool isFlying = false;
    private bool isColliding = true;
    private Collider2D collider;
    public GameObject screen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isColliding)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.Space))
            {
                isFlying = true;
            }
            else
            {
                isFlying = false;
            }

            if (isFlying)
            {
                rb.velocity = new Vector2(x * flySpeed, y * flySpeed);
                rb.gravityScale = flyGravity;
            }
            else
            {
                rb.velocity = new Vector2(x * speed, rb.velocity.y);
                rb.gravityScale = gravity;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cube"))
        {
            isColliding = false;
            screen.SetActive(true);
            Destroy(collider);
            StartCoroutine(SlowDownAndRestart());
        }
    }

    IEnumerator SlowDownAndRestart()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
