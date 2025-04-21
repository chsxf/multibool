using System;
using chsxf;
using NUnit.Framework;

namespace chsxf
{
    [TestFixture]
    public static class MultiBoolT64Tests
    {
        [Test]
        public static void BitSwitchTest() {
            MultiBool64<MultiBool64TestEnum> bool8 = default;
            Assert.That(bool8.bits, Is.EqualTo(expected: 0));

            MultiBool64TestEnum[] values = (MultiBool64TestEnum[]) Enum.GetValues(typeof(MultiBool64TestEnum));

            foreach (MultiBool64TestEnum value in values) {
                bool8[value] = true;
                Assert.That(bool8.bits, Is.EqualTo((ulong) (1L << (int) value)));

                bool8[value] = false;
                Assert.That(bool8.bits, Is.EqualTo(expected: 0));
            }

            foreach (MultiBool64TestEnum value in values) {
                bool8[value] = true;

                ulong b = 0;
                for (int j = 0; j <= (int) value; j++) {
                    b |= (ulong) (1L << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }

            foreach (MultiBool64TestEnum value in values) {
                bool8[value] = false;

                ulong b = ulong.MaxValue;
                for (int j = 0; j <= (int) value; j++) {
                    b &= (ulong) ~(1L << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }
        }
    }
}
