using System;
using UnityEngine;

namespace chsxf
{
    [Serializable]
    public struct MultiBool64<T> : IEquatable<MultiBool64<T>> where T : struct, Enum
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

        public bool this[T _enum] {
            get => this[EnumValueRepository<T>.GetIntValue(_enum)];
            set => this[EnumValueRepository<T>.GetIntValue(_enum)] = value;
        }

        public bool Equals(MultiBool64<T> _other) {
            return bits == _other.bits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool64<T> other && Equals(other);
        }

        public override int GetHashCode() {
            return bits.GetHashCode();
        }
    }
}
