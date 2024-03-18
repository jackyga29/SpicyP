using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    public string sceneName; // Variable to hold the name of the scene to load

    public AudioClip sceneChangeSound; // Audio clip to play when the scene changes
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure the AudioSource is not null and the audio clip is assigned
        if (audioSource == null || sceneChangeSound == null)
        {
            Debug.LogWarning("AudioSource or sceneChangeSound is not assigned.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") {
            // Play the scene change sound if AudioSource and audio clip are assigned
            if (audioSource != null && sceneChangeSound != null)
            {
                audioSource.PlayOneShot(sceneChangeSound);
            }

            // Load the scene specified in the inspector
            SceneManager.LoadScene(sceneName); 
        }
    }
}
