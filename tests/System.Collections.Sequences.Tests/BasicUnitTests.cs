﻿using System.Collections.Generic;
using Xunit;

namespace System.Collections.Sequences.Tests
{
    public class SequenceTests
    {
        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3})]
        public void ArrayList(int[] array)
        {
            ArrayList<int> collection = CreateArrayList(array);

            var position = Position.First;
            int arrayIndex = 0;
            int item;
            while (collection.TryGet(ref position, out item, advance: true)) {
                Assert.Equal(array[arrayIndex++], item);
            }

            arrayIndex = 0;
            var sequence = (ISequence<int>)collection;
            foreach (var sequenceItem in sequence) {
                Assert.Equal(array[arrayIndex++], sequenceItem);
            }
        }

        private static ArrayList<int> CreateArrayList(int[] array)
        {
            var collection = new ArrayList<int>();
            foreach (var arrayItem in array) collection.Add(arrayItem);
            return collection;
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3 })]
        public void LinkedContainer(int[] array)
        {
            LinkedContainer<int> collection = CreateLinkedContainer(array);

            var position = Position.First;
            int arrayIndex = array.Length;
            int item;
            while (collection.TryGet(ref position, out item, advance: true)) {
                Assert.Equal(array[--arrayIndex], item);
            }

            arrayIndex = array.Length;
            var sequence = (ISequence<int>)collection;
            foreach (var sequenceItem in sequence) {
                Assert.Equal(array[--arrayIndex], sequenceItem);
            }
        }

        private static LinkedContainer<int> CreateLinkedContainer(int[] array)
        {
            var collection = new LinkedContainer<int>();
            foreach (var item in array) collection.Add(item); // this adds to front
            return collection;
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3 })]
        public void Hashtable(int[] array)
        {
            Hashtable<int, string> collection = CreateHashtable(array);

            int arrayIndex = 0;
            var position = Position.First;
            KeyValuePair<int, string> item;
            while (collection.TryGet(ref position, out item, advance: true)) {
                Assert.Equal(array[arrayIndex++], item.Key);
            }

            arrayIndex = 0;
            var sequence = (ISequence<KeyValuePair<int, string>>)collection;
            foreach (var sequenceItem in sequence) {
                Assert.Equal(array[arrayIndex++], sequenceItem.Key);
            }
        }

        private static Hashtable<int, string> CreateHashtable(int[] array)
        {
            var collection = new Hashtable<int, string>(EqualityComparer<int>.Default);
            foreach (var item in array) collection.Add(item, item.ToString());
            return collection;
        }
    }
}
