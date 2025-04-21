using chsxf;
using NUnit.Framework;

namespace chsxf
{
    public static class MultiBool64Tests
    {
        [Test]
        public static void BitSwitchTest() {
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