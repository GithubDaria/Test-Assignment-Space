using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5f;
    private Vector3 initialPosition;
    [Header("Animation Settings")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 90f, 0f);
    [SerializeField] private float rotationSwingAmplitude = 45f;
    [SerializeField] private float bounceAmplitude = 0.005f;
    [SerializeField] private float bounceFrequency = 3f;
    private void Start()
    {
        initialPosition = transform.position;
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
        float swingAngle = Mathf.Sin(Time.time) * rotationSwingAmplitude;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, swingAngle, transform.localRotation.eulerAngles.z);
        transform.position = initialPosition + Vector3.up * Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        AudioManager.Instance.PlayStarCollectedSound();
        GameManager.Instance?.AddScore(1);
        Destroy(gameObject);
    }
    
}
