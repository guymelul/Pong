using UnityEngine;

namespace Assets.Scripts
{
    public class SoundEffectsManager : MonoBehaviour
    {
        private AudioSource audioSource;

        public AudioClip ballPaddleCollisionSfx;
        public AudioClip goalSfx;

        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void onGoal(int _)
        {
            audioSource.PlayOneShot(goalSfx);
        }

        public void onBallPaddleCollision()
        {
            audioSource.PlayOneShot(ballPaddleCollisionSfx);
        }
    }
}