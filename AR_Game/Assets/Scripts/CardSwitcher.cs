using UnityEngine;
using uVegas.Core.Cards;
using uVegas.UI;

public class CardSwitcher : MonoBehaviour
{
    [SerializeField] private Suit suit = Suit.Hearts;
    [SerializeField] private Rank rank = Rank.Ace;
    [SerializeField] private CardTheme theme;

    private CardUIVariation uiCard;

    private void Awake()
    {
        uiCard = GetComponentInChildren<CardUIVariation>();
    }

    private void Start()
    {
        UpdateCard();
    }

    public void UpdateCard()
    {
        if (uiCard == null || theme == null)
        {
            Debug.LogError("CardSwitcher: missing UICard or CardTheme.");
            return;
        }

        uiCard.Init(new Card(suit, rank), theme);
    }
}