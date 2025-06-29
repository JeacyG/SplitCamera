using UnityEngine;

[ExecuteAlways]
public class Linker : MonoBehaviour
{
    [SerializeField] private bool bFollowCenter;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform object1;
    [SerializeField] private Transform object2;
    [SerializeField] private float radius;

    private void Update()
    {
        if (bFollowCenter)
            cam.transform.position = GetCenterPoint();
    }

    private Vector2 GetCenterPoint()
    {
        return (object1.position + object2.position) / 2f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(object1.position, object2.position);
        
        Gizmos.DrawWireSphere(GetCenterPoint(), radius);
    }
}
