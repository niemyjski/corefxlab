﻿using System.Text;
using System.Text.Utf8;
using Xunit;

namespace System.Text.Primitives.Tests
{
    public partial class PrimitiveParserTests
    {
        [Theory]
        [InlineData("123", 3, 123, 3)]
        [InlineData("12a", 3, 12, 2)]
        [InlineData("0", 1, ulong.MinValue, 1)]
        [InlineData("18446744073709551615", 20, ulong.MaxValue, 20)]
        public unsafe void UInt64PositiveTests(string text, int length, ulong expectedValue, int expectedConsumed)
        {
            byte[] byteBuffer = new Utf8String(text).CopyBytes();
            ReadOnlySpan<byte> byteSpan = new ReadOnlySpan<byte>(byteBuffer);
            
            char[] charBuffer = text.ToCharArray();
            ReadOnlySpan<char> charSpan = new ReadOnlySpan<char>(charBuffer);

            bool result;
            ulong actualValue;
            int actualConsumed;

            result = PrimitiveParser.TryParseUInt64(byteSpan, out actualValue, out actualConsumed, EncodingData.InvariantUtf8);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed);

            fixed (byte* bytePointer = byteBuffer)
            {
                result = PrimitiveParser.InvariantUtf8.TryParseUInt64(bytePointer, length, out actualValue);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);

                result = PrimitiveParser.InvariantUtf8.TryParseUInt64(bytePointer, length, out actualValue, out actualConsumed);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);
                Assert.Equal(expectedConsumed, actualConsumed);
            }

            result = PrimitiveParser.InvariantUtf8.TryParseUInt64(byteSpan, out actualValue);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);

            result = PrimitiveParser.InvariantUtf8.TryParseUInt64(byteSpan, out actualValue, out actualConsumed);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed);

            ReadOnlySpan<byte> utf16ByteSpan = charSpan.Cast<char, byte>();
            result = PrimitiveParser.TryParseUInt64(utf16ByteSpan, out actualValue, out actualConsumed, EncodingData.InvariantUtf16);
            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed / 2);

            fixed (char* charPointer = charBuffer)
            {
                result = PrimitiveParser.InvariantUtf16.TryParseUInt64(charPointer, length, out actualValue);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);

                result = PrimitiveParser.InvariantUtf16.TryParseUInt64(charPointer, length, out actualValue, out actualConsumed);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);
                Assert.Equal(expectedConsumed, actualConsumed);
            }

            result = PrimitiveParser.InvariantUtf16.TryParseUInt64(charSpan, out actualValue);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);

            result = PrimitiveParser.InvariantUtf16.TryParseUInt64(charSpan, out actualValue, out actualConsumed);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed);
        }
        
        [Theory (Skip = "UInt64 hex parsing not implemented yet")]
        public unsafe void UInt64PositiveHexTests(string text, int length, ulong expectedValue, int expectedConsumed)
        {
            byte[] byteBuffer = new Utf8String(text).CopyBytes();
            ReadOnlySpan<byte> byteSpan = new ReadOnlySpan<byte>(byteBuffer);

            char[] charBuffer = text.ToCharArray();
            ReadOnlySpan<char> charSpan = new ReadOnlySpan<char>(charBuffer);

            bool result;
            ulong actualValue;
            int actualConsumed;

            result = PrimitiveParser.TryParseUInt64(byteSpan, out actualValue, out actualConsumed, EncodingData.InvariantUtf8, 'X');

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed);

            fixed (byte* bytePointer = byteBuffer)
            {
                result = PrimitiveParser.InvariantUtf8.Hex.TryParseUInt64(bytePointer, length, out actualValue);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);

                result = PrimitiveParser.InvariantUtf8.Hex.TryParseUInt64(bytePointer, length, out actualValue, out actualConsumed);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);
                Assert.Equal(expectedConsumed, actualConsumed);
            }

            result = PrimitiveParser.InvariantUtf8.Hex.TryParseUInt64(byteSpan, out actualValue);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);

            result = PrimitiveParser.InvariantUtf8.Hex.TryParseUInt64(byteSpan, out actualValue, out actualConsumed);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed);

            ReadOnlySpan<byte> utf16ByteSpan = charSpan.Cast<char, byte>();
            result = PrimitiveParser.TryParseUInt64(utf16ByteSpan, out actualValue, out actualConsumed, EncodingData.InvariantUtf16, 'X');
            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed / 2);

            fixed (char* charPointer = charBuffer)
            {
                result = PrimitiveParser.InvariantUtf16.Hex.TryParseUInt64(charPointer, length, out actualValue);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);

                result = PrimitiveParser.InvariantUtf16.Hex.TryParseUInt64(charPointer, length, out actualValue, out actualConsumed);

                Assert.True(result);
                Assert.Equal(expectedValue, actualValue);
                Assert.Equal(expectedConsumed, actualConsumed);
            }

            result = PrimitiveParser.InvariantUtf16.Hex.TryParseUInt64(charSpan, out actualValue);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);

            result = PrimitiveParser.InvariantUtf16.Hex.TryParseUInt64(charSpan, out actualValue, out actualConsumed);

            Assert.True(result);
            Assert.Equal(expectedValue, actualValue);
            Assert.Equal(expectedConsumed, actualConsumed);
        }
    }
}
