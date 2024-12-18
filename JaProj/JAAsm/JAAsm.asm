; Sharpening mask
.data
    align 16
    mask_sharpen dw 0, -1, 0, -1, 5, -1, 0, -1, 0


; Makro do przetwarzania jednego piksela dla algorytmu wyostrzania
filterPixel macro

endm

.code

ASMSharpen proc EXPORT
    ; Param:
    ; RCX - output (BYTE*)
    ; RDX - input (BYTE*)
    ; R8  - width (int)
    ; R9  - height (int)
    ; [RSP + 40] - stride (int)

        ; Prolog funkcji
    push rbp
    mov rbp, rsp
    push rbx
    push rsi
    push rdi
    push r12
    push r13
    push r14
    push r15

    mov r10, [RSP + 40]
    mov rsi, rcx    ; RSI - output pointer
    mov rdi, rdx    ; RDI - input pointer
    ; Wyzeruj ymm11 (u¿ywane w makrze)
    vpxor ymm11, ymm11, ymm11
    mov r12, 1          ; r12 = y

y_loop:
    cmp r12, r9
    jge end_processing

    mov r13, 1          ; r13 = x

    mov rax, r8
    dec rax             ; rax = r8 - 1
    cmp r13, rax
    jge end_y_loop

x_loop:
    cmp r13, r8
    jge next_row
    mov rbx, r9
    dec rbx          ; rbx = r9 - 1
    cmp r12, rbx
    jge next_row
     ; Ustaw RBX na pocz¹tek filtra
    lea rbx, [mask_sharpen]

    ; Wyzeruj sumy
    vpxor ymm4, ymm4, ymm4
    vpxor ymm5, ymm5, ymm5

    ; Oblicz offset dla piksela centralnego
    mov r14, r12            ; r14 = y
    imul r14, r10           ; r14 = y * stride
    mov r15, r13            ; r15 = x
    imul r15, 3             ; r15 = x * 3
    add r14, r15            ; r14 = y * stride + x * 3

    ; Ustaw RDI na pocz¹tek danych wejœciowych
    lea rdi, [rdx + r14]

    ; Przesuñ RDI na piksel (x-1, y-1)
    sub rdi, r10            ; RDI -= stride (y - 1)
    sub rdi, 3              ; RDI -= 3 (x - 1) * 3

    ; Przetwarzanie 9 pikseli filtra 3x3
    ; Pierwszy wiersz
    
    filterPixel    ; (x-1, y-1)
    filterPixel    ; (x, y-1)
    filterPixel    ; (x+1, y-1)
    
    ; PrzejdŸ do kolejnego piksela i kolejnej wartoœci filtru
    add RBX, 2    ; Przesuñ wskaŸnik filtra o 2 bajty (16-bit)
    add RDI, 3    ; Przesuñ wskaŸnik danych wejœciowych o 3 bajty (RGB)


    ; Przesuñ RDI do pocz¹tku kolejnego wiersza
    sub rdi, 9              ; Cofnij RDI o 9 bajtów (3 piksele)
    add rdi, r10            ; PrzejdŸ do nastêpnego wiersza (y)

    ; Drugi wiersz
    filterPixel    ; (x-1, y)
    filterPixel    ; (x, y)
    filterPixel    ; (x+1, y)

    ; Przesuñ RDI do pocz¹tku kolejnego wiersza
    sub rdi, 9
    add rdi, r10

    ; Trzeci wiersz
    filterPixel    ; (x-1, y+1)
    filterPixel    ; (x, y+1)
    filterPixel   ; (x+1, y+1)

    ; Po przetworzeniu, ogranicz wartoœci i zapisz wynik
    ; Pakowanie wyników i zapis do bufora wyjœciowego
    vpackuswb ymm0, ymm5, ymm4
    ; Zapisz wynik do danych wyjœciowych
    lea rdi, [rsi + r14]
    vmovdqu ymmword ptr[rdi], ymm0

    ; PrzejdŸ do nastêpnego piksela w wierszu
    inc r13
    jmp x_loop



next_row:
    inc r12
    jmp y_loop

end_y_loop:

end_processing:
        ; Epilog funkcji
    pop r15
    pop r14
    pop r13
    pop r12
    pop rdi
    pop rsi
    pop rbx
    pop rbp
    ret

ASMSharpen endp

end
