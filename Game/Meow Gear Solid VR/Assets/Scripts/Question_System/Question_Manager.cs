using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question_Manager : MonoBehaviour{
    public TextAsset QuestionSheet;
    private Question_CSV CSV;

    public int PointValue;
    public int PointRadius;
    public int NextQuestionPointValue;

    void Start(){
        CSV = new Question_CSV(QuestionSheet);

        PointValue = 0;
        PointRadius = 5;

        if ((PointValue - PointRadius) >= 0){
            NextQuestionPointValue = Random.Range((PointValue - PointRadius), (PointValue + PointRadius));
        }
        else{
            NextQuestionPointValue = Random.Range(0, (PointValue + PointRadius));
        }
    }

    public void GetQuestionFromPointValue(Question_Parent Question, Question_Type QuestionType){
        CSV.ReadQuestion(Question, QuestionType);
    }
}
