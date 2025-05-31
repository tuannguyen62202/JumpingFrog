using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Scene2")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().WinGame();
            }
            else
            {
                SceneManager.LoadScene("Scene2");
            }
        }
    }
}
