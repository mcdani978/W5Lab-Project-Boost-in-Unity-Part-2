using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float transportDistance = 0; // Distance to transport the player

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
                break;
            case "Transporter":
                TransportPlayer();
                break;
            default:
                Debug.Log("Sorry, you blew up!");
                ReloadLevel();
                break;
        }
    }

    void TransportPlayer()
    {
        // Move the player a certain distance forward on the x-axis
        Vector3 newPosition = transform.position + new Vector3(transportDistance, 0, 0);
        transform.position = newPosition;
        Debug.Log("Player transported to: " + newPosition);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Loop back to the first scene
        }

        SceneManager.LoadScene(nextSceneIndex); // Load the next scene
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
