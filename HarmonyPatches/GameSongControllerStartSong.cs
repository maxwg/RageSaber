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
    [HarmonyPatch(typeof(GameSongController), "StartSong",
        new Type[] { })]
    public class GameSongControllerStartSong
    {
        static void Prefix()
        {
            RageScore.instance = new RageScore();
        }
    }
}