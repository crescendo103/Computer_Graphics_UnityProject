using UnityEngine;

public class CreateColoredSpheres : MonoBehaviour
{
    void Start()
    {
        GameObject redSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        redSphere.transform.position = new Vector3(5, 6, 20); 
        Renderer redRenderer = redSphere.GetComponent<Renderer>();
        redRenderer.material.color = Color.red;


        GameObject greenSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        greenSphere.transform.position = new Vector3(-15, 5, 40); 
        Renderer greenRenderer = greenSphere.GetComponent<Renderer>();
        greenRenderer.material.color = Color.green;
    }
}
