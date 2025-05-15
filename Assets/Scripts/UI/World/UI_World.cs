using System.Collections;
using UnityEngine;

public class UI_World : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)] private float downUpSpeed;
    private Camera _camera;

    public void Initialize()
    {
        _camera = Camera.main;
        StartCoroutine(nameof(Live));
    }

    private IEnumerator Live()
    {
        while (true)
        {
            // rotation
            transform.rotation = _camera.transform.rotation;

            // animation down up
            float t = Mathf.PingPong(Time.time * downUpSpeed, 1f);
            float y = Mathf.Lerp(0.75f, 1.25f, t);
            transform.position = new Vector3(
                transform.position.x,
                y,
                transform.position.z
            );

            yield return null;
        }
    }
}
