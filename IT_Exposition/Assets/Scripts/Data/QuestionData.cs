using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestionInfo
{
    public string content;    
    public QUESTION type;

    public QuestionInfo(QUESTION _ttype, string _ccontent)
    {
        type = _ttype;
        content = _ccontent;
    }
}

public class QuestionData
{
    private QuestionInfo[] _questionInfo = new QuestionInfo[(int)QUESTION.Q_END];
    public QuestionInfo GetQuestion(QUESTION type) { return _questionInfo[(int)type]; }

    private ANSWER[] _answerInfo = new ANSWER[(int)QUESTION.Q_END];
    public void SetAnswer(QUESTION questionType, ANSWER answer) { _answerInfo[(int)questionType] = answer; }
    public ANSWER GetAnswer(QUESTION questionType) { return _answerInfo[(int)questionType]; }

    private AGE _selectedAgeInfo;
    public void SetAge(AGE age) { _selectedAgeInfo = age; }
    public AGE GetAge() { return _selectedAgeInfo; }

    public void AwakeInit()
    {
        _questionInfo[(int)QUESTION.Q1] = new QuestionInfo(QUESTION.Q1, "���������� ���� �Ͻó���?");
        _questionInfo[(int)QUESTION.Q2] = new QuestionInfo(QUESTION.Q2, "���� ��õ�� ������ �����Ű���?");        
    }

    public void OnInit()
    {
        for (int i = 0; i < _answerInfo.Length; ++i)
            _answerInfo[i] = ANSWER.NONE;
    }

    public string GetAnswerText(QUESTION type)
    {
        ANSWER answer = GetAnswer(type);

        if (ANSWER.NONE == answer)
            return "X";

        return answer == ANSWER.YES ? "Y" : "N";
    }

    public string GetAgeText()
    {
        switch (_selectedAgeInfo)
        {
            case AGE.AGE_10:
                return "10��";                
            case AGE.AGE_20:
                return "20��";
            case AGE.AGE_30:
                return "30��";
            case AGE.AGE_40:
                return "40��";
            case AGE.AGE_50:
                return "50��";
            case AGE.AGE_60:
                return "60�� �̻�";
        }

        return "X";
    }
}
