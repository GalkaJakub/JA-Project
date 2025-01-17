; File: JAAsmSharpen.asm
; Purpose: Sharpening an image using a 3x3 convolution mask.
; Version 0.9
; Update:
; Better comments for asm

.data
; mask3x3: 3x3 sharpening mask matrix stored as 16-bit signed words 
    align 16
mask3x3 dw  0, -1, 0, -1, 5,  -1, 0, -1, 0

.code

; Makro: FilterNeighbor offsetX, offsetY, maskIndex
; macro processes a single neighboring pixel relative to the current pixel.
;
; Parameters:
;   offsetX  - horizontal offset from the current pixel, range: -1 to +1.
;   offsetY  - vertical offset from the current pixel, range: -1 to +1.
;   maskIdx  - index into the mask3x3 table, range: 0 to 8.
;   mm7      - accumulator for sums of pixel components.
;
; Macro uses:
;   rax, r8: to calculate offsets (x, y)
;   rsi: base pointer for input data (inData)
;   r13: stride (number of bytes in one row)

FilterNeighbor macro offsetX, offsetY, maskIdx
    ; calculate the offset address for pixel at (x+offsetX, y+offsetY)
    mov rcx, rax    ; rcx = y
    add rcx, offsetY
    imul rcx, r13   ; rcx = (y+offY)*stride

    mov rdx, r8     ; rdx = x
    add rdx, offsetX
    imul rdx, 4     ; *4 (4 bytes per pixel)
    add rcx, rdx
    add rcx, rsi    ; rcx = base pointer + offset -> pointer to neighbor pixel

    ; load 4 bytes from the neighbor pixel into mm0
    movd mm0, dword ptr [rcx]

    ; unpack the 4 bytes into 16-bit words
    pxor mm6, mm6          ; mm6 = 0
    punpcklbw mm0, mm6 

    ; load the mask value
    movsx rdx, word ptr [mask3x3 + maskIdx*2]
    movd mm1, rdx  ; move mask value into mm1
    ; duplicate the mask value across all words in mm1
    punpcklwd mm1, mm1
    punpcklwd mm1, mm1

    ; multiply each component of the pixel by mask value
    pmullw mm0, mm1

    ; add values to accumulator mm7
    paddw mm7, mm0
endm


; Procedure: ASMSharpen
;
;   For each pixel (x,y) apply the 3x3 sharpening filter by calculating weighted sum of its neighboring pixels. 
;   Writes the new pixel value into outData.
;   Borders are not processed.
;
; Parameters:
;   RCX -> inData:  pointer to the input image bytes
;       - must not be null
;       - must point to at least (width * height * 4) bytes for a 32bpp image
;   RDX -> outData: pointer to the output image bytes
;       - must not be null
;       - must be large enough to hold the result (width * height * 4)
;   R8  -> width:   width of the image in pixels, range: >= 3
;   R9  -> height:  height of the image in pixels, range >= 3
;   [RSP+40] -> stride: number of bytes per image row, range >= (width * 4) 
;
; Returns:
;   No return in registers. The output is written to outData.
;
; Registers/flags changed:
;   - RAX, RBX, RCX, RDX, RSI, RDI, R12, R13, R14, R15 are modified.
;   - MM registers: mm0, mm1, mm6, mm7
;   - EFLAGS are affected by instructions (cmp, imul).

ASMSharpen PROC EXPORT

    ; preserve registers
    push rbx
    push rbp
    push rsi
    push rdi
    push r12
    push r13
    push r14
    push r15

    ; load parameters
    mov rsi, rcx    ; rsi = inData
    mov rdi, rdx    ; rdi = outData
    mov rbx, r8     ; rbx = width
    mov r12, r9     ; r12 = height
    mov r13, [rsp+40]   ; r13 = stride

    ; y loop: iterate over y from 1 to height-2
    mov rax, 1
y_loop:
    cmp rax, r12
    jge end_proc    ; if y >= height: end loop
    mov r14, r12
    sub r14, 1      ;r14 = height - 1
    cmp rax, r14
    jge inc_y       ; if y >= height-1: skip

    ; x loop: iterate over x from 1 to width-2
    mov r8, 1   ; start at x = 1
x_loop:
    cmp r8, rbx
    jge next_y  ; if x >= width: go to next row
    mov r9, rbx
    sub r9, 1   ;r9 = width-1
    cmp r8, r9
    jge next_y  ;if x >= width-1: go to next row

    ; zero out the accumulator mm7 for current pixel
    pxor mm7, mm7

    ; apply the 3x3 filter to the current pixel by processing each neighbor:
    FilterNeighbor -1, -1, 0
    FilterNeighbor  0, -1, 1
    FilterNeighbor  1, -1, 2
    FilterNeighbor -1,  0, 3
    FilterNeighbor  0,  0, 4
    FilterNeighbor  1,  0, 5
    FilterNeighbor -1,  1, 6
    FilterNeighbor  0,  1, 7
    FilterNeighbor  1,  1, 8

    ; saturate and pack the 16-bit values in mm7 back to 8-bit values
    packuswb mm7, mm7

    ; calculate output pixel address: (y * stride + x * 4) + outData
    mov r9, rax
    imul r9, r13
    mov r10, r8
    imul r10, 4
    add r9, r10     ; r9 = y*stride + x*4
    add r9, rdi     ; r9 = output base pointer + offset -> pointer to output pixel

    ; save the processed pixel value (4 bytes) to the output image
    movd dword ptr [r9], mm7

    ; increment x
    inc r8
    jmp x_loop

next_y:
    ; end of row: increment y
    inc rax
    jmp y_loop

inc_y:
    ; if y >= height-1: increment y and continue
    inc rax
    jmp y_loop

end_proc:
    ; restore registers
    pop r15
    pop r14
    pop r13
    pop r12
    pop rdi
    pop rsi
    pop rbp
    pop rbx

    emms    ; clear MMX state
    ret

ASMSharpen ENDP

END