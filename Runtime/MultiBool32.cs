using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace chsxf
{
    [Serializable, StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct MultiBool32 : IEquatable<MultiBool32>
    {
        private const int BIT_COUNT = sizeof(uint) * 8;

        [SerializeField, MultiBoolPackedBits, FieldOffset(offset: 0)] internal uint bits;

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

        public bool Equals(MultiBool32 _other) {
            return bits == _other.bits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool32 other && Equals(other);
        }

        public override int GetHashCode() {
            return bits.GetHashCode();
        }
    }
}
