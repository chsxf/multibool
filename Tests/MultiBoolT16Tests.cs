using System;
using chsxf;
using NUnit.Framework;

namespace chsxf
{
    [TestFixture]
    public static class MultiBoolT16Tests
    {
        [Test]
        public static void BitSwitchTest() {
            MultiBool16<MultiBool16TestEnum> bool8 = default;
            Assert.That(bool8.bits, Is.EqualTo(expected: 0));

            MultiBool16TestEnum[] values = (MultiBool16TestEnum[]) Enum.GetValues(typeof(MultiBool16TestEnum));

            foreach (MultiBool16TestEnum value in values) {
                bool8[value] = true;
                Assert.That(bool8.bits, Is.EqualTo(1 << (int) value));

                bool8[value] = false;
                Assert.That(bool8.bits, Is.EqualTo(expected: 0));
            }

            foreach (MultiBool16TestEnum value in values) {
                bool8[value] = true;

                ushort b = 0;
                for (int j = 0; j <= (int) value; j++) {
                    b |= (ushort) (1 << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }

            foreach (MultiBool16TestEnum value in values) {
                bool8[value] = false;

                ushort b = ushort.MaxValue;
                for (int j = 0; j <= (int) value; j++) {
                    b &= (ushort) ~(1 << j);
                }
                Assert.That(bool8.bits, Is.EqualTo(b));
            }
        }
    }
}
