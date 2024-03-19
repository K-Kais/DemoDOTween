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
    [SerializeField] private Image star;
    [SerializeField] private Image gift;
    [SerializeField] private TextMeshPro winText;

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
                star.transform.DOScale(Vector3.one + new Vector3(0.5f, 0.5f, 0f), 0.5f).SetEase(Ease.InOutBack);
                star.transform.DOScale(Vector3.one, duration).OnComplete(() =>
                {
                    LeanTween.move(star.gameObject, new Vector3(Screen.width / 2, Screen.height / 2, 0f), 1f);
                    //LeanTween.value(star.rectTransform.localRotation.z, 360f, 1f).setOnUpdate((float f) =>
                    //{
                    //    star.rectTransform.localRotation = Quaternion.Euler(0f, 0f, f);
                    //});
                    star.transform.DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360).OnComplete(() =>
                    {
                        star.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack);
                        gift.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack).OnComplete(() =>
                        {
                            gift.transform.DOMoveY(gift.transform.position.y + 200f, 1f).SetEase(Ease.InOutBack).OnComplete(() =>
                            {
                                winText.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack);
                            });
                        });
                    }).SetEase(Ease.Linear).SetLoops(2);
                }).SetDelay(0.5f).SetEase(Ease.OutBack);

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
