using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDirection : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 mousePos;
    // Update is called once per frame

    // Start is called before the first frame update
    void FixedUpdate()
    {
        mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = worldPosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90.0f;
        rb.rotation = angle;
    }

}
