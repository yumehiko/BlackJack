using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// カードのゲーム上での実体。
/// </summary>
public class CardInstance : MonoBehaviour
{
    /// <summary>
    /// カードに表示される数字。
    /// </summary>
    [SerializeField] private Text number = default;

    /// <summary>
    /// カードに表示されるスート。
    /// </summary>
    [SerializeField] private SpriteRenderer suitSprite = default;

    /// <summary>
    /// スートを表すスプライトのリスト。
    /// </summary>
    [SerializeField] private List<Sprite> suits = default;

    /// <summary>
    /// カードの裏面。
    /// </summary>
    [SerializeField] private GameObject cardBack = default;

    /// <summary>
    /// カード情報を実体に反映する。
    /// </summary>
    public void SetCardInfo(Card card)
    {
        number.text = card.number.ToString();
        suitSprite.sprite = suits[(int)card.suit];
    }

    /// <summary>
    /// カードを裏返す。Trueなら裏向きに。
    /// </summary>
    /// <param name="isActive"></param>
    public void ReverseCard(bool isReverse)
    {
        cardBack.SetActive(isReverse);
    }

    /// <summary>
    /// カードを指定の位置に移動する。
    /// </summary>
    /// <param name="position"></param>
    public void CardMove(Vector3 position)
    {
        position.z = -5.0f;
        transform.DOMove(position, BlackJackManager.animeDuration);
        Vector3 rotate = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
        transform.DORotate(rotate, BlackJackManager.animeDuration);
    }

    /// <summary>
    /// カードを裏返す。
    /// </summary>
    public void FlipCard()
    {
        
        Vector3 rotate = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180.0f, transform.eulerAngles.z);
        transform.DORotate(rotate, BlackJackManager.animeDuration);
        
        //transform.DORotate(Vector3.zero, 1.0f);
    }
}
