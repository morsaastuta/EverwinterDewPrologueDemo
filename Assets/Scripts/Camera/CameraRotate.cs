using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraRotate : MonoBehaviour
{
    public CameraProperties cam;

    public Vector2 turn;
    private float prevTurnY;
    public float verTop = 30f;
    public float verBot = -10f;

    void Update()
    {
        if (cam.canRotate)
        {
            turn.y += Input.GetAxis("Mouse Y");
            turn.x += Input.GetAxis("Mouse X") * 5f;
            if (turn.y > -verBot || turn.y < -verTop)
            {
                turn.y = prevTurnY;
            }
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
            prevTurnY = turn.y;
        }
    }
}
