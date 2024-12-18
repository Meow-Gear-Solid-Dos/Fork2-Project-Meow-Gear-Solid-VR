using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question_Parent : MonoBehaviour{
    public int Points;
    public string Question;
    public List<KeyValuePair<string, bool>> Answers = new List<KeyValuePair<string, bool>>();

    Question_Manager QuestionManager;

    Question_Parent(){
        Points = 0;
    }

    Question_Parent(int Points){
        this.Points = Points;
    }

    void Start(){
        QuestionManager = GameObject.FindWithTag("Question").GetComponent<Question_Manager>();

        QuestionManager.GetQuestionFromPointValue(this, Question_Type.Point);

        Debug.Log(Question);
        Debug.Log(Answers[0].Key + " " + Answers[0].Value);
        Debug.Log(Answers[1].Key + " " + Answers[1].Value);
        Debug.Log(Answers[2].Key + " " + Answers[2].Value);
        Debug.Log(Answers[3].Key + " " + Answers[3].Value);
    }
}
