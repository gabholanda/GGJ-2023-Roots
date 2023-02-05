using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDirection : MonoBehaviour
{
    //TODO: Find a way to rotate the firePoint without rotating the Flower itself
    //It will look weird when we have art seeing it rotating like a dumbass with pot and all
    public Rigidbody2D rb;
    Vector3 mousePos;

    void FixedUpdate()
    {
        mousePos = Mouse.current.position.ReadValue();
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (Vector2)mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
