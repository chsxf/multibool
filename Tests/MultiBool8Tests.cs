using chsxf;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public static class MultiBool8Tests
    {
        [Test]
        public static void BitSwitchTest() {
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
    }
}
