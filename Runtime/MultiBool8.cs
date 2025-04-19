using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace chsxf
{
    [Serializable, StructLayout(LayoutKind.Explicit, Size = 1)]
    public struct MultiBool8 : IEquatable<MultiBool8>
    {
        private const int BIT_COUNT = sizeof(byte) * 8;

        [SerializeField, MultiBoolPackedBits, FieldOffset(offset: 0)] internal byte bits;

        public bool None => bits == 0;
        public bool Any => bits != 0;
        public bool All => bits == byte.MaxValue;

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

        public static implicit operator bool(MultiBool8 _multiBool) {
            return _multiBool.All;
        }

        public static implicit operator MultiBool8(bool _bool) {
            MultiBool8 multiBool = default;
            if (_bool) {
                multiBool.bits = byte.MaxValue;
            }
            return multiBool;
        }
    }
}
