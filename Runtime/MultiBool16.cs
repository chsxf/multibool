using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace chsxf
{
    [Serializable, StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct MultiBool16 : IEquatable<MultiBool16>
    {
        private const int BIT_COUNT = sizeof(ushort) * 8;

        [SerializeField, MultiBoolPackedBits, FieldOffset(offset: 0)] internal ushort bits;

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

        public bool Equals(MultiBool16 _other) {
            return bits == _other.bits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool16 other && Equals(other);
        }

        public override int GetHashCode() {
            return bits.GetHashCode();
        }
    }
}
