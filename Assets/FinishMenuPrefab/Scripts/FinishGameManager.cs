using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace FinishMenuPrefab.Scripts.GameFinishPrefab
{
    // Class that manages the game finish actions like spawning particles and showing the finish prefab
    public class FinishGameManager : MonoBehaviour
    {
        [Tooltip("Reference to the FinishPrefabSpawner for spawning the finish prefab.")]
        [SerializeField] private FinishPrefabSpawner finishPrefabSpawner;
        [Tooltip("Reference to the ParticleSpawner for spawning particles.")]
        [SerializeField] private ParticleSpawner particleSpawner;
        [Tooltip("Time to wait before activating the finish prefab.")]
        [SerializeField] private float spawnTimer;

        [Tooltip("Time interval between each particle spawn.")]
        [SerializeField] private float particleSpawnTimer;

        [SerializeField] private AudioSource fireworkSoundAudioSource;
        [SerializeField] private AudioClip fireworkSound;
        
        // Subscribes to the OnGameFinish event when the object is enabled
        private void OnEnable() => GameFinishHandler.OnGameFinish += Finish;

        // Unsubscribes from the OnGameFinish event when the object is disabled
        private void OnDisable() => GameFinishHandler.OnGameFinish -= Finish;

        // Method called when the game finishes (triggered by the event)
        private void Finish() => StartCoroutine(SpawnParticles());

        // Coroutine that handles the particle spawning and showing the finish prefab
        private IEnumerator SpawnParticles()
        {
            // Loop through each particle spawn clone and activate them with a delay between each activation
            foreach (var t in particleSpawner.ParticleSpawnClones)
            {
                t.gameObject.SetActive(true);
                if (fireworkSoundAudioSource != null) fireworkSoundAudioSource.PlayOneShot(fireworkSound);
                yield return
                    new WaitForSecondsRealtime(particleSpawnTimer); // Wait for the defined particle spawn timer
            }

            // Wait for the spawnTimer before showing the finish prefab
            yield return new WaitForSeconds(spawnTimer);

            // Activate the finish prefab and move it to the specified position with animation
            finishPrefabSpawner.Prefab.SetActive(true);
            finishPrefabSpawner.Prefab.transform.DOMove(new Vector3(0, 0, 0), 0.2f) // Animate the prefab's movement
                .SetEase(Ease.OutBack); // Apply a smooth easing to the animation

        }
    }
}
