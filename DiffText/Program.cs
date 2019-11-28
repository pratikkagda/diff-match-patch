using System;
using DiffMatchPatch;
using System.Collections.Generic;

namespace DiffText
{
    class Program
    {
        static void Main(string[] args)
        {
            diff_match_patch dmp = new diff_match_patch();

            // Execute one reverse diff as a warmup.
            var result = dmp.diff_main("Hello World!123", "Hello World, How are you??");
            var result1 = dmp.diff_prettyHtml(result);
            GC.Collect();
            GC.WaitForPendingFinalizers();


            Console.WriteLine("Hello World!");
        }
    }
}
