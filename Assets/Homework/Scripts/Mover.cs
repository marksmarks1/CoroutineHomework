using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Vector3 _start;
        [SerializeField] private Vector3 _end;
        [SerializeField] private float _speed = 1f;

        private Rigidbody _rb;
        private Vector3 _worldStart;
        private Vector3 _worldEnd;

        private IEnumerator Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;

            Vector3 basePosition = transform.position;
            _worldStart = basePosition + transform.TransformDirection(_start);
            _worldEnd = basePosition + transform.TransformDirection(_end);

            Vector3 from = _worldStart;
            Vector3 to = _worldEnd;
            float progress = 0f;

            while (true)
            {
                yield return new WaitForFixedUpdate();

                float distance = Vector3.Distance(from, to);
                float duration = distance / _speed;

                progress += Time.fixedDeltaTime / duration;

                if (progress >= 1f)
                {
                    progress = 0f;
                    (from, to) = (to, from);
                    continue;
                }

                Vector3 pos = Vector3.Lerp(from, to, progress);
                _rb.MovePosition(pos);
            }
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying) return;

            Vector3 basePosition = transform.position;
            Vector3 worldStart = basePosition + transform.TransformDirection(_start);
            Vector3 worldEnd = basePosition + transform.TransformDirection(_end);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(worldStart, 0.1f);
            Gizmos.DrawSphere(worldEnd, 0.1f);
            Gizmos.DrawLine(worldStart, worldEnd);
        }
    }
}
