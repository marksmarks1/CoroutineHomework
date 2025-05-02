using UnityEngine;

namespace Netologia.Homework
{
    public class Gates : MonoBehaviour
    {
        private static int _score;

        private void OnTriggerEnter(Collider other)
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null)
            {
                Destroy(other.gameObject);
                _score++;
                Debug.Log("Goal! Current account: " + _score);
            }
        }
    }
}
