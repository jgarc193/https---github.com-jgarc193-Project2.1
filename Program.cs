using System;
using System.Diagnostics;
using System.Threading;
namespace app{
class Process{
    public int id;
    public int arrivalTime;
    public int burstTime;
    public int remainingTime;
    public int waitingTime;
    public int turnAroundTime;
    public int completionTime;
    public Process(int i, int a, int b){
        id =i;
        arrivalTime=a;
        burstTime= b;
        remainingTime = burstTime;
    }
}
class Program
{
    static void Main(string[] args) {
        Console.WriteLine("How many Process?");
        int n = Convert.ToInt32(Console.ReadLine());
        Process[] processes = new Process[n];

        for (int i = 0; i < n; i++){
            string[] input = Console.ReadLine().Split();
            int arrivalTime = int.Parse(input[0]);
            int burstTime = int.Parse(input[1]);
            processes[i] = new Process(i + 1, arrivalTime, burstTime);
        }
         processes = processes.OrderBy(p => p.arrivalTime).ToArray();
          int currentTime = 0;
          int completed = 0;

          while (completed < n) {
            int idx = -1;
            for (int i = 0; i < n; i++){
                if (processes[i].arrivalTime <= currentTime && processes[i].remainingTime > 0 && (idx == -1 || processes[i].remainingTime < processes[idx].remainingTime)){
                    idx = i;
                }
            }
             if (idx != -1){
                processes[idx].remainingTime--;
                currentTime++;
                //calculations
                if (processes[idx].remainingTime == 0) {
                    processes[idx].completionTime = currentTime;
                    processes[idx].turnAroundTime = currentTime - processes[idx].arrivalTime;
                    processes[idx].waitingTime = processes[idx].turnAroundTime - processes[idx].burstTime;
                    completed++;
                }
            }
            else{
                currentTime++;
            }
        }
        double totalWaitingTime =0;
        double totalTurnAroundTime =0;
        Console.WriteLine("ID\tAT\tBT\tWT\tTAT\t");
        foreach(Process p in processes){
            totalWaitingTime+= p.waitingTime;
            totalTurnAroundTime += p.turnAroundTime;
            Console.WriteLine(p.id + "\t" + p.arrivalTime + "\t" + p.burstTime+ "\t" + p.waitingTime + "\t" + p.turnAroundTime);
        }
        double AVGwaitingTime = totalWaitingTime/n;
        double AVGTurnAroundTime = totalTurnAroundTime/n;
        Console.WriteLine("Average waiting time: "+ AVGwaitingTime );
        Console.WriteLine("Average Turn Around Time:" + AVGTurnAroundTime);
    }
    }
}