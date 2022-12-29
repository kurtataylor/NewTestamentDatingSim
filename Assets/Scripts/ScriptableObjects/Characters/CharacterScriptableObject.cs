using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/CharacterScriptableObject", order = 2)]
public class CharacterScriptableObject : ScriptableObject
{
    public string Name;
    public List<ExpressionSprite> Expressions;
}


