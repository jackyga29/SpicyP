using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public Material newBackgroundMaterial; // Drag the new background material to this field in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming you have a reference to the ParallaxController script on the background GameObject
            ParallaxController parallaxController = FindObjectOfType<ParallaxController>();
            if (parallaxController != null)
            {
                // Change the background material to the new one
                parallaxController.ChangeBackground(newBackgroundMaterial);
            }
        }
    }
}
