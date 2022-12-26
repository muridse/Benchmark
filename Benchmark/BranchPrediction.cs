using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    class BranchPrediction
    {
        private long size { get; set; }
        private int[] sortedArray;
        private int[] unsortedArray;
        private Random rand = new Random();

        public BranchPrediction(long size)
        {
            this.size = size;
            this.sortedArray = new int[size];
            this.unsortedArray = new int[size];

            unsortedArray = GenerateAndFill(unsortedArray);
            sortedArray = CopyTo(unsortedArray);
            sortedArray = SortArray(sortedArray, 0, sortedArray.Length);


        }

        public int[] SortArray(int[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex - 1;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }

                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                SortArray(array, leftIndex, j);
            if (i < rightIndex)
                SortArray(array, i, rightIndex);
            return array;
        }
        private int[] CopyTo(int[] array) 
        {
            int[] newArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }
        private int[] GenerateAndFill(int[] array) 
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(0, 1000);
            }
            return array;
        }
        private bool Check(int[] arr) 
        {
            bool check = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 500) check = true; 
            }
            return check;
        }
        public void Run(int count) 
        {
            var timeStampsUS = new List<long>();
            var timeStampsS = new List<long>();
            var stopwatch = new Stopwatch();
            for (int i = 0; i < count; i++)
            {
                GC.Collect(1, GCCollectionMode.Forced);
                stopwatch.Restart();
                Check(unsortedArray);
                stopwatch.Stop();
                timeStampsUS.Add(stopwatch.ElapsedMilliseconds);
            }
            for (int i = 0; i < count; i++)
            {
                GC.Collect(1, GCCollectionMode.Forced);
                stopwatch.Restart();
                Check(sortedArray);
                stopwatch.Stop();
                timeStampsS.Add(stopwatch.ElapsedMilliseconds);
            }

            Console.WriteLine($"Average time for unsorted array {timeStampsUS.Average()} ms");
            Console.WriteLine($"Average time for sorted array {timeStampsS.Average()} ms");

            GC.Collect(1, GCCollectionMode.Forced);
        }
    }
}
