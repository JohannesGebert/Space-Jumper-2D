using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_front : MonoBehaviour
{
    public float scrollspeed = 5f;

    // Update is called once per frame
    void Update()
    {

        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x += Time.deltaTime / scrollspeed;

        mat.mainTextureOffset = offset;
    }
}
