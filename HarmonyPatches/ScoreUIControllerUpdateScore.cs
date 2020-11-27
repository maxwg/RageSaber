using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using TMPro;

using static RageSaber.Utils.ReflectionUtil;
/// <summary>
/// See https://github.com/pardeike/Harmony/wiki for a full reference on Harmony.
/// </summary>
namespace RageSaber.HarmonyPatches
{
    /// <summary>
    /// This patches ClassToPatch.MethodToPatch(Parameter1Type arg1, Parameter2Type arg2)
    /// </summary>
    [HarmonyPatch(typeof(ScoreUIController), "UpdateScore",
        new Type[] { // List the Types of the method's parameters.
        typeof(int),
        typeof(int)})]
    public class ScoreUIControllerUpdateScore
    {
        static void Postfix(ScoreUIController __instance)
        {
            TextMeshProUGUI text = __instance.getPrivateField<TextMeshProUGUI>("_scoreText");
            float ragePct = RageScore.instance.curScore / RageScore.instance.hitCount;
            text.paragraphSpacing = 10;
            text.lineSpacing = 10;
            text.richText = true;
            text.text = "<line-height=50%>" + text.text + "<br><color=red> " + ragePct.ToString("n3") + "%</color>";
        }
    }
}