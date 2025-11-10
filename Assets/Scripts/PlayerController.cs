using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    private Rigidbody2D rb2d;
    public float speed = 0.2f;
    public GameObject explodePrefab;
    public TMP_Text winText;
    public GameObject background;
    private GameController gameController;

    public PlayerShoot weapon;

    private Vector3 _direction;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameController = background.GetComponent<GameController>();
    }

 
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        if (movement.magnitude > 0.1f) // Use a small threshold to avoid issues with GetAxis easing to 0
        {
            Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, movement);

            // Smoothly transition the current rotation towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Fire();
        }   
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Aster")
        {
            gameObject.SetActive(false);
            Instantiate(explodePrefab, transform.position, transform.rotation);
            winText.text = "Game Over";
            gameController.setGameOver(true);

        }
    }

    
}

