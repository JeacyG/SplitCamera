using UnityEngine;

[ExecuteAlways]
public class Linker : MonoBehaviour
{
    [SerializeField] private Transform object1;
    [SerializeField] private Transform object2;
    
    [Header("Debug Gizmo")]
    [SerializeField] private float radius;
    [SerializeField] private float angleLength;

    private float GetSeparatingAngle()
    {
        Vector2 dir = object2.position - object1.position;
        float angleRad = Mathf.Atan2(dir.y, dir.x);
        float angleDeg = angleRad * Mathf.Rad2Deg + 90f;
        return angleDeg;
    }
    
    private Vector2 GetCenterPoint()
    {
        return (object1.position + object2.position) / 2f;
    }

    private void OnDrawGizmos()
    {
        if (object1 == null || object2 == null)
            return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(object1.position, object2.position);
        
        Gizmos.DrawWireSphere(GetCenterPoint(), radius);
        
        Gizmos.color = Color.red;
        
        Vector2 center = GetCenterPoint();
        float angleDeg = GetSeparatingAngle();
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        Vector2 startPoint = center - direction * angleLength / 2f;
        Vector2 endPoint = center + direction * angleLength / 2f;

        Gizmos.DrawLine(startPoint, endPoint);
    }
}
