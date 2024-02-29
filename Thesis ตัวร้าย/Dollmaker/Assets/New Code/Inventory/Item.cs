using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType type;

    
}
public enum ItemType
{
    Cross,
    Doll,
    Cloth,
    RedCloth,
    BlueCloth,
    GreenCloth,
    YellowCloth,
    PieceClothRed,
    PieceClothBlue,
    PieceClothGreen,
    PieceClothYellow,
    FinishClothRed,
    FinishClothBlue,
    FinishClothGreen,
    FinishClothYellow,
    Scissors,
    FinishDoll,
        Key
}