using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotate;

        private IEnumerator Start()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;

            while (true)
            {
                yield return new WaitForFixedUpdate();
                Quaternion deltaRotation = Quaternion.Euler(_rotate * Time.fixedDeltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
        }
    }
}
