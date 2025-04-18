using System;
using UnityEngine;

namespace chsxf
{
    [Serializable]
    public struct MultiBool<T> : IEquatable<MultiBool<T>> where T : struct, Enum
    {
        [SerializeField, MultiBoolPackedBits] internal byte boolBits;

        public bool First {
            get => (boolBits & 1) != 0;
            set => this[_index: 0] = value;
        }

        public bool Second {
            get => (boolBits & 2) != 0;
            set => this[_index: 1] = value;
        }

        public bool Third {
            get => (boolBits & 4) != 0;
            set => this[_index: 2] = value;
        }

        public bool Fourth {
            get => (boolBits & 8) != 0;
            set => this[_index: 3] = value;
        }

        public bool Fifth {
            get => (boolBits & 16) != 0;
            set => this[_index: 4] = value;
        }

        public bool Sixth {
            get => (boolBits & 32) != 0;
            set => this[_index: 5] = value;
        }

        public bool Seventh {
            get => (boolBits & 64) != 0;
            set => this[_index: 6] = value;
        }

        public bool Eighth {
            get => (boolBits & 128) != 0;
            set => this[_index: 7] = value;
        }

        public bool this[int _index] {
            get {
                if ((_index < 0) || (_index > 7)) {
                    throw new IndexOutOfRangeException();
                }
                return (boolBits & (1 << _index)) != 0;
            }

            set {
                if ((_index < 0) || (_index > 7)) {
                    throw new IndexOutOfRangeException();
                }

                if (value) {
                    boolBits |= (byte) (1 << _index);
                }
                else {
                    boolBits &= (byte) ~(1 << _index);
                }
            }
        }

        public bool this[Enum _enum] {
            get => this[Convert.ToInt32(_enum)];
            set => this[Convert.ToInt32(_enum)] = value;
        }

        public MultiBool(bool _first = false,
                         bool _second = false,
                         bool _third = false,
                         bool _fourth = false,
                         bool _fifth = false,
                         bool _sixth = false,
                         bool _seventh = false,
                         bool _eighth = false) {
            boolBits = (byte) (
                                  (_first ? 1 : 0)
                                  | (_second ? 1 << 1 : 0)
                                  | (_third ? 1 << 2 : 0)
                                  | (_fourth ? 1 << 3 : 0)
                                  | (_fifth ? 1 << 4 : 0)
                                  | (_sixth ? 1 << 5 : 0)
                                  | (_seventh ? 1 << 6 : 0)
                                  | (_eighth ? 1 << 7 : 0)
                              );
        }

        public bool Equals(MultiBool<T> _other) {
            return boolBits == _other.boolBits;
        }

        public override bool Equals(object _obj) {
            return _obj is MultiBool<T> other && Equals(other);
        }

        public override int GetHashCode() {
            return boolBits.GetHashCode();
        }
    }
}
