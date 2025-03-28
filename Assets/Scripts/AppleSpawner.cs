using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject applePrefab;
    [SerializeField]
    private Transform appleParent;

    private readonly int width = 17, height = 10;
    private readonly int spacing = 20;
    private List<Apple> appleList = new List<Apple>();
    public List<Apple> AppleList => appleList;

    private void Awake()
    {
        SpawnApples();
    }

    public void SpawnApples()
    {
        // ������ ũ�⸦ ���ؼ� ����(spacing)��ŭ �����ش�.
        Vector2 size = applePrefab.GetComponent<RectTransform>().sizeDelta;
        size += new Vector2(spacing, spacing);

        int sum = 0;
        // width x height ������ŭ ��� ����
        for ( int y = 0; y < height; ++y )
        {
            for ( int x = 0; x < width; ++x)
            {
                GameObject clone = Instantiate(applePrefab, appleParent);
                RectTransform rect = clone.GetComponent<RectTransform>();

                float px = (-width * 0.5f + 0.5f + x) * size.x;
                float py = (height * 0.5f - 0.5f - y) * size.y;
                rect.anchoredPosition = new Vector2(px, py);

                Apple apple = clone.GetComponent<Apple>();
                int rand = Random.Range(0, 4);

                switch (rand)
                {
                    case 0:
                        apple.Number = Random.Range(1, 10);
                        break;
                    case 1:
                        apple.Number = Random.Range(1, 4);
                        break;
                    case 2:
                        apple.Number = Random.Range(1, 3);
                        break;
                    case 3:
                        apple.Number = 1;
                        break;
                }

                // ��ü ���� 10�� ����� �ǵ��� ������ ����� ���� ����
                if ( y == height -1 && x == width - 1 )
                {
                    apple.Number =  10 - (sum % 10);
                    if (apple.Number == 10 || sum > 600) // ��ü���� 600�� ���� �ʰ� ���̵� ����
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }

                sum += apple.Number;

                appleList.Add(apple);
            }
        }

        Debug.Log($"AppleSpawner::SpawnApples() : {sum}");
    }

    public void DestroyApple(Apple removeItem)
    {
        appleList.Remove(removeItem);
        Destroy(removeItem.gameObject);
    }
}
