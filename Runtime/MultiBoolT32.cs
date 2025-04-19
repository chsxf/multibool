using System;
using UnityEngine;

namespace chsxf
{
    [Serializable]
    public struct MultiBool32<T> : IEquatable<MultiBool32<T>> where T : struct, Enum
    {
        private const int BIT_COUNT = sizeof(uint) * 8;

        [SerializeField, MultiBoolPackedBits] internal uint bits;

        public bool None => bits == 0;
        public bool Any => bits != 0;
        public bool All => bits == uint.MaxValue;

        public bool this[int _index] {
            get {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }
                return (bits & (1U << _index)) != 0;
            }

            set {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }

                if (value) {
                    bits |= 1U << _index;
                }
                else {
                    bits &= ~(1U << _index);
                }
            }
        }

        public bool this[T _enum] {
            get => this[EnumValueRepository<T>.GetIntValue(_enum)];
            set => this[EnumValueRepository<T>.GetIntValue(_enum)] = value;
        }

        public bool Equals(MultiBool32<T> _other) {
            return bits == _other.bits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool32<T> other && Equals(other);
        }

        public override int GetHashCode() {
            return bits.GetHashCode();
        }

        public static implicit operator bool(MultiBool32<T> _multiBool) {
            return _multiBool.All;
        }

        public static implicit operator MultiBool32<T>(bool _bool) {
            MultiBool32<T> multiBool = default;
            if (_bool) {
                multiBool.bits = uint.MaxValue;
            }
            return multiBool;
        }
    }
}
