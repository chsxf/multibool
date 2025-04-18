using System;
using UnityEngine;

namespace chsxf
{
    [Serializable]
    public struct MultiBool8 : IEquatable<MultiBool8>
    {
        private const int BIT_COUNT = sizeof(byte) * 8;

        [SerializeField, MultiBoolPackedBits] internal byte bits;

        public bool this[int _index] {
            get {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }
                return (bits & (1 << _index)) != 0;
            }

            set {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }

                if (value) {
                    bits |= (byte) (1 << _index);
                }
                else {
                    bits &= (byte) ~(1 << _index);
                }
            }
        }

        public bool Equals(MultiBool8 _other) {
            return bits == _other.bits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool8 other && Equals(other);
        }

        public override int GetHashCode() {
            return bits.GetHashCode();
        }
    }
}
