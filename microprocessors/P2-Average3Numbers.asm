; Averaging of 3 numbers
; author: @myoluk

org 100h
    
    ;load and sum numbers
    lea si,num1
    mov ax,[si]
    lea si,num2
    add ax,[si]
    lea si,num3
    add ax,[si]
    
    ;to divide by 3 add 1/3 of sum and
    ;divide by 4 for same result
    ;add division result to sum
    mov bx,ax
    shr bx,2
    add ax,bx
    
    ;add division result to sum
    mov bx,ax
    shr bx,4
    add ax,bx
    
    ;add division result to sum
    mov bx,ax
    shr bx,8
    add ax,bx
    
    ;3/4 * sum = original sum
    ;sum / 4 = (original sum) / 3
    shr ax,2

ret

    num1 dw 025
    num2 dw 155
    num3 dw 17
    