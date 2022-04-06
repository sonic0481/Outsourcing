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
                return "디지털정보활용능력";
            case EVENT.PYTHON:
                return "파이썬마스터";
            case EVENT.LINUX:
                return "리눅스마스터";
            case EVENT.SEARCH:
                return "검색광고마케터";
            case EVENT.SNS:
                return "SNS광고마케터";
        }

        return string.Empty;
    }
}
