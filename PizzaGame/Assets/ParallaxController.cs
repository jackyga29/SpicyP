using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam; 
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds; 
    Renderer[] renderers; // Change to Renderer array instead of Material array
    float[] backSpeed; 

    float farthestBack = float.NegativeInfinity; 

    [Range(0.01f, 0.05f)] 
    public float parallaxSpeed;   

    void Start()
    {
        cam = Camera.main.transform; 
        camStartPos = cam.position;

        int backCount = transform.childCount;
        renderers = new Renderer[backCount]; // Change to Renderer array
        backSpeed = new float[backCount]; 
        backgrounds = new GameObject[backCount];

        for(int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject; 
            renderers[i] = backgrounds[i].GetComponent<Renderer>(); // Get the Renderer component
        }
        
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for(int i = 0; i < backCount; i++)
        {
            if((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z; 
            }
        }

        for(int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x; 
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);

        for(int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed; 
            renderers[i].material.SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed); // Access the material through the renderer
        }
    }

    // Add the ChangeBackground method
    public void ChangeBackground(Material newMaterial)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = newMaterial; // Change the material of each background object
        }
    }
}
