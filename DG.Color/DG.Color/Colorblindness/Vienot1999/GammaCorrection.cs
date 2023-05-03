using System;
using System.Collections.Generic;

namespace DG.Color.Colorblindness.Vienot1999
{
    internal class GammaCorrection
    {
        private const int _maxEncodedValue = 1024;

        private static readonly Dictionary<int, byte> _applyLookUpTable;
        private static readonly Dictionary<byte, float> _removeLookUpTable;

        static GammaCorrection()
        {
            _applyLookUpTable = new Dictionary<int, byte>(_maxEncodedValue);
            _removeLookUpTable = new Dictionary<byte, float>(byte.MaxValue + 1);

            PreComputeApply();
            PreComputeRemove();
        }

        public static void PreComputeApply()
        {
            for (int i = 0; i < _maxEncodedValue; i++)
            {
                _applyLookUpTable[i] = (byte)(255 * CalculateApply(i / (float)_maxEncodedValue));
            }
        }

        public static void PreComputeRemove()
        {
            for (byte b = 0; b <= byte.MaxValue; b++)
            {
                _removeLookUpTable[b] = CalculateRemove(b / 255f);
            }
        }

        public static float RemoveFromByte(byte b)
        {
            return _removeLookUpTable[b];
        }

        public static byte ApplyForIntensity(float v)
        {
            if (v < 0)
            {
                v = 0;
            }
            if (v > 1)
            {
                v = 1;
            }
            return Apply((int)(v * _maxEncodedValue - 1));
        }

        public static byte Apply(int i)
        {
            return _applyLookUpTable[i];
        }

        private static float CalculateRemove(float s)
        {
            if (s <= 0.04045f)
            {
                return s / 12.92f;
            }
            return (float)Math.Pow((s + 0.055) / 1.055, 2.4);
        }

        private static float CalculateApply(float s)
        {
            if (s <= 0.0031308)
            {
                return 12.92f * s;
            }
            return (float)(1.055 * Math.Pow(s, 0.41666) - 0.055);
        }
    }
}
