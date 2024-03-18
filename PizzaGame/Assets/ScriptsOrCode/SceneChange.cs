using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    [SerializeField]
    private string sceneName; // Variable to hold the name of the scene to load

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") {
            SceneManager.LoadScene(sceneName); // Load the scene specified in the inspector
        }
    }
}