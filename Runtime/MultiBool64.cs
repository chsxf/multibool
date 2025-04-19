using System;
using UnityEngine;

namespace chsxf
{
    [Serializable]
    public struct MultiBool64 : IEquatable<MultiBool64>
    {
        private const int BIT_COUNT = sizeof(ulong) * 8;

        [SerializeField, MultiBoolPackedBits] internal ulong bits;

        public bool this[int _index] {
            get {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }
                return (bits & (ulong) (1L << _index)) != 0;
            }

            set {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }

                if (value) {
                    bits |= (ulong) (1L << _index);
                }
                else {
                    bits &= (ulong) ~(1L << _index);
                }
            }
        }

        public bool Equals(MultiBool64 _other) {
            return bits == _other.bits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool64 other && Equals(other);
        }

        public override int GetHashCode() {
            return bits.GetHashCode();
        }
    }
}
