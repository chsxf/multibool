using chsxf;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public static class BasicTests
    {
        [Test]
        public static void BitSwitchTest8() {
            MultiBool8 bool8 = default;
            Assert.That(bool8.bits, Is.EqualTo(expected: 0));

            for (int i = 0; i < 8; i++) {
                bool8[i] = true;
                Assert.That(bool8.bits, Is.EqualTo(1 << i));

                bool8[i] = false;
                Assert.That(bool8.bits, Is.EqualTo(expected: 0));
            }

            for (int i = 0; i < 8; i++) {
                bool8[i] = true;

                byte b = 0;
                for (int j = 0; j <= i; j++) {
                    b |= (byte) (1 << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }

            for (int i = 0; i < 8; i++) {
                bool8[i] = false;

                byte b = byte.MaxValue;
                for (int j = 0; j <= i; j++) {
                    b &= (byte) ~(1 << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }
        }

        [Test]
        public static void BitSwitchTest16() {
            MultiBool16 bool16 = default;
            Assert.That(bool16.bits, Is.EqualTo(expected: 0));

            for (int i = 0; i < 16; i++) {
                bool16[i] = true;
                Assert.That(bool16.bits, Is.EqualTo(1 << i));

                bool16[i] = false;
                Assert.That(bool16.bits, Is.EqualTo(expected: 0));
            }

            for (int i = 0; i < 16; i++) {
                bool16[i] = true;

                ushort b = 0;
                for (int j = 0; j <= i; j++) {
                    b |= (ushort) (1 << j);
                }
                Assert.That(bool16.bits, Is.EqualTo(b));
            }

            for (int i = 0; i < 16; i++) {
                bool16[i] = false;

                ushort b = ushort.MaxValue;
                for (int j = 0; j <= i; j++) {
                    b &= (ushort) ~(1 << j);
                }
                Assert.That(bool16.bits, Is.EqualTo(b));
            }
        }

        [Test]
        public static void BitSwitchTest32() {
            MultiBool32 bool32 = default;
            Assert.That(bool32.bits, Is.EqualTo(expected: 0));

            for (int i = 0; i < 32; i++) {
                bool32[i] = true;
                Assert.That(bool32.bits, Is.EqualTo(1L << i));

                bool32[i] = false;
                Assert.That(bool32.bits, Is.EqualTo(expected: 0));
            }

            for (int i = 0; i < 32; i++) {
                bool32[i] = true;

                uint b = 0;
                for (int j = 0; j <= i; j++) {
                    b |= (uint) (1L << j);
                }
                Assert.That(bool32.bits, Is.EqualTo(b));
            }

            for (int i = 0; i < 32; i++) {
                bool32[i] = false;

                uint b = uint.MaxValue;
                for (int j = 0; j <= i; j++) {
                    b &= (uint) ~(1L << j);
                }
                Assert.That(bool32.bits, Is.EqualTo(b));
            }
        }

        [Test]
        public static void BitSwitchTest64() {
            MultiBool64 bool64 = default;
            Assert.That(bool64.bits, Is.EqualTo(expected: 0));

            for (int i = 0; i < 64; i++) {
                bool64[i] = true;
                Assert.That(bool64.bits, Is.EqualTo((ulong) 1L << i));

                bool64[i] = false;
                Assert.That(bool64.bits, Is.EqualTo(expected: 0));
            }

            for (int i = 0; i < 64; i++) {
                bool64[i] = true;

                ulong b = 0;
                for (int j = 0; j <= i; j++) {
                    b |= (ulong) (1L << j);
                }
                Assert.That(bool64.bits, Is.EqualTo(b));
            }

            for (int i = 0; i < 64; i++) {
                bool64[i] = false;

                ulong b = ulong.MaxValue;
                for (int j = 0; j <= i; j++) {
                    b &= (ulong) ~(1L << j);
                }
                Assert.That(bool64.bits, Is.EqualTo(b));
            }
        }
    }
}
