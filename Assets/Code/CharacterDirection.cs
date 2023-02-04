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
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 15 * Time.deltaTime);
    }

}
