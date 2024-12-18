using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Question_CSV{
    public TextAsset text;

    private HashSet<int> UsedQuestions = new HashSet<int>();

    public Question_CSV(TextAsset text){
        this.text = text;
    }

    public void ReadQuestion(Question_Parent Question, Question_Type QuestionType){
        string[] data = text.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 6;

        switch (QuestionType){
            case Question_Type.Point:
                for (int i = 0; i < (tableSize - 1); i++){
                    if (!UsedQuestions.Contains(i)){
                        if (int.Parse(data[6 * (i + 1)]) == Question.Points){
                            Question.Question = data[(6 * (i + 1)) + 1];
                            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 2], true));
                            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 3], false));
                            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 4], false));
                            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 5], false));

                            UsedQuestions.Add(i);
                            
                            break;
                        }
                    }
                }

                break;

            default:
                return;
        }
    }

    public void ReadQuestions(){
        string[] data = text.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 6;
        
        for (int i = 0; i < (tableSize - 1); i++){
            Question.Question = data[(6 * (i + 1)) + 1];
            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 2], true));
            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 3], false));
            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 4], false));
            Question.Answers.Add(new KeyValuePair<string, bool>(data[(6 * (i + 1)) + 5], false));
        }
    }
}
