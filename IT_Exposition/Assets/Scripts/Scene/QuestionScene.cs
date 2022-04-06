using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestionScene : SceneBase
{
    [SerializeField] Text       _eventName;
    [SerializeField] Text       _questionText;

    [SerializeField] Toggle[]   _answerToggles = new Toggle[4];
    [SerializeField] Text[]     _answerText = new Text[4];

    [SerializeField] Button     _previousBtn;
    [SerializeField] Button     _nextBtn;

    [SerializeField]  Image     _answer;
    [SerializeField]  Image     _wrongAnswer;

    private QUESTION _question;

    private DefQuestionTable.Data   questionData;

    private ANSWER              _selectAnswer;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _previousBtn.onClick.AddListener(OnPrev);
        _nextBtn.onClick.AddListener(SelectAnswer);

        _question = (QUESTION)((int)MYSCENE - 2);
    }

    public override void Init()
    {
        for(int i = 0; i < _answerToggles.Length; ++i)
        {
            _answerToggles[i].isOn = false;
        }

        SetToggleEnable(true);

        _answer.color = new Color(1, 1, 1, 0);
        _wrongAnswer.color = new Color(1, 1, 1, 0);
    }

    public override void On()
    {
        DefQuestionTable.Data data = DataManager.Instance.QuestionData.QuestionList[(int)_question];

        if (null == data)
            return;

        questionData = data;

        _eventName.text = Strings.GetEventName(DataManager.Instance.QuestionData.SelectEvent);
        _questionText.text = data.Question;

        _answerText[(int)ANSWER.ANSWER_1].text = data.AnswerList_1;
        _answerText[(int)ANSWER.ANSWER_2].text = data.AnswerList_2;
        _answerText[(int)ANSWER.ANSWER_3].text = data.AnswerList_3;
        _answerText[(int)ANSWER.ANSWER_4].text = data.AnswerList_4;

        for (int i = 0; i < _answerToggles.Length; ++i)
        {
            _answerToggles[i].isOn = false;
        }        
    }

    private void SetToggleEnable(bool isEnable)
    {
        for (int i = 0; i < _answerToggles.Length; ++i)
        {
            _answerToggles[i].interactable = isEnable;
        }            
    }

    private void SelectAnswer()
    {
        ANSWER answer = ANSWER.NONE;

        for (int i = 0; i < _answerToggles.Length; ++i)
        {
            if (_answerToggles[i].isOn)
            {
                answer = (ANSWER)i;
                break;
            }
        }

        if (ANSWER.NONE == answer)
            return;

        SetToggleEnable(false);

        _selectAnswer = answer;
        DataManager.Instance.QuestionData.SetAnswer(_question, answer);

        if(_selectAnswer == questionData.Answer)
        {
            _answer.DOColor(Color.white, 1f).OnComplete(() => {
                Invoke("OnNext", 2f);
            });
        }
        else
        {
            _wrongAnswer.DOColor(Color.white, 1f).OnComplete(() => {
                Invoke("OnNext", 2f);
            });
        }
    }

    protected override void OnNext()
    {
        _sceneMgr.NextScene();
    }
}
