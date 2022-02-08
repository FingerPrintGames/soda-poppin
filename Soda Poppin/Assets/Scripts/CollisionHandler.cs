using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip levelCompleteSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem levelCompleteParticles;

    Movement movement;
    AudioSource audioSource;
    BoxCollider sodaCanCollider;

    bool isAlive = true;
    bool collisionEnabled = true;

    void Awake()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        sodaCanCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        LKeyAction();
        CKeyAction();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isAlive) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is fine to land on");
                break;

            case "Finish":
                StartCoroutine(GoToNextLevel());
                DisableControls();
                break;

            default:
                StartCoroutine(RestartLevel());
                DisableControls();
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
        movement.StopEngineParticles();
        movement.enabled = false;
        isAlive = false;
    }

    void PlayAudioClip(AudioClip audioClip, float audioVolume)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip, audioVolume);
    }
    
    IEnumerator RestartLevel()
    {
        PlayAudioClip(crashSFX, .5f);
        crashParticles.Play();
        yield return new WaitForSeconds(loadLevelDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator GoToNextLevel()
    {
        PlayAudioClip(levelCompleteSFX, .75f);
        levelCompleteParticles.Play();
        yield return new WaitForSeconds(loadLevelDelay);
        LoadNextLevel();
    }

    static void LoadNextLevel()
    {
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

    #region Debug

    void LKeyAction()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Loading next level.");
            LoadNextLevel();
        }
    }

    void CKeyAction()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
            TogglePlayerCollsion(collisionEnabled);
        }
    }

    void TogglePlayerCollsion(bool value)
    {
        sodaCanCollider.enabled = value;
    }

    #endregion
}
