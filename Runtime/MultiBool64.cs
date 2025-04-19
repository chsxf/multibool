using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace chsxf
{
    [Serializable, StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct MultiBool64 : IEquatable<MultiBool64>
    {
        private const int BIT_COUNT = sizeof(ulong) * 8;

        [SerializeField, MultiBoolPackedBits, FieldOffset(offset: 0)] internal ulong bits;

        public bool None => bits == 0;
        public bool Any => bits != 0;
        public bool All => bits == ulong.MaxValue;

        public bool this[int _index] {
            get {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }
                return (bits & (1UL << _index)) != 0;
            }

            set {
                if ((_index < 0) || (_index >= BIT_COUNT)) {
                    throw new IndexOutOfRangeException();
                }

                if (value) {
                    bits |= 1UL << _index;
                }
                else {
                    bits &= ~(1UL << _index);
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

        public static implicit operator bool(MultiBool64 _multiBool) {
            return _multiBool.All;
        }

        public static implicit operator MultiBool64(bool _bool) {
            MultiBool64 multiBool = default;
            if (_bool) {
                multiBool.bits = ulong.MaxValue;
            }
            return multiBool;
        }
    }
}
