using UnityEngine;

public class DragMapa : MonoBehaviour
{
    public float dragVelocidad = 0.5f;
    public float minY = -10f;
    public float maxY = 10f;

    private Vector3 posRaton;
    private bool isDragging = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {

        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("SubirMapa") && stateInfo.normalizedTime >= 1f)
            {
                Destroy(animator);
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            posRaton = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - posRaton;
            float moveAmount = mouseDelta.y * dragVelocidad * Time.deltaTime;
            float newY = Mathf.Clamp(transform.position.y + moveAmount, minY, maxY);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            posRaton = Input.mousePosition;
        }
    }
}