using System;
using chsxf;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public static class MultiBoolT8Tests
    {
        [Test]
        public static void BitSwitchTest() {
            MultiBool8<MultiBool8TestEnum> bool8 = default;
            Assert.That(bool8.bits, Is.EqualTo(expected: 0));

            MultiBool8TestEnum[] values = (MultiBool8TestEnum[]) Enum.GetValues(typeof(MultiBool8TestEnum));

            foreach (MultiBool8TestEnum value in values) {
                bool8[value] = true;
                Assert.That(bool8.bits, Is.EqualTo(1 << (int) value));

                bool8[value] = false;
                Assert.That(bool8.bits, Is.EqualTo(expected: 0));
            }

            foreach (MultiBool8TestEnum value in values) {
                bool8[value] = true;

                byte b = 0;
                for (int j = 0; j <= (int) value; j++) {
                    b |= (byte) (1 << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }

            foreach (MultiBool8TestEnum value in values) {
                bool8[value] = false;

                byte b = byte.MaxValue;
                for (int j = 0; j <= (int) value; j++) {
                    b &= (byte) ~(1 << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }
        }
    }
}
