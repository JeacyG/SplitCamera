using UnityEngine;
using Image = UnityEngine.UI.Image;

[ExecuteAlways]
public class CamerasController : MonoBehaviour
{
    [SerializeField] private Camera leftCamera;
    [SerializeField] private Camera rightCamera;

    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    [SerializeField] private float manualSweetSpot = 8.9f;
    [SerializeField] private Image centerLine;

    private void Update()
    {
        if (GetXDistance() <= manualSweetSpot)
        {
            centerLine.enabled = false;
            if (IsPlayer1Left())
            {
                leftCamera.transform.position = new Vector3(player1.position.x - (manualSweetSpot - GetXDistance()) / 2f, 0, 0);
                rightCamera.transform.position = new Vector3(player2.position.x + (manualSweetSpot - GetXDistance()) / 2f, 0, 0);
            }
            else
            {
                leftCamera.transform.position = new Vector3(player2.position.x - (manualSweetSpot - GetXDistance()) / 2f, 0, 0);
                rightCamera.transform.position = new Vector3(player1.position.x + (manualSweetSpot - GetXDistance()) / 2f, 0, 0);
            }
        }
        else
        {
            centerLine.enabled = true;
            if (IsPlayer1Left())
            {
                leftCamera.transform.position = new Vector3(player1.position.x, 0, 0);
                rightCamera.transform.position = new Vector3(player2.position.x, 0, 0);
            }
            else
            {
                leftCamera.transform.position = new Vector3(player2.position.x, 0, 0);
                rightCamera.transform.position = new Vector3(player1.position.x, 0, 0);
            }
        }
    }

    private float GetXDistance()
    {
        return Mathf.Abs(player1.position.x - player2.position.x);
    }

    private bool IsPlayer1Left()
    {
        return player1.position.x < player2.position.x;
    }
}
