using System;
using Nest.Memory;

namespace Nest.Hardware
{
    public static partial class Mos6502Decoder
    {
        public static Mos6502Instruction Decode(MemoryUnit memory, int offset)
        {
            var opcode = memory.ReadByte(offset);
            var operandSize = _operandSizeTable[opcode];

            var operand = operandSize switch
            {
                0 => 0,
                1 => (int)memory.ReadByte(offset + 1),
                2 => (int)memory.ReadUInt16BigEndian(offset + 1),
                _ => throw new NotSupportedException("Operand size must be between 0 and 2 inclusive!"),
            };

            return new Mos6502Instruction(
                _opcodeTable[opcode],
                _addressingModeTable[opcode],
                _cycleCountTable[opcode],
                operand);
        }
    }

    public readonly struct Mos6502Instruction
    {
        public Mos6502Instruction(Mos6502Opcode opcode, Mos6502AddressingMode addressingMode, int cycleCount, int operand)
        {
            Opcode = opcode;
            AddressingMode = addressingMode;
            CycleCount = cycleCount;
            Operand = operand;
        }

        public Mos6502Opcode Opcode { get; }
        public Mos6502AddressingMode AddressingMode { get; }
        public int CycleCount { get; }
        public int Operand { get; }
    }

    public enum Mos6502AddressingMode
    {
        Implicit,
        Accumulator,
        Immediate,
        ZeroPage,
        ZeroPageY,
        ZeroPageX,
        Relative,
        Absolute,
        AbsoluteX,
        AbsoluteY,
        Indirect,
        IndexedIndirect,
        IndirectIndexed,
    }

    public enum Mos6502Opcode
    {
        ADC,
        AHX,
        ALR,
        ANC,
        AND,
        ARR,
        ASL,
        AXS,
        BCC,
        BCS,
        BEQ,
        BIT,
        BMI,
        BNE,
        BPL,
        BRK,
        BVC,
        BVS,
        CLC,
        CLD,
        CLI,
        CLV,
        CMP,
        CPX,
        CPY,
        DCP,
        DEC,
        DEX,
        DEY,
        EOR,
        INC,
        INX,
        INY,
        ISC,
        JMP,
        JSR,
        KIL,
        LAS,
        LAX,
        LDA,
        LDX,
        LDY,
        LSR,
        NOP,
        ORA,
        PHA,
        PHP,
        PLA,
        PLP,
        RLA,
        ROL,
        ROR,
        RRA,
        RTI,
        RTS,
        SAX,
        SBC,
        SEC,
        SED,
        SEI,
        SHX
        SHY,
        SLO,
        SRE,
        STA,
        STX,
        STY,
        TAS,
        TAX,
        TAY,
        TSX,
        TXA,
        TXS,
        TYA,
        XAA,
    }
}