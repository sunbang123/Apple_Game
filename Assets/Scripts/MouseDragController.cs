using System.Collections.Generic;
using UnityEngine;

public class MouseDragController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AppleSpawner appleSpawner;
    [SerializeField]
    private RectTransform dragRectangle;

    private int sum = 0;
    private Rect dragRect;
    private Vector2 start = Vector2.zero;
    private Vector2 end = Vector2.zero;
    private List<Apple> selectedAppleList = new List<Apple>();

    private void Awake()
    {
        dragRect = new Rect();

        // start, end�� (0,0)�� ���·� �̹��� ũ�⸦ (0,0)���� ������ ȭ�鿡 ������ �ʵ��� ��
        DrawDragRectangle();
    }

    private void Update()
    {
        if (gameController.IsGameStart == false ) return;
        if (Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;

            dragRect.Set(0, 0, 0, 0);
        }
        if (Input.GetMouseButton(0))
        {
            end = Input.mousePosition;

            // ���콺�� Ŭ���� ���·� �巡�� �ϴ� ���� �巡�� ������ �̹����� ǥ��
            DrawDragRectangle();
            CalculateDragRect();
            SelectApples();
        }

        if ( Input.GetMouseButtonUp(0) )
        {
            Debug.Log($"���� : {sum}");

            if (sum == 10)
            {
                Debug.Log("10 �ϼ�");
                foreach ( Apple apple in selectedAppleList )
                {
                    appleSpawner.DestroyApple(apple);
                }
            }
            else
            {
                // ���õ� ������� ���� ����
                foreach ( Apple apple in selectedAppleList )
                {
                    apple.OnDeselected();
                }
            }

            // ���콺 Ŭ���� ������ �� �巡�� ������ ������ �ʵ���
            // start, end ��ġ�� (0,0)���� �����ϰ� �巡�� ������ �׸���.
            start = end = Vector2.zero;
            DrawDragRectangle();
        }
    }

    private void DrawDragRectangle()
    {
        // �巡�� ������ ��Ÿ���� Image UI�� ��ġ
        dragRectangle.position = (start + end) * 0.5f;
        // �巡�� ������ ��Ÿ���� Image UI�� ũ��
        dragRectangle.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
    }

    private void CalculateDragRect()
    {
        if ( Input.mousePosition.x < start.x )
        {
            dragRect.xMin = Input.mousePosition.x;
            dragRect.xMax = start.x;
        }
        else
        {
            dragRect.xMin = start.x;
            dragRect.xMax = Input.mousePosition.x;
        }

        if ( Input.mousePosition.y < start.y )
        {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        }
        else
        {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }
    }

    private void SelectApples()
    {
        sum = 0;
        selectedAppleList.Clear();
        foreach ( Apple apple in appleSpawner.AppleList )
        {
            // ����� �߽��� �巡�� ������ �����ؾ� ��
            if ( dragRect.Contains(apple.Position) )
            {
                apple.OnSelected();
                selectedAppleList.Add(apple);
                sum += apple.Number;
            }
            else
            {
                apple.OnDeselected();
            }
        }
    }
}
