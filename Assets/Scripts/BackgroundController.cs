using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPosX, length;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; // Lấy chiều dài của background
    }

    void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffect;
        float temp = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPosX + distanceX, transform.position.y, transform.position.z);

        // Kiểm tra nếu camera đã di chuyển quá xa so với vị trí ban đầu của background
        if (temp > startPosX + length)
        {
            startPosX += length; // Dịch nền sang phải
        }
    }
}
