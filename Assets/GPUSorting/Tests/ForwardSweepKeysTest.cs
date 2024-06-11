/******************************************************************************
 * GPUSorting
 *
 * SPDX-License-Identifier: MIT
 * Copyright Thomas Smith 4/28/2024
 * https://github.com/b0nes164/GPUSorting
 *
 ******************************************************************************/
using System;
using System.Collections;
using UnityEngine;

namespace GPUSorting.Tests
{
    public class ForwardSweepKeysTest : TestBase
    {
        [SerializeField]
        ComputeShader forwardSweep;

        GPUSorting.Runtime.ForwardSweep m_fs;

        void Start()
        {
            m_sortName = "OneSweep";
            Initialize();
            m_fs = new GPUSorting.Runtime.ForwardSweep(
                forwardSweep,
                k_maxTestSize,
                ref alt,
                ref globalHist,
                ref passHist,
                ref index);
            StartCoroutine(KeysFullCoroutine());
        }

        protected override bool KeysTest(
            int testSize,
            bool shouldAscend,
            Type keyType,
            int seed)
        {
            SetKeyTypeKeywords(keyType);
            m_util.DisableKeyword(m_sortPairKeyword);
            SetAscendingKeyWords(shouldAscend);
            PreSort(testSize, seed, true);
            m_fs.Sort(
                testSize,
                toTest,
                alt,
                globalHist,
                passHist,
                index,
                keyType,
                shouldAscend);
            return PostSort(testSize, true, false);
        }

        protected override bool KeysCmdTest(
            int testSize,
            bool shouldAscend,
            Type keyType,
            int seed)
        {
            SetKeyTypeKeywords(keyType);
            m_util.DisableKeyword(m_sortPairKeyword);
            SetAscendingKeyWords(shouldAscend);
            PreSort(testSize, seed, true);
            m_cmd.Clear();
            m_fs.Sort(
                m_cmd,
                testSize,
                toTest,
                alt,
                globalHist,
                passHist,
                index,
                keyType,
                shouldAscend);
            Graphics.ExecuteCommandBuffer(m_cmd);
            return PostSort(testSize, true, false);
        }

        private IEnumerator KeysFullCoroutine()
        {
            yield return StartCoroutine(KeysTestCoroutine());
            yield return StartCoroutine(KeysCmdTestCoroutine());

            if (m_totalTestPassed == k_totalTestsExpected)
                Debug.Log("ALL FORWARD_SWEEP TESTS PASSED");
            else
                Debug.LogError("FORWARD_SWEEP TESTS FAILED");
        }
    }
}
