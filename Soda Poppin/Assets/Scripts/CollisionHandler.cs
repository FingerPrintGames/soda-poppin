using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is fine to land on");
                break;

            case "Finish":
                Debug.Log("Level Complete!");
                break;

            default:
                Debug.Log("You crashed!");
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fuel")
        {
            Debug.Log("Fuel collected");
            Destroy(other.gameObject);
        }
    }
}
