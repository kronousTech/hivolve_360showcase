using UnityEngine;

public class UIFollowsPlayer : MonoBehaviour
{
    private Vector3 _position;

    public void SetInFrontOfPlayer()
    {
        var newDir = Camera.main.transform.forward;
        newDir.y = 0;

        _position = Camera.main.transform.position + newDir.normalized * 1.50f;
        _position += Vector3.up * Camera.main.transform.position.y;

        transform.position = _position;
        //transform.position = Vector3.Lerp(transform.position, _position, 5 * Time.deltaTime);

        transform.LookAt(Camera.main.transform.position);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
