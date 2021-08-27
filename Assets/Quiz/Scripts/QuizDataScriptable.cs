using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="QuestionData", menuName="QuestionData")]

public class QuizDataScriptable : ScriptableObject
{
    public List<Question> questions;

}
public class AnimalData : ScriptableObject
{
    public List<Question> questions;

}