using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed = 0.5f;

    [SerializeField] private GameObject[] parallaxLayers;

    Vector3 endPos;
    Vector3 startPos;
    public Vector3 offside;
    private void Start()
    {
        //parallaxLayers = transform.gameObject.GetComponentsInChildren<SpriteRenderer>();
        endPos = parallaxLayers[0].transform.position;
        startPos = parallaxLayers[parallaxLayers.Length - 1].transform.position;
    }

    void Update()
    {
        foreach (GameObject layer in parallaxLayers)
        {
            layer.transform.Translate(Vector3.down * parallaxSpeed);

            if (layer.transform.position.y <= endPos.y)
            {
                layer.transform.position = startPos + offside;
            }
        }
    }
}
