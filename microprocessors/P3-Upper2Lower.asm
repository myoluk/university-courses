; Lower-Upper Letters
; author: @myoluk

org 100h

.data    
    blocka db 'nAME sURNAME id nO. 12-345-678 aSSG1=100, aSSG2=100, aSSG3=100 sPRING 2020 #'
    blockb db 100h dup(?)
    lower db 0
    upper db 0 

.code
    lea si, blocka
    lea di, blockb
    mov cx, 100h          ;check maximum 100h characters
    
    letter:
        mov al, [si]
        cmp al, 23h       ;exit if character is #
        je  exit
        cmp al, 61h       ;may character is a lower letter
        jge lowerLetter
        cmp al, 41h       ;may character is a upper letter
        jge upperLetter
        jmp caseConvert   ;next character
        
    lowerLetter:
        ;character is not a letter
        cmp al, 7Ah       ;character ascii over lower letter ascii
        jg  caseConvert
        
        ;character is a lower letter       
        inc lower
        sub al, 32        ;lower ascii - 32 = upper ascii      
        jmp caseConvert
         
    upperLetter:
        ;character is not a letter
        cmp al, 5Ah       ;character ascii over upper letter ascii
        jg  caseConvert
        
        ;character is a upper letter
        inc upper 
        add al, 32        ;upper ascii + 32 = lower ascii
        jmp caseConvert

    caseConvert:
        inc si            ;next character of blocka
        mov [di], al      ;add previous blocka character to blockb
        inc di            ;increase blockb address for next character
        loop letter       ;do again for next character of blocka
    
    exit:
        mov [di], al      ;add # character to blockb
        
ret