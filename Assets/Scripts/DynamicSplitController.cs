using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class DynamicSplitController : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private Camera player1Cam;
    [SerializeField] private Camera player2Cam;
    [SerializeField] private Camera centerCam;
    [SerializeField] private Material splitMaterial;
    [SerializeField] private RawImage renderOutput;
    [SerializeField] private float sweetSpot = 8.9f;
    [Header("Debug and Gizmo")]
    [SerializeField] private bool bSplitCamera;
    [SerializeField] private float radius;
    [SerializeField] private float angleLength;
    
    private static readonly int SplitAngleID = Shader.PropertyToID("_SplitAngle");

    private void Update()
    {
        renderOutput.enabled = bSplitCamera;
            
        player1Cam.transform.position = new Vector3(player1.position.x, player1.position.y, player1Cam.transform.position.z);
        player2Cam.transform.position = new Vector3(player2.position.x, player2.position.y, player2Cam.transform.position.z);
        Vector2 center = GetCenterPoint();
        centerCam.transform.position = new Vector3(center.x, center.y, centerCam.transform.position.z);
        
        splitMaterial.SetFloat(SplitAngleID, 180f - GetSeparatingAngle());
    }

    private float GetSeparatingAngle()
    {
        Vector2 dir = player2.position - player1.position;
        float angleRad = Mathf.Atan2(dir.y, dir.x);
        float angleDeg = angleRad * Mathf.Rad2Deg + 90f;
        return angleDeg;
    }
    
    private Vector2 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2f;
    }

    private void OnDrawGizmos()
    {
        if (player1 == null || player2 == null)
            return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(player1.position, player2.position);
        
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
