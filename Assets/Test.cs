using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private List<Ease> easeList;
    [SerializeField] private float endX = 5f;
    [SerializeField] private float duration = 2f;
    [SerializeField] private Image popup;

    private void Start()
    {
        int i = 0;
        foreach (Transform objChild in transform)
        {
            objChild.GetChild(0).GetComponent<TextMeshPro>().text = $"{easeList[i]}";
            objChild.DOMove(objChild.position + new Vector3(endX, 0f), duration).OnComplete(() =>
            {
                Debug.Log("DOTween Complete");
                popup.transform.DOScale(Vector3.one + new Vector3(0.1f, 0.1f, 0f), duration).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    popup.transform.DOScale(Vector3.one, duration / 2).SetEase(Ease.InOutBack);
                });


            // Test master 5
            }).SetEase(easeList[i++]);
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            LeanTween.move(gameObject, transform.position + new Vector3(endX, 0f), duration).setOnComplete(() =>
            {
                Debug.Log("LeanTween Complete");
            });
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LeanTween.value(transform.position.y, transform.position.y + 2f, 1f).setOnUpdate((float f) =>
            {
                transform.position = new Vector3(transform.position.x, f);
            }).setOnComplete(() =>
            {
                Debug.Log("LeanTween Value Complete");
            });
        }
    }
    public void ClosePopup()
    {
        popup.transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
    }
}
