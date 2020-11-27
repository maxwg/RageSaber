using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

using static RageSaber.Utils.ReflectionUtil;

namespace HitScoreVisualizer.Harmony_Patches
{
    [HarmonyPatch(typeof(FlyingScoreEffect), "InitAndPresent",
        new Type[] {
            typeof(NoteCutInfo),
            typeof(int),
            typeof(float),
            typeof(Vector3),
            typeof(Quaternion),
            typeof(Color)})]
    class FlyingScoreEffectInitAndPresent
    {
        public static FlyingScoreEffect currentEffect = null;
        static void Prefix(ref Vector3 targetPos, FlyingScoreEffect __instance)
        {
        }

        static void handleEffectDidFinish(FlyingObjectEffect effect)
        {
            effect.didFinishEvent -= handleEffectDidFinish;
            if (currentEffect == effect) currentEffect = null;
        }

        static void Postfix(FlyingScoreEffect __instance, ref Color ____color, NoteCutInfo noteCutInfo)
        {
            float score = RageSaber.RageScore.calculateFromSpeed(noteCutInfo.saberSpeed);

            RageSaber.RageScore.instance.curScore += score;
            RageSaber.RageScore.instance.hitCount++;

            void judge(SaberSwingRatingCounter counter)
            {
                TextMeshPro text = __instance.getPrivateField<TextMeshPro>("_text");
                // enable rich text
                text.richText = true;
                // disable word wrap, make sure full text displays
                text.enableWordWrapping = false;
                text.overflowMode = TextOverflowModes.Overflow;
                text.text = score.ToString("n0");

                // If the counter is finished, remove our event from it
                counter.didFinishEvent -= judge;
            }

            // Apply judgments a total of twice - once when the effect is created, once when it finishes.
            judge(noteCutInfo.swingRatingCounter);
            noteCutInfo.swingRatingCounter.didFinishEvent += judge;
        }
    }
}