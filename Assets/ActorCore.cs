using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ActorCore : MonoBehaviour
{
    [SerializeField]
    private string ActorName;


    private Animator animator;

    [System.Serializable]
    public class Expression
    {
        public string name;
        public Sprite image;
        public Transform transform;
    }

    public Image currentPose;
    public List<Expression> poses;

    public Image currentLeftEyes;
    public Image currentRightEyes;
    public List<Expression> eyes;

    public Image currentMouth;
    public List<Expression> mouthes;



    [CustomEditor(typeof(ActorCore))]
    public class LevelScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ActorCore myTarget = (ActorCore)target;
            if (GUILayout.Button("!!ACTOR!!"))
            {
                myTarget.actorCrud(); 
            }
            DrawDefaultInspector();
        }
    }

    

    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void actorCrud()
    {
        //if (File.Exists(Application.dataPath + "/Scripts/" + ActorName)) UpdateTheActor();
        //Debug.Log(File.Exists(Application.dataPath + "/Scripts/InkManager.cs") ? "yes":"no");
        SetPose("Closeup");
    }

    private void UpdateTheActor()
    {

    }

    //Functions for ink to call to update the sprite with a string.
    public void SetPose (string poseType) { 
        SetCharacterSprite(poseType, currentPose, poses);
    }

    public void SetEyes(string passedEyes) { 
        SetCharacterSprite(passedEyes, currentLeftEyes, eyes); 
        SetCharacterSprite(passedEyes, currentRightEyes, eyes); 
        //currentLeftEyes.rectTransform.transform = 
    }

    public void SetMouth(string passedMouth) { 
        SetCharacterSprite(passedMouth, currentMouth, mouthes); 
    }


    public void SetCharacterSprite(string type, Image image, List<Expression> expressions)
    {
        var Type = expressions.FirstOrDefault(e => e.name.ToLower() == type.ToLower());
        if (Type == null) expressions.FirstOrDefault(e => e.name == CheckClosest(type.ToLower(), expressions.Select(e => e.name.ToLower())));
        image.sprite = Type.image;
    }

    public void PlayEmote(string emote)
    {
        var animationClips = animator.runtimeAnimatorController.animationClips;
        //test the string to see which emote it is and then play it. 
        var testForAnimationName = animationClips.FirstOrDefault(a => a.name.ToLower() == emote.ToLower());
        if (testForAnimationName == null) animationClips.FirstOrDefault(a => a.name == CheckClosest(emote.ToLower(), animationClips.Select(c => c.name.ToLower())));

        animator.Play(testForAnimationName.name);
    }

    //Tool that checks a list of strings against a single string to find which is the closest match
    public string CheckClosest(string stringWeAreTesting, IEnumerable<string> checkingAgainst)
    {
        var testedValues = new Dictionary<int, string>();
        foreach(var testingString in checkingAgainst)
        {
            testedValues.Add(LevenshteinDistance(stringWeAreTesting, testingString), testingString);
        }

        return testedValues.OrderBy(v => v.Key).First().Value;
    }

    //Function for testing how similar two strings are to one another
    public static int LevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];
        if (n == 0)
        {
            return m;
        }
        if (m == 0)
        {
            return n;
        }
        for (int i = 0; i <= n; d[i, 0] = i++)
            ;
        for (int j = 0; j <= m; d[0, j] = j++)
            ;
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        return d[n, m];
    }
}

public class Actor
{
    public string Name { get; set; }
    public List<Sprite> Sprites { get; set; }

}



public class EmoteAnimation
{
    public Transform Transform { get; set; }
    public GameObject Animation { get; set; }

}