using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums 
{
    public enum CharacterLocation { Center, Left, Right };
    public enum CharacterMood { Fine, Happy, Sad, SadHappy, Upset, Blush, Crying, Serious, Surprised, Uncomfortable };
    public enum CharacterStance { };

    //Compound facial expressions based off of this article https://www.pnas.org/doi/10.1073/pnas.1322355111 Each face should be a compound of the two facial expressions. Eye Expression and Mouth Expression should be a 
    public enum FacialExpression { Neutral, Happy, Sad, Fear, Anger, Surprise, Disgust, Contempt};
    public enum EyeDirection { Neutral, Left, Right, Up, Down, Roll, Closed, Wink}
}
