using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

namespace chsxf
{
    public static class PerformanceTester
    {
        private const int ITERATION_COUNT = 1_000_000;
        private const string MENU_PATH = "Tools/MultiBool/Performance Tester/";

        private enum MultiBool8TestEnum
        {
            One = 0,
            Two = 1,
            Three = 2,
            Four = 3,
            Five = 4,
            Six = 5,
            Seven = 6,
            Eight = 7
        }

        private enum MultiBool64TestEnum
        {
            One = 0,
            Two = 1,
            Three = 2,
            Four = 3,
            Five = 4,
            Six = 5,
            Seven = 6,
            Eight = 7,
            Nine = 8,
            Ten = 9,
            Eleven = 10,
            Twelve = 11,
            Thirteen = 12,
            Fourteen = 13,
            Fifteen = 14,
            Sixteen = 15,
            Seventeen = 16,
            Eighteen = 17,
            Nineteen = 18,
            Twenty = 19,
            TwentyOne = 20,
            TwentyTwo = 21,
            TwentyThree = 22,
            TwentyFour = 23,
            TwentyFive = 24,
            TwentySix = 25,
            TwentySeven = 26,
            TwentyEight = 27,
            TwentyNine = 28,
            Thirty = 29,
            ThirtyOne = 30,
            ThirtyTwo = 31,
            ThirtyThree = 32,
            ThirtyFour = 33,
            ThirtyFive = 34,
            ThirtySix = 35,
            ThirtySeven = 36,
            ThirtyEight = 37,
            ThirtyNine = 38,
            Forty = 39,
            FortyOne = 40,
            FortyTwo = 41,
            FortyThree = 42,
            FortyFour = 43,
            FortyFive = 44,
            FortySix = 45,
            FortySeven = 46,
            FortyEight = 47,
            FortyNine = 48,
            Fifty = 49,
            FiftyOne = 50,
            FiftyTwo = 51,
            FiftyThree = 52,
            FiftyFour = 53,
            FiftyFive = 54,
            FiftySix = 55,
            FiftySeven = 56,
            FiftyEight = 57,
            FiftyNine = 58,
            Sixty = 59,
            SixtyOne = 60,
            SixtyTwo = 61,
            SixtyThree = 62,
            SixtyFour = 63
        }

        [Flags]
        public enum FlagsTestEnum : ulong
        {
            One = 1UL << 0,
            Two = 1UL << 1,
            Three = 1UL << 2,
            Four = 1UL << 3,
            Five = 1UL << 4,
            Six = 1UL << 5,
            Seven = 1UL << 6,
            Eight = 1UL << 7,
            Nine = 1UL << 8,
            Ten = 1UL << 9,
            Eleven = 1UL << 10,
            Twelve = 1UL << 11,
            Thirteen = 1UL << 12,
            Fourteen = 1UL << 13,
            Fifteen = 1UL << 14,
            Sixteen = 1UL << 15,
            Seventeen = 1UL << 16,
            Eighteen = 1UL << 17,
            Nineteen = 1UL << 18,
            Twenty = 1UL << 19,
            TwentyOne = 1UL << 20,
            TwentyTwo = 1UL << 21,
            TwentyThree = 1UL << 22,
            TwentyFour = 1UL << 23,
            TwentyFive = 1UL << 24,
            TwentySix = 1UL << 25,
            TwentySeven = 1UL << 26,
            TwentyEight = 1UL << 27,
            TwentyNine = 1UL << 28,
            Thirty = 1UL << 29,
            ThirtyOne = 1UL << 30,
            ThirtyTwo = 1UL << 31,
            ThirtyThree = 1UL << 32,
            ThirtyFour = 1UL << 33,
            ThirtyFive = 1UL << 34,
            ThirtySix = 1UL << 35,
            ThirtySeven = 1UL << 36,
            ThirtyEight = 1UL << 37,
            ThirtyNine = 1UL << 38,
            Forty = 1UL << 39,
            FortyOne = 1UL << 40,
            FortyTwo = 1UL << 41,
            FortyThree = 1UL << 42,
            FortyFour = 1UL << 43,
            FortyFive = 1UL << 44,
            FortySix = 1UL << 45,
            FortySeven = 1UL << 46,
            FortyEight = 1UL << 47,
            FortyNine = 1UL << 48,
            Fifty = 1UL << 49,
            FiftyOne = 1UL << 50,
            FiftyTwo = 1UL << 51,
            FiftyThree = 1UL << 52,
            FiftyFour = 1UL << 53,
            FiftyFive = 1UL << 54,
            FiftySix = 1UL << 55,
            FiftySeven = 1UL << 56,
            FiftyEight = 1UL << 57,
            FiftyNine = 1UL << 58,
            Sixty = 1UL << 59,
            SixtyOne = 1UL << 60,
            SixtyTwo = 1UL << 61,
            SixtyThree = 1UL << 62,
            SixtyFour = 1UL << 63
        }

        [MenuItem(MENU_PATH + "Execute All Tests", priority = 100)]
        private static void ExecuteAllTests() {
            ComparePerformance8Bits();
            ComparePerformance64Bits();
            ComparePerformance256Bits();
            ComparePerformance1024Bits();
        }

        [MenuItem(MENU_PATH + "Compare with regular boolean array and enum flags (8 bits)", priority = 1000)]
        private static void ComparePerformance8Bits() {
            List<int> indices = new();
            List<MultiBool64TestEnum> tIndices = new();

            Random.InitState(seed: 1234);
            for (int i = 0; i < ITERATION_COUNT; i++) {
                int newIndex = Random.Range(minInclusive: 0, maxExclusive: 8);
                indices.Add(newIndex);
                tIndices.Add((MultiBool64TestEnum) newIndex);
            }

            MultiBool8 multiBool = default;
            MultiBool8<MultiBool8TestEnum> multiBoolT = default;
            FlagsTestEnum flags = default;
            bool[] boolArray = new bool[8];

            Stopwatch sw = new();
            sw.Start();
            foreach (int index in indices) {
                boolArray[index] = !boolArray[index];
            }
            sw.Stop();
            long arrayElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach (int index in indices) {
                multiBool[index] = !multiBool[index];
            }
            sw.Stop();
            long multiBoolElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach (int index in tIndices) {
                multiBoolT[index] = !multiBoolT[index];
            }
            sw.Stop();
            long multiBoolTElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach (int index in indices) {
                ulong bit = 1UL << index;
                flags = (FlagsTestEnum) (((ulong) flags & ~bit) | (bit & ~(ulong) flags & bit));
            }
            sw.Stop();
            long flagsElapsed = sw.ElapsedMilliseconds;

            bool isEqual = true;
            for (int i = 0; i < boolArray.Length; i++) {
                bool mb = boolArray[i] == multiBool[i];
                bool mbt = boolArray[i] == multiBoolT[i];
                bool f = boolArray[i] == flags.HasFlag((FlagsTestEnum) (1UL << i));
                isEqual = isEqual && mb && mbt && f;
            }

            string msg = "Results:\n";
            msg += $" - Regular array: {arrayElapsed} ms\n";
            msg += $" - MultiBool64: {multiBoolElapsed} ms\n";
            msg += $" - MultiBoolT64: {multiBoolTElapsed} ms\n";
            msg += $" - Flags: {flagsElapsed} ms\n";
            msg += $" - Equals: {isEqual}";
            Debug.Log(msg);
        }

        [MenuItem(MENU_PATH + "Compare with regular boolean array and enum flags (64 bits)", priority = 1000)]
        private static void ComparePerformance64Bits() {
            List<int> indices = new();
            List<MultiBool64TestEnum> tIndices = new();

            Random.InitState(seed: 1234);
            for (int i = 0; i < ITERATION_COUNT; i++) {
                int newIndex = Random.Range(minInclusive: 0, maxExclusive: 64);
                indices.Add(newIndex);
                tIndices.Add((MultiBool64TestEnum) newIndex);
            }

            MultiBool64 multiBool = default;
            MultiBool64<MultiBool64TestEnum> multiBoolT = default;
            FlagsTestEnum flags = default;
            bool[] boolArray = new bool[64];

            Stopwatch sw = new();
            sw.Start();
            foreach (int index in indices) {
                boolArray[index] = !boolArray[index];
            }
            sw.Stop();
            long arrayElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach (int index in indices) {
                multiBool[index] = !multiBool[index];
            }
            sw.Stop();
            long multiBoolElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach (int index in tIndices) {
                multiBoolT[index] = !multiBoolT[index];
            }
            sw.Stop();
            long multiBoolTElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach (int index in indices) {
                ulong bit = 1UL << index;
                flags = (FlagsTestEnum) (((ulong) flags & ~bit) | (bit & ~(ulong) flags & bit));
            }
            sw.Stop();
            long flagsElapsed = sw.ElapsedMilliseconds;

            bool isEqual = true;
            for (int i = 0; i < boolArray.Length; i++) {
                bool mb = boolArray[i] == multiBool[i];
                bool mbt = boolArray[i] == multiBoolT[i];
                bool f = boolArray[i] == flags.HasFlag((FlagsTestEnum) (1UL << i));
                isEqual = isEqual && mb && mbt && f;
            }

            string msg = "Results:\n";
            msg += $" - Regular array: {arrayElapsed} ms\n";
            msg += $" - MultiBool64: {multiBoolElapsed} ms\n";
            msg += $" - MultiBoolT64: {multiBoolTElapsed} ms\n";
            msg += $" - Flags: {flagsElapsed} ms\n";
            msg += $" - Equals: {isEqual}";
            Debug.Log(msg);
        }

        [MenuItem(MENU_PATH + "Compare with regular boolean array and enum flags (256 bits)", priority = 1000)]
        private static void ComparePerformance256Bits() {
            List<int> indices = new();
            List<(int, int)> mbIndices = new();
            List<(int, MultiBool64TestEnum)> mbtIndices = new();

            Random.InitState(seed: 1234);
            for (int i = 0; i < ITERATION_COUNT; i++) {
                int newIndex = Random.Range(minInclusive: 0, maxExclusive: 256);
                indices.Add(newIndex);

                mbIndices.Add((newIndex / 64, newIndex % 64));
                mbtIndices.Add((newIndex / 64, (MultiBool64TestEnum) (newIndex % 64)));
            }

            MultiBool64[] multiBool = new MultiBool64[4];
            MultiBool64<MultiBool64TestEnum>[] multiBoolT = new MultiBool64<MultiBool64TestEnum>[4];
            bool[] boolArray = new bool[256];

            Stopwatch sw = new();
            sw.Start();
            foreach (int index in indices) {
                boolArray[index] = !boolArray[index];
            }
            sw.Stop();
            long arrayElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach ((int bucket, int index) in mbIndices) {
                MultiBool64 mb = multiBool[bucket];
                mb[index] = !mb[index];
                multiBool[bucket] = mb;
            }
            sw.Stop();
            long multiBoolElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach ((int bucket, MultiBool64TestEnum index) in mbtIndices) {
                MultiBool64<MultiBool64TestEnum> mb = multiBoolT[bucket];
                mb[index] = !mb[index];
                multiBoolT[bucket] = mb;
            }
            sw.Stop();
            long multiBoolTElapsed = sw.ElapsedMilliseconds;

            bool isEqual = true;
            for (int i = 0; i < boolArray.Length; i++) {
                int bucket = i / 64;
                int subIndex = i % 64;
                bool mb = boolArray[i] == multiBool[bucket][subIndex];
                bool mbt = boolArray[i] == multiBoolT[bucket][subIndex];
                isEqual = isEqual && mb && mbt;
            }

            string msg = "Results:\n";
            msg += $" - Regular array: {arrayElapsed} ms\n";
            msg += $" - MultiBool64: {multiBoolElapsed} ms\n";
            msg += $" - MultiBoolT64: {multiBoolTElapsed} ms\n";
            msg += $" - Equals: {isEqual}";
            Debug.Log(msg);
        }

        [MenuItem(MENU_PATH + "Compare with regular boolean array and enum flags (1024 bits)", priority = 1000)]
        private static void ComparePerformance1024Bits() {
            List<int> indices = new();
            List<(int, int)> mbIndices = new();
            List<(int, MultiBool64TestEnum)> mbtIndices = new();

            Random.InitState(seed: 1234);
            for (int i = 0; i < ITERATION_COUNT; i++) {
                int newIndex = Random.Range(minInclusive: 0, maxExclusive: 1024);
                indices.Add(newIndex);

                mbIndices.Add((newIndex / 64, newIndex % 64));
                mbtIndices.Add((newIndex / 64, (MultiBool64TestEnum) (newIndex % 64)));
            }

            MultiBool64[] multiBool = new MultiBool64[16];
            MultiBool64<MultiBool64TestEnum>[] multiBoolT = new MultiBool64<MultiBool64TestEnum>[16];
            bool[] boolArray = new bool[1024];

            Stopwatch sw = new();
            sw.Start();
            foreach (int index in indices) {
                boolArray[index] = !boolArray[index];
            }
            sw.Stop();
            long arrayElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach ((int bucket, int index) in mbIndices) {
                MultiBool64 mb = multiBool[bucket];
                mb[index] = !mb[index];
                multiBool[bucket] = mb;
            }
            sw.Stop();
            long multiBoolElapsed = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            foreach ((int bucket, MultiBool64TestEnum index) in mbtIndices) {
                MultiBool64<MultiBool64TestEnum> mb = multiBoolT[bucket];
                mb[index] = !mb[index];
                multiBoolT[bucket] = mb;
            }
            sw.Stop();
            long multiBoolTElapsed = sw.ElapsedMilliseconds;

            bool isEqual = true;
            for (int i = 0; i < boolArray.Length; i++) {
                int bucket = i / 64;
                int subIndex = i % 64;
                bool mb = boolArray[i] == multiBool[bucket][subIndex];
                bool mbt = boolArray[i] == multiBoolT[bucket][subIndex];
                isEqual = isEqual && mb && mbt;
            }

            string msg = "Results:\n";
            msg += $" - Regular array: {arrayElapsed} ms\n";
            msg += $" - MultiBool64: {multiBoolElapsed} ms\n";
            msg += $" - MultiBoolT64: {multiBoolTElapsed} ms\n";
            msg += $" - Equals: {isEqual}";
            Debug.Log(msg);
        }
    }
}
