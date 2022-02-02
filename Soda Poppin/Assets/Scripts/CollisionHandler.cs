using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 2f;
    
    Movement movement;
    AudioSource audioSource;

    void Awake()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is fine to land on");
                break;

            case "Finish":
                DisableControls();
                StartCoroutine(GoToNextLevel());
                break;

            default:
                DisableControls();
                StartCoroutine(RestartLevel());
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

    void DisableControls()
    {
        movement.enabled = false;
        audioSource.enabled = false;
    }
    
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(loadLevelDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(loadLevelDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex + 1 < totalScenes)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
