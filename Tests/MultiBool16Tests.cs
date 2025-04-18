using chsxf;
using NUnit.Framework;

namespace Tests
{
    public static class MultiBool16Tests
    {
        [Test]
        public static void BitSwitchTest() {
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
    }
}