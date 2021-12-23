using System;

namespace ElevatorProject4
{
    class Program
    {
        public static int N = 50;
        public static int Samples = 10;

        public static int FIFORes, IMPROVEDRes; // at 0 = Total time taken // at 1 = mi

        public static int[] FIFOScores = new int[Samples];
        public static int[] IMPROVEDScores = new int[Samples];

        static void Main(string[] args)
        {
            int[] Requests = new int[N];
            Random GenNumber = new Random();

            for (int i = 0; i < Samples; i++)
            {
                for (int x = 0; x < N; x++)
                {
                    Requests[x] = GenNumber.Next(1, 10); // range 1 to 9
                }

                FIFO(ref Requests, i);     // each floor takes 1 second to go to
                IMPROVED(ref Requests, i); // each floor takes 1 second to go to
            }

            float FIFOaverage = FIFORes / Samples;
            float IMPROVEDaverage = IMPROVEDRes / Samples;

            int FIFO_MIN = FIFOScores[0], IMPROVED_MIN = IMPROVEDScores[0];

            for (int i = 0; i < Samples; i++)
            {
                if (FIFO_MIN > FIFOScores[i]) {FIFO_MIN = FIFOScores[i];}
                if (IMPROVED_MIN > IMPROVEDScores[i]) {IMPROVED_MIN = IMPROVEDScores[i];}
            }

            Console.WriteLine("FIFO: ");
            Console.WriteLine("Average : " + FIFOaverage + "s");
            Console.WriteLine("Best: " + FIFO_MIN + "s");

            Console.WriteLine("IMPROVED: ");
            Console.WriteLine("Average : " + IMPROVEDaverage + "s");
            Console.WriteLine("Best: " + IMPROVED_MIN + "s");
        }

        static void FIFO(ref int[] Requests, int z)
        {
            int LastTotal = FIFORes;
            for (int i = 0; i < N; i++)
            {
                if (Requests[i] != 1)
                {
                    FIFORes += (Requests[i] - 1) * 2; // seconds taken
                }
            }
            FIFOScores[z] = (FIFORes - LastTotal);
        }

        static void IMPROVED(ref int[] Requests, int z)
        {
            int LastTotal = IMPROVEDRes;
            int Previouse = 1;
            for (int i = 0; i < N; i++)
            {
                if (i == 0)
                {
                    IMPROVEDRes += (Requests[i] - 1);
                }
                else if (Requests[i] < Previouse)
                {
                    IMPROVEDRes += (Previouse - Requests[i]);
                }
                else if(Requests[i] > Previouse)
                {
                    if (Previouse != 1)
                    {
                        IMPROVEDRes += (Previouse - 1);
                        Previouse = 1;
                    }
                    IMPROVEDRes += (Requests[i] - 1);
                }
                Previouse = Requests[i];
            }

            IMPROVEDRes += (Previouse -1);
            IMPROVEDScores[z] = (IMPROVEDRes - LastTotal);
        }
    }
}
