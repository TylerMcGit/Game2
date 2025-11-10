using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AsterController : MonoBehaviour
{
    public GameObject explodePrefab;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GameController.Instance.AddScore(1);
            gameObject.SetActive(false);
            Instantiate(explodePrefab, transform.position, transform.rotation);
            Destroy(other.gameObject);


        }
    }
}
