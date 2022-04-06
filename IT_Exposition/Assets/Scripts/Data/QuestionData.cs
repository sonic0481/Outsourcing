using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestionInfo
{
    public EVENT    questionEvent;
    public string   content;    
    public QUESTION type;

    public QuestionInfo(EVENT _questionEvent, QUESTION _ttype, string _ccontent)
    {
        questionEvent = _questionEvent;
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

    public EVENT SelectEvent { set; get; }
    public List<DefQuestionTable.Data> QuestionList { set; get; } = new List<DefQuestionTable.Data>();

    public void AwakeInit()
    {
        //_questionInfo[(int)QUESTION.Q1] = new QuestionInfo(QUESTION.Q1, "전반적으로 만족 하시나요?");
        //_questionInfo[(int)QUESTION.Q2] = new QuestionInfo(QUESTION.Q2, "앱을 추천할 의향이 있으신가요?");        
    }

    public void OnInit()
    {
        for (int i = 0; i < _answerInfo.Length; ++i)
            _answerInfo[i] = ANSWER.NONE;
    }

    public void SetQuestion(bool isRenewal)
    {
        if(isRenewal)
        {
            QuestionList = CSVTableManager.Instance.QuestionTable.GetDataByEvent_RandomQuestion(SelectEvent);            
        }
        else
        {
            if(null == QuestionList || 0 == QuestionList.Count)
                QuestionList = CSVTableManager.Instance.QuestionTable.GetDataByEvent_RandomQuestion(SelectEvent);
        }   
    }

    public void ResetQuestion()
    {
        QuestionList?.Clear();
    }
}
