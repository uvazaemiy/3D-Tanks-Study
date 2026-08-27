using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 0;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", speed);
    }
}
