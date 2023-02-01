using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ThrottleSprite : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gearNumberText;
    [SerializeField] private RectTransform gearRect0;
    [SerializeField] private RectTransform gearRect1;
    [SerializeField] private RectTransform gearRect2;
    [SerializeField] private RectTransform gearRect3;
    [SerializeField] private RectTransform gearRect4;
    [SerializeField] private RectTransform gearRect5;
    [SerializeField] private RectTransform middleRect;
    [SerializeField] private float shiftSpeed = 0.1f;

    private int currentGear = 0;
    private RectTransform rect;
    private RectTransform[] gears;

    private Queue<Vector2> movement = new Queue<Vector2>();
    private Vector2? target;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        gears = new RectTransform[6]
        {
            gearRect0,
            gearRect1,
            gearRect2,
            gearRect3,
            gearRect4,
            gearRect5
        };
        rect.anchoredPosition = gearRect0.anchoredPosition;
    }

    public void UpdateGear(Component c, object data) {
        if (c is BoatGearBox && data is int)
        {
            Shift((int)data);
            currentGear = (int)data;
            gearNumberText.SetText(currentGear.ToString());
        }
    }

    private void Shift(int targetGear)
    {
        Vector2 pos = gears[currentGear].anchoredPosition;
        Vector2 targetPos = gears[targetGear].anchoredPosition;
        pos.y = middleRect.anchoredPosition.y;
        movement.Enqueue(pos);
        if (pos.x != targetPos.x)
        {
            pos.x = targetPos.x;
            movement.Enqueue(pos);
        }
        pos.y = targetPos.y;
        movement.Enqueue(pos);

    }

    private void Update()
    {
        if (target == null)
        {
            if (movement.Count > 0)
            {
                target = movement.Dequeue();
            }
        }

        if(target != null)
        {
            Vector2 pos = rect.anchoredPosition;
            rect.anchoredPosition = Vector2.MoveTowards(pos, (Vector2)target, shiftSpeed * Time.deltaTime);

            if (pos == target || Vector2.Distance(pos, (Vector2)target) <= 1)
            {
                target = null;
            }
        }
    }
}
