using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class proto_parallaxScroll : MonoBehaviour
{ 
    public RawImage Image;
    public float x_limit;
    public float y_limit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Image.uvRect = new Rect(Image.uvRect.position + new Vector2(x_limit, y_limit) *  Time.deltaTime, Image.uvRect.size);
        
    }
}
