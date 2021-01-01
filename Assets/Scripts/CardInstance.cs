using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void SetCardInfo(Card card, bool isReverse)
    {
        number.text = card.number.ToString();
        suitSprite.sprite = suits[(int)card.suit];
        cardBack.SetActive(isReverse);
    }

    /// <summary>
    /// カードを裏返す。Trueなら裏向きに。
    /// </summary>
    /// <param name="isActive"></param>
    public void ReverseCard(bool isReverse)
    {
        cardBack.SetActive(isReverse);
    }
}
