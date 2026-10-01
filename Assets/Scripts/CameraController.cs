using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Cible")]
    [SerializeField] private Transform target;

    [Header("Position de la caméra")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -8f);

    [Header("Suivi")]
    [SerializeField, Min(0f)] private float smoothTime = 0.15f;

    [Header("Rotation")]
    [SerializeField] private float lookAtHeight = 1.2f;
    [SerializeField, Min(0f)] private float rotationSpeed = 10f;

    private Vector3 velocity;

    private void Start()
    {
        if (target == null)
        {
            PlayerCharacter player = FindFirstObjectByType<PlayerCharacter>();

            if (player != null)
            {
                target = player.transform;
            }
        }

        if (target != null)
        {
            transform.position = target.position + offset;

            Vector3 lookPosition = target.position + Vector3.up * lookAtHeight;
            transform.LookAt(lookPosition);
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );

        Vector3 lookPosition = target.position + Vector3.up * lookAtHeight;

        Quaternion desiredRotation = Quaternion.LookRotation(
            lookPosition - transform.position
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}