using System.Collections.Generic;
using UnityEngine;

public class RuneViewFactory : MonoBehaviour
{
    [SerializeField] private RuneView runeViewPrefab;
    [SerializeField] Transform cardSpawnPoint;

    public void CreateRuneView(
        Transform handParent,
        Rune rune,
        float defaultPositionY,
        float selectedPositionY,
        int sortingOrder,
        CardManager cardManager,
        List<RuneView> runeViewHand
    ){
        RuneView newRuneView = Instantiate(runeViewPrefab, handParent);
        newRuneView.transform.position = cardSpawnPoint.position;

        newRuneView.Setup(rune, defaultPositionY, selectedPositionY, cardManager);
        newRuneView.SetSortingOrder(sortingOrder);

        runeViewHand.Add(newRuneView);
    }
}