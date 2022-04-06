using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Strings
{
    public static string GetEventName(EVENT eventType)
    {
        switch (eventType)
        {
            case EVENT.DIGITAL:
                return "����������Ȱ��ɷ�";
            case EVENT.PYTHON:
                return "���̽㸶����";
            case EVENT.LINUX:
                return "������������";
            case EVENT.SEARCH:
                return "�˻���������";
            case EVENT.SNS:
                return "SNS��������";
        }

        return string.Empty;
    }
}
