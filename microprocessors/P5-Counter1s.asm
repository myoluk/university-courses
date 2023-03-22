;Write an X86 assembly program that counts the number of ones in a byte-sized variable stored 
;in memory location VAR and stores the resulted number of ones in memory location ONES.
;author: @myoluk

org 100h

.data
    var db '10100111#'
    ones db 0

.code
    lea si, var
    
    count:
        mov al, [si]
        cmp al, 23h     ;exit if character is #
        je exit
        cmp al, 31h     ;1's ascii code is 31h
        je increase
        inc si          ;next character
        jmp count       
    
    increase:
        inc ones
        inc si
        jmp count
    
    exit:     
    
ret