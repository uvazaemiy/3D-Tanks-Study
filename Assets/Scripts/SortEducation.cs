using UnityEngine;

public class SortEducation : MonoBehaviour
{
    int[] array = {3, 5, 7, 15, 13};
    
    int k = 0;     //15

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (array[i] >= k)
            {
                k = array[i];
            }
        }
    }
}
