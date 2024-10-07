using UnityEngine;

public class OctopusController : MonoBehaviour
{
    public float octopusSpeed = 3.0f;
    public float jumpForce = 5.0f;
    private Rigidbody octopusRb;
    public OVRPlayerController playerController;
    private OVRCameraRig cameraRig;

    void Start()
    {
        octopusRb = GetComponent<Rigidbody>();
        if (playerController == null)
        {
            playerController = FindObjectOfType<OVRPlayerController>();
        }
        cameraRig = FindObjectOfType<OVRCameraRig>();
    }

    void Update()
    {
        MoveOctopus();
    }

    void MoveOctopus()
    {
        // 使用右手拇指摇杆的上下移动来控制章鱼的前后移动
        float verticalInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;

        // 使用A和B按钮控制章鱼的左右移动
        float horizontalInput = 0f;
        if (OVRInput.Get(OVRInput.Button.One)) // A按钮
        {
            horizontalInput = -1f;
        }
        else if (OVRInput.Get(OVRInput.Button.Two)) // B按钮
        {
            horizontalInput = 1f;
        }

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // 将移动方向转换为相对于相机的方向
        if (cameraRig != null && cameraRig.centerEyeAnchor != null)
        {
            movement = cameraRig.centerEyeAnchor.TransformDirection(movement);
            movement.y = 0; // 保持在水平面上移动
        }

        // 移动小章鱼
        octopusRb.MovePosition(octopusRb.position + movement * octopusSpeed * Time.deltaTime);

        // 使用右手拇指摇杆按下（Click）来跳跃
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            octopusRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}