using UnityEngine;
using uVegas.Core.Cards;

public class CardUIVariation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer baseRenderer;
    [SerializeField] private SpriteRenderer rankRenderer;
    [SerializeField] private SpriteRenderer suitRenderer;

    [SerializeField] private Card currentCard;
    [SerializeField] private CardTheme currentTheme;

    public Card Data => currentCard;

    public void Init(Card card, CardTheme theme)
    {
        currentCard = card;
        currentTheme = theme;
        UpdateTheme();
    }

    public void UpdateTheme()
    {
        if (currentCard == null || currentTheme == null) return;

        baseRenderer.sprite = currentTheme.baseImage;
        baseRenderer.color = currentTheme.frontColor;

        if (currentCard.suit == Suit.Hidden)
        {
            suitRenderer.gameObject.SetActive(false);
            rankRenderer.sprite = currentTheme.backImage;
            rankRenderer.color = currentTheme.backColor;
            return;
        }

        if (currentCard.suit == Suit.Joker)
        {
            suitRenderer.gameObject.SetActive(false);
            rankRenderer.sprite = currentTheme.jokerImage;
            rankRenderer.color = currentTheme.jokerColor;
            return;
        }

        suitRenderer.gameObject.SetActive(true);

        RankEntry? rankEntry = currentTheme.GetRank(currentCard.rank);
        SuitEntry? suitEntry = currentTheme.GetSuit(currentCard.suit);

        if (rankEntry.HasValue)
        {
            rankRenderer.sprite = rankEntry.Value.image;
            rankRenderer.color = currentTheme.rankColor;
        }

        if (suitEntry.HasValue)
        {
            suitRenderer.sprite = suitEntry.Value.image;
            suitRenderer.color = suitEntry.Value.color;
        }

    }

    public void Reveal(Card card)
    {
        if (currentCard != null && currentCard.suit == Suit.Hidden)
            Init(card, currentTheme);
    }
}