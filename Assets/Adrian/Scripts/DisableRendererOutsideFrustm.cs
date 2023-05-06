using UnityEngine;

public class DisableRendererOutsideFrustm : MonoBehaviour
{
    public LayerMask hideLayer; // Add a public variable for the HIDE layer

    private Camera mainCamera;
    private Plane[] frustumPlanes;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Get the camera's frustum planes
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // Loop through all objects with a renderer in the scene
        foreach (Renderer renderer in FindObjectsOfType<Renderer>())
        {
            // Check if the object's layer is the HIDE layer, and if so, skip it
            if (((1 << renderer.gameObject.layer) & hideLayer) != 0)
            {
                continue;
            }

            // Check if the object's bounding box intersects with any of the frustum planes
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds))
            {
                // The object is inside the camera's frustum, so enable its renderer
                renderer.enabled = true;
            }
            else
            {
                // The object is outside the camera's frustum, so disable its renderer
                renderer.enabled = false;
            }
        }
    }
}
