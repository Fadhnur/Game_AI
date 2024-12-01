using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    private Transform[] path;
    public Transform pathGroup;
    public int currentPathObj = 0;
    public float distFromPath = 10f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f; // Kecepatan rotasi

    void Start()
    {
        GetPath();
    }

    void Update()
    {
        if (path == null || path.Length == 0) return;

        // Hitung arah menuju waypoint saat ini
        Vector3 targetPosition = path[currentPathObj].position;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Pindahkan objek ke waypoint
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotasi objek menghadap ke arah waypoint
        if (moveDirection != Vector3.zero)
        {
            // Metode 1: Instant Look
            // transform.LookAt(targetPosition);

            // Metode 2: Smooth Rotation
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Cek jarak ke waypoint
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Jika sudah dekat dengan waypoint, pindah ke waypoint berikutnya
        if (distanceToTarget <= distFromPath)
        {
            currentPathObj++;
            if (currentPathObj >= path.Length)
                currentPathObj = 0;
        }
    }

    private void GetPath()
    {
        Transform[] path_objs = pathGroup.GetComponentsInChildren<Transform>();
        path = new Transform[path_objs.Length - 1];

        int index = 0;
        for (int i = 0; i < path_objs.Length; i++)
        {
            if (path_objs[i] != pathGroup)
            {
                path[index] = path_objs[i];
                index++;
            }
        }
    }

    
}