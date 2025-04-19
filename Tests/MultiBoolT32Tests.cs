using System;
using chsxf;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public static class MultiBoolT32Tests
    {
        [Test]
        public static void BitSwitchTest() {
            MultiBool32<MultiBool32TestEnum> bool8 = default;
            Assert.That(bool8.bits, Is.EqualTo(expected: 0));

            MultiBool32TestEnum[] values = (MultiBool32TestEnum[]) Enum.GetValues(typeof(MultiBool32TestEnum));

            foreach (MultiBool32TestEnum value in values) {
                bool8[value] = true;
                Assert.That(bool8.bits, Is.EqualTo(1L << (int) value));

                bool8[value] = false;
                Assert.That(bool8.bits, Is.EqualTo(expected: 0));
            }

            foreach (MultiBool32TestEnum value in values) {
                bool8[value] = true;

                uint b = 0;
                for (int j = 0; j <= (int) value; j++) {
                    b |= (uint) (1L << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }

            foreach (MultiBool32TestEnum value in values) {
                bool8[value] = false;

                uint b = uint.MaxValue;
                for (int j = 0; j <= (int) value; j++) {
                    b &= (uint) ~(1L << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }
        }
    }
}
