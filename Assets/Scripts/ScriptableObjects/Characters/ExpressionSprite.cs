using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/ExpressionSpriteScriptrableObject", order = 3)]
public class ExpressionSprite : ScriptableObject
{
    public Enums.CharacterMood CharacterMood;
    public Sprite Sprite;
}