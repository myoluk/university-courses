;Develop an x86 assembly program that finds the average and the maximum of fifteen numbers 
;which are stored starting from memory location  NUM, and puts the average and the maximum 
;in memory locations AVG and MAX.
;author: @myoluk

org 100h
NUM DB 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 
26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50
NOFN DB 50 ;Number of numbers
AVG DB ?
MAX DB ? 
MOV AX, 0 ;Sum
MOV CL, 0 ;Count
LEA SI, NUM ;Load numbers to si
SumNumbers:
    MOV BX, 0 ;Clear
    CMP CL, NOFN ;if counter=50 break
    JE SumDone
    MOV BX, [SI]
    ADD AX, BX
    INC SI
    INC CL
    JMP SumNumbers
SumDone:
MOV BL, NOFN
DIV BL
MOV AVG, AL
LEA SI, NUM
MOV AL, [SI]
MOV CX, 0 
MaxNumber:
    INC SI
    INC CX
    CMP CX, NOFN
    JE MaxDone
    MOV BX, 0 ;Clear
    MOV BL,[SI]
    CMP BL, AL
    JAE MaxNumber
    MOV AL, BL
    JMP MaxNumber
MaxDone:
MOV MAX, AL
    
ret