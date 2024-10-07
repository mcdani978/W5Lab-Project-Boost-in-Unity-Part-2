using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float transportDistance = 10f; // Distance to transport the player
    [SerializeField] private AudioSource rocketAudioSource; // AudioSource for the rocket sound
    [SerializeField] private AudioSource effectsAudioSource; // Separate AudioSource for sound effects
    [SerializeField] private AudioClip transporterClip; // Sound to play when hitting the transporter
    [SerializeField] private AudioClip obstacleCollisionClip; // Sound for collisions with obstacles or the ground

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
                PlayTransporterSound();
                break;
            default:
                Debug.Log("Sorry, you blew up!");
                PlayObstacleCollisionSound();
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

    void PlayTransporterSound()
    {
        // Play the transporter sound using the effects AudioSource
        if (effectsAudioSource != null && transporterClip != null)
        {
            effectsAudioSource.PlayOneShot(transporterClip);
            Debug.Log("Transporter sound played");
        }
    }

    void PlayObstacleCollisionSound()
    {
        // Play the obstacle collision sound using the effects AudioSource
        if (effectsAudioSource != null && obstacleCollisionClip != null)
        {
            effectsAudioSource.PlayOneShot(obstacleCollisionClip);
            Debug.Log("You hit an obstacle or the ground");
        }
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
