using System;
using UnityEngine;

namespace chsxf
{
    [Serializable]
    public struct MultiBool16<T> : IEquatable<MultiBool16<T>> where T : struct, Enum
    {
        private const int BIT_COUNT = sizeof(ushort) * 8;

        [SerializeField, MultiBoolPackedBits] internal ushort bits;

        public bool None => bits == 0;
        public bool Any => bits != 0;
        public bool All => bits == ushort.MaxValue;

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
                    bits |= (ushort) (1 << _index);
                }
                else {
                    bits &= (ushort) ~(1 << _index);
                }
            }
        }

        public bool this[T _enum] {
            get => this[EnumValueRepository<T>.GetIntValue(_enum)];
            set => this[EnumValueRepository<T>.GetIntValue(_enum)] = value;
        }

        public bool Equals(MultiBool16<T> _other) {
            return bits == _other.bits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool16<T> other && Equals(other);
        }

        public override int GetHashCode() {
            return bits.GetHashCode();
        }

        public static implicit operator bool(MultiBool16<T> _multiBool) {
            return _multiBool.All;
        }

        public static implicit operator MultiBool16<T>(bool _bool) {
            MultiBool16<T> multiBool = default;
            if (_bool) {
                multiBool.bits = ushort.MaxValue;
            }
            return multiBool;
        }
    }
}
