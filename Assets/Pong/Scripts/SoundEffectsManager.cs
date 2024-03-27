using UnityEngine;

namespace MalulsArcade.Pong
{
    [RequireComponent(typeof(AudioSource))]
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
            if (goalSfx != null)
                audioSource.PlayOneShot(goalSfx);
        }

        public void onBallPaddleCollision()
        {
            if (ballPaddleCollisionSfx != null)
                audioSource.PlayOneShot(ballPaddleCollisionSfx);
        }
    }
}