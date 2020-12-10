#!/usr/bin/env dotnet script

var DEBUG = bool.Parse(Environment.GetEnvironmentVariable("DEBUG") ?? "false");

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

enum Op
{
    NOP, ACC, JMP
}

struct Instruction
{
    public Op Op;
    public int Arg;
}

List<Instruction> ParseInput(List<string> lines) {

    return lines.Select(it => {
        var parts = it.Split(" ");
        var instruction = new Instruction();
        switch (parts[0])
        {
            case "nop": instruction.Op = Op.NOP; break;
            case "acc": instruction.Op = Op.ACC; break;
            case "jmp": instruction.Op = Op.JMP; break;
            default: throw new Exception($"Unknown op [{parts[0]}]");
        }
        instruction.Arg = int.Parse( parts[1][0] == '+' ? parts[1].Substring(1) : parts[1] );
        return instruction;
    }).ToList();
}

void DebugPrint(int acc, int frame, List<Instruction> instructions)
{
    if (!DEBUG) return;

    System.Console.WriteLine("--------------------------------------------------------------------------------");
    System.Console.WriteLine($"[acc {acc}]");
    for (int i = 0; i < instructions.Count; i++)
    {
        System.Console.Write(i == frame ? "-> " : "   ");
        System.Console.WriteLine($"{instructions[i].Op} {instructions[i].Arg}");
    }
    ReadKey();
}

int Run(List<Instruction> instructions)
{
    int acc = 0, frame = 0;
    Instruction i;
    HashSet<int> seen = new HashSet<int>();
    while (true)
    {
        DebugPrint(acc, frame, instructions);

        i = instructions[frame];
        if (seen.Contains(frame)) return acc;
        seen.Add(frame);

        int dFrame = 1, dAcc = 0;
        switch(i.Op) {
            case Op.NOP:
                break;
            case Op.ACC:
                dAcc = i.Arg;
                break;
            case Op.JMP:
                dFrame = i.Arg;
                break;
        }
        frame += dFrame;
        acc += dAcc;
    }
}

void main()
{
    var lines = ReadInput();
    var instructions = ParseInput(lines);
    System.Console.WriteLine(Run(instructions));
}

main();
