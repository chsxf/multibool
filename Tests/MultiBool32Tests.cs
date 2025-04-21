using chsxf;
using NUnit.Framework;

namespace chsxf
{
    public static class MultiBool32Tests
    {
        [Test]
        public static void BitSwitchTest() {
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
    }
}