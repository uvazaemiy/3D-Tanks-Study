using UnityEngine;

public class RaycastBasic : MonoBehaviour
{
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Під курсором: " + hit.transform.name);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Відстань: " + hit.distance.ToString("F2") + "m");
                Renderer rend = hit.transform.GetComponent<Renderer>();
                if (rend != null)
                    rend.material.color = Color.red;
            }
        }
    }
}
