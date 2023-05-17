using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private Vector3 respawnPoint;

    [SerializeField] private Text checkpointText;
    [SerializeField] private Text livesText;

    [SerializeField] private AudioSource deathSoundEffect;

    [SerializeField] private int lives;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        checkpointText.enabled = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        anim.SetBool("isRespawned", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            lives--;
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            StartCoroutine(ShowMessageCoroutine(4));
            respawnPoint = transform.position;
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        livesText.text = "Lives: " + lives;
        anim.SetBool("isRespawned", false);
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void ResetToCheckPoint()
    {
        if (lives == 0)
        {
            EndOfGame();
        }
        else
        {
            transform.position = respawnPoint;
            rb.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("isRespawned", true);
        }
    }

    private IEnumerator ShowMessageCoroutine(float timeToShow = 4)
    {
        // Show the text
        checkpointText.enabled = true;

        // Wait for some time
        yield return new WaitForSeconds(timeToShow);

        // Hide the text
        checkpointText.enabled = false;
    }

    private void EndOfGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
