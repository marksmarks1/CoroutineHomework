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
        [SerializeField] private float _delay = 1f;

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

            while (true)
            {
                yield return MoveBetween(_worldStart, _worldEnd);
                yield return new WaitForSeconds(_delay);
                yield return MoveBetween(_worldEnd, _worldStart);
                yield return new WaitForSeconds(_delay);
            }
        }

        private IEnumerator MoveBetween(Vector3 from, Vector3 to)
        {
            float distance = Vector3.Distance(from, to);
            float duration = distance / _speed;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                yield return new WaitForFixedUpdate();
                elapsed += Time.fixedDeltaTime;
                //Vector3 pos = Vector3.Lerp(from, to, elapsed / duration);
                float t = elapsed / duration;
                t = Mathf.SmoothStep(0f, 1f, t);
                Vector3 pos = Vector3.Lerp(from, to, t);
                _rb.MovePosition(pos);
            }

            _rb.MovePosition(to);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Vector3 basePosition = transform.position;
                Vector3 previewStart = basePosition + transform.TransformDirection(_start);
                Vector3 previewEnd = basePosition + transform.TransformDirection(_end);

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(previewStart, 0.1f);
                Gizmos.DrawSphere(previewEnd, 0.1f);
                Gizmos.DrawLine(previewStart, previewEnd);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(_worldStart, 0.1f);
                Gizmos.DrawSphere(_worldEnd, 0.1f);
                Gizmos.DrawLine(_worldStart, _worldEnd);
            }
        }
    }
}
