; Add/Subtract
; author: @myoluk

org 100h
    
    ;adding first two numbers
    lea si,num1
    lea di,num2
    mov al,[si]
    mov bl,[di]
    add al,bl
    
    ;adding second two numbers
    lea si,num3
    lea di,num4
    mov ah,[si]
    mov bh,[di]
    add ah,bh
    
    ;subtracting two results
    mov cl,al
    sub cl,ah

ret

    num1 db 3
    num2 db 4
    num3 db 5
    num4 db 6


