using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EULog
{
    public static string exc = "[!][!][!][!][!]  ".Bold().Color("red");
    public static string star = "***************  ".Bold().Color("yellow");
    public static string excBlue = "[!][!][!][!][!]  ".Bold().Color("blue");


}
public static class StringExtension
{
    public static string Bold(this string str) => "<b>" + str + "</b>";
    public static string Color(this string str, string clr) => string.Format("<color={0}>{1}</color>", clr, str);
    public static string Italic(this string str) => "<i>" + str + "</i>";
    public static string Size(this string str, int size) => string.Format("<size={0}>{1}</size>", size, str);
}


