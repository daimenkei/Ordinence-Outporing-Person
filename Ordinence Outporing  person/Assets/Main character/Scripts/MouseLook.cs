using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Camera-Control/MouseLook")]
public class MouseLook : MonoBehaviour
{
    //выпадающий список для настройки осей вращения
    public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY = 2 };
    public RotationAxes axes = RotationAxes.MouseXandY;
    //переменные чувствительности мыши
    public float sensitivityY = 2F;
    public float sensitivityX = 2F;
    //переменные максимального угла вращения по оси X
    public float minimumX = -360F;
    public float maximumX = 360F;

    //переменные максимального угла вращения по оси Y
    public float minimumY = -360F;
    public float maximumY = 360F;

    //переменные определяющие текущий угол вращения
    float rotationX = 0F;
    float rotationY = 0F;

    //переменная содержащая тип вращения "Quaternion"
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        originalRotation = transform.localRotation;

        // Добавляем эти строки для скрытия курсора
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }


    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseXandY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }

        // Дополнительная проверка, чтобы курсор оставался заблокированным
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
