using UnityEngine;

public class RaycastColorChanger : MonoBehaviour
{
    [Header("Кольори")] 
    [SerializeField] private Color hoverColor = Color.yellow;   // при наведенні
    [SerializeField] private Color clickColor  = Color.red;     // при кліку

    private GameObject lastHovered;           // попередній об'єкт
    private Color      lastOriginalColor;     // його оригінальний колір

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<TypeOfMesh>() == null)
            {
                Debug.Log("Не знайшли TypeOfMesh");
                return;
            }
            
            TypeOfMeshList typeOfMesh = hit.transform.GetComponent<TypeOfMesh>().typeOfMeshList.TypeOfMesh;
            
            if (typeOfMesh == null)
            {
                Debug.Log("Не знайшли TypeOfMeshList!");
            }
            
            switch (typeOfMesh)
            {
                case TypeOfMeshList.Renderer:
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    
                    // Новий об'єкт під курсором — відновлюємо попередній
                    if (lastHovered != hit.transform.gameObject)
                    {
                        RestoreLastColor();
                        lastHovered = hit.transform.gameObject;
                        lastOriginalColor = rend.material.color;
                        rend.material.color = hoverColor;
                    }

                    // Лівий клік — фарбуємо в clickColor
                    if (Input.GetMouseButtonDown(0))
                    {
                        rend.material.color = clickColor;
                        Debug.Log("Клік по: " + hit.transform.name);
                    }
                    
                    break;
                }
                case TypeOfMeshList.SkinnedMesh:
                {
                    SkinnedMeshRenderer skinedRen = hit.transform.GetComponent<SkinnedMeshRenderer>();
                    
                    // Новий об'єкт під курсором — відновлюємо попередній
                    if (lastHovered != hit.transform.gameObject)
                    {
                        RestoreLastColor();
                        lastHovered = hit.transform.gameObject;
                        lastOriginalColor = skinedRen.material.color;
                        skinedRen.material.color = hoverColor;
                    }

                    // Лівий клік — фарбуємо в clickColor
                    if (Input.GetMouseButtonDown(0))
                    {
                        skinedRen.material.color = clickColor;
                        Debug.Log("Клік по: " + hit.transform.name);
                    }
                    
                    break;
                }
            }
        }
        else
        {
            // Курсор не на жодному об'єкті
            RestoreLastColor();
            lastHovered = null;
        }
    }

    void RestoreLastColor()
    {
        if (lastHovered == null) return;

        Renderer rend = lastHovered.GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = lastOriginalColor;
    }
}