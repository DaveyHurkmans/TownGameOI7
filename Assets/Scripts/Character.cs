using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    private Rigidbody rigidbody;

    private bool canMove = true; // New variable to control movement

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canMove)
        {
            float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(new Vector3(xMovement, 0, 0));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            }
        }
    }

    // Public method to toggle movement externally
    public void ToggleMovement(bool enableMovement)
    {
        canMove = enableMovement;
    }
}
