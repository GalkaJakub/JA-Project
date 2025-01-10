.data
; 3x3 sharpening mask matrix stored as 16-bit words. 
    align 16
mask3x3 dw  0, -1, 0, -1, 5,  -1, 0, -1, 0

.code

; Makro: FilterNeighbor offsetX, offsetY, maskIndex
; macro processes a single neighboring pixel relative to the current pixel.
; Parameters:
;   offsetX, offsetY: offsets relative to the current pixel.
;   maskIdx: index in the mask3x3.
;   xmm4: accumulator for the sum.

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

    ; load 4 bytes from the neighbor pixel into xmm0
    vmovd xmm0, dword ptr [rcx]

    ; unpack the 4 bytes into 4 16-bit words
    vpxor xmm15, xmm15, xmm15   ; zero xmm15 register
    vpunpcklbw xmm0, xmm0, xmm15    ; unpack lower bytes of xmm0 with zeros

    ; load the mask value
    movsx rdx, word ptr [mask3x3 + maskIdx*2]
    movd xmm1, rdx  ; move mask value into xmm1
    ; duplicate the mask value across all words in xmm1
    pshuflw   xmm1, xmm1, 0
    pshufd    xmm1, xmm1, 0

    ; multiply each component of the pixel by mask value
    pmullw xmm0, xmm1

    ; add values to accumulator xmm4
    paddw xmm4, xmm0
endm

ASMSharpen PROC EXPORT
    ; parameters: RCX=inputData, RDX=outputData, R8=width, R9=height, [RSP+40]=stride

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

    ; zero out the accumulator xmm4 for current pixel
    vpxor xmm4, xmm4, xmm4

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

    ; saturate and pack the 16-bit values in xmm4 back to 8-bit values
    packuswb xmm4, xmm4

    ; calculate output pixel address
    mov r9, rax
    imul r9, r13
    mov r10, r8
    imul r10, 4
    add r9, r10     ; r9 = y*stride + x*4
    add r9, rdi     ; r9 = output base pointer + offset -> pointer to output pixel

    ; save the processed pixel value (4 bytes) to the output image
    vmovd dword ptr [r9], xmm4

    ; increment x
    inc r8
    jmp x_loop

next_y:
    ; end of row: increment y
    inc rax
    jmp y_loop

inc_y:
    ; if y >= height-1: increment y
    inc rax
    jmp y_loop

end_proc:
    ret

ASMSharpen ENDP

END