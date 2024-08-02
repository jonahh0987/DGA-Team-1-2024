using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{

    private bool _isGameWon = false;

    [SerializeField] private GameObject WinText;

    // Update is called once per frame
    void Update()
    {
        if (_isGameWon && Input.GetKeyDown("r"))
        {
            GameManager.Instance.ResetScene();
        }
    }


    // Check for any collisions with the player. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // when the player collides with the win flag, enable the win text. 
            _isGameWon = true;
            WinText.SetActive(true);
        }
    }
}
