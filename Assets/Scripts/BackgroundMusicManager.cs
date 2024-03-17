using UnityEngine;

namespace Assets.Scripts
{
    public class BackgroundMusicManager : MonoBehaviour
    {
        private AudioSource audioSource;

        public AudioClip gameplayMusic;
        public AudioClip matchEndMusic;

        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = gameplayMusic;
            audioSource.Play();
        }

        public void onMatchEnded(int _)
        {
            audioSource.Stop();
            audioSource.clip = matchEndMusic;
            audioSource.Play();
        }
    }
}