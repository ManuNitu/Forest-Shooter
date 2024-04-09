using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private void Awake()
    {
      Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (Vector2)cursorPos;
    }
}
