using chsxf;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public static class BasicTests
    {
        [Test]
        public static void BitSwitchTest() {
            MultiBool bools = default;
            Assert.That(bools.boolBits, Is.EqualTo(expected: 0));

            for (int i = 0; i < 8; i++) {
                bools[i] = true;
                Assert.That(bools.boolBits, Is.EqualTo(1 << i));

                bools[i] = false;
                Assert.That(bools.boolBits, Is.EqualTo(expected: 0));
            }

            for (int i = 0; i < 8; i++) {
                bools[i] = true;

                byte b = 0;
                for (int j = 0; j <= i; j++) {
                    b |= (byte) (1 << j);
                }
                Assert.That(bools.boolBits, Is.EqualTo(b));
            }

            for (int i = 0; i < 8; i++) {
                bools[i] = false;

                byte b = 0xff;
                for (int j = 0; j <= i; j++) {
                    b &= (byte) ~(1 << j);
                }
                Assert.That(bools.boolBits, Is.EqualTo(b));
            }
        }
    }
}
