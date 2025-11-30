using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Range(0, 100)]
    public float Value;
    public float speed;
    public float margin;
    public float multiplier;

    public GameObject player;

    public RectTransform Top, Bottom, Left, Right, Center;

    void Update()
    {
        Value = player.GetComponent<CharacterController>().currentSpeed * multiplier;

        float TopValue, BottomValue, LeftValue, RightValue;

        TopValue = Mathf.Lerp(Top.position.y, Center.position.y + Value + margin, Time.deltaTime * speed);
        BottomValue = Mathf.Lerp(Bottom.position.y, Center.position.y - Value - margin, Time.deltaTime * speed);

        LeftValue = Mathf.Lerp(Left.position.x, Center.position.x - Value - margin, Time.deltaTime * speed);
        RightValue = Mathf.Lerp(Right.position.x, Center.position.x + Value + margin, Time.deltaTime * speed);

        Top.position = new Vector2(Top.position.x, TopValue);
        Bottom.position = new Vector2(Bottom.position.x, BottomValue);

        Left.position = new Vector2(LeftValue, Center.position.y);
        Right.position = new Vector2(RightValue, Center.position.y);
    }
}